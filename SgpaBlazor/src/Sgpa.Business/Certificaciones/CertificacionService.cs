using Sgpa.Data;
using Sgpa.Domain.Entities;

namespace Sgpa.Business.Certificaciones;

/// <summary>Certificación efectiva que se superpone con el período ingresado (310_CertificacionAnterior).</summary>
public sealed record CertificacionSuperpuesta(int NroLlamado, int? NroRecibo, DateTime FechaIni, DateTime FechaFin);

/// <summary>Resumen acumulado de certificaciones efectivas de un afiliado (102_DiasCertificados).</summary>
public sealed record CertificadoResumen(int Dias, int Cantidad);

/// <summary>
/// Reglas de negocio de certificaciones (port parcial de AbmCerti.frm). Por ahora cubre la validación
/// de superposición de períodos al dar de alta/modificar una certificación efectiva (port del bloque de
/// <c>DatosOk</c> que consulta la query 310_CertificacionAnterior).
/// </summary>
public sealed class CertificacionService
{
    private const int DiffDiasDefault = 90;

    private readonly IDbExecutor _db;
    public CertificacionService(IDbExecutor db) => _db = db;

    /// <summary>Próximo NroLlamado para una certificación nueva (001_Certificacion_Max = MAX+1).</summary>
    public async Task<int> ProximoNroLlamadoAsync(CancellationToken ct = default)
        => (int)(await _db.ExecuteScalarAsync<double?>(
            "SELECT [Max] FROM dbo.acc_sgpa_001_Certificacion_Max_q", cancellationToken: ct).ConfigureAwait(false) ?? 1d);

    /// <summary>Máxima diferencia de días aceptable de un período (xUsrParam 'DiffDiasCertificacion'; default 90).</summary>
    public async Task<int> GetDiffDiasCertificacionAsync(CancellationToken ct = default)
    {
        var v = await _db.ExecuteScalarAsync<string?>(
            "SELECT TOP 1 value1 FROM dbo.xUsrParam WHERE clave='DiffDiasCertificacion' AND value1 IS NOT NULL ORDER BY login",
            cancellationToken: ct).ConfigureAwait(false);
        return int.TryParse(v, out var d) ? d : DiffDiasDefault;
    }

    /// <summary>
    /// Validación bloqueante del alta/modificación (port de la parte que aborta de <c>DatosOk</c>):
    /// para una certificación efectiva exige fecha de certificación + inicio + fin, y verifica que no
    /// se superponga con otra (310_CertificacionAnterior). Devuelve el mensaje de error, o null si OK.
    /// </summary>
    public async Task<string?> ValidarAsync(Certificacion c, CancellationToken ct = default)
    {
        if (c.Efectiva && (c.FechaCertificacion is null || c.FechaIni is null || c.FechaFin is null))
            return "Debe ingresar fecha de certificación, inicio y fin del período para una certificación efectiva.";
        return await ValidarSuperposicionAsync(c, ct).ConfigureAwait(false);
    }

    /// <summary>
    /// Busca una certificación efectiva de OTRO llamado, del mismo afiliado, cuyo período se superponga
    /// con [fechaIni, fechaFin] (port de 310_CertificacionAnterior). Devuelve la primera o null si no hay.
    /// </summary>
    public async Task<CertificacionSuperpuesta?> BuscarSuperposicionAsync(
        long ci, DateTime fechaIni, DateTime fechaFin, int nroLlamado, CancellationToken ct = default)
        => (await _db.QueryAsync<CertificacionSuperpuesta>(
            @"SELECT TOP 1 NroLlamado, NroRecibo, FechaIni, FechaFin
              FROM dbo.acc_sgpa_310_CertificacionAnterior_q(@fechaIni, @fechaFin, @ci, @nroLlamado)",
            new { fechaIni, fechaFin, ci = (int)ci, nroLlamado }, cancellationToken: ct).ConfigureAwait(false)).FirstOrDefault();

    /// <summary>
    /// Valida la superposición de períodos (port del bloque de superposición de <c>DatosOk</c>). Sólo aplica
    /// a certificaciones efectivas con fechas válidas. Devuelve el mensaje de error, o null si está OK.
    /// </summary>
    public async Task<string?> ValidarSuperposicionAsync(Certificacion c, CancellationToken ct = default)
    {
        // La regla del VB6 sólo corre para certificaciones efectivas (chkEfectiva); sin fechas no se evalúa.
        if (!c.Efectiva || c.CI is null || c.FechaIni is null || c.FechaFin is null)
            return null;
        if (c.FechaIni > c.FechaFin)
            return "La fecha de inicio no puede ser mayor que la de fin.";

        var sup = await BuscarSuperposicionAsync(c.CI.Value, c.FechaIni.Value, c.FechaFin.Value, c.NroLlamado, ct).ConfigureAwait(false);
        if (sup is null) return null;

        return $"Existe una certificación que se superpone con el período ingresado " +
               $"(Nº llamado {sup.NroLlamado}, recibo {sup.NroRecibo}, " +
               $"período {sup.FechaIni:dd/MM/yyyy} – {sup.FechaFin:dd/MM/yyyy}).";
    }

    /// <summary>
    /// Días certificados efectivos acumulados del afiliado hasta el llamado dado, inclusive
    /// (102_DiasCertificados: suma de DATEDIFF(FechaIni,FechaFin)+1 con NroLlamado ≤ N; N=0 = todos).
    /// </summary>
    public async Task<int> GetDiasCertificadosAsync(long ci, int nroLlamado, CancellationToken ct = default)
        => await _db.ExecuteScalarAsync<int?>(
            "SELECT Dias FROM dbo.acc_sgpa_102_DiasCertificados_q(@ci, @nroLlamado)",
            new { ci = (int)ci, nroLlamado }, cancellationToken: ct).ConfigureAwait(false) ?? 0;

    /// <summary>
    /// Resumen acumulado de certificaciones efectivas del afiliado (días + cantidad), para mostrar al operador
    /// mientras carga (port del display de <c>txtCI_LostFocus</c>: lblDiasCertificados / lblCantidadCertificados).
    /// Usa 102_DiasCertificados con NroLlamado=0 (todas).
    /// </summary>
    public async Task<CertificadoResumen> GetResumenCertificadosAsync(long ci, CancellationToken ct = default)
        => await _db.QuerySingleOrDefaultAsync<CertificadoResumen>(
            "SELECT ISNULL(Dias,0) AS Dias, ISNULL(Cantidad,0) AS Cantidad FROM dbo.acc_sgpa_102_DiasCertificados_q(@ci, 0)",
            new { ci = (int)ci }, cancellationToken: ct).ConfigureAwait(false) ?? new CertificadoResumen(0, 0);

    /// <summary>Fin de prórroga más lejano del afiliado (403_UltProxCI: MAX(Fecha+Dias) de CertificacionProrroga); null si no tiene.</summary>
    public Task<DateTime?> GetFinProrrogaAsync(long ci, CancellationToken ct = default)
        => _db.ExecuteScalarAsync<DateTime?>(
            "SELECT Fecha FROM dbo.acc_sgpa_403_UltProxCI_q(@ci)", new { ci = (int)ci }, cancellationToken: ct);

    /// <summary>
    /// Avisos por días acumulados de certificación (port del bloque informativo de <c>DatosOk</c>). Como el VB6,
    /// con prórroga vigente sólo se evalúa la proximidad a su fin (a 30 días); sin prórroga, se avisa al
    /// acercarse a 365 (≥335 días) o 720 (≥690 días). No bloquea el guardado; sólo informa.
    /// </summary>
    public async Task<IReadOnlyList<string>> GetAvisosDiasAsync(Certificacion c, CancellationToken ct = default)
    {
        if (!c.Efectiva || c.CI is null || c.FechaFin is null)
            return Array.Empty<string>();

        var avisos = new List<string>();

        // Diferencia de días del período muy alta (VB6: confirmación YesNo; acá aviso no bloqueante).
        if (c.FechaIni is not null)
        {
            var span = (c.FechaFin.Value - c.FechaIni.Value).Days;
            var maxDiff = await GetDiffDiasCertificacionAsync(ct).ConfigureAwait(false);
            if (span > maxDiff)
                avisos.Add($"La diferencia de días entre el inicio y el fin del período ({span}) supera el máximo configurado ({maxDiff}).");
        }

        var finProrroga = await GetFinProrrogaAsync(c.CI.Value, ct).ConfigureAwait(false);
        // Sólo se necesitan los días acumulados cuando no hay prórroga (el VB6 ahí evalúa los topes de 365/720).
        var dias = finProrroga is null
            ? await GetDiasCertificadosAsync(c.CI.Value, c.NroLlamado, ct).ConfigureAwait(false)
            : 0;
        avisos.AddRange(ConstruirAvisosDias(dias, finProrroga, c.FechaFin.Value));

        return avisos;
    }

    /// <summary>
    /// Lógica pura de los avisos por días (umbrales, prioridad y fecha estimada). Con prórroga sólo aplica
    /// el aviso de su fin (si está a ≤30 días del fin del período); sin prórroga, 720 (≥690) o 365 (≥335).
    /// </summary>
    public static IReadOnlyList<string> ConstruirAvisosDias(int diasAcumulados, DateTime? finProrroga, DateTime fechaFin)
    {
        var avisos = new List<string>();

        if (finProrroga is not null)
        {
            if (finProrroga.Value >= fechaFin.AddDays(-30))
                avisos.Add($"El afiliado alcanzó, o está por alcanzar, la fecha de fin de la prórroga ({finProrroga.Value:dd/MM/yyyy}).");
            return avisos; // con prórroga, el VB6 no evalúa los topes de 365/720
        }

        if (diasAcumulados >= 690)
            avisos.Add($"El afiliado alcanzó, o está por alcanzar, 720 días (2 años) de certificación ({fechaFin.AddDays(720 - diasAcumulados):dd/MM/yyyy}).");
        else if (diasAcumulados >= 335)
            avisos.Add($"El afiliado alcanzó, o está por alcanzar, 365 días (1 año) de certificación ({fechaFin.AddDays(365 - diasAcumulados):dd/MM/yyyy}).");

        return avisos;
    }
}
