using System.Collections;
using DevExpress.DataAccess.ObjectBinding;
using DevExpress.XtraPrinting.Drawing;
using DevExpress.XtraReports.UI;

namespace Sgpa.Web.Reports.Certificaciones
{
    /// <summary>
    /// Ficha de certificación (port de "Fecha de certificacion.xps"): por afiliado, datos (cédula, nombre, fecha de
    /// nacimiento, mutualista) + tabla tipo de afección / cantidad / días con total. Datos agregados
    /// (<see cref="global::Sgpa.Web.Reporting.Predefinidos.CertificacionAfeccionLinea"/>). Layout en el .Designer.cs.
    /// </summary>
    public partial class CertificacionFichaReport : XtraReport
    {
        public CertificacionFichaReport()
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
