using System.Collections;
using DevExpress.DataAccess.ObjectBinding;
using DevExpress.XtraPrinting.Drawing;
using DevExpress.XtraReports.UI;

namespace Sgpa.Web.Reports.Subsidios
{
    /// <summary>
    /// Resumen de liquidación (port de ResumenLiquidacion.xps): tabla de los subsidios seleccionados con cédula,
    /// nombre, fecha de nac., período, días, aguinaldo, subsidio y líquido, con totales. Layout en el .Designer.cs.
    /// </summary>
    public partial class SubsidioResumenReport : XtraReport
    {
        public SubsidioResumenReport()
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
