using System.Collections;
using DevExpress.DataAccess.ObjectBinding;
using DevExpress.XtraPrinting.Drawing;
using DevExpress.XtraReports.UI;

namespace Sgpa.Web.Reports.Prestamos
{
    /// <summary>Cesión de créditos (préstamos de fideicomiso). Layout en el .Designer.cs; datos + logo vía <see cref="Bind"/>.</summary>
    public partial class CesionReport : XtraReport
    {
        public CesionReport()
        {
            InitializeComponent();
        }

        public void Bind(IEnumerable data, string? logoPath)
        {
            if (DataSource is ObjectDataSource ods)
                ods.DataSource = data;
            if (logoPath is not null && File.Exists(logoPath))
                xrLogo.ImageSource = ImageSource.FromFile(logoPath);
        }
    }
}
