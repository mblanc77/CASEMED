using System.Collections;
using DevExpress.DataAccess.ObjectBinding;
using DevExpress.XtraPrinting.Drawing;
using DevExpress.XtraReports.UI;

namespace Sgpa.Web.Reports.Certificaciones
{
    /// <summary>
    /// Informe de certificaciones por mutualista (port de "Certificaciones por empresa.xps"): las certificaciones
    /// seleccionadas, agrupadas por mutualista (IAMC), en tabla (cédula+nombre, fecha inicio/fin, certificador).
    /// Layout en el .Designer.cs; datos + logo vía <see cref="Bind"/>.
    /// </summary>
    public partial class CertificacionPorMutualistaReport : XtraReport
    {
        public CertificacionPorMutualistaReport()
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
