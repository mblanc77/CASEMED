namespace Sgpa.Web.Reports.Mutualistas
{
    partial class MutualistaListadoReport
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
            this.hCod = new DevExpress.XtraReports.UI.XRLabel();
            this.hNombre = new DevExpress.XtraReports.UI.XRLabel();
            this.hTel = new DevExpress.XtraReports.UI.XRLabel();
            this.hDir = new DevExpress.XtraReports.UI.XRLabel();
            this.hCuota = new DevExpress.XtraReports.UI.XRLabel();
            this.ruleHead = new DevExpress.XtraReports.UI.XRLine();
            this.dCod = new DevExpress.XtraReports.UI.XRLabel();
            this.dNombre = new DevExpress.XtraReports.UI.XRLabel();
            this.dTel = new DevExpress.XtraReports.UI.XRLabel();
            this.dDir = new DevExpress.XtraReports.UI.XRLabel();
            this.dCuota = new DevExpress.XtraReports.UI.XRLabel();
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
                this.xrLogo, this.xrTitulo, this.xrLinea,
                this.hCod, this.hNombre, this.hTel, this.hDir, this.hCuota, this.ruleHead});
            this.ReportHeader.Dpi = 96F;
            this.ReportHeader.HeightF = 96F;
            this.ReportHeader.Name = "ReportHeader";
            //
            // Detail
            //
            this.Detail.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
                this.dCod, this.dNombre, this.dTel, this.dDir, this.dCuota});
            this.Detail.Dpi = 96F;
            this.Detail.HeightF = 22F;
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
            this.xrTitulo.LocationFloat = new DevExpress.Utils.PointFloat(0F, 10F);
            this.xrTitulo.Name = "xrTitulo";
            this.xrTitulo.SizeF = new System.Drawing.SizeF(714F, 26F);
            this.xrTitulo.Text = "Listado de mutualistas";
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
            // hCod
            //
            this.hCod.Dpi = 96F;
            this.hCod.Font = new DevExpress.Drawing.DXFont("Segoe UI", 9F, DevExpress.Drawing.DXFontStyle.Bold);
            this.hCod.ForeColor = System.Drawing.Color.FromArgb(102, 102, 102);
            this.hCod.LocationFloat = new DevExpress.Utils.PointFloat(0F, 72F);
            this.hCod.Name = "hCod";
            this.hCod.SizeF = new System.Drawing.SizeF(70F, 18F);
            this.hCod.Text = "Código";
            //
            // hNombre
            //
            this.hNombre.Dpi = 96F;
            this.hNombre.Font = new DevExpress.Drawing.DXFont("Segoe UI", 9F, DevExpress.Drawing.DXFontStyle.Bold);
            this.hNombre.ForeColor = System.Drawing.Color.FromArgb(102, 102, 102);
            this.hNombre.LocationFloat = new DevExpress.Utils.PointFloat(74F, 72F);
            this.hNombre.Name = "hNombre";
            this.hNombre.SizeF = new System.Drawing.SizeF(250F, 18F);
            this.hNombre.Text = "Nombre";
            //
            // hTel
            //
            this.hTel.Dpi = 96F;
            this.hTel.Font = new DevExpress.Drawing.DXFont("Segoe UI", 9F, DevExpress.Drawing.DXFontStyle.Bold);
            this.hTel.ForeColor = System.Drawing.Color.FromArgb(102, 102, 102);
            this.hTel.LocationFloat = new DevExpress.Utils.PointFloat(328F, 72F);
            this.hTel.Name = "hTel";
            this.hTel.SizeF = new System.Drawing.SizeF(130F, 18F);
            this.hTel.Text = "Teléfono";
            //
            // hDir
            //
            this.hDir.Dpi = 96F;
            this.hDir.Font = new DevExpress.Drawing.DXFont("Segoe UI", 9F, DevExpress.Drawing.DXFontStyle.Bold);
            this.hDir.ForeColor = System.Drawing.Color.FromArgb(102, 102, 102);
            this.hDir.LocationFloat = new DevExpress.Utils.PointFloat(462F, 72F);
            this.hDir.Name = "hDir";
            this.hDir.SizeF = new System.Drawing.SizeF(184F, 18F);
            this.hDir.Text = "Dirección";
            //
            // hCuota
            //
            this.hCuota.Dpi = 96F;
            this.hCuota.Font = new DevExpress.Drawing.DXFont("Segoe UI", 9F, DevExpress.Drawing.DXFontStyle.Bold);
            this.hCuota.ForeColor = System.Drawing.Color.FromArgb(102, 102, 102);
            this.hCuota.LocationFloat = new DevExpress.Utils.PointFloat(646F, 72F);
            this.hCuota.Name = "hCuota";
            this.hCuota.SizeF = new System.Drawing.SizeF(68F, 18F);
            this.hCuota.Text = "Cuota";
            this.hCuota.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
            //
            // ruleHead
            //
            this.ruleHead.Dpi = 96F;
            this.ruleHead.ForeColor = System.Drawing.Color.FromArgb(28, 54, 128);
            this.ruleHead.LocationFloat = new DevExpress.Utils.PointFloat(0F, 92F);
            this.ruleHead.Name = "ruleHead";
            this.ruleHead.SizeF = new System.Drawing.SizeF(714F, 1F);
            //
            // dCod
            //
            this.dCod.Borders = DevExpress.XtraPrinting.BorderSide.Bottom;
            this.dCod.BorderColor = System.Drawing.Color.FromArgb(225, 228, 240);
            this.dCod.Dpi = 96F;
            this.dCod.ExpressionBindings.AddRange(new DevExpress.XtraReports.UI.ExpressionBinding[] {
                new DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[CodMutualista]")});
            this.dCod.LocationFloat = new DevExpress.Utils.PointFloat(0F, 1F);
            this.dCod.Name = "dCod";
            this.dCod.SizeF = new System.Drawing.SizeF(70F, 20F);
            //
            // dNombre
            //
            this.dNombre.Borders = DevExpress.XtraPrinting.BorderSide.Bottom;
            this.dNombre.BorderColor = System.Drawing.Color.FromArgb(225, 228, 240);
            this.dNombre.Dpi = 96F;
            this.dNombre.ExpressionBindings.AddRange(new DevExpress.XtraReports.UI.ExpressionBinding[] {
                new DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[NombreTexto]")});
            this.dNombre.LocationFloat = new DevExpress.Utils.PointFloat(74F, 1F);
            this.dNombre.Name = "dNombre";
            this.dNombre.SizeF = new System.Drawing.SizeF(250F, 20F);
            //
            // dTel
            //
            this.dTel.Borders = DevExpress.XtraPrinting.BorderSide.Bottom;
            this.dTel.BorderColor = System.Drawing.Color.FromArgb(225, 228, 240);
            this.dTel.Dpi = 96F;
            this.dTel.ExpressionBindings.AddRange(new DevExpress.XtraReports.UI.ExpressionBinding[] {
                new DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[TelefonoTexto]")});
            this.dTel.LocationFloat = new DevExpress.Utils.PointFloat(328F, 1F);
            this.dTel.Name = "dTel";
            this.dTel.SizeF = new System.Drawing.SizeF(130F, 20F);
            //
            // dDir
            //
            this.dDir.Borders = DevExpress.XtraPrinting.BorderSide.Bottom;
            this.dDir.BorderColor = System.Drawing.Color.FromArgb(225, 228, 240);
            this.dDir.Dpi = 96F;
            this.dDir.ExpressionBindings.AddRange(new DevExpress.XtraReports.UI.ExpressionBinding[] {
                new DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[DireccionTexto]")});
            this.dDir.LocationFloat = new DevExpress.Utils.PointFloat(462F, 1F);
            this.dDir.Name = "dDir";
            this.dDir.SizeF = new System.Drawing.SizeF(184F, 20F);
            //
            // dCuota
            //
            this.dCuota.Borders = DevExpress.XtraPrinting.BorderSide.Bottom;
            this.dCuota.BorderColor = System.Drawing.Color.FromArgb(225, 228, 240);
            this.dCuota.Dpi = 96F;
            this.dCuota.ExpressionBindings.AddRange(new DevExpress.XtraReports.UI.ExpressionBinding[] {
                new DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[CuotaFmt]")});
            this.dCuota.LocationFloat = new DevExpress.Utils.PointFloat(646F, 1F);
            this.dCuota.Name = "dCuota";
            this.dCuota.SizeF = new System.Drawing.SizeF(68F, 20F);
            this.dCuota.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
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
                new DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "'Total de mutualistas: ' + sumCount()")});
            this.xrTotal.Font = new DevExpress.Drawing.DXFont("Segoe UI", 9F, DevExpress.Drawing.DXFontStyle.Bold);
            this.xrTotal.LocationFloat = new DevExpress.Utils.PointFloat(0F, 12F);
            this.xrTotal.Name = "xrTotal";
            this.xrTotal.SizeF = new System.Drawing.SizeF(360F, 16F);
            //
            // objectDataSource1
            //
            this.objectDataSource1.DataSource = typeof(global::Sgpa.Web.Reporting.Predefinidos.MutualistaLinea);
            this.objectDataSource1.Name = "objectDataSource1";
            //
            // MutualistaListadoReport
            //
            this.Bands.AddRange(new DevExpress.XtraReports.UI.Band[] {
                this.TopMargin, this.ReportHeader, this.Detail, this.ReportFooter, this.BottomMargin});
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
        private DevExpress.XtraReports.UI.DetailBand Detail;
        private DevExpress.XtraReports.UI.ReportFooterBand ReportFooter;
        internal DevExpress.XtraReports.UI.XRPictureBox xrLogo;
        private DevExpress.XtraReports.UI.XRLabel xrTitulo;
        private DevExpress.XtraReports.UI.XRLine xrLinea;
        private DevExpress.XtraReports.UI.XRLabel hCod;
        private DevExpress.XtraReports.UI.XRLabel hNombre;
        private DevExpress.XtraReports.UI.XRLabel hTel;
        private DevExpress.XtraReports.UI.XRLabel hDir;
        private DevExpress.XtraReports.UI.XRLabel hCuota;
        private DevExpress.XtraReports.UI.XRLine ruleHead;
        private DevExpress.XtraReports.UI.XRLabel dCod;
        private DevExpress.XtraReports.UI.XRLabel dNombre;
        private DevExpress.XtraReports.UI.XRLabel dTel;
        private DevExpress.XtraReports.UI.XRLabel dDir;
        private DevExpress.XtraReports.UI.XRLabel dCuota;
        private DevExpress.XtraReports.UI.XRLine ruleFoot;
        private DevExpress.XtraReports.UI.XRLabel xrTotal;
        private DevExpress.DataAccess.ObjectBinding.ObjectDataSource objectDataSource1;
    }
}
