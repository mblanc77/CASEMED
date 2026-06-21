using System.Collections;
using DevExpress.DataAccess.ObjectBinding;
using DevExpress.XtraPrinting.Drawing;
using DevExpress.XtraReports.UI;

namespace Sgpa.Web.Reports.Certificaciones
{
    /// <summary>
    /// Certificados del afiliado (port de Certificado(s).rpt): un bloque por certificación con nº de llamado/recibo,
    /// fechas, cédula, nombre, certificador, afección e indicaciones. Las filas iteran las certificaciones del CI.
    /// Layout en el .Designer.cs; datos + logo vía <see cref="Bind"/>.
    /// </summary>
    public partial class CertificacionDetalleReport : XtraReport
    {
        public CertificacionDetalleReport()
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
