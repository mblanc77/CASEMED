using Sgpa.Data;
using Sgpa.Domain.Security;

namespace Sgpa.Business.Pagos;

/// <summary>
/// Ingreso y consulta de pagos de préstamos (port de <c>cAdmPago.Ingresar</c> + <c>cAdmFactura.ImportexMora</c>,
/// app VB6 "SP", forms frmIngPago / frmCargaPago). El ingreso corre dentro de una transacción y es atómico
/// (como el BeginTrans/Commit/Rollback del form): si la factura no admite el pago, se revierte todo y se
/// devuelve el motivo. La carga batch desde archivo Abitab (ProcesarPagos) queda para un paso posterior.
/// </summary>
public sealed class PagoService
{
    private const int UsrMaxLen = 8;

    // Estados / ítems (Bcpart.bas).
    private const string FacturaEstadoEmitida = "emi";
    private const string FacturaEstadoCancelada = "can";
    private const string FacturaEstadoRetenida = "ret";
    private const string FacturaEstadoAnulada = "anu";
    private const string PrestamoEstadoEmitido = "emi";
    private const string CuotaEstadoPendiente = "pen";
    private const string CuotaEstadoCancelada = "can";
    private const string CuotaEstadoRetenida = "ret";
    private const string ItemPagoCancelacion = "can";
    private const string ItemPagoCuota = "cuo";
    private const string ItemPagoMora = "inm";
    private const string PrestamoEstadoCancelado = "can";
    private const string PrestamoEstadoEnProceso = "pro";
    private const string PrestamoEstadoRetencion = "ret";

    private readonly IDbExecutor _db;
    private readonly ICurrentUser _user;

    public PagoService(IDbExecutor db, ICurrentUser user)
    {
        _db = db;
        _user = user;
    }

    private static string ClampUsr(string usr) => usr.Length <= UsrMaxLen ? usr : usr[..UsrMaxLen];

    /// <summary>Facturas de un préstamo para la grilla de cobro / consulta.</summary>
    public Task<IReadOnlyList<FacturaPagoView>> GetFacturasAsync(int idPrestamo, CancellationToken ct = default)
        => _db.QueryAsync<FacturaPagoView>(
            @"SELECT f.NroFactura, f.IDFactura, f.FechaVencimiento, f.FechaPago,
                     CAST(ISNULL(f.Importe,0) AS float) Importe, f.CodFacturaEstado,
                     e.Descrip AS DescFacturaEstado, f.CodMoneda
              FROM dbo.SP_Factura f
              LEFT JOIN dbo.SP_FacturaEstado e ON e.CodFacturaEstado = f.CodFacturaEstado
              WHERE f.IdPrestamo = @id
              ORDER BY f.NroFactura",
            new { id = idPrestamo }, cancellationToken: ct);

    /// <summary>Datos de una factura para el diálogo de cobro (por Nº de factura).</summary>
    public Task<FacturaCobro?> GetFacturaCobroAsync(int nroFactura, CancellationToken ct = default)
        => _db.QuerySingleOrDefaultAsync<FacturaCobro>(
            @"SELECT TOP 1 f.NroFactura, f.IDFactura, f.FechaVencimiento,
                     CAST(ISNULL(f.Importe,0) AS float) Importe, f.CodFacturaEstado,
                     e.Descrip AS DescFacturaEstado, f.CodMoneda
              FROM dbo.SP_Factura f
              LEFT JOIN dbo.SP_FacturaEstado e ON e.CodFacturaEstado = f.CodFacturaEstado
              WHERE f.NroFactura = @n",
            new { n = nroFactura }, cancellationToken: ct);

    /// <summary>
    /// Interés por mora de una factura a una fecha (port de ImportexMora): se aplica sólo si los días de
    /// atraso superan la tolerancia del sistema (SP_Parametro.DiasTolerancia), salvo que se pida sin tolerancia.
    /// </summary>
    public async Task<double> GetImporteMoraAsync(int nroFactura, DateTime fecha, bool tolerancia = true, CancellationToken ct = default)
    {
        var fac = await _db.QuerySingleOrDefaultAsync<FacturaMoraRow>(
            @"SELECT TOP 1 CAST(ISNULL(Importe,0) AS float) Importe, FechaVencimiento, CodMoneda
              FROM dbo.SP_Factura WHERE NroFactura = @n",
            new { n = nroFactura }, cancellationToken: ct).ConfigureAwait(false);
        if (fac is null) return 0;

        var diasTolerancia = await _db.ExecuteScalarAsync<int>(
            "SELECT TOP 1 ISNULL(DiasTolerancia,0) FROM dbo.SP_Parametro", cancellationToken: ct).ConfigureAwait(false);
        var dias = (int)(fecha.Date - fac.FechaVencimiento.Date).TotalDays;
        if (dias <= diasTolerancia && tolerancia) return 0;

        var tasaMora = await _db.ExecuteScalarAsync<double>(
            "SELECT CAST(ISNULL(TasaMora,0) AS float) FROM dbo.SP_Moneda WHERE CodMoneda = @m",
            new { m = fac.CodMoneda }, cancellationToken: ct).ConfigureAwait(false);
        return PagoCalculo.CalcularMora(fac.Importe, tasaMora, dias);
    }

    /// <summary>
    /// Ingresa el pago de una factura (port de cAdmPago.Ingresar para el camino manual de frmIngPago).
    /// Actualiza la factura, registra el pago y su detalle, cancela las cuotas correspondientes, ajusta
    /// el saldo y las cuotas pagas del préstamo y recalcula su estado. Si la factura no existe, está
    /// retenida o no está emitida, revierte todo y devuelve el motivo (no se persiste nada).
    /// </summary>
    public async Task<ResultadoPago> IngresarPagoAsync(IngresarPagoRequest req, CancellationToken ct = default)
    {
        var usr = ClampUsr(_user.UserName);
        var ts = DateTime.Now;

        await using var uow = await _db.BeginTransactionAsync(ct).ConfigureAwait(false);
        var r = await IngresarCoreAsync(uow, req, usr, ts, ct).ConfigureAwait(false);
        // Sólo se confirma si salió bien; ante cualquier motivo de rechazo se revierte (incluido el
        // registro de pago con error de una factura retenida, que en el camino manual no se conserva).
        if (r == ResultadoPago.Ok) await uow.CommitAsync(ct).ConfigureAwait(false);
        return r;
    }

    /// <summary>
    /// Núcleo del ingreso de pago (port de cAdmPago.Ingresar) sin gestión de transacción: opera sobre la
    /// conexión/transacción que recibe. Lo usan tanto el ingreso manual (que confirma sólo si devuelve Ok)
    /// como la carga batch (que comparte una única transacción para todo el archivo). No hace commit.
    /// </summary>
    private async Task<ResultadoPago> IngresarCoreAsync(IDbExecutor uow, IngresarPagoRequest req, string usr, DateTime ts, CancellationToken ct)
    {
        var sucursal = req.CodSucursal ?? "";

        var fac = await uow.QuerySingleOrDefaultAsync<FacturaRow>(
            @"SELECT TOP 1 IDFactura, IdPrestamo, CAST(ISNULL(Importe,0) AS float) Importe,
                     CodFacturaEstado, CodMoneda
              FROM dbo.SP_Factura WHERE NroFactura = @n",
            new { n = req.NroFactura }, cancellationToken: ct).ConfigureAwait(false);
        if (fac is null) return ResultadoPago.FacturaInexistente;
        // Factura retenida: se registra el pago con error (SP_PagoError) y se rechaza. En estado no emitido
        // (ya cancelada/anulada) no se admite pago.
        if (fac.CodFacturaEstado == FacturaEstadoRetenida)
        {
            await InsertarPagoErrorAsync(uow, fac, req, FacturaEstadoRetenida, usr, ts, ct).ConfigureAwait(false);
            return ResultadoPago.Retenida;
        }
        if (fac.CodFacturaEstado != FacturaEstadoEmitida) return ResultadoPago.EstadoIncorrecto;

        var hayRetencion = await uow.ExecuteScalarAsync<int>(
            "SELECT COUNT(*) FROM dbo.acc_sp_1100_FacturaRetenidaXNroFactura_q(@n)",
            new { n = req.NroFactura }, cancellationToken: ct).ConfigureAwait(false) > 0;

        // Actualizo la factura (cancelada, o retenida si hay retención pendiente).
        await uow.ExecuteAsync(
            "UPDATE dbo.SP_Factura SET FechaPago=@f, CodFacturaEstado=@estado, Usr=@usr, Ts=@ts WHERE IDFactura=@id",
            new { f = req.FechaPago, estado = hayRetencion ? FacturaEstadoRetenida : FacturaEstadoCancelada, usr, ts, id = fac.IDFactura },
            cancellationToken: ct).ConfigureAwait(false);

        var pres = await uow.QuerySingleOrDefaultAsync<PrestamoRow>(
            @"SELECT TOP 1 Cuotas, ISNULL(CuotasPagas,0) CuotasPagas,
                     CAST(ISNULL(Saldo,0) AS float) Saldo, CodPrestamoEstado
              FROM dbo.SP_Prestamo WHERE IDPrestamo = @id",
            new { id = fac.IdPrestamo }, cancellationToken: ct).ConfigureAwait(false)
            ?? throw new InvalidOperationException($"La factura {req.NroFactura} referencia un préstamo inexistente.");

        var tasaCambio = await uow.ExecuteScalarAsync<double>(
            "SELECT CAST(ISNULL(TasaCambio,0) AS float) FROM dbo.SP_Moneda WHERE CodMoneda = @m",
            new { m = fac.CodMoneda }, cancellationToken: ct).ConfigureAwait(false);

        await uow.ExecuteAsync(
            @"INSERT INTO dbo.SP_Pago (IDFactura, Fecha, Importe, CodSucursal, TasaCambio, CodPagoOrigen, Usr, Ts)
              VALUES (@IDFactura, @Fecha, @Importe, @CodSucursal, @TasaCambio, @CodPagoOrigen, @Usr, @Ts)",
            new
            {
                fac.IDFactura,
                Fecha = req.FechaPago,
                Importe = req.Importe + req.ImporteMora,
                CodSucursal = sucursal,
                TasaCambio = tasaCambio,
                req.CodPagoOrigen,
                Usr = usr,
                Ts = ts,
            }, cancellationToken: ct).ConfigureAwait(false);

        var detalles = await uow.QueryAsync<FacturaDetalleRow>(
            "SELECT CodItemPago, NroCuota, CAST(ISNULL(Importe,0) AS float) Importe FROM dbo.SP_FacturaDetalle WHERE IdFactura = @id",
            new { id = fac.IDFactura }, cancellationToken: ct).ConfigureAwait(false);

        var cuotasPagas = pres.CuotasPagas;
        var saldo = pres.Saldo;
        var estadoCuota = hayRetencion ? CuotaEstadoRetenida : CuotaEstadoCancelada;

        foreach (var d in detalles)
        {
            if (d.CodItemPago != ItemPagoCancelacion && d.CodItemPago != ItemPagoCuota) continue;

            // Cancelación → todas las cuotas pendientes; cuota → la cuota indicada en el detalle.
            var cuotas = d.CodItemPago == ItemPagoCancelacion
                ? await uow.QueryAsync<int>(
                    "SELECT Nro FROM dbo.SP_Cuota WHERE IDPrestamo=@id AND CodCuotaEstado=@pen ORDER BY Nro",
                    new { id = fac.IdPrestamo, pen = CuotaEstadoPendiente }, cancellationToken: ct).ConfigureAwait(false)
                : await uow.QueryAsync<int>(
                    "SELECT Nro FROM dbo.SP_Cuota WHERE IDPrestamo=@id AND Nro=@nro",
                    new { id = fac.IdPrestamo, nro = d.NroCuota }, cancellationToken: ct).ConfigureAwait(false);

            foreach (var nro in cuotas)
            {
                await uow.ExecuteAsync(
                    "UPDATE dbo.SP_Cuota SET FechaPago=@f, CodCuotaEstado=@estado, Usr=@usr, Ts=@ts WHERE IDPrestamo=@id AND Nro=@nro",
                    new { f = req.FechaPago, estado = estadoCuota, usr, ts, id = fac.IdPrestamo, nro }, cancellationToken: ct).ConfigureAwait(false);

                var amort = await uow.ExecuteScalarAsync<double>(
                    "SELECT CAST(ISNULL(Amortizacion,0) AS float) FROM dbo.SP_CuadroAmortizacion WHERE IDPrestamo=@id AND NroCuota=@nro",
                    new { id = fac.IdPrestamo, nro }, cancellationToken: ct).ConfigureAwait(false);
                cuotasPagas += 1;
                saldo -= amort;
            }

            // NroCuota no lo setea el VB6 (queda en 0, el default de Access; la columna es NOT NULL acá).
            await uow.ExecuteAsync(
                "INSERT INTO dbo.SP_Pago_ItemPago (IDFactura, CodItemPago, NroCuota, Importe, Usr, Ts) VALUES (@id, @item, 0, @imp, @usr, @ts)",
                new { id = fac.IDFactura, item = ItemPagoCuota, imp = d.Importe, usr, ts }, cancellationToken: ct).ConfigureAwait(false);
        }

        if (req.ImporteMora > 0)
            await uow.ExecuteAsync(
                "INSERT INTO dbo.SP_Pago_ItemPago (IDFactura, CodItemPago, NroCuota, Importe, Usr, Ts) VALUES (@id, @item, 0, @imp, @usr, @ts)",
                new { id = fac.IDFactura, item = ItemPagoMora, imp = req.ImporteMora, usr, ts }, cancellationToken: ct).ConfigureAwait(false);

        // Estado final: cancelado si no quedan cuotas; si no, en proceso (o retención si hay retención).
        string estadoPres;
        if (pres.Cuotas - cuotasPagas <= 0)
        {
            estadoPres = await VerificarEstadoAsync(uow, PrestamoEstadoCancelado, pres.CodPrestamoEstado, ct).ConfigureAwait(false);
            saldo = 0;
        }
        else
        {
            estadoPres = await VerificarEstadoAsync(uow,
                hayRetencion ? PrestamoEstadoRetencion : PrestamoEstadoEnProceso, pres.CodPrestamoEstado, ct).ConfigureAwait(false);
        }

        await uow.ExecuteAsync(
            "UPDATE dbo.SP_Prestamo SET CuotasPagas=@cp, Saldo=@s, CodPrestamoEstado=@estado, Usr=@usr, Ts=@ts WHERE IDPrestamo=@id",
            new { cp = cuotasPagas, s = saldo, estado = estadoPres, usr, ts, id = fac.IdPrestamo }, cancellationToken: ct).ConfigureAwait(false);

        return ResultadoPago.Ok;
    }

    /// <summary>Registra un pago con error (port de IngresarPagoError) en SP_PagoError.</summary>
    private static async Task InsertarPagoErrorAsync(IDbExecutor uow, FacturaRow fac, IngresarPagoRequest req,
        string codFacturaEstado, string usr, DateTime ts, CancellationToken ct)
    {
        var tasaCambio = await uow.ExecuteScalarAsync<double>(
            "SELECT CAST(ISNULL(TasaCambio,0) AS float) FROM dbo.SP_Moneda WHERE CodMoneda = @m",
            new { m = fac.CodMoneda }, cancellationToken: ct).ConfigureAwait(false);
        await uow.ExecuteAsync(
            @"INSERT INTO dbo.SP_PagoError (IDFactura, Fecha, Importe, CodSucursal, TasaCambio, CodFacturaEstado, Usr, Ts)
              VALUES (@id, @f, @imp, @suc, @tc, @estado, @usr, @ts)",
            new
            {
                id = fac.IDFactura,
                f = req.FechaPago,
                imp = req.Importe + req.ImporteMora,
                suc = req.CodSucursal ?? "",
                tc = tasaCambio,
                estado = codFacturaEstado,
                usr,
                ts,
            }, cancellationToken: ct).ConfigureAwait(false);
    }

    /// <summary>Mapeo de columnas del archivo Abitab (tabla MapeoAbitab).</summary>
    public Task<IReadOnlyList<AbitabCampo>> GetMapeoAbitabAsync(CancellationToken ct = default)
        => _db.QueryAsync<AbitabCampo>(
            "SELECT Campo, Inicio, Largo FROM dbo.MapeoAbitab ORDER BY Inicio", cancellationToken: ct);

    /// <summary>
    /// Carga batch de pagos desde un archivo de cobranza Abitab (port de cAdmPago.ProcesarPagos). Procesa
    /// todas las líneas en una sola transacción: las que se pueden cobrar se aplican; las que no, se
    /// registran en ErrCargaAbitab y se siguen procesando. Un error inesperado (línea mal formada, falla de
    /// base) revierte toda la carga. Requiere el mapeo de columnas configurado (MapeoAbitab).
    /// </summary>
    public async Task<CargaPagosResultado> ProcesarPagosAsync(IReadOnlyList<string> lineas, DateTime fecha, CancellationToken ct = default)
    {
        var usr = ClampUsr(_user.UserName);
        var mapeo = await GetMapeoAbitabAsync(ct).ConfigureAwait(false);
        if (mapeo.Count == 0)
            throw new InvalidOperationException("El mapeo del archivo Abitab no está configurado (la tabla MapeoAbitab está vacía).");

        var errores = new List<CargaPagoError>();
        var conError = 0;

        await using var uow = await _db.BeginTransactionAsync(ct).ConfigureAwait(false);

        var nroReng = 0;
        foreach (var linea in lineas)
        {
            if (string.IsNullOrWhiteSpace(linea)) continue;
            nroReng++;

            var p = AbitabPagoParser.Parse(mapeo, linea);
            var req = new IngresarPagoRequest(
                (int)p.NroFactura, p.FechaPago, p.Importe, p.Interes, PagoOrigen.Abitab, p.CodSucursal);

            var r = await IngresarCoreAsync(uow, req, usr, DateTime.Now, ct).ConfigureAwait(false);
            if (r == ResultadoPago.Ok) continue;

            conError++;
            var descrip = DescribirError(r, nroReng);
            // Limpia un eventual registro previo de esa línea y deja el del error actual (idempotente al reprocesar).
            await uow.ExecuteAsync(
                "DELETE FROM dbo.ErrCargaAbitab WHERE Fecha=@f AND NroReng=@r", new { f = fecha, r = nroReng }, cancellationToken: ct).ConfigureAwait(false);
            await uow.ExecuteAsync(
                "INSERT INTO dbo.ErrCargaAbitab (Fecha, NroReng, Descrip, Usr, Ts) VALUES (@f, @r, @d, @usr, @ts)",
                new { f = fecha, r = nroReng, d = descrip, usr, ts = DateTime.Now }, cancellationToken: ct).ConfigureAwait(false);
            errores.Add(new CargaPagoError(nroReng, r, descrip));
        }

        await uow.CommitAsync(ct).ConfigureAwait(false);
        return new CargaPagosResultado(nroReng, conError, errores);
    }

    private static string DescribirError(ResultadoPago r, int nroReng) => r switch
    {
        ResultadoPago.NoCoincideImporte => $"Línea: {nroReng} - el importe cobrado es incorrecto.",
        ResultadoPago.FacturaInexistente => $"Línea: {nroReng} - no existe el número de factura.",
        ResultadoPago.NoCoincideInteres => $"Línea: {nroReng} - los intereses cobrados son incorrectos.",
        ResultadoPago.EstadoIncorrecto => $"Línea: {nroReng} - el estado de la factura no admite ingreso de pagos.",
        ResultadoPago.Retenida => $"Línea: {nroReng} - la factura fue retenida; revise el informe de pagos con error.",
        _ => $"Línea: {nroReng} - error inesperado.",
    };

    /// <summary>
    /// Port de VerificarEstado: si la transición (ant → sig) está habilitada en SP_CtrlPrestamoEstado
    /// devuelve el estado siguiente; si no, mantiene el actual.
    /// </summary>
    private static async Task<string> VerificarEstadoAsync(IDbExecutor db, string sig, string ant, CancellationToken ct)
    {
        var existe = await db.ExecuteScalarAsync<int>(
            "SELECT COUNT(*) FROM dbo.SP_CtrlPrestamoEstado WHERE PrestamoEstadoSig=@sig AND PrestamoEstadoAnt=@ant",
            new { sig, ant }, cancellationToken: ct).ConfigureAwait(false);
        return existe > 0 ? sig : ant;
    }

    /// <summary>
    /// Ingresa un pago participando de una transacción externa (lo usa la retención, que cobra varias
    /// facturas en su propia transacción). No hace commit; devuelve el motivo si la factura no admite pago.
    /// </summary>
    public Task<ResultadoPago> IngresarPagoEnAsync(IDbExecutor uow, IngresarPagoRequest req, CancellationToken ct = default)
        => IngresarCoreAsync(uow, req, ClampUsr(_user.UserName), DateTime.Now, ct);

    /// <summary>
    /// Deshace el pago de una factura (port de cAdmPrestamo.DeshacerPago) participando de una transacción
    /// externa: borra el pago, vuelve la factura a emitida y su cuota a pendiente, y revierte saldo, cuotas
    /// pagas y estado del préstamo (CalcularEstado). Opera por IDFactura. No hace commit.
    /// </summary>
    public async Task DeshacerPagoEnAsync(IDbExecutor uow, int idFactura, CancellationToken ct = default)
    {
        var usr = ClampUsr(_user.UserName);
        var ts = DateTime.Now;

        var fac = await uow.QuerySingleOrDefaultAsync<FacturaDeshacerRow>(
            "SELECT TOP 1 IDFactura, IdPrestamo, CAST(ISNULL(ImpAmortizable,0) AS float) ImpAmortizable FROM dbo.SP_Factura WHERE IDFactura=@id",
            new { id = idFactura }, cancellationToken: ct).ConfigureAwait(false)
            ?? throw new InvalidOperationException($"No existe la factura con IDFactura {idFactura}.");

        // PASO 1: eliminar el pago.
        await uow.ExecuteAsync("DELETE FROM dbo.SP_Pago WHERE IDFactura=@id", new { id = fac.IDFactura }, cancellationToken: ct).ConfigureAwait(false);
        // PASO 2: la factura vuelve a emitida.
        await uow.ExecuteAsync(
            "UPDATE dbo.SP_Factura SET CodFacturaEstado=@emi, FechaPago=NULL, Usr=@usr, Ts=@ts WHERE IDFactura=@id",
            new { emi = FacturaEstadoEmitida, usr, ts, id = fac.IDFactura }, cancellationToken: ct).ConfigureAwait(false);
        // PASO 3: la cuota vuelve a pendiente (la cuota de la factura está en su detalle).
        var nroCuota = await uow.ExecuteScalarAsync<int?>(
            "SELECT TOP 1 NroCuota FROM dbo.SP_FacturaDetalle WHERE IdFactura=@id AND CodItemPago=@cuo",
            new { id = fac.IDFactura, cuo = ItemPagoCuota }, cancellationToken: ct).ConfigureAwait(false);
        if (nroCuota is not null)
            await uow.ExecuteAsync(
                "UPDATE dbo.SP_Cuota SET CodCuotaEstado=@pen, FechaPago=NULL, Usr=@usr, Ts=@ts WHERE IDPrestamo=@id AND Nro=@nro",
                new { pen = CuotaEstadoPendiente, usr, ts, id = fac.IdPrestamo, nro = nroCuota }, cancellationToken: ct).ConfigureAwait(false);
        // PASO 4: revertir saldo, cuotas pagas y estado del préstamo.
        var estado = await CalcularEstadoAsync(uow, fac.IdPrestamo, ct).ConfigureAwait(false);
        await uow.ExecuteAsync(
            "UPDATE dbo.SP_Prestamo SET Saldo=Saldo+@amort, CuotasPagas=CuotasPagas-1, CodPrestamoEstado=@estado, Usr=@usr, Ts=@ts WHERE IDPrestamo=@id",
            new { amort = fac.ImpAmortizable, estado, usr, ts, id = fac.IdPrestamo }, cancellationToken: ct).ConfigureAwait(false);
    }

    /// <summary>
    /// Port de CalcularEstado: sobre las facturas no anuladas del préstamo, "ret" si hay alguna retenida;
    /// si no "pro" si hay alguna cancelada; si no "emi".
    /// </summary>
    private static async Task<string> CalcularEstadoAsync(IDbExecutor uow, int idPrestamo, CancellationToken ct)
    {
        var estados = await uow.QueryAsync<string>(
            "SELECT CodFacturaEstado FROM dbo.SP_Factura WHERE IdPrestamo=@id AND CodFacturaEstado<>@anu",
            new { id = idPrestamo, anu = FacturaEstadoAnulada }, cancellationToken: ct).ConfigureAwait(false);
        if (estados.Any(e => e == FacturaEstadoRetenida)) return PrestamoEstadoRetencion;
        if (estados.Any(e => e == FacturaEstadoCancelada)) return PrestamoEstadoEnProceso;
        return PrestamoEstadoEmitido;
    }

    private sealed class FacturaDeshacerRow
    {
        public int IDFactura { get; set; }
        public int IdPrestamo { get; set; }
        public double ImpAmortizable { get; set; }
    }

    private sealed class FacturaRow
    {
        public int IDFactura { get; set; }
        public int IdPrestamo { get; set; }
        public double Importe { get; set; }
        public string CodFacturaEstado { get; set; } = "";
        public string CodMoneda { get; set; } = "";
    }

    private sealed class FacturaMoraRow
    {
        public double Importe { get; set; }
        public DateTime FechaVencimiento { get; set; }
        public string CodMoneda { get; set; } = "";
    }

    private sealed class PrestamoRow
    {
        public int Cuotas { get; set; }
        public int CuotasPagas { get; set; }
        public double Saldo { get; set; }
        public string CodPrestamoEstado { get; set; } = "";
    }

    private sealed class FacturaDetalleRow
    {
        public string CodItemPago { get; set; } = "";
        public int? NroCuota { get; set; }
        public double Importe { get; set; }
    }
}
