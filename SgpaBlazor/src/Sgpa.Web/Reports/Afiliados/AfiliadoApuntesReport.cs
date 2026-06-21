using System.Collections;
using DevExpress.DataAccess.ObjectBinding;
using DevExpress.XtraPrinting.Drawing;
using DevExpress.XtraReports.UI;

namespace Sgpa.Web.Reports.Afiliados
{
    /// <summary>
    /// Lista de apuntes del afiliado (port de AfApunte.rpt): tabla de fecha + descripción. El encabezado
    /// (nombre + CI) va por parámetros; las filas iteran la lista. Layout en el .Designer.cs; datos + logo vía <see cref="Bind"/>.
    /// </summary>
    public partial class AfiliadoApuntesReport : XtraReport
    {
        public AfiliadoApuntesReport()
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
