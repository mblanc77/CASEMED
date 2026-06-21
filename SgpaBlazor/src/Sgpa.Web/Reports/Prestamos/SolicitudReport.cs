using System.Collections;
using DevExpress.DataAccess.ObjectBinding;
using DevExpress.XtraPrinting.Drawing;
using DevExpress.XtraReports.UI;

namespace Sgpa.Web.Reports.Prestamos
{
    /// <summary>
    /// Solicitud de imponibles (port de PrestamoSoli.rpt): formulario con los datos del afiliado pre-cargados y
    /// secciones de empresa + tabla de líquidos en blanco para completar a mano. Un único registro (por CI).
    /// Layout en el .Designer.cs; datos + logo vía <see cref="Bind"/>.
    /// </summary>
    public partial class SolicitudReport : XtraReport
    {
        public SolicitudReport()
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
