namespace Sgpa.Web.Reports.Subsidios
{
    partial class SubsidioResumenReport
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
            this.xrLinea = new DevExpress.XtraReports.UI.XRLine();
            this.hCed = new DevExpress.XtraReports.UI.XRLabel();
            this.hNom = new DevExpress.XtraReports.UI.XRLabel();
            this.hFNac = new DevExpress.XtraReports.UI.XRLabel();
            this.hPer = new DevExpress.XtraReports.UI.XRLabel();
            this.hLiq = new DevExpress.XtraReports.UI.XRLabel();
            this.hDias = new DevExpress.XtraReports.UI.XRLabel();
            this.hAgu = new DevExpress.XtraReports.UI.XRLabel();
            this.hSub = new DevExpress.XtraReports.UI.XRLabel();
            this.hLiquido = new DevExpress.XtraReports.UI.XRLabel();
            this.ruleHead = new DevExpress.XtraReports.UI.XRLine();
            this.dCed = new DevExpress.XtraReports.UI.XRLabel();
            this.dNom = new DevExpress.XtraReports.UI.XRLabel();
            this.dFNac = new DevExpress.XtraReports.UI.XRLabel();
            this.dPer = new DevExpress.XtraReports.UI.XRLabel();
            this.dLiq = new DevExpress.XtraReports.UI.XRLabel();
            this.dDias = new DevExpress.XtraReports.UI.XRLabel();
            this.dAgu = new DevExpress.XtraReports.UI.XRLabel();
            this.dSub = new DevExpress.XtraReports.UI.XRLabel();
            this.dLiquido = new DevExpress.XtraReports.UI.XRLabel();
            this.ruleFoot = new DevExpress.XtraReports.UI.XRLine();
            this.xrTotal = new DevExpress.XtraReports.UI.XRLabel();
            this.xrTotLiq = new DevExpress.XtraReports.UI.XRLabel();
            this.objectDataSource1 = new DevExpress.DataAccess.ObjectBinding.ObjectDataSource(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.objectDataSource1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            //
            // TopMargin / BottomMargin
            //
            this.TopMargin.Dpi = 96F;
            this.TopMargin.HeightF = 36F;
            this.TopMargin.Name = "TopMargin";
            this.BottomMargin.Dpi = 96F;
            this.BottomMargin.HeightF = 36F;
            this.BottomMargin.Name = "BottomMargin";
            //
            // ReportHeader
            //
            this.ReportHeader.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
                this.xrLogo, this.xrTitulo, this.xrLinea,
                this.hCed, this.hNom, this.hFNac, this.hPer, this.hLiq, this.hDias, this.hAgu, this.hSub, this.hLiquido, this.ruleHead});
            this.ReportHeader.Dpi = 96F;
            this.ReportHeader.HeightF = 96F;
            this.ReportHeader.Name = "ReportHeader";
            //
            // Detail
            //
            this.Detail.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
                this.dCed, this.dNom, this.dFNac, this.dPer, this.dLiq, this.dDias, this.dAgu, this.dSub, this.dLiquido});
            this.Detail.Dpi = 96F;
            this.Detail.HeightF = 18F;
            this.Detail.Name = "Detail";
            //
            // ReportFooter
            //
            this.ReportFooter.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
                this.ruleFoot, this.xrTotal, this.xrTotLiq});
            this.ReportFooter.Dpi = 96F;
            this.ReportFooter.HeightF = 34F;
            this.ReportFooter.Name = "ReportFooter";
            //
            // xrLogo / xrTitulo / xrLinea
            //
            this.xrLogo.Dpi = 96F;
            this.xrLogo.LocationFloat = new DevExpress.Utils.PointFloat(0F, 0F);
            this.xrLogo.Name = "xrLogo";
            this.xrLogo.SizeF = new System.Drawing.SizeF(150F, 44F);
            this.xrLogo.Sizing = DevExpress.XtraPrinting.ImageSizeMode.ZoomImage;
            this.xrTitulo.Dpi = 96F;
            this.xrTitulo.Font = new DevExpress.Drawing.DXFont("Segoe UI", 15F, DevExpress.Drawing.DXFontStyle.Bold);
            this.xrTitulo.ForeColor = System.Drawing.Color.FromArgb(28, 54, 128);
            this.xrTitulo.LocationFloat = new DevExpress.Utils.PointFloat(0F, 8F);
            this.xrTitulo.Name = "xrTitulo";
            this.xrTitulo.SizeF = new System.Drawing.SizeF(714F, 26F);
            this.xrTitulo.Text = "Resumen de liquidación";
            this.xrTitulo.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter;
            this.xrLinea.Dpi = 96F;
            this.xrLinea.ForeColor = System.Drawing.Color.FromArgb(28, 54, 128);
            this.xrLinea.LocationFloat = new DevExpress.Utils.PointFloat(0F, 46F);
            this.xrLinea.Name = "xrLinea";
            this.xrLinea.SizeF = new System.Drawing.SizeF(714F, 2F);
            //
            // Cabeceras de columna (Y=72)
            //
            this.hCed.Dpi = 96F;
            this.hCed.Font = new DevExpress.Drawing.DXFont("Segoe UI", 8.5F, DevExpress.Drawing.DXFontStyle.Bold);
            this.hCed.ForeColor = System.Drawing.Color.FromArgb(102, 102, 102);
            this.hCed.LocationFloat = new DevExpress.Utils.PointFloat(0F, 72F);
            this.hCed.Name = "hCed";
            this.hCed.SizeF = new System.Drawing.SizeF(78F, 18F);
            this.hCed.Text = "Cédula";
            this.hNom.Dpi = 96F;
            this.hNom.Font = new DevExpress.Drawing.DXFont("Segoe UI", 8.5F, DevExpress.Drawing.DXFontStyle.Bold);
            this.hNom.ForeColor = System.Drawing.Color.FromArgb(102, 102, 102);
            this.hNom.LocationFloat = new DevExpress.Utils.PointFloat(78F, 72F);
            this.hNom.Name = "hNom";
            this.hNom.SizeF = new System.Drawing.SizeF(156F, 18F);
            this.hNom.Text = "Nombre";
            this.hFNac.Dpi = 96F;
            this.hFNac.Font = new DevExpress.Drawing.DXFont("Segoe UI", 8.5F, DevExpress.Drawing.DXFontStyle.Bold);
            this.hFNac.ForeColor = System.Drawing.Color.FromArgb(102, 102, 102);
            this.hFNac.LocationFloat = new DevExpress.Utils.PointFloat(234F, 72F);
            this.hFNac.Name = "hFNac";
            this.hFNac.SizeF = new System.Drawing.SizeF(64F, 18F);
            this.hFNac.Text = "F. Nac.";
            this.hPer.Dpi = 96F;
            this.hPer.Font = new DevExpress.Drawing.DXFont("Segoe UI", 8.5F, DevExpress.Drawing.DXFontStyle.Bold);
            this.hPer.ForeColor = System.Drawing.Color.FromArgb(102, 102, 102);
            this.hPer.LocationFloat = new DevExpress.Utils.PointFloat(298F, 72F);
            this.hPer.Name = "hPer";
            this.hPer.SizeF = new System.Drawing.SizeF(128F, 18F);
            this.hPer.Text = "Período";
            this.hLiq.Dpi = 96F;
            this.hLiq.Font = new DevExpress.Drawing.DXFont("Segoe UI", 8.5F, DevExpress.Drawing.DXFontStyle.Bold);
            this.hLiq.ForeColor = System.Drawing.Color.FromArgb(102, 102, 102);
            this.hLiq.LocationFloat = new DevExpress.Utils.PointFloat(426F, 72F);
            this.hLiq.Name = "hLiq";
            this.hLiq.SizeF = new System.Drawing.SizeF(28F, 18F);
            this.hLiq.Text = "B";
            this.hLiq.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.hDias.Dpi = 96F;
            this.hDias.Font = new DevExpress.Drawing.DXFont("Segoe UI", 8.5F, DevExpress.Drawing.DXFontStyle.Bold);
            this.hDias.ForeColor = System.Drawing.Color.FromArgb(102, 102, 102);
            this.hDias.LocationFloat = new DevExpress.Utils.PointFloat(454F, 72F);
            this.hDias.Name = "hDias";
            this.hDias.SizeF = new System.Drawing.SizeF(40F, 18F);
            this.hDias.Text = "Días";
            this.hDias.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
            this.hAgu.Dpi = 96F;
            this.hAgu.Font = new DevExpress.Drawing.DXFont("Segoe UI", 8.5F, DevExpress.Drawing.DXFontStyle.Bold);
            this.hAgu.ForeColor = System.Drawing.Color.FromArgb(102, 102, 102);
            this.hAgu.LocationFloat = new DevExpress.Utils.PointFloat(494F, 72F);
            this.hAgu.Name = "hAgu";
            this.hAgu.SizeF = new System.Drawing.SizeF(70F, 18F);
            this.hAgu.Text = "Aguinaldo";
            this.hAgu.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
            this.hSub.Dpi = 96F;
            this.hSub.Font = new DevExpress.Drawing.DXFont("Segoe UI", 8.5F, DevExpress.Drawing.DXFontStyle.Bold);
            this.hSub.ForeColor = System.Drawing.Color.FromArgb(102, 102, 102);
            this.hSub.LocationFloat = new DevExpress.Utils.PointFloat(564F, 72F);
            this.hSub.Name = "hSub";
            this.hSub.SizeF = new System.Drawing.SizeF(74F, 18F);
            this.hSub.Text = "Subsidio";
            this.hSub.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
            this.hLiquido.Dpi = 96F;
            this.hLiquido.Font = new DevExpress.Drawing.DXFont("Segoe UI", 8.5F, DevExpress.Drawing.DXFontStyle.Bold);
            this.hLiquido.ForeColor = System.Drawing.Color.FromArgb(102, 102, 102);
            this.hLiquido.LocationFloat = new DevExpress.Utils.PointFloat(638F, 72F);
            this.hLiquido.Name = "hLiquido";
            this.hLiquido.SizeF = new System.Drawing.SizeF(76F, 18F);
            this.hLiquido.Text = "Líquido";
            this.hLiquido.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
            //
            // ruleHead
            //
            this.ruleHead.Dpi = 96F;
            this.ruleHead.ForeColor = System.Drawing.Color.FromArgb(28, 54, 128);
            this.ruleHead.LocationFloat = new DevExpress.Utils.PointFloat(0F, 92F);
            this.ruleHead.Name = "ruleHead";
            this.ruleHead.SizeF = new System.Drawing.SizeF(714F, 1F);
            //
            // Detalle (Y=0, h18)
            //
            this.dCed.Borders = DevExpress.XtraPrinting.BorderSide.Bottom;
            this.dCed.BorderColor = System.Drawing.Color.FromArgb(225, 228, 240);
            this.dCed.Dpi = 96F;
            this.dCed.Font = new DevExpress.Drawing.DXFont("Segoe UI", 8.5F);
            this.dCed.ExpressionBindings.AddRange(new DevExpress.XtraReports.UI.ExpressionBinding[] {
                new DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[CIFormato]")});
            this.dCed.LocationFloat = new DevExpress.Utils.PointFloat(0F, 0F);
            this.dCed.Name = "dCed";
            this.dCed.SizeF = new System.Drawing.SizeF(78F, 18F);
            this.dNom.Borders = DevExpress.XtraPrinting.BorderSide.Bottom;
            this.dNom.BorderColor = System.Drawing.Color.FromArgb(225, 228, 240);
            this.dNom.Dpi = 96F;
            this.dNom.Font = new DevExpress.Drawing.DXFont("Segoe UI", 8.5F);
            this.dNom.ExpressionBindings.AddRange(new DevExpress.XtraReports.UI.ExpressionBinding[] {
                new DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[NombreTexto]")});
            this.dNom.LocationFloat = new DevExpress.Utils.PointFloat(78F, 0F);
            this.dNom.Name = "dNom";
            this.dNom.SizeF = new System.Drawing.SizeF(156F, 18F);
            this.dFNac.Borders = DevExpress.XtraPrinting.BorderSide.Bottom;
            this.dFNac.BorderColor = System.Drawing.Color.FromArgb(225, 228, 240);
            this.dFNac.Dpi = 96F;
            this.dFNac.Font = new DevExpress.Drawing.DXFont("Segoe UI", 8.5F);
            this.dFNac.ExpressionBindings.AddRange(new DevExpress.XtraReports.UI.ExpressionBinding[] {
                new DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[FNacFmt]")});
            this.dFNac.LocationFloat = new DevExpress.Utils.PointFloat(234F, 0F);
            this.dFNac.Name = "dFNac";
            this.dFNac.SizeF = new System.Drawing.SizeF(64F, 18F);
            this.dPer.Borders = DevExpress.XtraPrinting.BorderSide.Bottom;
            this.dPer.BorderColor = System.Drawing.Color.FromArgb(225, 228, 240);
            this.dPer.Dpi = 96F;
            this.dPer.Font = new DevExpress.Drawing.DXFont("Segoe UI", 8F);
            this.dPer.ExpressionBindings.AddRange(new DevExpress.XtraReports.UI.ExpressionBinding[] {
                new DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[PeriodoTexto]")});
            this.dPer.LocationFloat = new DevExpress.Utils.PointFloat(298F, 0F);
            this.dPer.Name = "dPer";
            this.dPer.SizeF = new System.Drawing.SizeF(128F, 18F);
            this.dLiq.Borders = DevExpress.XtraPrinting.BorderSide.Bottom;
            this.dLiq.BorderColor = System.Drawing.Color.FromArgb(225, 228, 240);
            this.dLiq.Dpi = 96F;
            this.dLiq.Font = new DevExpress.Drawing.DXFont("Segoe UI", 8.5F, DevExpress.Drawing.DXFontStyle.Bold);
            this.dLiq.ExpressionBindings.AddRange(new DevExpress.XtraReports.UI.ExpressionBinding[] {
                new DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[LiquidarMarca]")});
            this.dLiq.LocationFloat = new DevExpress.Utils.PointFloat(426F, 0F);
            this.dLiq.Name = "dLiq";
            this.dLiq.SizeF = new System.Drawing.SizeF(28F, 18F);
            this.dLiq.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.dDias.Borders = DevExpress.XtraPrinting.BorderSide.Bottom;
            this.dDias.BorderColor = System.Drawing.Color.FromArgb(225, 228, 240);
            this.dDias.Dpi = 96F;
            this.dDias.Font = new DevExpress.Drawing.DXFont("Segoe UI", 8.5F);
            this.dDias.ExpressionBindings.AddRange(new DevExpress.XtraReports.UI.ExpressionBinding[] {
                new DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[Dias]")});
            this.dDias.LocationFloat = new DevExpress.Utils.PointFloat(454F, 0F);
            this.dDias.Name = "dDias";
            this.dDias.SizeF = new System.Drawing.SizeF(40F, 18F);
            this.dDias.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
            this.dAgu.Borders = DevExpress.XtraPrinting.BorderSide.Bottom;
            this.dAgu.BorderColor = System.Drawing.Color.FromArgb(225, 228, 240);
            this.dAgu.Dpi = 96F;
            this.dAgu.Font = new DevExpress.Drawing.DXFont("Segoe UI", 8.5F);
            this.dAgu.ExpressionBindings.AddRange(new DevExpress.XtraReports.UI.ExpressionBinding[] {
                new DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[AguinaldoFmt]")});
            this.dAgu.LocationFloat = new DevExpress.Utils.PointFloat(494F, 0F);
            this.dAgu.Name = "dAgu";
            this.dAgu.SizeF = new System.Drawing.SizeF(70F, 18F);
            this.dAgu.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
            this.dSub.Borders = DevExpress.XtraPrinting.BorderSide.Bottom;
            this.dSub.BorderColor = System.Drawing.Color.FromArgb(225, 228, 240);
            this.dSub.Dpi = 96F;
            this.dSub.Font = new DevExpress.Drawing.DXFont("Segoe UI", 8.5F);
            this.dSub.ExpressionBindings.AddRange(new DevExpress.XtraReports.UI.ExpressionBinding[] {
                new DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[NominalFmt]")});
            this.dSub.LocationFloat = new DevExpress.Utils.PointFloat(564F, 0F);
            this.dSub.Name = "dSub";
            this.dSub.SizeF = new System.Drawing.SizeF(74F, 18F);
            this.dSub.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
            this.dLiquido.Borders = DevExpress.XtraPrinting.BorderSide.Bottom;
            this.dLiquido.BorderColor = System.Drawing.Color.FromArgb(225, 228, 240);
            this.dLiquido.Dpi = 96F;
            this.dLiquido.Font = new DevExpress.Drawing.DXFont("Segoe UI", 8.5F, DevExpress.Drawing.DXFontStyle.Bold);
            this.dLiquido.ExpressionBindings.AddRange(new DevExpress.XtraReports.UI.ExpressionBinding[] {
                new DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[LiquidoFmt]")});
            this.dLiquido.LocationFloat = new DevExpress.Utils.PointFloat(638F, 0F);
            this.dLiquido.Name = "dLiquido";
            this.dLiquido.SizeF = new System.Drawing.SizeF(76F, 18F);
            this.dLiquido.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
            //
            // ruleFoot / totales
            //
            this.ruleFoot.Dpi = 96F;
            this.ruleFoot.ForeColor = System.Drawing.Color.FromArgb(28, 54, 128);
            this.ruleFoot.LocationFloat = new DevExpress.Utils.PointFloat(0F, 6F);
            this.ruleFoot.Name = "ruleFoot";
            this.ruleFoot.SizeF = new System.Drawing.SizeF(714F, 1F);
            this.xrTotal.Dpi = 96F;
            this.xrTotal.ExpressionBindings.AddRange(new DevExpress.XtraReports.UI.ExpressionBinding[] {
                new DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "'Total de subsidios: ' + sumCount()")});
            this.xrTotal.Font = new DevExpress.Drawing.DXFont("Segoe UI", 9F, DevExpress.Drawing.DXFontStyle.Bold);
            this.xrTotal.LocationFloat = new DevExpress.Utils.PointFloat(0F, 12F);
            this.xrTotal.Name = "xrTotal";
            this.xrTotal.SizeF = new System.Drawing.SizeF(300F, 16F);
            this.xrTotLiq.Dpi = 96F;
            this.xrTotLiq.ExpressionBindings.AddRange(new DevExpress.XtraReports.UI.ExpressionBinding[] {
                new DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "'Total líquido $ ' + FormatString('{0:N2}', sumSum([ImpLiquido]))")});
            this.xrTotLiq.Font = new DevExpress.Drawing.DXFont("Segoe UI", 9F, DevExpress.Drawing.DXFontStyle.Bold);
            this.xrTotLiq.ForeColor = System.Drawing.Color.FromArgb(28, 54, 128);
            this.xrTotLiq.LocationFloat = new DevExpress.Utils.PointFloat(414F, 12F);
            this.xrTotLiq.Name = "xrTotLiq";
            this.xrTotLiq.SizeF = new System.Drawing.SizeF(300F, 16F);
            this.xrTotLiq.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
            //
            // objectDataSource1
            //
            this.objectDataSource1.DataSource = typeof(global::Sgpa.Web.Reporting.Predefinidos.SubsidioResumenLinea);
            this.objectDataSource1.Name = "objectDataSource1";
            //
            // SubsidioResumenReport
            //
            this.Bands.AddRange(new DevExpress.XtraReports.UI.Band[] {
                this.TopMargin, this.ReportHeader, this.Detail, this.ReportFooter, this.BottomMargin});
            this.ComponentStorage.AddRange(new System.ComponentModel.IComponent[] { this.objectDataSource1 });
            this.DataSource = this.objectDataSource1;
            this.Font = new DevExpress.Drawing.DXFont("Segoe UI", 9F);
            this.Margins = new DevExpress.Drawing.DXMargins(40F, 40F, 36F, 36F);
            this.PaperKind = DevExpress.Drawing.Printing.DXPaperKind.A4;
            this.Version = "25.2";
            ((System.ComponentModel.ISupportInitialize)(this.objectDataSource1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();
        }
        #endregion

        private DevExpress.XtraReports.UI.TopMarginBand TopMargin;
        private DevExpress.XtraReports.UI.BottomMarginBand BottomMargin;
        private DevExpress.XtraReports.UI.ReportHeaderBand ReportHeader;
        private DevExpress.XtraReports.UI.DetailBand Detail;
        private DevExpress.XtraReports.UI.ReportFooterBand ReportFooter;
        internal DevExpress.XtraReports.UI.XRPictureBox xrLogo;
        private DevExpress.XtraReports.UI.XRLabel xrTitulo;
        private DevExpress.XtraReports.UI.XRLine xrLinea;
        private DevExpress.XtraReports.UI.XRLabel hCed;
        private DevExpress.XtraReports.UI.XRLabel hNom;
        private DevExpress.XtraReports.UI.XRLabel hFNac;
        private DevExpress.XtraReports.UI.XRLabel hPer;
        private DevExpress.XtraReports.UI.XRLabel hLiq;
        private DevExpress.XtraReports.UI.XRLabel hDias;
        private DevExpress.XtraReports.UI.XRLabel hAgu;
        private DevExpress.XtraReports.UI.XRLabel hSub;
        private DevExpress.XtraReports.UI.XRLabel hLiquido;
        private DevExpress.XtraReports.UI.XRLine ruleHead;
        private DevExpress.XtraReports.UI.XRLabel dCed;
        private DevExpress.XtraReports.UI.XRLabel dNom;
        private DevExpress.XtraReports.UI.XRLabel dFNac;
        private DevExpress.XtraReports.UI.XRLabel dPer;
        private DevExpress.XtraReports.UI.XRLabel dLiq;
        private DevExpress.XtraReports.UI.XRLabel dDias;
        private DevExpress.XtraReports.UI.XRLabel dAgu;
        private DevExpress.XtraReports.UI.XRLabel dSub;
        private DevExpress.XtraReports.UI.XRLabel dLiquido;
        private DevExpress.XtraReports.UI.XRLine ruleFoot;
        private DevExpress.XtraReports.UI.XRLabel xrTotal;
        private DevExpress.XtraReports.UI.XRLabel xrTotLiq;
        private DevExpress.DataAccess.ObjectBinding.ObjectDataSource objectDataSource1;
    }
}
