namespace Sgpa.Web.Reports.Afiliados
{
    partial class AfiliadoApuntesReport
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
            this.xrNombre = new DevExpress.XtraReports.UI.XRLabel();
            this.xrCISub = new DevExpress.XtraReports.UI.XRLabel();
            this.hFecha = new DevExpress.XtraReports.UI.XRLabel();
            this.hDescrip = new DevExpress.XtraReports.UI.XRLabel();
            this.ruleHead = new DevExpress.XtraReports.UI.XRLine();
            this.dFecha = new DevExpress.XtraReports.UI.XRLabel();
            this.dDescrip = new DevExpress.XtraReports.UI.XRLabel();
            this.ruleFoot = new DevExpress.XtraReports.UI.XRLine();
            this.xrTotal = new DevExpress.XtraReports.UI.XRLabel();
            this.pNombre = new DevExpress.XtraReports.Parameters.Parameter();
            this.pCI = new DevExpress.XtraReports.Parameters.Parameter();
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
                this.xrLogo, this.xrTitulo, this.xrLinea, this.xrNombre, this.xrCISub,
                this.hFecha, this.hDescrip, this.ruleHead});
            this.ReportHeader.Dpi = 96F;
            this.ReportHeader.HeightF = 140F;
            this.ReportHeader.Name = "ReportHeader";
            //
            // Detail
            //
            this.Detail.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
                this.dFecha, this.dDescrip});
            this.Detail.Dpi = 96F;
            this.Detail.HeightF = 24F;
            this.Detail.Name = "Detail";
            //
            // ReportFooter
            //
            this.ReportFooter.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
                this.ruleFoot, this.xrTotal});
            this.ReportFooter.Dpi = 96F;
            this.ReportFooter.HeightF = 36F;
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
            this.xrTitulo.Text = "Apuntes del afiliado";
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
            // xrNombre
            //
            this.xrNombre.Dpi = 96F;
            this.xrNombre.ExpressionBindings.AddRange(new DevExpress.XtraReports.UI.ExpressionBinding[] {
                new DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[Parameters.pNombre]")});
            this.xrNombre.Font = new DevExpress.Drawing.DXFont("Segoe UI", 14F, DevExpress.Drawing.DXFontStyle.Bold);
            this.xrNombre.ForeColor = System.Drawing.Color.FromArgb(28, 54, 128);
            this.xrNombre.LocationFloat = new DevExpress.Utils.PointFloat(0F, 62F);
            this.xrNombre.Name = "xrNombre";
            this.xrNombre.SizeF = new System.Drawing.SizeF(714F, 24F);
            //
            // xrCISub
            //
            this.xrCISub.Dpi = 96F;
            this.xrCISub.ExpressionBindings.AddRange(new DevExpress.XtraReports.UI.ExpressionBinding[] {
                new DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "'C.I. ' + [Parameters.pCI]")});
            this.xrCISub.Font = new DevExpress.Drawing.DXFont("Segoe UI", 9.5F);
            this.xrCISub.ForeColor = System.Drawing.Color.FromArgb(102, 102, 102);
            this.xrCISub.LocationFloat = new DevExpress.Utils.PointFloat(0F, 88F);
            this.xrCISub.Name = "xrCISub";
            this.xrCISub.SizeF = new System.Drawing.SizeF(714F, 16F);
            //
            // hFecha
            //
            this.hFecha.Dpi = 96F;
            this.hFecha.Font = new DevExpress.Drawing.DXFont("Segoe UI", 9F, DevExpress.Drawing.DXFontStyle.Bold);
            this.hFecha.ForeColor = System.Drawing.Color.FromArgb(102, 102, 102);
            this.hFecha.LocationFloat = new DevExpress.Utils.PointFloat(0F, 116F);
            this.hFecha.Name = "hFecha";
            this.hFecha.SizeF = new System.Drawing.SizeF(100F, 18F);
            this.hFecha.Text = "Fecha";
            //
            // hDescrip
            //
            this.hDescrip.Dpi = 96F;
            this.hDescrip.Font = new DevExpress.Drawing.DXFont("Segoe UI", 9F, DevExpress.Drawing.DXFontStyle.Bold);
            this.hDescrip.ForeColor = System.Drawing.Color.FromArgb(102, 102, 102);
            this.hDescrip.LocationFloat = new DevExpress.Utils.PointFloat(104F, 116F);
            this.hDescrip.Name = "hDescrip";
            this.hDescrip.SizeF = new System.Drawing.SizeF(610F, 18F);
            this.hDescrip.Text = "Descripción";
            //
            // ruleHead
            //
            this.ruleHead.Dpi = 96F;
            this.ruleHead.ForeColor = System.Drawing.Color.FromArgb(28, 54, 128);
            this.ruleHead.LocationFloat = new DevExpress.Utils.PointFloat(0F, 136F);
            this.ruleHead.Name = "ruleHead";
            this.ruleHead.SizeF = new System.Drawing.SizeF(714F, 1F);
            //
            // dFecha
            //
            this.dFecha.Borders = DevExpress.XtraPrinting.BorderSide.Bottom;
            this.dFecha.BorderColor = System.Drawing.Color.FromArgb(225, 228, 240);
            this.dFecha.Dpi = 96F;
            this.dFecha.ExpressionBindings.AddRange(new DevExpress.XtraReports.UI.ExpressionBinding[] {
                new DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[FechaFmt]")});
            this.dFecha.LocationFloat = new DevExpress.Utils.PointFloat(0F, 2F);
            this.dFecha.Name = "dFecha";
            this.dFecha.SizeF = new System.Drawing.SizeF(100F, 20F);
            //
            // dDescrip
            //
            this.dDescrip.Borders = DevExpress.XtraPrinting.BorderSide.Bottom;
            this.dDescrip.BorderColor = System.Drawing.Color.FromArgb(225, 228, 240);
            this.dDescrip.Dpi = 96F;
            this.dDescrip.ExpressionBindings.AddRange(new DevExpress.XtraReports.UI.ExpressionBinding[] {
                new DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[DescripTexto]")});
            this.dDescrip.LocationFloat = new DevExpress.Utils.PointFloat(104F, 2F);
            this.dDescrip.Multiline = true;
            this.dDescrip.Name = "dDescrip";
            this.dDescrip.SizeF = new System.Drawing.SizeF(610F, 20F);
            //
            // ruleFoot
            //
            this.ruleFoot.Dpi = 96F;
            this.ruleFoot.ForeColor = System.Drawing.Color.FromArgb(210, 216, 232);
            this.ruleFoot.LocationFloat = new DevExpress.Utils.PointFloat(0F, 6F);
            this.ruleFoot.Name = "ruleFoot";
            this.ruleFoot.SizeF = new System.Drawing.SizeF(714F, 1F);
            //
            // xrTotal
            //
            this.xrTotal.Dpi = 96F;
            this.xrTotal.ExpressionBindings.AddRange(new DevExpress.XtraReports.UI.ExpressionBinding[] {
                new DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "'Total de apuntes: ' + sumCount()")});
            this.xrTotal.Font = new DevExpress.Drawing.DXFont("Segoe UI", 9F, DevExpress.Drawing.DXFontStyle.Bold);
            this.xrTotal.LocationFloat = new DevExpress.Utils.PointFloat(0F, 12F);
            this.xrTotal.Name = "xrTotal";
            this.xrTotal.SizeF = new System.Drawing.SizeF(360F, 16F);
            //
            // pNombre
            //
            this.pNombre.Description = "pNombre";
            this.pNombre.Name = "pNombre";
            this.pNombre.Visible = false;
            //
            // pCI
            //
            this.pCI.Description = "pCI";
            this.pCI.Name = "pCI";
            this.pCI.Visible = false;
            //
            // objectDataSource1
            //
            this.objectDataSource1.DataSource = typeof(global::Sgpa.Web.Reporting.Predefinidos.AfiliadoApunteLinea);
            this.objectDataSource1.Name = "objectDataSource1";
            //
            // AfiliadoApuntesReport
            //
            this.Bands.AddRange(new DevExpress.XtraReports.UI.Band[] {
                this.TopMargin, this.ReportHeader, this.Detail, this.ReportFooter, this.BottomMargin});
            this.ComponentStorage.AddRange(new System.ComponentModel.IComponent[] { this.objectDataSource1 });
            this.DataSource = this.objectDataSource1;
            this.Font = new DevExpress.Drawing.DXFont("Segoe UI", 9.5F);
            this.Margins = new DevExpress.Drawing.DXMargins(40F, 40F, 40F, 40F);
            this.PaperKind = DevExpress.Drawing.Printing.DXPaperKind.A4;
            this.Parameters.AddRange(new DevExpress.XtraReports.Parameters.Parameter[] { this.pNombre, this.pCI });
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
        private DevExpress.XtraReports.UI.XRLabel xrNombre;
        private DevExpress.XtraReports.UI.XRLabel xrCISub;
        private DevExpress.XtraReports.UI.XRLabel hFecha;
        private DevExpress.XtraReports.UI.XRLabel hDescrip;
        private DevExpress.XtraReports.UI.XRLine ruleHead;
        private DevExpress.XtraReports.UI.XRLabel dFecha;
        private DevExpress.XtraReports.UI.XRLabel dDescrip;
        private DevExpress.XtraReports.UI.XRLine ruleFoot;
        private DevExpress.XtraReports.UI.XRLabel xrTotal;
        private DevExpress.XtraReports.Parameters.Parameter pNombre;
        private DevExpress.XtraReports.Parameters.Parameter pCI;
        private DevExpress.DataAccess.ObjectBinding.ObjectDataSource objectDataSource1;
    }
}
