using System.Collections.Generic;
using System.Linq;
using DevExpress.XtraCharts;
using DevExpress.XtraPrinting.Drawing;
using DevExpress.XtraReports.UI;
using Sgpa.Web.Reporting.Predefinidos;

namespace Sgpa.Web.Reports.Estadisticos
{
    /// <summary>Tipo de gráfico elegible (réplica del combo Circular/Barras/Área del VB6 frmInformeEstadistico).</summary>
    public enum TipoGrafico { Circular, Barras, Area }

    /// <summary>
    /// Reporte genérico de los Informes Estadísticos: un único <see cref="XRChart"/> alimentado por la lista de
    /// puntos (Descrip / Cantidad) que arma <see cref="IEstadisticaReporteData"/>. El título, el filtro y la nota
    /// se setean en <see cref="Bind"/>; el tipo de serie (torta/barras/área) se construye en runtime.
    /// </summary>
    public partial class EstadisticaChartReport : XtraReport
    {
        public EstadisticaChartReport()
        {
            InitializeComponent();
        }

        // Tope de categorías en el gráfico: si se superan, se muestran las de mayor magnitud y el resto se
        // consolida en "Otros" (la tabla de detalle de abajo siempre lista todos los ítems). La torta tolera
        // menos sectores que las barras. Réplica del comportamiento de los gráficos Crystal del VB6.
        // El VB6 muestra 10 ítems nombrados + "Otros" en la torta. Como Consolidar() toma (max-1) y agrega
        // "Otros", max=11 => 10 nombrados + Otros.
        private const int MaxItemsTorta = 11;
        private const int MaxItemsBarras = 21;

        private static readonly System.Globalization.CultureInfo EsUy = System.Globalization.CultureInfo.GetCultureInfo("es-UY");

        public void Bind(IReadOnlyList<EstadisticaPunto> puntos, string titulo, string filtro, string? nota,
            TipoGrafico tipo, string? logoPath)
        {
            xrTitulo.Text = titulo;
            xrFiltro.Text = filtro;
            xrNota.Text = string.IsNullOrWhiteSpace(nota) ? "" : nota!.Trim();

            if (logoPath is not null && File.Exists(logoPath))
                xrLogo.ImageSource = ImageSource.FromFile(logoPath);

            if (puntos.Count == 0)
            {
                xrChart.Visible = false;
                xrSinDatos.Visible = true;
                xrDetalleTit.Visible = hDescrip.Visible = hCantidad.Visible = ruleHead.Visible = false;
                Detail.Visible = ReportFooter.Visible = false;
                return;
            }

            // Tabla de detalle = todos los ítems; el gráfico = top-N consolidado.
            DataSource = puntos.ToList();
            xrTotal.Text = $"Total:  {puntos.Sum(p => p.Cantidad).ToString("N0", EsUy)}";
            ConfigurarChart(Consolidar(puntos, tipo), tipo);
        }

        private static IReadOnlyList<EstadisticaPunto> Consolidar(IReadOnlyList<EstadisticaPunto> puntos, TipoGrafico tipo)
        {
            int max = tipo == TipoGrafico.Circular ? MaxItemsTorta : MaxItemsBarras;
            bool multi = puntos.Any(p => !string.IsNullOrEmpty(p.Serie));

            if (!multi)
            {
                if (puntos.Count <= max) return puntos;
                var ord = puntos.OrderByDescending(p => p.Cantidad).ToList();
                var top = ord.Take(max - 1).ToList();
                top.Add(new EstadisticaPunto { Descrip = "Otros", Cantidad = ord.Skip(max - 1).Sum(p => p.Cantidad) });
                return top;
            }

            // Cross-tab: top-N argumentos por total; el resto se consolida en "Otros" por cada serie.
            var totales = puntos.GroupBy(p => p.Descrip)
                .Select(g => new { Desc = g.Key, Tot = g.Sum(x => x.Cantidad) })
                .OrderByDescending(x => x.Tot).ToList();
            if (totales.Count <= max) return puntos;

            var keep = totales.Take(max - 1).Select(x => x.Desc).ToHashSet();
            var res = puntos.Where(p => keep.Contains(p.Descrip)).ToList();
            foreach (var g in puntos.Where(p => !keep.Contains(p.Descrip)).GroupBy(p => p.Serie))
                res.Add(new EstadisticaPunto { Descrip = "Otros", Serie = g.Key, Cantidad = g.Sum(x => x.Cantidad) });
            return res;
        }

        private void ConfigurarChart(IReadOnlyList<EstadisticaPunto> puntos, TipoGrafico tipo)
        {
            // Cross-tab: si los puntos traen una segunda dimensión (Serie), se genera una serie por valor
            // distinto (barras/área agrupadas). La torta no aplica a 2 dimensiones => se fuerza a barras.
            if (puntos.Any(pt => !string.IsNullOrEmpty(pt.Serie)))
            {
                ConfigurarChartMultiSerie(puntos, tipo);
                return;
            }

            var viewType = tipo switch
            {
                TipoGrafico.Circular => ViewType.Pie,
                TipoGrafico.Area => ViewType.Area,
                _ => ViewType.Bar,
            };

            var serie = new Series("Cantidad", viewType)
            {
                ArgumentDataMember = nameof(EstadisticaPunto.Descrip),
                ArgumentScaleType = ScaleType.Qualitative,
                LabelsVisibility = DevExpress.Utils.DefaultBoolean.True,
            };
            serie.ValueDataMembers.AddRange(nameof(EstadisticaPunto.Cantidad));

            switch (tipo)
            {
                case TipoGrafico.Circular:
                    if (serie.View is PieSeriesView pieView)
                        pieView.RuntimeExploding = false;
                    // Etiquetas que apuntan a los sectores: SÓLO la magnitud (no la descripción), para que la
                    // torta no se achique por el texto. Paridad con el VB6.
                    if (serie.Label is PieSeriesLabel pieLabel)
                    {
                        pieLabel.Position = PieSeriesLabelPosition.TwoColumns;
                        pieLabel.ResolveOverlappingMode = ResolveOverlappingMode.Default;
                    }
                    serie.LabelsVisibility = DevExpress.Utils.DefaultBoolean.True;
                    serie.Label.TextPattern = "{V:n0}";
                    // Leyenda: marcador de color + descripción + magnitud + % de cada ítem.
                    serie.LegendTextPattern = "{A}:  {V:n0}  ({VP:p1})";
                    xrChart.Legend.Visibility = DevExpress.Utils.DefaultBoolean.True;
                    // Total como ítem custom AL FINAL de la leyenda (debajo de los ítems por sector), sin tocar
                    // los automáticos (modo AutoGeneratedAndCustom). Paridad VB6.
                    var totalTorta = puntos.Sum(p => p.Cantidad);
                    xrChart.Legend.ItemVisibilityMode = LegendItemVisibilityMode.AutoGeneratedAndCustom;
                    xrChart.Legend.CustomItems.Clear();
                    xrChart.Legend.CustomItems.Add(new CustomLegendItem("total",
                        $"Total:  {totalTorta.ToString("N0", EsUy)}  (100,0%)") { MarkerVisible = false });
                    break;

                case TipoGrafico.Area:
                case TipoGrafico.Barras:
                default:
                    serie.Label.TextPattern = "{V:n0}";
                    xrChart.Legend.Visibility = DevExpress.Utils.DefaultBoolean.False;
                    if (xrChart.Diagram is XYDiagram diagram)
                    {
                        diagram.AxisX.Label.Angle = -30;
                        diagram.AxisY.Title.Text = "Cantidad";
                        diagram.AxisY.Title.Visibility = DevExpress.Utils.DefaultBoolean.True;
                        diagram.Rotated = false;
                    }
                    break;
            }

            xrChart.Series.Clear();
            xrChart.Series.Add(serie);
            xrChart.DataSource = puntos.ToList();
            xrChart.SeriesDataMember = null;
            xrChart.PaletteName = "Office";
        }

        private void ConfigurarChartMultiSerie(IReadOnlyList<EstadisticaPunto> puntos, TipoGrafico tipo)
        {
            var viewType = tipo == TipoGrafico.Area ? ViewType.Area : ViewType.Bar;   // torta no aplica a cross-tab

            xrChart.Series.Clear();
            xrChart.SeriesDataMember = nameof(EstadisticaPunto.Serie);

            var tmpl = xrChart.SeriesTemplate;
            tmpl.ArgumentDataMember = nameof(EstadisticaPunto.Descrip);
            tmpl.ArgumentScaleType = ScaleType.Qualitative;
            tmpl.ValueDataMembers.Clear();
            tmpl.ValueDataMembers.AddRange(nameof(EstadisticaPunto.Cantidad));
            tmpl.ChangeView(viewType);
            tmpl.LabelsVisibility = DevExpress.Utils.DefaultBoolean.False;

            xrChart.DataSource = puntos.ToList();
            xrChart.PaletteName = "Office";
            xrChart.Legend.Visibility = DevExpress.Utils.DefaultBoolean.True;

            if (xrChart.Diagram is XYDiagram diagram)
            {
                diagram.AxisX.Label.Angle = -30;
                diagram.AxisY.Title.Text = "Cantidad";
                diagram.AxisY.Title.Visibility = DevExpress.Utils.DefaultBoolean.True;
            }
        }
    }
}
