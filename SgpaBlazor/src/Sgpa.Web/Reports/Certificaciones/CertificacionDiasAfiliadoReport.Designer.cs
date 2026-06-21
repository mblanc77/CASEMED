namespace Sgpa.Web.Reports.Certificaciones
{
    partial class CertificacionDiasAfiliadoReport
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
            this.GroupHeader1 = new DevExpress.XtraReports.UI.GroupHeaderBand();
            this.Detail = new DevExpress.XtraReports.UI.DetailBand();
            this.GroupFooter1 = new DevExpress.XtraReports.UI.GroupFooterBand();
            this.ReportFooter = new DevExpress.XtraReports.UI.ReportFooterBand();
            this.xrLogo = new DevExpress.XtraReports.UI.XRPictureBox();
            this.xrTitulo = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLinea = new DevExpress.XtraReports.UI.XRLine();
            this.gAfiliado = new DevExpress.XtraReports.UI.XRLabel();
            this.hAfec = new DevExpress.XtraReports.UI.XRLabel();
            this.hCant = new DevExpress.XtraReports.UI.XRLabel();
            this.hDias = new DevExpress.XtraReports.UI.XRLabel();
            this.ruleG = new DevExpress.XtraReports.UI.XRLine();
            this.dAfec = new DevExpress.XtraReports.UI.XRLabel();
            this.dCant = new DevExpress.XtraReports.UI.XRLabel();
            this.dDias = new DevExpress.XtraReports.UI.XRLabel();
            this.gTotalDias = new DevExpress.XtraReports.UI.XRLabel();
            this.ruleFoot = new DevExpress.XtraReports.UI.XRLine();
            this.xrTotal = new DevExpress.XtraReports.UI.XRLabel();
            this.objectDataSource1 = new DevExpress.DataAccess.ObjectBinding.ObjectDataSource(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.objectDataSource1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            //
            // TopMargin / BottomMargin
            //
            this.TopMargin.Dpi = 96F;
            this.TopMargin.HeightF = 40F;
            this.TopMargin.Name = "TopMargin";
            this.BottomMargin.Dpi = 96F;
            this.BottomMargin.HeightF = 40F;
            this.BottomMargin.Name = "BottomMargin";
            //
            // ReportHeader
            //
            this.ReportHeader.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
                this.xrLogo, this.xrTitulo, this.xrLinea});
            this.ReportHeader.Dpi = 96F;
            this.ReportHeader.HeightF = 56F;
            this.ReportHeader.Name = "ReportHeader";
            //
            // GroupHeader1
            //
            this.GroupHeader1.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
                this.gAfiliado, this.hAfec, this.hCant, this.hDias, this.ruleG});
            this.GroupHeader1.Dpi = 96F;
            this.GroupHeader1.GroupFields.AddRange(new DevExpress.XtraReports.UI.GroupField[] {
                new DevExpress.XtraReports.UI.GroupField("CI", DevExpress.XtraReports.UI.XRColumnSortOrder.Ascending)});
            this.GroupHeader1.HeightF = 46F;
            this.GroupHeader1.Name = "GroupHeader1";
            this.GroupHeader1.RepeatEveryPage = true;
            //
            // Detail
            //
            this.Detail.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
                this.dAfec, this.dCant, this.dDias});
            this.Detail.Dpi = 96F;
            this.Detail.HeightF = 20F;
            this.Detail.Name = "Detail";
            //
            // GroupFooter1
            //
            this.GroupFooter1.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] { this.gTotalDias });
            this.GroupFooter1.Dpi = 96F;
            this.GroupFooter1.HeightF = 26F;
            this.GroupFooter1.Name = "GroupFooter1";
            //
            // ReportFooter
            //
            this.ReportFooter.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] { this.ruleFoot, this.xrTotal });
            this.ReportFooter.Dpi = 96F;
            this.ReportFooter.HeightF = 34F;
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
            this.xrTitulo.SizeF = new System.Drawing.SizeF(714F, 26F);
            this.xrTitulo.Text = "Días de certificación por afiliado";
            this.xrTitulo.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter;
            //
            // xrLinea
            //
            this.xrLinea.Dpi = 96F;
            this.xrLinea.ForeColor = System.Drawing.Color.FromArgb(28, 54, 128);
            this.xrLinea.LocationFloat = new DevExpress.Utils.PointFloat(0F, 46F);
            this.xrLinea.Name = "xrLinea";
            this.xrLinea.SizeF = new System.Drawing.SizeF(714F, 2F);
            //
            // gAfiliado
            //
            this.gAfiliado.Dpi = 96F;
            this.gAfiliado.ExpressionBindings.AddRange(new DevExpress.XtraReports.UI.ExpressionBinding[] {
                new DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "'Afiliado:  ' + [AfiliadoEncabezado]")});
            this.gAfiliado.Font = new DevExpress.Drawing.DXFont("Segoe UI", 11F, DevExpress.Drawing.DXFontStyle.Bold);
            this.gAfiliado.ForeColor = System.Drawing.Color.FromArgb(28, 54, 128);
            this.gAfiliado.LocationFloat = new DevExpress.Utils.PointFloat(0F, 2F);
            this.gAfiliado.Name = "gAfiliado";
            this.gAfiliado.SizeF = new System.Drawing.SizeF(714F, 20F);
            //
            // hAfec
            //
            this.hAfec.Dpi = 96F;
            this.hAfec.Font = new DevExpress.Drawing.DXFont("Segoe UI", 9F, DevExpress.Drawing.DXFontStyle.Bold);
            this.hAfec.ForeColor = System.Drawing.Color.FromArgb(102, 102, 102);
            this.hAfec.LocationFloat = new DevExpress.Utils.PointFloat(0F, 26F);
            this.hAfec.Name = "hAfec";
            this.hAfec.SizeF = new System.Drawing.SizeF(450F, 18F);
            this.hAfec.Text = "Tipo de afección";
            //
            // hCant
            //
            this.hCant.Dpi = 96F;
            this.hCant.Font = new DevExpress.Drawing.DXFont("Segoe UI", 9F, DevExpress.Drawing.DXFontStyle.Bold);
            this.hCant.ForeColor = System.Drawing.Color.FromArgb(102, 102, 102);
            this.hCant.LocationFloat = new DevExpress.Utils.PointFloat(450F, 26F);
            this.hCant.Name = "hCant";
            this.hCant.SizeF = new System.Drawing.SizeF(120F, 18F);
            this.hCant.Text = "Cantidad";
            this.hCant.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
            //
            // hDias
            //
            this.hDias.Dpi = 96F;
            this.hDias.Font = new DevExpress.Drawing.DXFont("Segoe UI", 9F, DevExpress.Drawing.DXFontStyle.Bold);
            this.hDias.ForeColor = System.Drawing.Color.FromArgb(102, 102, 102);
            this.hDias.LocationFloat = new DevExpress.Utils.PointFloat(570F, 26F);
            this.hDias.Name = "hDias";
            this.hDias.SizeF = new System.Drawing.SizeF(144F, 18F);
            this.hDias.Text = "Días";
            this.hDias.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
            //
            // ruleG
            //
            this.ruleG.Dpi = 96F;
            this.ruleG.ForeColor = System.Drawing.Color.FromArgb(28, 54, 128);
            this.ruleG.LocationFloat = new DevExpress.Utils.PointFloat(0F, 44F);
            this.ruleG.Name = "ruleG";
            this.ruleG.SizeF = new System.Drawing.SizeF(714F, 1F);
            //
            // dAfec
            //
            this.dAfec.Borders = DevExpress.XtraPrinting.BorderSide.Bottom;
            this.dAfec.BorderColor = System.Drawing.Color.FromArgb(225, 228, 240);
            this.dAfec.Dpi = 96F;
            this.dAfec.ExpressionBindings.AddRange(new DevExpress.XtraReports.UI.ExpressionBinding[] {
                new DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[AfeccionTexto]")});
            this.dAfec.LocationFloat = new DevExpress.Utils.PointFloat(0F, 1F);
            this.dAfec.Name = "dAfec";
            this.dAfec.SizeF = new System.Drawing.SizeF(450F, 18F);
            //
            // dCant
            //
            this.dCant.Borders = DevExpress.XtraPrinting.BorderSide.Bottom;
            this.dCant.BorderColor = System.Drawing.Color.FromArgb(225, 228, 240);
            this.dCant.Dpi = 96F;
            this.dCant.ExpressionBindings.AddRange(new DevExpress.XtraReports.UI.ExpressionBinding[] {
                new DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[Cantidad]")});
            this.dCant.LocationFloat = new DevExpress.Utils.PointFloat(450F, 1F);
            this.dCant.Name = "dCant";
            this.dCant.SizeF = new System.Drawing.SizeF(120F, 18F);
            this.dCant.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
            //
            // dDias
            //
            this.dDias.Borders = DevExpress.XtraPrinting.BorderSide.Bottom;
            this.dDias.BorderColor = System.Drawing.Color.FromArgb(225, 228, 240);
            this.dDias.Dpi = 96F;
            this.dDias.ExpressionBindings.AddRange(new DevExpress.XtraReports.UI.ExpressionBinding[] {
                new DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[Dias]")});
            this.dDias.LocationFloat = new DevExpress.Utils.PointFloat(570F, 1F);
            this.dDias.Name = "dDias";
            this.dDias.SizeF = new System.Drawing.SizeF(144F, 18F);
            this.dDias.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
            //
            // gTotalDias
            //
            this.gTotalDias.Dpi = 96F;
            this.gTotalDias.ExpressionBindings.AddRange(new DevExpress.XtraReports.UI.ExpressionBinding[] {
                new DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "'Total días del afiliado: ' + sumSum([Dias])")});
            this.gTotalDias.Font = new DevExpress.Drawing.DXFont("Segoe UI", 9F, DevExpress.Drawing.DXFontStyle.Bold);
            this.gTotalDias.LocationFloat = new DevExpress.Utils.PointFloat(0F, 4F);
            this.gTotalDias.Name = "gTotalDias";
            this.gTotalDias.SizeF = new System.Drawing.SizeF(714F, 16F);
            this.gTotalDias.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
            //
            // ruleFoot
            //
            this.ruleFoot.Dpi = 96F;
            this.ruleFoot.ForeColor = System.Drawing.Color.FromArgb(28, 54, 128);
            this.ruleFoot.LocationFloat = new DevExpress.Utils.PointFloat(0F, 6F);
            this.ruleFoot.Name = "ruleFoot";
            this.ruleFoot.SizeF = new System.Drawing.SizeF(714F, 1F);
            //
            // xrTotal
            //
            this.xrTotal.Dpi = 96F;
            this.xrTotal.ExpressionBindings.AddRange(new DevExpress.XtraReports.UI.ExpressionBinding[] {
                new DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "'Total general de días: ' + sumSum([Dias])")});
            this.xrTotal.Font = new DevExpress.Drawing.DXFont("Segoe UI", 9F, DevExpress.Drawing.DXFontStyle.Bold);
            this.xrTotal.LocationFloat = new DevExpress.Utils.PointFloat(0F, 12F);
            this.xrTotal.Name = "xrTotal";
            this.xrTotal.SizeF = new System.Drawing.SizeF(714F, 16F);
            this.xrTotal.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
            //
            // objectDataSource1
            //
            this.objectDataSource1.DataSource = typeof(global::Sgpa.Web.Reporting.Predefinidos.CertificacionAfeccionLinea);
            this.objectDataSource1.Name = "objectDataSource1";
            //
            // CertificacionDiasAfiliadoReport
            //
            this.Bands.AddRange(new DevExpress.XtraReports.UI.Band[] {
                this.TopMargin, this.ReportHeader, this.GroupHeader1, this.Detail, this.GroupFooter1, this.ReportFooter, this.BottomMargin});
            this.ComponentStorage.AddRange(new System.ComponentModel.IComponent[] { this.objectDataSource1 });
            this.DataSource = this.objectDataSource1;
            this.Font = new DevExpress.Drawing.DXFont("Segoe UI", 9.5F);
            this.Margins = new DevExpress.Drawing.DXMargins(40F, 40F, 40F, 40F);
            this.PaperKind = DevExpress.Drawing.Printing.DXPaperKind.A4;
            this.Version = "25.2";
            ((System.ComponentModel.ISupportInitialize)(this.objectDataSource1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();
        }
        #endregion

        private DevExpress.XtraReports.UI.TopMarginBand TopMargin;
        private DevExpress.XtraReports.UI.BottomMarginBand BottomMargin;
        private DevExpress.XtraReports.UI.ReportHeaderBand ReportHeader;
        private DevExpress.XtraReports.UI.GroupHeaderBand GroupHeader1;
        private DevExpress.XtraReports.UI.DetailBand Detail;
        private DevExpress.XtraReports.UI.GroupFooterBand GroupFooter1;
        private DevExpress.XtraReports.UI.ReportFooterBand ReportFooter;
        internal DevExpress.XtraReports.UI.XRPictureBox xrLogo;
        private DevExpress.XtraReports.UI.XRLabel xrTitulo;
        private DevExpress.XtraReports.UI.XRLine xrLinea;
        private DevExpress.XtraReports.UI.XRLabel gAfiliado;
        private DevExpress.XtraReports.UI.XRLabel hAfec;
        private DevExpress.XtraReports.UI.XRLabel hCant;
        private DevExpress.XtraReports.UI.XRLabel hDias;
        private DevExpress.XtraReports.UI.XRLine ruleG;
        private DevExpress.XtraReports.UI.XRLabel dAfec;
        private DevExpress.XtraReports.UI.XRLabel dCant;
        private DevExpress.XtraReports.UI.XRLabel dDias;
        private DevExpress.XtraReports.UI.XRLabel gTotalDias;
        private DevExpress.XtraReports.UI.XRLine ruleFoot;
        private DevExpress.XtraReports.UI.XRLabel xrTotal;
        private DevExpress.DataAccess.ObjectBinding.ObjectDataSource objectDataSource1;
    }
}
