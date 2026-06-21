using System.Collections;
using DevExpress.DataAccess.ObjectBinding;
using DevExpress.XtraPrinting.Drawing;
using DevExpress.XtraReports.UI;

namespace Sgpa.Web.Reports.Afiliados
{
    /// <summary>
    /// Lista de empleos del afiliado (port de EmpleoAfiliado.rpt): tabla de empresas con fechas de ingreso/baja y
    /// estado. El encabezado (nombre + CI del afiliado) va por parámetros; las filas iteran la lista. Layout en el
    /// .Designer.cs; datos + logo vía <see cref="Bind"/>.
    /// </summary>
    public partial class AfiliadoEmpleosReport : XtraReport
    {
        public AfiliadoEmpleosReport()
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
