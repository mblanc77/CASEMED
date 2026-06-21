using DevExpress.DataAccess.ObjectBinding;
using DevExpress.XtraPrinting.Drawing;
using DevExpress.XtraReports.UI;
using Sgpa.Web.Reporting.Predefinidos;

namespace Sgpa.Web.Reports.Prestamos
{
    /// <summary>
    /// Ficha de préstamo (tabular). El cuadro de amortización (Detail) se bindea a la colección de cuotas; los
    /// datos de cabecera (afiliado/préstamo) y el total van por <b>parámetros</b> del reporte — porque las bandas
    /// ReportHeader/ReportFooter no tienen contexto de fila y un binding directo a campo saldría vacío.
    /// Layout en el .Designer.cs; datos + logo vía <see cref="Bind"/>.
    /// </summary>
    public partial class FichaReport : XtraReport
    {
        public FichaReport()
        {
            InitializeComponent();
        }

        public void Bind(IReadOnlyList<PrestamoCuadroLinea> cuadro, string? logoPath)
        {
            if (DataSource is ObjectDataSource ods)
                ods.DataSource = cuadro;

            if (cuadro.Count > 0)
            {
                var h = cuadro[0];
                Parameters["pNum"].Value = h.IDPrestamo;
                Parameters["pFecha"].Value = h.Fecha;
                Parameters["pCI"].Value = h.CI;
                Parameters["pNombre"].Value = h.NombreCompleto;
                Parameters["pImponibles"].Value = h.Promedio;
                Parameters["pMoneda"].Value = h.DescMonedaLarga ?? "";
                Parameters["pCuotas"].Value = h.Cuotas;
                Parameters["pValorCuota"].Value = h.ImporteCuota;
                Parameters["pTasa"].Value = h.Tasa;
                Parameters["pMontoTotal"].Value = h.Importe;
            }

            if (logoPath is not null && File.Exists(logoPath))
                xrLogo.ImageSource = ImageSource.FromFile(logoPath);
        }
    }
}
