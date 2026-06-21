using Sgpa.Data;

namespace Sgpa.Business.Dashboard;

/// <summary>Cantidad de préstamos en un estado (para el gráfico del dashboard).</summary>
public sealed record EstadoPrestamo(string Estado, int Cantidad);

/// <summary>Indicadores del dashboard de inicio.</summary>
public sealed record DashboardInfo(
    int Afiliados,
    int AfiliadosActivos,
    int Empresas,
    int Prestamos,
    int PrestamosVigentes,
    decimal ImportePendiente,
    int Subsidios,
    int Certificaciones,
    int Prestaciones,
    IReadOnlyList<EstadoPrestamo> PrestamosPorEstado);

/// <summary>Arma los indicadores del dashboard (conteos por tabla + distribución de préstamos por estado).</summary>
public sealed class DashboardService
{
    private readonly IDbExecutor _db;

    public DashboardService(IDbExecutor db) => _db = db;

    public async Task<DashboardInfo> GetAsync(CancellationToken ct = default)
    {
        var c = await _db.QuerySingleOrDefaultAsync<Counts>(
            """
            SELECT
                (SELECT COUNT(*) FROM dbo.Afiliado)        AS Afiliados,
                -- Activos: con al menos un empleo vigente (sin baja) en una empresa real (CodEmpresa < 900).
                (SELECT COUNT(DISTINCT CI) FROM dbo.Trabaja WHERE FechaBaja IS NULL AND CodEmpresa < 900) AS AfiliadosActivos,
                (SELECT COUNT(*) FROM dbo.Empresa)         AS Empresas,
                (SELECT COUNT(*) FROM dbo.SP_Prestamo)     AS Prestamos,
                (SELECT COUNT(*) FROM dbo.SP_Prestamo WHERE CodPrestamoEstado NOT IN ('can','anu')) AS PrestamosVigentes,
                -- Importe pendiente de cobro: facturas emitidas no pagas, en pesos (se excluyen las anuladas).
                (SELECT ISNULL(SUM(Importe),0) FROM dbo.SP_Factura WHERE CodFacturaEstado='emi' AND FechaPago IS NULL AND CodMoneda='$') AS ImportePendiente,
                (SELECT COUNT(*) FROM dbo.SubsidioCabezal) AS Subsidios,
                (SELECT COUNT(*) FROM dbo.Certificacion)   AS Certificaciones,
                (SELECT COUNT(*) FROM dbo.Prestacion)      AS Prestaciones
            """, cancellationToken: ct).ConfigureAwait(false) ?? new Counts();

        // Sólo préstamos vigentes (se excluyen cancelados/anulados, que tapaban el resto del desglose).
        var estados = await _db.QueryAsync<EstadoPrestamo>(
            """
            SELECT COALESCE(e.Descrip, p.CodPrestamoEstado) AS Estado, COUNT(*) AS Cantidad
            FROM dbo.SP_Prestamo p
            LEFT JOIN dbo.SP_PrestamoEstado e ON e.CodPrestamoEstado = p.CodPrestamoEstado
            WHERE p.CodPrestamoEstado NOT IN ('can','anu')
            GROUP BY COALESCE(e.Descrip, p.CodPrestamoEstado)
            ORDER BY COUNT(*) DESC
            """, cancellationToken: ct).ConfigureAwait(false);

        return new DashboardInfo(c.Afiliados, c.AfiliadosActivos, c.Empresas, c.Prestamos, c.PrestamosVigentes,
            c.ImportePendiente, c.Subsidios, c.Certificaciones, c.Prestaciones, estados);
    }

    private sealed class Counts
    {
        public int Afiliados { get; set; }
        public int AfiliadosActivos { get; set; }
        public int Empresas { get; set; }
        public int Prestamos { get; set; }
        public int PrestamosVigentes { get; set; }
        public decimal ImportePendiente { get; set; }
        public int Subsidios { get; set; }
        public int Certificaciones { get; set; }
        public int Prestaciones { get; set; }
    }
}
