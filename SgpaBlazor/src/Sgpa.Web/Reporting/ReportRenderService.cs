using System.Globalization;
using System.Text;
using System.Text.RegularExpressions;
using DevExpress.DataAccess.ObjectBinding;
using DevExpress.XtraReports.UI;
using DevExpress.XtraReports.Web.Extensions;
using Sgpa.Data.Reporting;
using Sgpa.Domain.Metadata;

namespace Sgpa.Web.Reporting;

/// <summary>
/// Arma un <see cref="XtraReport"/> listo para ver/imprimir/exportar: carga el layout de dbo.Reporte y, si es
/// ObjectDataSource, le bindea el grafo de objetos (sólo las relaciones que el reporte usa, filtrado por claves
/// si se pasan). Compartido por el visor, el launcher del ListView y el endpoint de PDF.
/// </summary>
public interface IReportRenderService
{
    Task<XtraReport?> BuildAsync(int reportId, IReadOnlyCollection<string>? keys, CancellationToken ct = default);
}

public sealed class ReportRenderService(
    ReportStorageWebExtension storage,
    IReporteCatalogo catalogo,
    IReportGraphLoader loader) : IReportRenderService
{
    public async Task<XtraReport?> BuildAsync(int reportId, IReadOnlyCollection<string>? keys, CancellationToken ct = default)
    {
        var bytes = storage.GetData(reportId.ToString());
        if (bytes is null || bytes.Length == 0) return null;

        var info = await catalogo.GetAsync(reportId, ct).ConfigureAwait(false);

        var report = new XtraReport();
        using (var ms = new MemoryStream(bytes))
            report.LoadLayoutFromXml(ms);

        if (report.DataSource is ObjectDataSource ods && info?.TablaRoot is not null)
        {
            var meta = EntityCatalog.All.FirstOrDefault(m =>
                string.Equals(m.Table, info.TablaRoot, StringComparison.OrdinalIgnoreCase));
            if (meta is not null)
            {
                var typedKeys = ConvertKeys(keys, meta);
                var depth = typedKeys is null ? 1 : 2;   // sin filtro: liviano; con selección: navega más hondo
                var data = await loader.LoadAsync(meta.EntityType, typedKeys, depth, ExtractUsedRelations(bytes), ct)
                    .ConfigureAwait(false);
                ods.DataSource = data;
            }
        }

        return report;
    }

    // Claves (string, de selección o query) tipadas según la columna clave del root. null si no hay/clave compuesta.
    private static IReadOnlyCollection<object>? ConvertKeys(IReadOnlyCollection<string>? keys, EntityMetadata meta)
    {
        if (keys is null || keys.Count == 0 || meta.Keys.Count != 1) return null;
        var t = meta.Key.UnderlyingType;
        var list = new List<object>(keys.Count);
        foreach (var s in keys)
        {
            try { list.Add(Convert.ChangeType(s, t, CultureInfo.InvariantCulture)); }
            catch { list.Add(s); }
        }
        return list;
    }

    // Relaciones que el reporte usa: DataMembers de bandas + referencias [Nav.Campo]/[Coleccion].[Campo]. El layout es XML.
    private static IReadOnlySet<string> ExtractUsedRelations(byte[] xmlBytes)
    {
        var xml = Encoding.UTF8.GetString(xmlBytes);
        var used = new HashSet<string>(StringComparer.OrdinalIgnoreCase);

        foreach (Match m in Regex.Matches(xml, "DataMember=\"([^\"]+)\""))
            foreach (var seg in m.Groups[1].Value.Split('.', StringSplitOptions.RemoveEmptyEntries))
                used.Add(seg);

        foreach (Match m in Regex.Matches(xml, @"\[([A-Za-z_]\w*)\]?\."))
            used.Add(m.Groups[1].Value);

        return used;
    }
}
