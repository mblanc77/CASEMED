using System.Collections;
using DevExpress.DataAccess.ObjectBinding;
using DevExpress.XtraPrinting.Drawing;
using DevExpress.XtraReports.UI;

namespace Sgpa.Web.Reports.Subsidios
{
    /// <summary>
    /// Detalle de liquidación (port de DetalleLiquidacion.xps): por afiliado, los imponibles por empresa/mes que
    /// componen el subsidio (mes, año, días, importe), con subtotal. Layout en el .Designer.cs.
    /// </summary>
    public partial class SubsidioDetalleReport : XtraReport
    {
        public SubsidioDetalleReport()
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
