using System.Collections;
using DevExpress.DataAccess.ObjectBinding;
using DevExpress.XtraPrinting.Drawing;
using DevExpress.XtraReports.UI;

namespace Sgpa.Web.Reports.Certificaciones
{
    /// <summary>
    /// Días de certificación por afiliado (port de "Dias certificacion por afiliado.xps"): agrupado por afiliado,
    /// con la tabla tipo de afección / cantidad / días y total de días por afiliado. Datos agregados
    /// (<see cref="global::Sgpa.Web.Reporting.Predefinidos.CertificacionAfeccionLinea"/>). Layout en el .Designer.cs.
    /// </summary>
    public partial class CertificacionDiasAfiliadoReport : XtraReport
    {
        public CertificacionDiasAfiliadoReport()
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
