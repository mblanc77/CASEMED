namespace Sgpa.Web.Reports.Certificaciones
{
    partial class CertificacionPorMutualistaReport
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
            this.gMutualista = new DevExpress.XtraReports.UI.XRLabel();
            this.hCedula = new DevExpress.XtraReports.UI.XRLabel();
            this.hNombre = new DevExpress.XtraReports.UI.XRLabel();
            this.hIni = new DevExpress.XtraReports.UI.XRLabel();
            this.hFin = new DevExpress.XtraReports.UI.XRLabel();
            this.hCert = new DevExpress.XtraReports.UI.XRLabel();
            this.ruleG = new DevExpress.XtraReports.UI.XRLine();
            this.dCedula = new DevExpress.XtraReports.UI.XRLabel();
            this.dNombre = new DevExpress.XtraReports.UI.XRLabel();
            this.dIni = new DevExpress.XtraReports.UI.XRLabel();
            this.dFin = new DevExpress.XtraReports.UI.XRLabel();
            this.dCert = new DevExpress.XtraReports.UI.XRLabel();
            this.gSubtotal = new DevExpress.XtraReports.UI.XRLabel();
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
                this.gMutualista, this.hCedula, this.hNombre, this.hIni, this.hFin, this.hCert, this.ruleG});
            this.GroupHeader1.Dpi = 96F;
            this.GroupHeader1.GroupFields.AddRange(new DevExpress.XtraReports.UI.GroupField[] {
                new DevExpress.XtraReports.UI.GroupField("DescMutualista", DevExpress.XtraReports.UI.XRColumnSortOrder.Ascending)});
            this.GroupHeader1.HeightF = 46F;
            this.GroupHeader1.Name = "GroupHeader1";
            this.GroupHeader1.RepeatEveryPage = true;
            //
            // Detail
            //
            this.Detail.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
                this.dCedula, this.dNombre, this.dIni, this.dFin, this.dCert});
            this.Detail.Dpi = 96F;
            this.Detail.HeightF = 20F;
            this.Detail.Name = "Detail";
            //
            // GroupFooter1
            //
            this.GroupFooter1.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] { this.gSubtotal });
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
            this.xrTitulo.Text = "Certificaciones por mutualista";
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
            // gMutualista
            //
            this.gMutualista.Dpi = 96F;
            this.gMutualista.ExpressionBindings.AddRange(new DevExpress.XtraReports.UI.ExpressionBinding[] {
                new DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "'I.A.M.C.:  ' + [MutualistaTexto]")});
            this.gMutualista.Font = new DevExpress.Drawing.DXFont("Segoe UI", 11F, DevExpress.Drawing.DXFontStyle.Bold);
            this.gMutualista.ForeColor = System.Drawing.Color.FromArgb(28, 54, 128);
            this.gMutualista.LocationFloat = new DevExpress.Utils.PointFloat(0F, 2F);
            this.gMutualista.Name = "gMutualista";
            this.gMutualista.SizeF = new System.Drawing.SizeF(714F, 20F);
            //
            // hCedula
            //
            this.hCedula.Dpi = 96F;
            this.hCedula.Font = new DevExpress.Drawing.DXFont("Segoe UI", 9F, DevExpress.Drawing.DXFontStyle.Bold);
            this.hCedula.ForeColor = System.Drawing.Color.FromArgb(102, 102, 102);
            this.hCedula.LocationFloat = new DevExpress.Utils.PointFloat(0F, 26F);
            this.hCedula.Name = "hCedula";
            this.hCedula.SizeF = new System.Drawing.SizeF(110F, 18F);
            this.hCedula.Text = "Cédula";
            //
            // hNombre
            //
            this.hNombre.Dpi = 96F;
            this.hNombre.Font = new DevExpress.Drawing.DXFont("Segoe UI", 9F, DevExpress.Drawing.DXFontStyle.Bold);
            this.hNombre.ForeColor = System.Drawing.Color.FromArgb(102, 102, 102);
            this.hNombre.LocationFloat = new DevExpress.Utils.PointFloat(110F, 26F);
            this.hNombre.Name = "hNombre";
            this.hNombre.SizeF = new System.Drawing.SizeF(270F, 18F);
            this.hNombre.Text = "Nombre";
            //
            // hIni
            //
            this.hIni.Dpi = 96F;
            this.hIni.Font = new DevExpress.Drawing.DXFont("Segoe UI", 9F, DevExpress.Drawing.DXFontStyle.Bold);
            this.hIni.ForeColor = System.Drawing.Color.FromArgb(102, 102, 102);
            this.hIni.LocationFloat = new DevExpress.Utils.PointFloat(380F, 26F);
            this.hIni.Name = "hIni";
            this.hIni.SizeF = new System.Drawing.SizeF(100F, 18F);
            this.hIni.Text = "Fecha inicio";
            //
            // hFin
            //
            this.hFin.Dpi = 96F;
            this.hFin.Font = new DevExpress.Drawing.DXFont("Segoe UI", 9F, DevExpress.Drawing.DXFontStyle.Bold);
            this.hFin.ForeColor = System.Drawing.Color.FromArgb(102, 102, 102);
            this.hFin.LocationFloat = new DevExpress.Utils.PointFloat(480F, 26F);
            this.hFin.Name = "hFin";
            this.hFin.SizeF = new System.Drawing.SizeF(100F, 18F);
            this.hFin.Text = "Fecha fin";
            //
            // hCert
            //
            this.hCert.Dpi = 96F;
            this.hCert.Font = new DevExpress.Drawing.DXFont("Segoe UI", 9F, DevExpress.Drawing.DXFontStyle.Bold);
            this.hCert.ForeColor = System.Drawing.Color.FromArgb(102, 102, 102);
            this.hCert.LocationFloat = new DevExpress.Utils.PointFloat(580F, 26F);
            this.hCert.Name = "hCert";
            this.hCert.SizeF = new System.Drawing.SizeF(134F, 18F);
            this.hCert.Text = "Certificador";
            //
            // ruleG
            //
            this.ruleG.Dpi = 96F;
            this.ruleG.ForeColor = System.Drawing.Color.FromArgb(28, 54, 128);
            this.ruleG.LocationFloat = new DevExpress.Utils.PointFloat(0F, 44F);
            this.ruleG.Name = "ruleG";
            this.ruleG.SizeF = new System.Drawing.SizeF(714F, 1F);
            //
            // dCedula
            //
            this.dCedula.Borders = DevExpress.XtraPrinting.BorderSide.Bottom;
            this.dCedula.BorderColor = System.Drawing.Color.FromArgb(225, 228, 240);
            this.dCedula.Dpi = 96F;
            this.dCedula.ExpressionBindings.AddRange(new DevExpress.XtraReports.UI.ExpressionBinding[] {
                new DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[CIFormato]")});
            this.dCedula.LocationFloat = new DevExpress.Utils.PointFloat(0F, 1F);
            this.dCedula.Name = "dCedula";
            this.dCedula.SizeF = new System.Drawing.SizeF(110F, 18F);
            //
            // dNombre
            //
            this.dNombre.Borders = DevExpress.XtraPrinting.BorderSide.Bottom;
            this.dNombre.BorderColor = System.Drawing.Color.FromArgb(225, 228, 240);
            this.dNombre.Dpi = 96F;
            this.dNombre.ExpressionBindings.AddRange(new DevExpress.XtraReports.UI.ExpressionBinding[] {
                new DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[NombreTexto]")});
            this.dNombre.LocationFloat = new DevExpress.Utils.PointFloat(110F, 1F);
            this.dNombre.Name = "dNombre";
            this.dNombre.SizeF = new System.Drawing.SizeF(270F, 18F);
            //
            // dIni
            //
            this.dIni.Borders = DevExpress.XtraPrinting.BorderSide.Bottom;
            this.dIni.BorderColor = System.Drawing.Color.FromArgb(225, 228, 240);
            this.dIni.Dpi = 96F;
            this.dIni.ExpressionBindings.AddRange(new DevExpress.XtraReports.UI.ExpressionBinding[] {
                new DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[FechaIniFmt]")});
            this.dIni.LocationFloat = new DevExpress.Utils.PointFloat(380F, 1F);
            this.dIni.Name = "dIni";
            this.dIni.SizeF = new System.Drawing.SizeF(100F, 18F);
            //
            // dFin
            //
            this.dFin.Borders = DevExpress.XtraPrinting.BorderSide.Bottom;
            this.dFin.BorderColor = System.Drawing.Color.FromArgb(225, 228, 240);
            this.dFin.Dpi = 96F;
            this.dFin.ExpressionBindings.AddRange(new DevExpress.XtraReports.UI.ExpressionBinding[] {
                new DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[FechaFinFmt]")});
            this.dFin.LocationFloat = new DevExpress.Utils.PointFloat(480F, 1F);
            this.dFin.Name = "dFin";
            this.dFin.SizeF = new System.Drawing.SizeF(100F, 18F);
            //
            // dCert
            //
            this.dCert.Borders = DevExpress.XtraPrinting.BorderSide.Bottom;
            this.dCert.BorderColor = System.Drawing.Color.FromArgb(225, 228, 240);
            this.dCert.Dpi = 96F;
            this.dCert.ExpressionBindings.AddRange(new DevExpress.XtraReports.UI.ExpressionBinding[] {
                new DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[CertificadorTexto]")});
            this.dCert.LocationFloat = new DevExpress.Utils.PointFloat(580F, 1F);
            this.dCert.Name = "dCert";
            this.dCert.SizeF = new System.Drawing.SizeF(134F, 18F);
            //
            // gSubtotal
            //
            this.gSubtotal.Dpi = 96F;
            this.gSubtotal.ExpressionBindings.AddRange(new DevExpress.XtraReports.UI.ExpressionBinding[] {
                new DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "'Subtotal: ' + sumCount() + ' certificación(es)'")});
            this.gSubtotal.Font = new DevExpress.Drawing.DXFont("Segoe UI", 8.5F, DevExpress.Drawing.DXFontStyle.Italic);
            this.gSubtotal.ForeColor = System.Drawing.Color.FromArgb(102, 102, 102);
            this.gSubtotal.LocationFloat = new DevExpress.Utils.PointFloat(0F, 4F);
            this.gSubtotal.Name = "gSubtotal";
            this.gSubtotal.SizeF = new System.Drawing.SizeF(714F, 16F);
            this.gSubtotal.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
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
                new DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "'Total de certificaciones: ' + sumCount()")});
            this.xrTotal.Font = new DevExpress.Drawing.DXFont("Segoe UI", 9F, DevExpress.Drawing.DXFontStyle.Bold);
            this.xrTotal.LocationFloat = new DevExpress.Utils.PointFloat(0F, 12F);
            this.xrTotal.Name = "xrTotal";
            this.xrTotal.SizeF = new System.Drawing.SizeF(400F, 16F);
            //
            // objectDataSource1
            //
            this.objectDataSource1.DataSource = typeof(global::Sgpa.Web.Reporting.Predefinidos.CertificacionDetalleLinea);
            this.objectDataSource1.Name = "objectDataSource1";
            //
            // CertificacionPorMutualistaReport
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
        private DevExpress.XtraReports.UI.XRLabel gMutualista;
        private DevExpress.XtraReports.UI.XRLabel hCedula;
        private DevExpress.XtraReports.UI.XRLabel hNombre;
        private DevExpress.XtraReports.UI.XRLabel hIni;
        private DevExpress.XtraReports.UI.XRLabel hFin;
        private DevExpress.XtraReports.UI.XRLabel hCert;
        private DevExpress.XtraReports.UI.XRLine ruleG;
        private DevExpress.XtraReports.UI.XRLabel dCedula;
        private DevExpress.XtraReports.UI.XRLabel dNombre;
        private DevExpress.XtraReports.UI.XRLabel dIni;
        private DevExpress.XtraReports.UI.XRLabel dFin;
        private DevExpress.XtraReports.UI.XRLabel dCert;
        private DevExpress.XtraReports.UI.XRLabel gSubtotal;
        private DevExpress.XtraReports.UI.XRLine ruleFoot;
        private DevExpress.XtraReports.UI.XRLabel xrTotal;
        private DevExpress.DataAccess.ObjectBinding.ObjectDataSource objectDataSource1;
    }
}
