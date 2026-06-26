using Sgpa.Data;

namespace Sgpa.Business.Imponibles;

/// <summary>
/// Carga de la Historia Laboral (BPS) — port de <c>frmCarHL</c> acotado al formato V2. Guarda el contenido
/// del archivo (ya parseado) en las tablas <c>Atyr_*</c> y deriva los imponibles a <c>dbo.Imponible</c>.
///
/// Todo corre en una transacción. La carga se identifica por (CodEmpresa, Anio, Mes) — la empresa la elige el
/// operador y el período sale del cabezal del archivo (tipo 4): recargar la misma empresa/mes <b>sobrescribe</b>
/// (modo Reemplazar, como el VB6). La derivación a <c>Imponible</c> replica <c>100_Delete_Imponible</c> +
/// <c>100_Insert_Imponible</c>: toma los conceptos 1 (sueldo) y 2 (aguinaldo) de las remuneraciones (tipo 7),
/// los suma por CI+concepto, y resuelve IdTrabaja/FechaIngreso contra el último Trabaja del afiliado en esa
/// empresa (<c>acc_sgpa_Rs_TrabajaUltimo_q</c>). DiasTrabajados queda en 30, igual que el VB6.
/// Sólo se cargan imponibles de personas que son afiliados con un Trabaja en la empresa; el resto (no cargados /
/// no encontrados) quedará para el reporte que se gestiona aparte (fuera de este alcance).
/// </summary>
public sealed class HistoriaLaboralImportService
{
    private readonly IDbExecutor _db;
    public HistoriaLaboralImportService(IDbExecutor db) => _db = db;

    public async Task<HistoriaLaboralResult> CargarAsync(int codEmpresa, ParsedAtyr p, string? usr, CancellationToken ct = default)
    {
        var (mes, anio) = (p.Mes, p.Anio);
        var scope = new { cod = codEmpresa, anio, mes };

        await using var uow = await _db.BeginTransactionAsync(ct).ConfigureAwait(false);

        // 1) Sobrescritura del landing Atyr_ para esta empresa/período.
        foreach (var tabla in new[] { "Atyr_Empresa", "Atyr_Cabezal", "Atyr_Persona", "Atyr_Actividad", "Atyr_Remuneracion" })
            await uow.ExecuteAsync(
                $"DELETE FROM dbo.{tabla} WHERE CodEmpresa=@cod AND Anio=@anio AND Mes=@mes",
                scope, cancellationToken: ct).ConfigureAwait(false);

        await uow.ExecuteAsync(InsertEmpresa, p.Empresas.Select(e => new
        {
            cod = codEmpresa, anio, mes, e.TipoDeclaracion, e.Version, e.Aplicacion, e.NroEmpresa,
            e.NroContribuyente, e.TipoAportacion, e.Denominacion, e.Domicilio, e.Telefono, usr
        }), cancellationToken: ct).ConfigureAwait(false);

        await uow.ExecuteAsync(InsertCabezal, p.Cabezales.Select(c => new
        {
            cod = codEmpresa, anio, mes, c.MesCargo, c.TipoContribuyente, c.Monto, c.FormaRealizacionObra,
            c.ActividadPrincipal, usr
        }), cancellationToken: ct).ConfigureAwait(false);

        await uow.ExecuteAsync(InsertPersona, p.Personas.Select(x => new
        {
            cod = codEmpresa, anio, mes, x.PaisDocumento, x.TipoDocumento, x.NroDocumento, x.CI,
            x.PrimerApellido, x.SegundoApellido, x.PrimerNombre, x.SegundoNombre, x.FechaNacimiento,
            x.Sexo, x.Nacionalidad, usr
        }), cancellationToken: ct).ConfigureAwait(false);

        await uow.ExecuteAsync(InsertActividad, p.Actividades.Select(x => new
        {
            cod = codEmpresa, anio, mes, x.PaisDocumento, x.TipoDocumento, x.NroDocumento, x.CI,
            x.AcumulacionLaboral, x.FechaIngreso, x.TipoRemuneracion, x.HorasSemanales, x.VinculoFuncional,
            x.CodExoneracion, x.ComputosEspeciales, x.Categoria, x.CajaActividad, x.AsignacionFamiliar,
            x.DiasTrabajados, x.HorasTrabajadas, x.SeguroSalud, x.CausalEgreso, x.FechaEgreso, usr
        }), cancellationToken: ct).ConfigureAwait(false);

        await uow.ExecuteAsync(InsertRemuneracion, p.Remuneraciones.Select(x => new
        {
            cod = codEmpresa, anio, mes, x.PaisDocumento, x.TipoDocumento, x.NroDocumento, x.CI,
            x.AcumulacionLaboral, x.Concepto, x.Remuneracion, x.Jornal, x.OtrosHaberes, usr
        }), cancellationToken: ct).ConfigureAwait(false);

        // 2) Imponibles (modo Reemplazar): borro el período y reinserto desde Atyr_Remuneracion (conceptos 1 y 2).
        await uow.ExecuteAsync(
            "DELETE FROM dbo.Imponible WHERE CodEmpresa=@cod AND Mes=@mes AND Anio=@anio",
            scope, cancellationToken: ct).ConfigureAwait(false);

        var imponibles = await uow.ExecuteAsync(InsertImponible,
            new { cod = codEmpresa, anio, mes, usr }, cancellationToken: ct).ConfigureAwait(false);

        await uow.CommitAsync(ct).ConfigureAwait(false);

        return new HistoriaLaboralResult(
            codEmpresa, mes, anio,
            p.Empresas.Count, p.Cabezales.Count, p.Personas.Count, p.Actividades.Count, p.Remuneraciones.Count,
            imponibles);
    }

    private const string InsertEmpresa = @"
INSERT INTO dbo.Atyr_Empresa
    (CodEmpresa, Anio, Mes, TipoDeclaracion, Version, Aplicacion, NroEmpresa, NroContribuyente, TipoAportacion, Denominacion, Domicilio, Telefono, Usr, Ts)
VALUES (@cod, @anio, @mes, @TipoDeclaracion, @Version, @Aplicacion, @NroEmpresa, @NroContribuyente, @TipoAportacion, @Denominacion, @Domicilio, @Telefono, @usr, SYSDATETIME())";

    private const string InsertCabezal = @"
INSERT INTO dbo.Atyr_Cabezal
    (CodEmpresa, Anio, Mes, MesCargo, TipoContribuyente, Monto, FormaRealizacionObra, ActividadPrincipal, Usr, Ts)
VALUES (@cod, @anio, @mes, @MesCargo, @TipoContribuyente, @Monto, @FormaRealizacionObra, @ActividadPrincipal, @usr, SYSDATETIME())";

    private const string InsertPersona = @"
INSERT INTO dbo.Atyr_Persona
    (CodEmpresa, Anio, Mes, PaisDocumento, TipoDocumento, NroDocumento, CI, PrimerApellido, SegundoApellido, PrimerNombre, SegundoNombre, FechaNacimiento, Sexo, Nacionalidad, Usr, Ts)
VALUES (@cod, @anio, @mes, @PaisDocumento, @TipoDocumento, @NroDocumento, @CI, @PrimerApellido, @SegundoApellido, @PrimerNombre, @SegundoNombre, @FechaNacimiento, @Sexo, @Nacionalidad, @usr, SYSDATETIME())";

    private const string InsertActividad = @"
INSERT INTO dbo.Atyr_Actividad
    (CodEmpresa, Anio, Mes, PaisDocumento, TipoDocumento, NroDocumento, CI, AcumulacionLaboral, FechaIngreso, TipoRemuneracion, HorasSemanales, VinculoFuncional, CodExoneracion, ComputosEspeciales, Categoria, CajaActividad, AsignacionFamiliar, DiasTrabajados, HorasTrabajadas, SeguroSalud, CausalEgreso, FechaEgreso, Usr, Ts)
VALUES (@cod, @anio, @mes, @PaisDocumento, @TipoDocumento, @NroDocumento, @CI, @AcumulacionLaboral, @FechaIngreso, @TipoRemuneracion, @HorasSemanales, @VinculoFuncional, @CodExoneracion, @ComputosEspeciales, @Categoria, @CajaActividad, @AsignacionFamiliar, @DiasTrabajados, @HorasTrabajadas, @SeguroSalud, @CausalEgreso, @FechaEgreso, @usr, SYSDATETIME())";

    private const string InsertRemuneracion = @"
INSERT INTO dbo.Atyr_Remuneracion
    (CodEmpresa, Anio, Mes, PaisDocumento, TipoDocumento, NroDocumento, CI, AcumulacionLaboral, Concepto, Remuneracion, Jornal, OtrosHaberes, Usr, Ts)
VALUES (@cod, @anio, @mes, @PaisDocumento, @TipoDocumento, @NroDocumento, @CI, @AcumulacionLaboral, @Concepto, @Remuneracion, @Jornal, @OtrosHaberes, @usr, SYSDATETIME())";

    // Port de 100_Insert_Imponible: agrupa por CI+Concepto, suma la remuneración, resuelve IdTrabaja/FechaIngreso
    // con el último Trabaja del afiliado en la empresa. Sólo conceptos 1 (sueldo) y 2 (aguinaldo), como el VB6.
    private const string InsertImponible = @"
INSERT INTO dbo.Imponible
    (CI, Concepto, Importe, CodEmpresa, Mes, Anio, DiasTrabajados, Usr, Ts, Fechaingreso, IdTrabaja, AnioMes)
SELECT r.CI, CAST(r.Concepto AS nvarchar(3)), SUM(r.Remuneracion),
       @cod, @mes, @anio, 30, @usr, SYSDATETIME(),
       MIN(t.FechaIngreso), MIN(t.IdTrabaja), @anio * 100 + @mes
FROM dbo.Atyr_Remuneracion r
    INNER JOIN dbo.Afiliado a ON r.CI = a.CI
    INNER JOIN dbo.acc_sgpa_Rs_TrabajaUltimo_q t ON a.CI = t.CI AND t.CodEmpresa = @cod
WHERE r.CodEmpresa = @cod AND r.Anio = @anio AND r.Mes = @mes
    AND r.Concepto IN (1, 2) AND r.CI IS NOT NULL AND r.Remuneracion IS NOT NULL
GROUP BY r.CI, r.Concepto";
}
