using System.Collections;
using DevExpress.DataAccess.ObjectBinding;
using DevExpress.XtraPrinting.Drawing;
using DevExpress.XtraReports.UI;

namespace Sgpa.Web.Reports.Mutualistas
{
    /// <summary>
    /// Listado de mutualistas (port de Mutualista.rpt): catálogo con código, nombre, teléfono, dirección y cuota.
    /// Es un listado completo (no per-registro). Layout en el .Designer.cs; datos + logo vía <see cref="Bind"/>.
    /// </summary>
    public partial class MutualistaListadoReport : XtraReport
    {
        public MutualistaListadoReport()
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
