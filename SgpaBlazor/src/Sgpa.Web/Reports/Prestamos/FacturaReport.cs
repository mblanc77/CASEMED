using System.Collections;
using DevExpress.DataAccess.ObjectBinding;
using DevExpress.XtraPrinting.Drawing;
using DevExpress.XtraReports.UI;

namespace Sgpa.Web.Reports.Prestamos
{
    /// <summary>
    /// Facturas del préstamo (port de Factura.rpt): una factura + talón de cobranza por cuota, una página cada una.
    /// Todo el contenido vive en el Detail (itera dbo.Rpt_Factura), por eso bindea directo a los campos. El código
    /// de barras del talón usa Code128 con el campo CodigoBarra. Layout en el .Designer.cs; datos + logo vía Bind.
    /// </summary>
    public partial class FacturaReport : XtraReport
    {
        public FacturaReport()
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
