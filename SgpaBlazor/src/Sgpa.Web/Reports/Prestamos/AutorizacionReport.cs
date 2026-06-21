using System.Collections;
using DevExpress.DataAccess.ObjectBinding;
using DevExpress.XtraPrinting.Drawing;
using DevExpress.XtraReports.UI;

namespace Sgpa.Web.Reports.Prestamos
{
    /// <summary>
    /// Autorización de descuento (reporte de diseñador, editable en VS). El layout vive en el .Designer.cs.
    /// En runtime <see cref="Bind"/> inyecta el registro y el logo.
    /// </summary>
    public partial class AutorizacionReport : XtraReport
    {
        public AutorizacionReport()
        {
            InitializeComponent();
        }

        /// <summary>Bindea los datos (una colección con el AutorizacionData) y el logo de marca.</summary>
        public void Bind(IEnumerable data, string? logoPath)
        {
            if (DataSource is ObjectDataSource ods)
                ods.DataSource = data;
            if (logoPath is not null && File.Exists(logoPath))
                xrLogo.ImageSource = ImageSource.FromFile(logoPath);
        }
    }
}
