using System.Collections;
using DevExpress.DataAccess.ObjectBinding;
using DevExpress.XtraPrinting.Drawing;
using DevExpress.XtraReports.UI;

namespace Sgpa.Web.Reports.Certificaciones
{
    /// <summary>
    /// Informe de certificaciones por médico certificador (port de "Informe de certificaciones.xps"): las
    /// certificaciones seleccionadas, agrupadas por certificador, en tabla (recibo, llamado, cédula, nombre, fecha).
    /// Layout en el .Designer.cs; datos + logo vía <see cref="Bind"/>.
    /// </summary>
    public partial class CertificacionPorCertificadorReport : XtraReport
    {
        public CertificacionPorCertificadorReport()
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
