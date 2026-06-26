using System.Data;
using Sgpa.Data;

namespace Sgpa.Web.Reporting.Predefinidos;

/// <summary>Un punto del gráfico estadístico: una categoría (Descrip) con su cantidad.</summary>
public sealed class EstadisticaPunto
{
    public int? Codigo { get; set; }
    public string Descrip { get; set; } = "";
    public double Cantidad { get; set; }

    /// <summary>Segunda dimensión opcional (sexo, grupo etario…) para los informes cross-tab: si está,
    /// el gráfico genera una serie por cada valor distinto.</summary>
    public string? Serie { get; set; }

    private static readonly System.Globalization.CultureInfo EsUy = System.Globalization.CultureInfo.GetCultureInfo("es-UY");

    /// <summary>Descripción para la tabla de detalle: incluye la 2da dimensión si la hay.</summary>
    public string DescripDetalle => string.IsNullOrEmpty(Serie) ? Descrip : $"{Descrip} — {Serie}";
    public string CantidadFmt => Cantidad.ToString("N0", EsUy);
}

/// <summary>Una fila del catálogo de informes estadísticos (vista dbo.Rs_InformeEstadistico).</summary>
public sealed class InformeEstadisticoDef
{
    public int IdRpt { get; set; }
    public string Grupo { get; set; } = "";
    public int Orden { get; set; }
    public string TituloPantalla { get; set; } = "";
    public string TituloRpt { get; set; } = "";
    public bool MesAnio { get; set; }
    public bool Periodo { get; set; }
    public bool Empresa { get; set; }
    public bool Fecha { get; set; }
    public bool GrupoEtario { get; set; }
    public bool Patologia { get; set; }
    public string? Comentario { get; set; }
}

/// <summary>Parámetros que el usuario carga en la pantalla (los visibles dependen de los flags del catálogo).</summary>
public sealed class InformeParametros
{
    public int CodEmpresa { get; set; }       // 0 = Todas
    public int? Mes { get; set; }              // yyyymm
    public DateTime? FechaIni { get; set; }
    public DateTime? FechaFin { get; set; }
    public DateTime? Fecha { get; set; }
    public int GrupoEtario { get; set; }       // 0 = Todos; 1=<30, 2=30-39, 3=40-49, 4=50-59, 5=60+
    public int? CodPatologia { get; set; }     // null = todas (filtro de los informes de afecciones)
}

public sealed class EmpresaItem
{
    public int CodEmpresa { get; set; }
    public string Nombre { get; set; } = "";
}

public sealed class PatologiaItem
{
    public int CodPatologia { get; set; }
    public string Descrip { get; set; } = "";
}

/// <summary>
/// Provee el catálogo y los datos de los Informes Estadísticos (gráficos), port de frmInformeEstadistico.frm.
/// Cada IdRpt corre la misma cadena de TVFs 8xx que hacían los GenRptN del VB6, pero arma la lista de puntos
/// en memoria (no muta la tabla global 600_Rpt_CantidadDescrip).
/// </summary>
public interface IEstadisticaReporteData
{
    Task<IReadOnlyList<InformeEstadisticoDef>> GetCatalogoAsync(CancellationToken ct = default);
    Task<InformeEstadisticoDef?> GetDefAsync(int idRpt, CancellationToken ct = default);
    Task<IReadOnlyList<EmpresaItem>> GetEmpresasAsync(CancellationToken ct = default);
    Task<IReadOnlyList<PatologiaItem>> GetPatologiasAsync(CancellationToken ct = default);
    Task<IReadOnlyList<EstadisticaPunto>> GetPuntosAsync(int idRpt, InformeParametros p, CancellationToken ct = default);
}

public sealed class EstadisticaReporteData(IDbExecutor db) : IEstadisticaReporteData
{
    public async Task<IReadOnlyList<InformeEstadisticoDef>> GetCatalogoAsync(CancellationToken ct = default)
        => await db.QueryAsync<InformeEstadisticoDef>(
            @"SELECT IdRpt, Grupo, Orden, TituloPantalla, TituloRpt, MesAnio, Periodo, Empresa, Fecha,
                     GrupoEtario, Patologia, Comentario
              FROM dbo.Rs_InformeEstadistico
              ORDER BY Grupo, Orden, IdRpt", cancellationToken: ct);

    public async Task<InformeEstadisticoDef?> GetDefAsync(int idRpt, CancellationToken ct = default)
        => await db.QuerySingleOrDefaultAsync<InformeEstadisticoDef>(
            @"SELECT IdRpt, Grupo, Orden, TituloPantalla, TituloRpt, MesAnio, Periodo, Empresa, Fecha,
                     GrupoEtario, Patologia, Comentario
              FROM dbo.Rs_InformeEstadistico WHERE IdRpt = @idRpt", new { idRpt }, cancellationToken: ct);

    public async Task<IReadOnlyList<EmpresaItem>> GetEmpresasAsync(CancellationToken ct = default)
        => await db.QueryAsync<EmpresaItem>(
            "SELECT CodEmpresa, Nombre FROM dbo.Rs_Empresa_Desc_Real ORDER BY Nombre", cancellationToken: ct);

    public async Task<IReadOnlyList<PatologiaItem>> GetPatologiasAsync(CancellationToken ct = default)
        => await db.QueryAsync<PatologiaItem>(
            "SELECT CodPatologia, Descrip FROM dbo.Patologia ORDER BY Descrip", cancellationToken: ct);

    // ---- fragmentos SQL para los cross-tabs de certificaciones (actos/días × sexo/edad × patología) ----
    // Join base AfeccionTipo → Afiliado+Certificacion → AfeccionGrupo → Patologia (igual que 817/818), filtrado por período.
    private const string CertifBaseFrom = @"
        FROM AfeccionTipo
          INNER JOIN (Afiliado INNER JOIN Certificacion ON Afiliado.CI = Certificacion.CI)
                  ON AfeccionTipo.CodAfeccionTipo = Certificacion.CodAfeccionTipo
          INNER JOIN AfeccionGrupo ON AfeccionTipo.CodAfeccionGrupo = AfeccionGrupo.CodAfeccionGrupo
          INNER JOIN Patologia ON AfeccionGrupo.CodPatologia = Patologia.CodPatologia
        WHERE Certificacion.Efectiva = 1
          AND Certificacion.FechaCertificacion BETWEEN (CASE WHEN @fi IS NOT NULL THEN @fi ELSE Certificacion.FechaCertificacion END)
                                                   AND (CASE WHEN @ff IS NOT NULL THEN @ff ELSE Certificacion.FechaCertificacion END)";
    private const string SexoSerie = "CASE Afiliado.Sexo WHEN 1 THEN 'Varones' WHEN 2 THEN 'Mujeres' ELSE 'Sin dato' END";
    private const string EdadExpr = "FLOOR(DATEDIFF(day, Afiliado.FechaNacimiento, Certificacion.FechaCertificacion) / 365.25)";
    private static readonly string GeBand = $"CASE WHEN {EdadExpr} < 30 THEN 1 WHEN {EdadExpr} < 40 THEN 2 WHEN {EdadExpr} < 50 THEN 3 WHEN {EdadExpr} < 60 THEN 4 ELSE 5 END";
    private static readonly string GeSerie = $"CASE WHEN {EdadExpr} < 30 THEN 'Menores de 30' WHEN {EdadExpr} < 40 THEN '30 a 39' WHEN {EdadExpr} < 50 THEN '40 a 49' WHEN {EdadExpr} < 60 THEN '50 a 59' ELSE '60 y más' END";

    /// <summary>Etiqueta de cada grupo etario (1..5); 0 = Todos.</summary>
    public static readonly (int Id, string Label)[] GruposEtarios =
    {
        (0, "Todos"), (1, "Menores de 30"), (2, "30 a 39"), (3, "40 a 49"), (4, "50 a 59"), (5, "60 y más"),
    };

    // ---- helpers ----

    private async Task<string> GetSmnAsync(CancellationToken ct)
        => (await db.ExecuteScalarAsync<string>("SELECT TOP 1 CONVERT(nvarchar(50), SMN) FROM dbo.Parametros", cancellationToken: ct))
           ?? "0";

    private async Task<double> ScalarAsync(string sql, object prm, CancellationToken ct)
        => await db.ExecuteScalarAsync<double?>(sql, prm, cancellationToken: ct) ?? 0d;

    /// <summary>yyyymm 5 meses antes (promedio de los últimos 6).</summary>
    private static int MesIni(int mes)
    {
        var d = new DateTime(mes / 100, mes % 100, 1).AddMonths(-5);
        return d.Year * 100 + d.Month;
    }

    private static EstadisticaPunto Pt(string desc, double cant, int? cod = null)
        => new() { Descrip = desc, Cantidad = cant, Codigo = cod };

    public async Task<IReadOnlyList<EstadisticaPunto>> GetPuntosAsync(int idRpt, InformeParametros p, CancellationToken ct = default)
    {
        int emp = p.CodEmpresa;
        int mes = p.Mes ?? 0;
        int mesIni = mes > 0 ? MesIni(mes) : 0;
        var r = new List<EstadisticaPunto>();

        switch (idRpt)
        {
            // ===== Empleos / Puestos =====
            case 1: // Cantidad de puestos de trabajo (empresa)
            {
                var fecha = p.Fecha ?? DateTime.Today;
                int fmes = fecha.Year * 100 + fecha.Month;
                if (emp > 0)
                {
                    var nombre = await db.ExecuteScalarAsync<string>(
                        "SELECT Nombre FROM dbo.Rs_Empresa_Desc_Real WHERE CodEmpresa=@emp", new { emp }, cancellationToken: ct);
                    r.Add(Pt(string.IsNullOrWhiteSpace(nombre) ? $"Empresa {emp}" : nombre!,
                        await ScalarAsync("SELECT Cantidad FROM [800_Cantidad_Empresa](@emp,@fecha)", new { emp, fecha }, ct)));
                    r.Add(Pt("Otras",
                        await ScalarAsync("SELECT Cantidad FROM [800_Cantidad_Otras](@emp,@fecha)", new { emp, fecha }, ct)));
                }
                else
                {
                    r.AddRange(await db.QueryAsync<EstadisticaPunto>(
                        @"SELECT CodEmpresa AS Codigo, Nombre AS Descrip, Cantidad
                          FROM [830_Rpt_Cantidad_Por_Puesto](@fecha,@fmes)
                          WHERE Cantidad > 0 ORDER BY Cantidad DESC", new { fecha, fmes }, cancellationToken: ct));
                }
                break;
            }

            // ===== Imponibles: discriminación por haberes =====
            case 2: // promedio últimos 6 meses
            {
                var smn = await GetSmnAsync(ct);
                double c0 = await ScalarAsync("SELECT Cantidad FROM [801_Ult6_Cantidad](@mes,@mesIni,@emp)", new { mes, mesIni, emp }, ct);
                double c125 = await ScalarAsync("SELECT Cantidad FROM [802_>125_Cantidad_Ult6](@mes,@mesIni,@smn,@emp)", new { mes, mesIni, smn, emp }, ct);
                double tot = await ScalarAsync("SELECT Cantidad FROM [801_Cantidad_Todos](@mes,@mesIni,@emp)", new { mes, mesIni, emp }, ct);
                r.Add(Pt("Haberes 0", c0));
                r.Add(Pt("Haberes > 1.25", c125));
                r.Add(Pt("Haberes < 1.25", tot - (c0 + c125)));
                break;
            }
            case 3: // último mes
            {
                var smn = await GetSmnAsync(ct);
                double c0 = await ScalarAsync("SELECT Cantidad FROM [801_UltMes_Cantidad](@mes,@emp)", new { mes, emp }, ct);
                double c125 = await ScalarAsync("SELECT Cantidad FROM [802_>125_Cantidad_UltMes](@mes,@smn,@emp)", new { mes, smn, emp }, ct);
                double tot = await ScalarAsync("SELECT Cantidad FROM [801_Cantidad_Todos](@mes,@mesIni,@emp)", new { mes, mesIni = mes, emp }, ct);
                r.Add(Pt("Haberes 0", c0));
                r.Add(Pt("Haberes > 1.25", c125));
                r.Add(Pt("Haberes < 1.25", tot - (c0 + c125)));
                break;
            }
            case 6: // > 20 SMN promedio 6 meses
            {
                var smn = await GetSmnAsync(ct);
                double mayor = await ScalarAsync("SELECT Cantidad FROM [803_>20_Cantidad_Ult6](@mes,@mesIni,@smn,@emp)", new { mes, mesIni, smn, emp }, ct);
                double total = await ScalarAsync("SELECT Cantidad FROM [802_>0_Cantidad_Ult6](@mes,@mesIni,@emp)", new { mes, mesIni, emp }, ct);
                r.Add(Pt("> 20 SMN", mayor));
                r.Add(Pt("< 20 SMN", total - mayor));
                break;
            }
            case 7: // > 20 SMN último mes
            {
                var smn = await GetSmnAsync(ct);
                double mayor = await ScalarAsync("SELECT Cantidad FROM [803_>20_Cantidad_UltMes](@mes,@smn,@emp)", new { mes, smn, emp }, ct);
                double total = await ScalarAsync("SELECT Cantidad FROM [802_>0_Cantidad_UltMes](@mes,@emp)", new { mes, emp }, ct);
                r.Add(Pt("> 20 SMN", mayor));
                r.Add(Pt("< 20 SMN", total - mayor));
                break;
            }
            case 8: // masa salarial > 20 SMN promedio 6 meses
            {
                var smn = await GetSmnAsync(ct);
                double mayor = await ScalarAsync("SELECT Masa FROM [804_>20_Masa_Ult6](@mes,@mesIni,@smn,@emp)", new { mes, mesIni, smn, emp }, ct);
                double total = await ScalarAsync("SELECT Masa FROM [804_>0_Masa_Ult6](@mes,@mesIni,@emp)", new { mes, mesIni, emp }, ct);
                r.Add(Pt("> 20 SMN", mayor));
                r.Add(Pt("< 20 SMN", total - mayor));
                break;
            }
            case 9: // masa salarial > 20 SMN último mes
            {
                var smn = await GetSmnAsync(ct);
                double mayor = await ScalarAsync("SELECT Masa FROM [804_>20_Masa_UltMes](@mes,@smn,@emp)", new { mes, smn, emp }, ct);
                double total = await ScalarAsync("SELECT Masa FROM [804_>0_Masa_UltMes](@mes,@emp)", new { mes, emp }, ct);
                r.Add(Pt("> 20 SMN", mayor));
                r.Add(Pt("< 20 SMN", total - mayor));
                break;
            }

            // ===== Certificaciones =====
            case 10: // Afiliados activos certificados (periodo)
            {
                double cert = await ScalarAsync("SELECT Cantidad FROM [805_CertificadosActivos](@fi,@ff)", new { fi = p.FechaIni, ff = p.FechaFin }, ct);
                double activos = await ScalarAsync("SELECT Cantidad FROM [805_Activos]", new { }, ct);
                r.Add(Pt("Certificados", cert));
                r.Add(Pt("Sin certificar", activos - cert));
                break;
            }
            case 11: // Afiliados certificados por Edad (periodo)
                r.AddRange(await EdadCertificadosAsync(p, ct));
                break;
            case 12: // Afiliados certificados por Sexo (periodo)
                r.AddRange(await db.QueryAsync<EstadisticaPunto>(
                    @"SELECT CASE Sexo WHEN 1 THEN 'Varones' WHEN 2 THEN 'Mujeres' END AS Descrip, Cantidad
                      FROM [806_CertificadosSexo](@fi,@ff) WHERE Sexo IN (1,2) ORDER BY Sexo",
                    new { fi = p.FechaIni, ff = p.FechaFin }, cancellationToken: ct));
                break;
            case 13: // Afiliados certificados por Especialidad (periodo)
            case 14: // Afiliados ACTIVOS certificados por Especialidad — en VB6 era otro layout Crystal (GenRpt9_1),
                     // misma query/params que el 13 (807). En gráfico es idéntico → se colapsa acá.
                r.AddRange(await db.QueryAsync<EstadisticaPunto>(
                    @"SELECT Codigo, Descripcion AS Descrip, Cantidad FROM [807_CertificadosEspecialidad](@fi,@ff) ORDER BY Cantidad DESC",
                    new { fi = p.FechaIni, ff = p.FechaFin }, cancellationToken: ct));
                break;
            case 15: // Actos certificatorios por Tipos de Afección (periodo)
            case 16: // Variante de layout Crystal (GenRpt10_1), misma query/params que el 15 (808) → se colapsa.
                r.AddRange(await db.QueryAsync<EstadisticaPunto>(
                    @"SELECT Codigo, Descripcion AS Descrip, Cantidad FROM [808_CertificadosAfecciones](@fi,@ff,@cod) ORDER BY Cantidad DESC",
                    new { fi = p.FechaIni, ff = p.FechaFin, cod = p.CodPatologia }, cancellationToken: ct));
                break;
            case 23: // Actos certificatorios diferentes por Tipos de Afección
                r.AddRange(await db.QueryAsync<EstadisticaPunto>(
                    @"SELECT Codigo, Descrip, Cantidad FROM [813_CertificadosAfeccion](@cod,@fi,@ff) ORDER BY Cantidad DESC",
                    new { fi = p.FechaIni, ff = p.FechaFin, cod = p.CodPatologia }, cancellationToken: ct));
                break;
            case 26: // Actos certificatorios por Grupos de Afección
            case 27: // Variante de layout Crystal (mismo GenRpt19 que el 26) → datos idénticos, se colapsa.
                r.AddRange(await db.QueryAsync<EstadisticaPunto>(
                    @"SELECT Codigo, Descripcion AS Descrip, Cantidad FROM [816_Certificados_GrupoAfeccion](@fi,@ff,@cod) ORDER BY Cantidad DESC",
                    new { fi = p.FechaIni, ff = p.FechaFin, cod = p.CodPatologia }, cancellationToken: ct));
                break;
            case 28: // Actos certificatorios por Patologías
            case 29: // Variante de layout Crystal (mismo GenRpt20 que el 28) → datos idénticos, se colapsa.
                r.AddRange(await db.QueryAsync<EstadisticaPunto>(
                    @"SELECT Codigo, Descripcion AS Descrip, Cantidad FROM [817_Certificados_Patologia](@fi,@ff) ORDER BY Cantidad DESC",
                    new { fi = p.FechaIni, ff = p.FechaFin }, cancellationToken: ct));
                break;
            case 30: // Días de certificación por Patologías
                r.AddRange(await db.QueryAsync<EstadisticaPunto>(
                    @"SELECT Codigo, Descripcion AS Descrip, Cantidad FROM [818_Certificados_Patologia](@fi,@ff) ORDER BY Cantidad DESC",
                    new { fi = p.FechaIni, ff = p.FechaFin }, cancellationToken: ct));
                break;
            case 31: // Días de certificación por Grupos de Afección
                r.AddRange(await db.QueryAsync<EstadisticaPunto>(
                    @"SELECT Codigo, Descrip, Cantidad FROM [819_Certificados_AfeccionGrupo](@fi,@ff,@cod) ORDER BY Cantidad DESC",
                    new { fi = p.FechaIni, ff = p.FechaFin, cod = p.CodPatologia }, cancellationToken: ct));
                break;
            case 32: // Días de certificación por Tipos de Afección
                r.AddRange(await db.QueryAsync<EstadisticaPunto>(
                    @"SELECT Codigo, Descrip, Cantidad FROM [820_Certificados_AfeccionTipo](@fi,@ff,@cod) ORDER BY Cantidad DESC",
                    new { fi = p.FechaIni, ff = p.FechaFin, cod = p.CodPatologia }, cancellationToken: ct));
                break;

            // ===== Afiliados (fecha) =====
            case 17: // Cantidad de Afiliados (activos / no activos)
            {
                double activos, total;
                if (p.Fecha is { } f)
                {
                    activos = await ScalarAsync("SELECT Cantidad FROM [809_AfiliadoActivoFecha_Cantidad](@f)", new { f }, ct);
                    total = await ScalarAsync("SELECT Cantidad FROM [809_AfiliadoFecha_Cantidad](@f)", new { f }, ct);
                }
                else
                {
                    activos = await ScalarAsync("SELECT Cantidad FROM [809_AfiliadoActivo_Cantidad]", new { }, ct);
                    total = await ScalarAsync("SELECT Cantidad FROM [809_Afiliado_Cantidad]", new { }, ct);
                }
                r.Add(Pt("Activos", activos));
                r.Add(Pt("No Activos", total - activos));
                break;
            }
            case 18: // Afiliados activos por Sexo (fecha)
                if (p.Fecha is { } fs)
                    r.AddRange(await db.QueryAsync<EstadisticaPunto>(
                        @"SELECT CASE Sexo WHEN 1 THEN 'Varones' WHEN 2 THEN 'Mujeres' END AS Descrip, Cantidad
                          FROM [810_AfiliadosSexo](@fs) WHERE Sexo IN (1,2) ORDER BY Sexo", new { fs }, cancellationToken: ct));
                else
                    r.AddRange(await db.QueryAsync<EstadisticaPunto>(
                        @"SELECT CASE Sexo WHEN 1 THEN 'Varones' WHEN 2 THEN 'Mujeres' END AS Descrip, Cantidad
                          FROM [810_AfiliadosActivoSexo] WHERE Sexo IN (1,2) ORDER BY Sexo", new { }, cancellationToken: ct));
                break;
            case 19: // Afiliados activos por Edad (fecha)
                r.AddRange(await EdadAfiliadosAsync(p, ct));
                break;
            case 22: // Afiliados activos por Especialidad (fecha)
                if (p.Fecha is { } fe)
                    r.AddRange(await db.QueryAsync<EstadisticaPunto>(
                        @"SELECT Codigo, Descripcion AS Descrip, Cantidad FROM [812_AfiliadosEspecialidad](@fe) ORDER BY Cantidad DESC",
                        new { fe }, cancellationToken: ct));
                else
                    r.AddRange(await db.QueryAsync<EstadisticaPunto>(
                        @"SELECT Codigo, Descripcion AS Descrip, Cantidad FROM [812_AfiliadoActivoEspecialidad] ORDER BY Cantidad DESC",
                        new { }, cancellationToken: ct));
                break;

            // ===== Imponibles: franjas (<125_Pct) =====
            case 20: // no superan 1,25 SMN último mes por franjas %
            {
                var smn = await GetSmnAsync(ct);
                r.AddRange(await db.QueryAsync<EstadisticaPunto>(
                    @"SELECT Grupo AS Descrip, COUNT(*) AS Cantidad FROM [811_Afiliado<125_Pct_UltMes](@mes,@smn,@emp)
                      WHERE Grupo IS NOT NULL GROUP BY Grupo ORDER BY MIN(Importe)", new { mes, smn, emp }, cancellationToken: ct));
                break;
            }
            case 21: // no superan 1,25 SMN promedio 6 meses por franjas %
            {
                var smn = await GetSmnAsync(ct);
                r.AddRange(await db.QueryAsync<EstadisticaPunto>(
                    @"SELECT Grupo AS Descrip, COUNT(*) AS Cantidad FROM [811_Afiliado<125_Pct_Ult6](@mes,@mesIni,@smn,@emp)
                      WHERE Grupo IS NOT NULL GROUP BY Grupo ORDER BY MIN(Importe)", new { mes, mesIni, smn, emp }, cancellationToken: ct));
                break;
            }
            case 24: // discriminados por franjas (+0-10, +10-25, +25) (mes, empresa)
            {
                var smn = await GetSmnAsync(ct);
                r.AddRange(await db.QueryAsync<EstadisticaPunto>(
                    @"SELECT Grupo AS Descrip, COUNT(*) AS Cantidad FROM [814_AfiliadoImponibleFranja](@mes,@smn,@emp)
                      WHERE Grupo IS NOT NULL GROUP BY Grupo ORDER BY MIN(Importe)", new { mes, smn, emp }, cancellationToken: ct));
                break;
            }
            case 25: // Afiliados con imponible discriminados por Especialidad (mes, empresa) — port de GenRpt18 /
                     // 815_Insert_AfiliadoEspecialidad: cada afiliado con imponible del mes se asigna a su(s)
                     // especialidad(es) y se cuenta por especialidad (NULL → '(Sin Especialidad)').
            {
                var smn = await GetSmnAsync(ct);
                r.AddRange(await db.QueryAsync<EstadisticaPunto>(
                    @"SELECT g.Grupo AS Descrip, COUNT(*) AS Cantidad
                      FROM (SELECT CASE WHEN ISNULL(e.Descrip,'') <> '' THEN e.Descrip ELSE '(Sin Especialidad)' END AS Grupo
                            FROM [815_AfiliadoImponible](@mes,@smn,@emp) ai
                              INNER JOIN dbo.Afiliado a ON ai.CI = a.CI
                              LEFT JOIN dbo.AfiliadoEspecialidad ae ON a.CI = ae.CI
                              LEFT JOIN dbo.Especialidad e ON ae.CodEspecialidad = e.CodEspecialidad) g
                      GROUP BY g.Grupo ORDER BY COUNT(*) DESC", new { mes, smn, emp }, cancellationToken: ct));
                break;
            }

            // ===== Prestaciones (periodo) =====
            case 36: // Cantidad de prestaciones por tipo
                r.AddRange(await db.QueryAsync<EstadisticaPunto>(
                    @"SELECT CodPrestacionTipo AS Codigo, DescPrestacionTipo AS Descrip, Cantidad
                      FROM [824_PrestacionesCantidad](@fi,@ff) ORDER BY Cantidad DESC",
                    new { fi = p.FechaIni, ff = p.FechaFin }, cancellationToken: ct));
                break;
            case 37: // Importe en pesos de prestaciones por tipo
                r.AddRange(await db.QueryAsync<EstadisticaPunto>(
                    @"SELECT CodPrestacionTipo AS Codigo, DescPrestacionTipo AS Descrip, Importe AS Cantidad
                      FROM [825_PrestacionesImporte_Pesos](@fi,@ff) ORDER BY Importe DESC",
                    new { fi = p.FechaIni, ff = p.FechaFin }, cancellationToken: ct));
                break;
            case 38: // Importe en dólares de prestaciones por tipo
                r.AddRange(await db.QueryAsync<EstadisticaPunto>(
                    @"SELECT CodPrestacionTipo AS Codigo, DescPrestacionTipo AS Descrip, Importe AS Cantidad
                      FROM [826_PrestacionesImporte_Dolares](@fi,@ff) ORDER BY Importe DESC",
                    new { fi = p.FechaIni, ff = p.FechaFin }, cancellationToken: ct));
                break;

            // ===== Cross-tabs sexo / grupo etario × patología (SQL nuevo de la app) =====
            case 33: // Actos certificatorios por sexo y patología
                r.AddRange(await db.QueryAsync<EstadisticaPunto>(
                    $@"SELECT Patologia.CodPatologia AS Codigo, MIN(Patologia.Descrip) AS Descrip,
                              {SexoSerie} AS Serie, COUNT(*) AS Cantidad
                       {CertifBaseFrom}
                       GROUP BY Patologia.CodPatologia, Afiliado.Sexo ORDER BY Codigo",
                    new { fi = p.FechaIni, ff = p.FechaFin }, cancellationToken: ct));
                break;
            case 43: // Días de certificación por sexo y patología
                r.AddRange(await db.QueryAsync<EstadisticaPunto>(
                    $@"SELECT Patologia.CodPatologia AS Codigo, MIN(Patologia.Descrip) AS Descrip,
                              {SexoSerie} AS Serie, SUM(DATEDIFF(day, Certificacion.FechaIni, Certificacion.FechaFin) + 1) AS Cantidad
                       {CertifBaseFrom}
                       GROUP BY Patologia.CodPatologia, Afiliado.Sexo ORDER BY Codigo",
                    new { fi = p.FechaIni, ff = p.FechaFin }, cancellationToken: ct));
                break;
            case 34: // Actos certificatorios por grupo etario y patología
                r.AddRange(await db.QueryAsync<EstadisticaPunto>(
                    $@"SELECT Patologia.CodPatologia AS Codigo, MIN(Patologia.Descrip) AS Descrip,
                              {GeSerie} AS Serie, COUNT(*) AS Cantidad
                       {CertifBaseFrom}
                       GROUP BY Patologia.CodPatologia, {GeBand}, {GeSerie} ORDER BY Codigo",
                    new { fi = p.FechaIni, ff = p.FechaFin }, cancellationToken: ct));
                break;
            case 35: // Actos certificatorios por patología (filtrado por grupo etario)
                r.AddRange(await db.QueryAsync<EstadisticaPunto>(
                    $@"SELECT Patologia.CodPatologia AS Codigo, MIN(Patologia.Descrip) AS Descrip, COUNT(*) AS Cantidad
                       {CertifBaseFrom}
                         AND (@ge = 0 OR ({GeBand}) = @ge)
                       GROUP BY Patologia.CodPatologia ORDER BY Cantidad DESC",
                    new { fi = p.FechaIni, ff = p.FechaFin, ge = p.GrupoEtario }, cancellationToken: ct));
                break;

            default:
                break;
        }

        return r;
    }

    private async Task<List<EstadisticaPunto>> EdadCertificadosAsync(InformeParametros p, CancellationToken ct)
    {
        var fi = p.FechaIni; var ff = p.FechaFin;
        return new List<EstadisticaPunto>
        {
            Pt("Menores de 30 años", await ScalarAsync("SELECT Cantidad FROM [806_CertificadosMenores](@a,@fi,@ff)", new { a = "29", fi, ff }, ct)),
            Pt("Entre de 30 y 39 años", await ScalarAsync("SELECT Cantidad FROM [806_CertificadosEntre](@ai,@af,@fi,@ff)", new { ai = "30", af = "39", fi, ff }, ct)),
            Pt("Entre de 40 y 49 años", await ScalarAsync("SELECT Cantidad FROM [806_CertificadosEntre](@ai,@af,@fi,@ff)", new { ai = "40", af = "49", fi, ff }, ct)),
            Pt("Entre de 50 y 59 años", await ScalarAsync("SELECT Cantidad FROM [806_CertificadosEntre](@ai,@af,@fi,@ff)", new { ai = "50", af = "59", fi, ff }, ct)),
            Pt("Mayores de 59 años", await ScalarAsync("SELECT Cantidad FROM [806_CertificadosMayores](@a,@fi,@ff)", new { a = "60", fi, ff }, ct)),
        };
    }

    private async Task<List<EstadisticaPunto>> EdadAfiliadosAsync(InformeParametros p, CancellationToken ct)
    {
        var res = new List<EstadisticaPunto>();
        if (p.Fecha is { } f)
        {
            res.Add(Pt("Menores de 30 años", await ScalarAsync("SELECT Cantidad FROM [810_AfiliadosMenores](@a,@f)", new { a = "29", f }, ct)));
            res.Add(Pt("Entre de 30 y 39 años", await ScalarAsync("SELECT Cantidad FROM [810_AfiliadosEntre](@ai,@af,@f)", new { ai = "30", af = "39", f }, ct)));
            res.Add(Pt("Entre de 40 y 49 años", await ScalarAsync("SELECT Cantidad FROM [810_AfiliadosEntre](@ai,@af,@f)", new { ai = "40", af = "49", f }, ct)));
            res.Add(Pt("Entre de 50 y 59 años", await ScalarAsync("SELECT Cantidad FROM [810_AfiliadosEntre](@ai,@af,@f)", new { ai = "50", af = "59", f }, ct)));
            res.Add(Pt("Mayores de 59 años", await ScalarAsync("SELECT Cantidad FROM [810_AfiliadosMayores](@a,@f)", new { a = "60", f }, ct)));
        }
        else
        {
            res.Add(Pt("Menores de 30 años", await ScalarAsync("SELECT Cantidad FROM [810_AfiliadoActivoMenores](@a)", new { a = "29" }, ct)));
            res.Add(Pt("Entre de 30 y 39 años", await ScalarAsync("SELECT Cantidad FROM [810_AfiliadoActivoEntre](@ai,@af)", new { ai = "30", af = "39" }, ct)));
            res.Add(Pt("Entre de 40 y 49 años", await ScalarAsync("SELECT Cantidad FROM [810_AfiliadoActivoEntre](@ai,@af)", new { ai = "40", af = "49" }, ct)));
            res.Add(Pt("Entre de 50 y 59 años", await ScalarAsync("SELECT Cantidad FROM [810_AfiliadoActivoEntre](@ai,@af)", new { ai = "50", af = "59" }, ct)));
            res.Add(Pt("Mayores de 59 años", await ScalarAsync("SELECT Cantidad FROM [810_AfiliadoActivoMayores](@a)", new { a = "60" }, ct)));
        }
        return res;
    }
}
