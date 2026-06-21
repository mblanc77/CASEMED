using System.Collections;
using DevExpress.DataAccess.ObjectBinding;
using DevExpress.XtraPrinting.Drawing;
using DevExpress.XtraReports.UI;

namespace Sgpa.Web.Reports.Afiliados
{
    /// <summary>
    /// Lista de imponibles del afiliado (port de Imponible.rpt): tabla de período, empresa, concepto, días e importe,
    /// con total. El encabezado (nombre + CI) va por parámetros; las filas iteran la lista. Layout en el .Designer.cs;
    /// datos + logo vía <see cref="Bind"/>.
    /// </summary>
    public partial class AfiliadoImponiblesReport : XtraReport
    {
        public AfiliadoImponiblesReport()
        {
            InitializeComponent();
        }

        public void Bind(IEnumerable data, string nombre, string ciFormato, string? logoPath)
        {
            if (DataSource is ObjectDataSource ods)
                ods.DataSource = data;
            Parameters["pNombre"].Value = nombre;
            Parameters["pCI"].Value = ciFormato;
            if (logoPath is not null && File.Exists(logoPath))
                xrLogo.ImageSource = ImageSource.FromFile(logoPath);
        }
    }
}
