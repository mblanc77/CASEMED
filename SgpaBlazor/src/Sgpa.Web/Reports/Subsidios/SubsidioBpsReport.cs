using DevExpress.XtraPrinting.Drawing;
using DevExpress.XtraReports.UI;
using Sgpa.Web.Reporting.Predefinidos;

namespace Sgpa.Web.Reports.Subsidios
{
    /// <summary>
    /// Obligación mensual (BPS) (port de InformeBPS.xps): grilla de casilleros etiquetados (cant. personas, monto
    /// gravado, imp. tributos, aportes obrero/patronal/ob.+pat., aporte mutual, trib. mutual 0,5%, tot. tributos,
    /// total general). Todos los valores van por parámetros. Layout en el .Designer.cs.
    /// </summary>
    public partial class SubsidioBpsReport : XtraReport
    {
        public SubsidioBpsReport()
        {
            InitializeComponent();
        }

        public void Bind(SubsidioBpsData d, string? logoPath)
        {
            Parameters["pPeriodo"].Value = d.Periodo;
            Parameters["pPersonas"].Value = d.Personas.ToString(SubsidioFmt.EsUy);
            Parameters["pGravado"].Value = d.GravadoFmt;
            Parameters["pImpTributos"].Value = d.ImpTributosFmt;
            Parameters["pObrero"].Value = d.AporteObreroFmt;
            Parameters["pPatronal"].Value = d.AportePatronalFmt;
            Parameters["pObPat"].Value = d.ApObPatFmt;
            Parameters["pMutual"].Value = d.AporteMutualFmt;
            Parameters["pTribMutual"].Value = d.TribMutualFmt;
            Parameters["pTotTributos"].Value = d.TotTributosFmt;
            Parameters["pTotalGeneral"].Value = d.TotalGeneralFmt;
            if (logoPath is not null && File.Exists(logoPath))
                xrLogo.ImageSource = ImageSource.FromFile(logoPath);
        }
    }
}
