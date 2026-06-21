using System.Collections;
using DevExpress.DataAccess.ObjectBinding;
using DevExpress.XtraPrinting.Drawing;
using DevExpress.XtraReports.UI;

namespace Sgpa.Web.Reports.Prestamos
{
    /// <summary>Vale por el préstamo (pagaré). Layout en el .Designer.cs; datos + logo vía <see cref="Bind"/>.</summary>
    public partial class ValeReport : XtraReport
    {
        public ValeReport()
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

        private void ValeReport_BeforePrint(object sender, System.ComponentModel.CancelEventArgs e)
        {

        }
    }
}
