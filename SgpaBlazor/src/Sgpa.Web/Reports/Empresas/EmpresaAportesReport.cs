using System.Collections;
using DevExpress.DataAccess.ObjectBinding;
using DevExpress.XtraPrinting.Drawing;
using DevExpress.XtraReports.UI;

namespace Sgpa.Web.Reports.Empresas
{
    /// <summary>
    /// Resumen de aportes de la empresa (port de AportesEmpresa.rpt): tabla mensual con aporte, aguinaldo y total,
    /// con gran total. El encabezado (empresa + código) va por parámetros; las filas iteran la lista. Layout en el
    /// .Designer.cs; datos + logo vía <see cref="Bind"/>.
    /// </summary>
    public partial class EmpresaAportesReport : XtraReport
    {
        public EmpresaAportesReport()
        {
            InitializeComponent();
        }

        public void Bind(IEnumerable data, string nombre, string codTexto, string? logoPath)
        {
            if (DataSource is ObjectDataSource ods)
                ods.DataSource = data;
            Parameters["pNombre"].Value = nombre;
            Parameters["pCod"].Value = codTexto;
            if (logoPath is not null && File.Exists(logoPath))
                xrLogo.ImageSource = ImageSource.FromFile(logoPath);
        }
    }
}
