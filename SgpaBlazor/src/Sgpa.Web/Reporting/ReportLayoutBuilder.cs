using DevExpress.DataAccess.ObjectBinding;
using DevExpress.XtraReports.UI;
using Sgpa.Domain.Metadata;

namespace Sgpa.Web.Reporting;

/// <summary>
/// Arma el layout (.repx) inicial de un reporte bindeado a la entidad root vía <see cref="ObjectDataSource"/>,
/// emulando el "report bound to a DataType" de XAF ReportsV2. El Field List del diseñador muestra el grafo de
/// objetos del tipo: escalares + referencias (N-a-1) + colecciones (1-a-N), que son las nav props generadas.
/// El master-detail sale natural de las colecciones (como las collection properties de XAF), sin armar joins.
/// Los datos reales se cargan en runtime con <c>IReportGraphLoader</c> y se setean en el ObjectDataSource.
/// </summary>
public static class ReportLayoutBuilder
{
    public const string DataSourceName = "objectDataSource";

    public static byte[] BuildInitialLayout(string tablaRoot)
    {
        using var report = new XtraReport();

        var rootType = EntityCatalog.All
            .FirstOrDefault(m => m.Table.Equals(tablaRoot, StringComparison.OrdinalIgnoreCase))?.EntityType;

        if (rootType is not null)
        {
            // DataSource = Type → el diseñador deriva el esquema (navegación incluida) del tipo, sin datos.
            var ods = new ObjectDataSource { Name = DataSourceName, DataSource = rootType };
            report.DataSource = ods;
            report.ComponentStorage.Add(ods);
        }

        using var ms = new MemoryStream();
        report.SaveLayoutToXml(ms);
        return ms.ToArray();
    }
}
