namespace Sgpa.Web.Reports.Estadisticos
{
    partial class EstadisticaChartReport
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        #region Designer generated code
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.TopMargin = new DevExpress.XtraReports.UI.TopMarginBand();
            this.BottomMargin = new DevExpress.XtraReports.UI.BottomMarginBand();
            this.ReportHeader = new DevExpress.XtraReports.UI.ReportHeaderBand();
            this.Detail = new DevExpress.XtraReports.UI.DetailBand();
            this.ReportFooter = new DevExpress.XtraReports.UI.ReportFooterBand();
            this.xrLogo = new DevExpress.XtraReports.UI.XRPictureBox();
            this.xrTitulo = new DevExpress.XtraReports.UI.XRLabel();
            this.xrFiltro = new DevExpress.XtraReports.UI.XRLabel();
            this.xrChart = new DevExpress.XtraReports.UI.XRChart();
            this.xrSinDatos = new DevExpress.XtraReports.UI.XRLabel();
            this.xrNota = new DevExpress.XtraReports.UI.XRLabel();
            this.xrDetalleTit = new DevExpress.XtraReports.UI.XRLabel();
            this.hDescrip = new DevExpress.XtraReports.UI.XRLabel();
            this.hCantidad = new DevExpress.XtraReports.UI.XRLabel();
            this.ruleHead = new DevExpress.XtraReports.UI.XRLine();
            this.dDescrip = new DevExpress.XtraReports.UI.XRLabel();
            this.dCantidad = new DevExpress.XtraReports.UI.XRLabel();
            this.ruleFoot = new DevExpress.XtraReports.UI.XRLine();
            this.xrTotal = new DevExpress.XtraReports.UI.XRLabel();
            ((System.ComponentModel.ISupportInitialize)(this.xrChart)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            //
            // TopMargin / BottomMargin
            //
            this.TopMargin.Dpi = 96F;
            this.TopMargin.HeightF = 30F;
            this.TopMargin.Name = "TopMargin";
            this.BottomMargin.Dpi = 96F;
            this.BottomMargin.HeightF = 30F;
            this.BottomMargin.Name = "BottomMargin";
            //
            // ReportHeader
            //
            this.ReportHeader.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
                this.xrLogo, this.xrTitulo, this.xrFiltro, this.xrChart, this.xrSinDatos,
                this.xrNota, this.xrDetalleTit, this.hDescrip, this.hCantidad, this.ruleHead});
            this.ReportHeader.Dpi = 96F;
            this.ReportHeader.HeightF = 506F;
            this.ReportHeader.Name = "ReportHeader";
            //
            // Detail
            //
            this.Detail.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
                this.dDescrip, this.dCantidad});
            this.Detail.Dpi = 96F;
            this.Detail.HeightF = 18F;
            this.Detail.Name = "Detail";
            //
            // ReportFooter
            //
            this.ReportFooter.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
                this.ruleFoot, this.xrTotal});
            this.ReportFooter.Dpi = 96F;
            this.ReportFooter.HeightF = 30F;
            this.ReportFooter.Name = "ReportFooter";
            //
            // xrLogo
            //
            this.xrLogo.Dpi = 96F;
            this.xrLogo.LocationFloat = new DevExpress.Utils.PointFloat(0F, 0F);
            this.xrLogo.Name = "xrLogo";
            this.xrLogo.SizeF = new System.Drawing.SizeF(150F, 44F);
            this.xrLogo.Sizing = DevExpress.XtraPrinting.ImageSizeMode.ZoomImage;
            //
            // xrTitulo
            //
            this.xrTitulo.Dpi = 96F;
            this.xrTitulo.Font = new DevExpress.Drawing.DXFont("Segoe UI", 15F, DevExpress.Drawing.DXFontStyle.Bold);
            this.xrTitulo.ForeColor = System.Drawing.Color.FromArgb(28, 54, 128);
            this.xrTitulo.LocationFloat = new DevExpress.Utils.PointFloat(0F, 8F);
            this.xrTitulo.Name = "xrTitulo";
            this.xrTitulo.SizeF = new System.Drawing.SizeF(1040F, 26F);
            this.xrTitulo.Text = "Informe estadístico";
            this.xrTitulo.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter;
            //
            // xrFiltro
            //
            this.xrFiltro.Dpi = 96F;
            this.xrFiltro.Font = new DevExpress.Drawing.DXFont("Segoe UI", 9F);
            this.xrFiltro.ForeColor = System.Drawing.Color.FromArgb(102, 102, 102);
            this.xrFiltro.LocationFloat = new DevExpress.Utils.PointFloat(0F, 40F);
            this.xrFiltro.Name = "xrFiltro";
            this.xrFiltro.SizeF = new System.Drawing.SizeF(1040F, 16F);
            this.xrFiltro.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter;
            //
            // xrChart
            //
            this.xrChart.Dpi = 96F;
            this.xrChart.LocationFloat = new DevExpress.Utils.PointFloat(0F, 62F);
            this.xrChart.Name = "xrChart";
            this.xrChart.SizeF = new System.Drawing.SizeF(1040F, 360F);
            //
            // xrSinDatos
            //
            this.xrSinDatos.Dpi = 96F;
            this.xrSinDatos.Font = new DevExpress.Drawing.DXFont("Segoe UI", 11F, DevExpress.Drawing.DXFontStyle.Italic);
            this.xrSinDatos.ForeColor = System.Drawing.Color.FromArgb(150, 150, 150);
            this.xrSinDatos.LocationFloat = new DevExpress.Utils.PointFloat(0F, 220F);
            this.xrSinDatos.Name = "xrSinDatos";
            this.xrSinDatos.SizeF = new System.Drawing.SizeF(1040F, 24F);
            this.xrSinDatos.Text = "Sin datos para los parámetros seleccionados.";
            this.xrSinDatos.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrSinDatos.Visible = false;
            //
            // xrNota
            //
            this.xrNota.Dpi = 96F;
            this.xrNota.Font = new DevExpress.Drawing.DXFont("Segoe UI", 8F, DevExpress.Drawing.DXFontStyle.Italic);
            this.xrNota.ForeColor = System.Drawing.Color.FromArgb(102, 102, 102);
            this.xrNota.LocationFloat = new DevExpress.Utils.PointFloat(0F, 426F);
            this.xrNota.Multiline = true;
            this.xrNota.Name = "xrNota";
            this.xrNota.SizeF = new System.Drawing.SizeF(1040F, 28F);
            //
            // xrDetalleTit
            //
            this.xrDetalleTit.Dpi = 96F;
            this.xrDetalleTit.Font = new DevExpress.Drawing.DXFont("Segoe UI", 10F, DevExpress.Drawing.DXFontStyle.Bold);
            this.xrDetalleTit.ForeColor = System.Drawing.Color.FromArgb(28, 54, 128);
            this.xrDetalleTit.LocationFloat = new DevExpress.Utils.PointFloat(0F, 460F);
            this.xrDetalleTit.Name = "xrDetalleTit";
            this.xrDetalleTit.SizeF = new System.Drawing.SizeF(1040F, 18F);
            this.xrDetalleTit.Text = "Detalle";
            //
            // hDescrip
            //
            this.hDescrip.Dpi = 96F;
            this.hDescrip.Font = new DevExpress.Drawing.DXFont("Segoe UI", 9F, DevExpress.Drawing.DXFontStyle.Bold);
            this.hDescrip.ForeColor = System.Drawing.Color.FromArgb(102, 102, 102);
            this.hDescrip.LocationFloat = new DevExpress.Utils.PointFloat(0F, 482F);
            this.hDescrip.Name = "hDescrip";
            this.hDescrip.SizeF = new System.Drawing.SizeF(860F, 18F);
            this.hDescrip.Text = "Descripción";
            //
            // hCantidad
            //
            this.hCantidad.Dpi = 96F;
            this.hCantidad.Font = new DevExpress.Drawing.DXFont("Segoe UI", 9F, DevExpress.Drawing.DXFontStyle.Bold);
            this.hCantidad.ForeColor = System.Drawing.Color.FromArgb(102, 102, 102);
            this.hCantidad.LocationFloat = new DevExpress.Utils.PointFloat(860F, 482F);
            this.hCantidad.Name = "hCantidad";
            this.hCantidad.SizeF = new System.Drawing.SizeF(180F, 18F);
            this.hCantidad.Text = "Cantidad";
            this.hCantidad.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
            //
            // ruleHead
            //
            this.ruleHead.Dpi = 96F;
            this.ruleHead.ForeColor = System.Drawing.Color.FromArgb(28, 54, 128);
            this.ruleHead.LocationFloat = new DevExpress.Utils.PointFloat(0F, 502F);
            this.ruleHead.Name = "ruleHead";
            this.ruleHead.SizeF = new System.Drawing.SizeF(1040F, 1F);
            //
            // dDescrip
            //
            this.dDescrip.Borders = DevExpress.XtraPrinting.BorderSide.Bottom;
            this.dDescrip.BorderColor = System.Drawing.Color.FromArgb(225, 228, 240);
            this.dDescrip.Dpi = 96F;
            this.dDescrip.ExpressionBindings.AddRange(new DevExpress.XtraReports.UI.ExpressionBinding[] {
                new DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[DescripDetalle]")});
            this.dDescrip.LocationFloat = new DevExpress.Utils.PointFloat(0F, 0F);
            this.dDescrip.Name = "dDescrip";
            this.dDescrip.SizeF = new System.Drawing.SizeF(860F, 18F);
            //
            // dCantidad
            //
            this.dCantidad.Borders = DevExpress.XtraPrinting.BorderSide.Bottom;
            this.dCantidad.BorderColor = System.Drawing.Color.FromArgb(225, 228, 240);
            this.dCantidad.Dpi = 96F;
            this.dCantidad.ExpressionBindings.AddRange(new DevExpress.XtraReports.UI.ExpressionBinding[] {
                new DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[CantidadFmt]")});
            this.dCantidad.LocationFloat = new DevExpress.Utils.PointFloat(860F, 0F);
            this.dCantidad.Name = "dCantidad";
            this.dCantidad.SizeF = new System.Drawing.SizeF(180F, 18F);
            this.dCantidad.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
            //
            // ruleFoot
            //
            this.ruleFoot.Dpi = 96F;
            this.ruleFoot.ForeColor = System.Drawing.Color.FromArgb(28, 54, 128);
            this.ruleFoot.LocationFloat = new DevExpress.Utils.PointFloat(0F, 4F);
            this.ruleFoot.Name = "ruleFoot";
            this.ruleFoot.SizeF = new System.Drawing.SizeF(1040F, 1F);
            //
            // xrTotal
            //
            this.xrTotal.Dpi = 96F;
            this.xrTotal.Font = new DevExpress.Drawing.DXFont("Segoe UI", 9F, DevExpress.Drawing.DXFontStyle.Bold);
            this.xrTotal.LocationFloat = new DevExpress.Utils.PointFloat(640F, 8F);
            this.xrTotal.Name = "xrTotal";
            this.xrTotal.SizeF = new System.Drawing.SizeF(400F, 16F);
            this.xrTotal.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
            //
            // EstadisticaChartReport
            //
            this.Bands.AddRange(new DevExpress.XtraReports.UI.Band[] {
                this.TopMargin, this.ReportHeader, this.Detail, this.ReportFooter, this.BottomMargin});
            this.Font = new DevExpress.Drawing.DXFont("Segoe UI", 9.5F);
            this.Margins = new DevExpress.Drawing.DXMargins(40F, 40F, 30F, 30F);
            this.PaperKind = DevExpress.Drawing.Printing.DXPaperKind.A4;
            this.Landscape = true;
            this.Version = "25.2";
            ((System.ComponentModel.ISupportInitialize)(this.xrChart)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();
        }
        #endregion

        private DevExpress.XtraReports.UI.TopMarginBand TopMargin;
        private DevExpress.XtraReports.UI.BottomMarginBand BottomMargin;
        private DevExpress.XtraReports.UI.ReportHeaderBand ReportHeader;
        internal DevExpress.XtraReports.UI.DetailBand Detail;
        internal DevExpress.XtraReports.UI.ReportFooterBand ReportFooter;
        internal DevExpress.XtraReports.UI.XRPictureBox xrLogo;
        private DevExpress.XtraReports.UI.XRLabel xrTitulo;
        private DevExpress.XtraReports.UI.XRLabel xrFiltro;
        internal DevExpress.XtraReports.UI.XRChart xrChart;
        private DevExpress.XtraReports.UI.XRLabel xrSinDatos;
        private DevExpress.XtraReports.UI.XRLabel xrNota;
        internal DevExpress.XtraReports.UI.XRLabel xrDetalleTit;
        internal DevExpress.XtraReports.UI.XRLabel hDescrip;
        internal DevExpress.XtraReports.UI.XRLabel hCantidad;
        internal DevExpress.XtraReports.UI.XRLine ruleHead;
        private DevExpress.XtraReports.UI.XRLabel dDescrip;
        private DevExpress.XtraReports.UI.XRLabel dCantidad;
        private DevExpress.XtraReports.UI.XRLine ruleFoot;
        internal DevExpress.XtraReports.UI.XRLabel xrTotal;
    }
}
