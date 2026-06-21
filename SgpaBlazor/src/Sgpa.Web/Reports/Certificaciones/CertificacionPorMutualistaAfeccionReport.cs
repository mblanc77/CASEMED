using System.Collections;
using DevExpress.DataAccess.ObjectBinding;
using DevExpress.XtraPrinting.Drawing;
using DevExpress.XtraReports.UI;

namespace Sgpa.Web.Reports.Certificaciones
{
    /// <summary>
    /// Informe de certificaciones por mutualista con afección (port de "Certificaciones por empresa (afeccion).xps"):
    /// como el de mutualista pero con la columna de afección. Layout en el .Designer.cs; datos + logo vía <see cref="Bind"/>.
    /// </summary>
    public partial class CertificacionPorMutualistaAfeccionReport : XtraReport
    {
        public CertificacionPorMutualistaAfeccionReport()
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
