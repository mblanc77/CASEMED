using Sgpa.Data;

namespace Sgpa.Business.Prestamos;

/// <summary>Ficha del afiliado para el workbench (1000_AfiladoCI2Nombre).</summary>
public sealed class AfiliadoFicha
{
    public string? DescAfiliado { get; set; }
    public string? Direccion { get; set; }
    public string? Telefono { get; set; }
}

/// <summary>Parámetros del sistema de préstamos (SP_Parametro).</summary>
public sealed class PrestamoParametros
{
    public double UR { get; set; }
    public double Dolar { get; set; }
    public double TopeUR { get; set; }
    public double PctPrestamo { get; set; }
    public int MesesCalculo { get; set; }
    public int MaxCuotas { get; set; }
    public double TopeSueldos { get; set; }
}

public sealed class MonedaInfo
{
    public string CodMoneda { get; set; } = "";
    public string? Descrip { get; set; }
    public double Tasa { get; set; }
}

public sealed class PrestamoAnterior
{
    public int IDPrestamo { get; set; }
    public string? CodMoneda { get; set; }
    public DateTime? Fecha { get; set; }
    public double? Importe { get; set; }
    public double? Pct_Retenidas { get; set; }
}

/// <summary>
/// Datos crediticios del afiliado para evaluar el préstamo (port de ProcesarCI + cAdmPrestamo):
/// ficha, promedio de ingresos, meses de aportes, saldo/cuota de préstamos abiertos (en pesos) y tope.
/// </summary>
public sealed record AfiliadoCredito(
    AfiliadoFicha? Ficha, double Promedio, int Aportes, double SaldoAbierto, double CuotaAbierta, double Tope);

/// <summary>
/// Acceso a datos del workbench de préstamos. Reusa las queries Access migradas (acc_sp_*_q):
/// promedio (1000_AfiliadoPromedioMeses), aportes (1002_LiquidoAnioMesxCI), préstamos abiertos
/// (1022_*), próximo id (1001_PrestamoMax), préstamos anteriores (1115_*).
/// </summary>
public sealed class PrestamoRepository
{
    public const string CodMonedaDolar = "u$s";
    private const int ElegibilidadMinAportes = 3;

    private readonly IDbExecutor _db;
    public PrestamoRepository(IDbExecutor db) => _db = db;

    public async Task<PrestamoParametros> GetParametrosAsync(CancellationToken ct = default)
        => await _db.QuerySingleOrDefaultAsync<PrestamoParametros>(
            @"SELECT TOP 1 CAST(ISNULL(UR,0) AS float) UR, CAST(ISNULL(Dolar,0) AS float) Dolar,
                     CAST(ISNULL(TopeUR,0) AS float) TopeUR, CAST(ISNULL(PctPrestamo,0) AS float) PctPrestamo,
                     CAST(ISNULL(MesesCalculo,3) AS int) MesesCalculo, CAST(ISNULL(MaxCuotas,12) AS int) MaxCuotas,
                     CAST(ISNULL(TopeSueldos,0) AS float) TopeSueldos
              FROM dbo.SP_Parametro", cancellationToken: ct).ConfigureAwait(false)
           ?? new PrestamoParametros { MesesCalculo = 3, MaxCuotas = 12 };

    public async Task<IReadOnlyList<MonedaInfo>> GetMonedasAsync(CancellationToken ct = default)
        => await _db.QueryAsync<MonedaInfo>(
            "SELECT CodMoneda, Descrip, CAST(ISNULL(Tasa,0) AS float) Tasa FROM dbo.SP_Moneda ORDER BY CodMoneda",
            cancellationToken: ct).ConfigureAwait(false);

    /// <summary>
    /// Ficha del afiliado. La app SP usaba SP_Afiliado (tabla linkeada, vacía en NewSgpa2): en el modelo
    /// consolidado el afiliado es el de SGPA (dbo.Afiliado, poblado; los 718 CIs con préstamo existen ahí).
    /// </summary>
    public async Task<AfiliadoFicha?> GetAfiliadoAsync(long ci, CancellationToken ct = default)
        => (await _db.QueryAsync<AfiliadoFicha>(
            @"SELECT TOP 1
                  LTRIM(RTRIM(ISNULL(Nombres,'') + ' ' + ISNULL(Apellido1,'') + ' ' + ISNULL(Apellido2,''))) AS DescAfiliado,
                  Direccion, Telefono
              FROM dbo.Afiliado WHERE CI = @ci",
            new { ci }, cancellationToken: ct).ConfigureAwait(false)).FirstOrDefault();

    /// <summary>Próximo IDPrestamo (1001_PrestamoMax = MAX+1).</summary>
    public Task<int> ProximoIdPrestamoAsync(CancellationToken ct = default)
        => _db.ExecuteScalarAsync<int>("SELECT ISNULL(MAX(IDPrestamo),0)+1 FROM dbo.SP_Prestamo", cancellationToken: ct);

    /// <summary>Próximo IDFactura (1001_FacturaIdMax = MAX+1, mínimo 1).</summary>
    public Task<int> ProximoIdFacturaAsync(CancellationToken ct = default)
        => _db.ExecuteScalarAsync<int>("SELECT ISNULL(MAX(IDFactura),0)+1 FROM dbo.SP_Factura", cancellationToken: ct);

    /// <summary>Próximo NroFactura (1001_FacturaMax = MAX+1, mínimo 1).</summary>
    public Task<int> ProximoNroFacturaAsync(CancellationToken ct = default)
        => _db.ExecuteScalarAsync<int>("SELECT ISNULL(MAX(NroFactura),0)+1 FROM dbo.SP_Factura", cancellationToken: ct);

    /// <summary>Nº de empresa Abitab para las facturas (SP_Parametro.NroEmpresa).</summary>
    public Task<string?> GetNroEmpresaAbitabAsync(CancellationToken ct = default)
        => _db.ExecuteScalarAsync<string?>("SELECT TOP 1 NroEmpresa FROM dbo.SP_Parametro", cancellationToken: ct);

    /// <summary>Último vencimiento de una cuota no pendiente (1019_UltVtoCuotaNoPendiente); null si todas están pendientes.</summary>
    public Task<DateTime?> GetUltVtoCuotaNoPendienteAsync(int idPrestamo, CancellationToken ct = default)
        => _db.ExecuteScalarAsync<DateTime?>(
            "SELECT FechaVencimiento FROM dbo.acc_sp_1019_UltVtoCuotaNoPendienteXIDPrestamo_q(@idPrestamo)",
            new { idPrestamo }, cancellationToken: ct);

    /// <summary>Datos de pago de la moneda (1002_TasasxCodMoneda): tasa de cambio y código Abitab.</summary>
    public async Task<MonedaPago?> GetMonedaPagoAsync(string codMoneda, CancellationToken ct = default)
        => (await _db.QueryAsync<MonedaPago>(
            "SELECT CAST(ISNULL(TasaCambio,0) AS float) TasaCambio, ISNULL(CodAbitab,'') CodAbitab FROM dbo.SP_Moneda WHERE CodMoneda=@codMoneda",
            new { codMoneda }, cancellationToken: ct).ConfigureAwait(false)).FirstOrDefault();

    /// <summary>Carga un préstamo para edición (datos financieros fijos + datos de cobro/cheque/banco editables).</summary>
    public async Task<PrestamoEditView?> GetPrestamoEditAsync(int idPrestamo, CancellationToken ct = default)
        => (await _db.QueryAsync<PrestamoEditView>(
            @"SELECT TOP 1 IDPrestamo, CI, CodMoneda, CodPrestamoTipo,
                     CAST(ISNULL(Importe,0) AS float) Importe, Cuotas,
                     CAST(ISNULL(ImporteCuota,0) AS float) ImporteCuota, CAST(ISNULL(Tasa,0) AS float) Tasa,
                     CAST(ISNULL(Saldo,0) AS float) Saldo, CodPrestamoEstado, Fecha, FechaCobro,
                     ISNULL(NroSerieCheque,0) NroSerieCheque, ISNULL(NroCheque,0) NroCheque,
                     NroCta, Banco, Sucursal, Observaciones
              FROM dbo.SP_Prestamo WHERE IDPrestamo = @idPrestamo",
            new { idPrestamo }, cancellationToken: ct).ConfigureAwait(false)).FirstOrDefault();

    public Task<IReadOnlyList<PrestamoAnterior>> GetPrestamosAnterioresAsync(long ci, int idPrestamo, CancellationToken ct = default)
        => _db.QueryAsync<PrestamoAnterior>(
            @"SELECT IDPrestamo, CodMoneda, Fecha, CAST(ISNULL(Importe,0) AS float) Importe,
                     CAST(ISNULL(Pct_Retenidas,0) AS float) Pct_Retenidas
              FROM dbo.acc_sp_1115_PrestamosAnterioresxCI_q(@ci, @idPrestamo) ORDER BY IDPrestamo DESC",
            new { ci, idPrestamo }, cancellationToken: ct);

    /// <summary>
    /// Reúne los datos crediticios y calcula el tope (port de ProcesarCI + TopePrestamo).
    /// Con TopeSueldos=0 el tope es UR×TopeUR − saldo de préstamos abiertos; /Dólar si la moneda es USD.
    /// </summary>
    public async Task<AfiliadoCredito> GetCreditoAsync(long ci, string? moneda, PrestamoParametros prm, CancellationToken ct = default)
    {
        var ficha = await GetAfiliadoAsync(ci, ct).ConfigureAwait(false);

        var hoy = int.Parse(DateTime.Today.ToString("yyyyMM"));
        // Promedio: ventana de MesesCalculo meses, terminando 2 meses atrás (AddMonth del VB6).
        var promFin = AddMonth(hoy, -2);
        var promIni = AddMonth(hoy, -2 - prm.MesesCalculo + 1);
        var promedio = await _db.ExecuteScalarAsync<double?>(
            "SELECT CAST(ISNULL(Importe,0) AS float) FROM dbo.acc_sp_1000_AfiliadoPromedioMeses_q(@ci,@ini,@fin,@meses)",
            new { ci, ini = promIni, fin = promFin, meses = prm.MesesCalculo }, cancellationToken: ct).ConfigureAwait(false) ?? 0d;

        // Aportes: meses con líquido en los últimos 12 (ventana [-13,-2]).
        var aportes = await _db.ExecuteScalarAsync<int>(
            "SELECT COUNT(*) FROM dbo.acc_sp_1002_LiquidoAnioMesxCI_q(@ci,@ini,@fin)",
            new { ci, ini = AddMonth(hoy, -13), fin = AddMonth(hoy, -2) }, cancellationToken: ct).ConfigureAwait(false);

        var saldoAbierto = await GetImporteAbiertoAsync("dbo.acc_sp_1022_PrestamoActivoXCI_q", hasTasaCambio: true, ci, prm, ct).ConfigureAwait(false);
        var cuotaAbierta = await GetImporteAbiertoAsync("dbo.acc_sp_1022_ImporteCuotaPrestamoActivoXCI_q", hasTasaCambio: false, ci, prm, ct).ConfigureAwait(false);

        var topeBase = prm.TopeSueldos > 0 ? 0d /* ImporteSueldos no disponible (1021 sin migrar) */ : prm.UR * prm.TopeUR;
        var tope = topeBase - saldoAbierto;
        if (moneda == CodMonedaDolar && prm.Dolar > 0) tope /= prm.Dolar;
        if (tope < 0) tope = 0;

        return new AfiliadoCredito(ficha, promedio, aportes, saldoAbierto, cuotaAbierta, tope);
    }

    /// <summary>¿El afiliado puede pedir préstamo? (≥3 aportes, salvo Fideicomiso — el tipo lo evalúa el caller).</summary>
    public static bool EsElegible(int aportes) => aportes >= ElegibilidadMinAportes;

    private async Task<double> GetImporteAbiertoAsync(string func, bool hasTasaCambio, long ci, PrestamoParametros prm, CancellationToken ct)
    {
        var tasaCol = hasTasaCambio ? "CAST(ISNULL(TasaCambio,0) AS float)" : "CAST(0 AS float)";
        var row = (await _db.QueryAsync<ImporteMonedaRow>(
            $"SELECT TOP 1 CAST(ISNULL(Importe,0) AS float) Importe, CodMoneda, {tasaCol} TasaCambio FROM {func}(@ci)",
            new { ci }, cancellationToken: ct).ConfigureAwait(false)).FirstOrDefault();
        if (row is null) return 0d;
        var importe = row.Importe;
        if (row.CodMoneda == CodMonedaDolar)
            importe *= row.TasaCambio > 0 ? row.TasaCambio : prm.Dolar;
        return importe;
    }

    /// <summary>AddMonth del VB6 sobre un AnioMes (yyyymm): suma n meses.</summary>
    internal static int AddMonth(int anioMes, int n)
    {
        var d = new DateTime(anioMes / 100, anioMes % 100, 1).AddMonths(n);
        return d.Year * 100 + d.Month;
    }

    private sealed class ImporteMonedaRow
    {
        public double Importe { get; set; }
        public string? CodMoneda { get; set; }
        public double TasaCambio { get; set; }
    }
}
