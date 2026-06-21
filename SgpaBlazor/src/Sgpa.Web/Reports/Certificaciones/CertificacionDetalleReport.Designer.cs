namespace Sgpa.Web.Reports.Certificaciones
{
    partial class CertificacionDetalleReport
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
            this.xrLogo = new DevExpress.XtraReports.UI.XRPictureBox();
            this.xrTitulo = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLinea = new DevExpress.XtraReports.UI.XRLine();
            this.capLlamado = new DevExpress.XtraReports.UI.XRLabel();
            this.valLlamado = new DevExpress.XtraReports.UI.XRLabel();
            this.capRecibo = new DevExpress.XtraReports.UI.XRLabel();
            this.valRecibo = new DevExpress.XtraReports.UI.XRLabel();
            this.capRecib = new DevExpress.XtraReports.UI.XRLabel();
            this.valRecib = new DevExpress.XtraReports.UI.XRLabel();
            this.capCert = new DevExpress.XtraReports.UI.XRLabel();
            this.valCert = new DevExpress.XtraReports.UI.XRLabel();
            this.capIni = new DevExpress.XtraReports.UI.XRLabel();
            this.valIni = new DevExpress.XtraReports.UI.XRLabel();
            this.capFin = new DevExpress.XtraReports.UI.XRLabel();
            this.valFin = new DevExpress.XtraReports.UI.XRLabel();
            this.capCed = new DevExpress.XtraReports.UI.XRLabel();
            this.valCed = new DevExpress.XtraReports.UI.XRLabel();
            this.capNom = new DevExpress.XtraReports.UI.XRLabel();
            this.valNom = new DevExpress.XtraReports.UI.XRLabel();
            this.capCertd = new DevExpress.XtraReports.UI.XRLabel();
            this.valCertd = new DevExpress.XtraReports.UI.XRLabel();
            this.capAfec = new DevExpress.XtraReports.UI.XRLabel();
            this.valAfec = new DevExpress.XtraReports.UI.XRLabel();
            this.capInd = new DevExpress.XtraReports.UI.XRLabel();
            this.valInd = new DevExpress.XtraReports.UI.XRLabel();
            this.ruleBlock = new DevExpress.XtraReports.UI.XRLine();
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
            this.ReportHeader.HeightF = 58F;
            this.ReportHeader.Name = "ReportHeader";
            //
            // Detail
            //
            this.Detail.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
                this.capLlamado, this.valLlamado, this.capRecibo, this.valRecibo, this.capRecib, this.valRecib,
                this.capCert, this.valCert, this.capIni, this.valIni, this.capFin, this.valFin,
                this.capCed, this.valCed, this.capNom, this.valNom,
                this.capCertd, this.valCertd, this.capAfec, this.valAfec, this.capInd, this.valInd, this.ruleBlock});
            this.Detail.Dpi = 96F;
            this.Detail.HeightF = 150F;
            this.Detail.Name = "Detail";
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
            this.xrTitulo.Text = "Certificados del afiliado";
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
            // Fila 1: Nº llamado / Nº recibo / Fecha recibido
            //
            this.capLlamado.Dpi = 96F;
            this.capLlamado.Font = new DevExpress.Drawing.DXFont("Segoe UI", 8.5F, DevExpress.Drawing.DXFontStyle.Bold);
            this.capLlamado.ForeColor = System.Drawing.Color.FromArgb(102, 102, 102);
            this.capLlamado.LocationFloat = new DevExpress.Utils.PointFloat(0F, 8F);
            this.capLlamado.Name = "capLlamado";
            this.capLlamado.SizeF = new System.Drawing.SizeF(118F, 18F);
            this.capLlamado.Text = "Nº de llamado";
            this.valLlamado.Dpi = 96F;
            this.valLlamado.ExpressionBindings.AddRange(new DevExpress.XtraReports.UI.ExpressionBinding[] {
                new DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[NroLlamado]")});
            this.valLlamado.LocationFloat = new DevExpress.Utils.PointFloat(120F, 8F);
            this.valLlamado.Name = "valLlamado";
            this.valLlamado.SizeF = new System.Drawing.SizeF(110F, 18F);
            this.capRecibo.Dpi = 96F;
            this.capRecibo.Font = new DevExpress.Drawing.DXFont("Segoe UI", 8.5F, DevExpress.Drawing.DXFontStyle.Bold);
            this.capRecibo.ForeColor = System.Drawing.Color.FromArgb(102, 102, 102);
            this.capRecibo.LocationFloat = new DevExpress.Utils.PointFloat(238F, 8F);
            this.capRecibo.Name = "capRecibo";
            this.capRecibo.SizeF = new System.Drawing.SizeF(100F, 18F);
            this.capRecibo.Text = "Nº de recibo";
            this.valRecibo.Dpi = 96F;
            this.valRecibo.ExpressionBindings.AddRange(new DevExpress.XtraReports.UI.ExpressionBinding[] {
                new DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[NroRecibo]")});
            this.valRecibo.LocationFloat = new DevExpress.Utils.PointFloat(340F, 8F);
            this.valRecibo.Name = "valRecibo";
            this.valRecibo.SizeF = new System.Drawing.SizeF(110F, 18F);
            this.capRecib.Dpi = 96F;
            this.capRecib.Font = new DevExpress.Drawing.DXFont("Segoe UI", 8.5F, DevExpress.Drawing.DXFontStyle.Bold);
            this.capRecib.ForeColor = System.Drawing.Color.FromArgb(102, 102, 102);
            this.capRecib.LocationFloat = new DevExpress.Utils.PointFloat(470F, 8F);
            this.capRecib.Name = "capRecib";
            this.capRecib.SizeF = new System.Drawing.SizeF(110F, 18F);
            this.capRecib.Text = "Fecha recibido";
            this.valRecib.Dpi = 96F;
            this.valRecib.ExpressionBindings.AddRange(new DevExpress.XtraReports.UI.ExpressionBinding[] {
                new DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[FechaRecibidoFmt]")});
            this.valRecib.LocationFloat = new DevExpress.Utils.PointFloat(584F, 8F);
            this.valRecib.Name = "valRecib";
            this.valRecib.SizeF = new System.Drawing.SizeF(130F, 18F);
            //
            // Fila 2: Fecha certificación / Fecha inicio / Fecha fin
            //
            this.capCert.Dpi = 96F;
            this.capCert.Font = new DevExpress.Drawing.DXFont("Segoe UI", 8.5F, DevExpress.Drawing.DXFontStyle.Bold);
            this.capCert.ForeColor = System.Drawing.Color.FromArgb(102, 102, 102);
            this.capCert.LocationFloat = new DevExpress.Utils.PointFloat(0F, 30F);
            this.capCert.Name = "capCert";
            this.capCert.SizeF = new System.Drawing.SizeF(118F, 18F);
            this.capCert.Text = "Fecha certificación";
            this.valCert.Dpi = 96F;
            this.valCert.ExpressionBindings.AddRange(new DevExpress.XtraReports.UI.ExpressionBinding[] {
                new DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[FechaCertFmt]")});
            this.valCert.LocationFloat = new DevExpress.Utils.PointFloat(120F, 30F);
            this.valCert.Name = "valCert";
            this.valCert.SizeF = new System.Drawing.SizeF(110F, 18F);
            this.capIni.Dpi = 96F;
            this.capIni.Font = new DevExpress.Drawing.DXFont("Segoe UI", 8.5F, DevExpress.Drawing.DXFontStyle.Bold);
            this.capIni.ForeColor = System.Drawing.Color.FromArgb(102, 102, 102);
            this.capIni.LocationFloat = new DevExpress.Utils.PointFloat(238F, 30F);
            this.capIni.Name = "capIni";
            this.capIni.SizeF = new System.Drawing.SizeF(100F, 18F);
            this.capIni.Text = "Fecha inicio";
            this.valIni.Dpi = 96F;
            this.valIni.ExpressionBindings.AddRange(new DevExpress.XtraReports.UI.ExpressionBinding[] {
                new DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[FechaIniFmt]")});
            this.valIni.LocationFloat = new DevExpress.Utils.PointFloat(340F, 30F);
            this.valIni.Name = "valIni";
            this.valIni.SizeF = new System.Drawing.SizeF(110F, 18F);
            this.capFin.Dpi = 96F;
            this.capFin.Font = new DevExpress.Drawing.DXFont("Segoe UI", 8.5F, DevExpress.Drawing.DXFontStyle.Bold);
            this.capFin.ForeColor = System.Drawing.Color.FromArgb(102, 102, 102);
            this.capFin.LocationFloat = new DevExpress.Utils.PointFloat(470F, 30F);
            this.capFin.Name = "capFin";
            this.capFin.SizeF = new System.Drawing.SizeF(110F, 18F);
            this.capFin.Text = "Fecha fin";
            this.valFin.Dpi = 96F;
            this.valFin.ExpressionBindings.AddRange(new DevExpress.XtraReports.UI.ExpressionBinding[] {
                new DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[FechaFinFmt]")});
            this.valFin.LocationFloat = new DevExpress.Utils.PointFloat(584F, 30F);
            this.valFin.Name = "valFin";
            this.valFin.SizeF = new System.Drawing.SizeF(130F, 18F);
            //
            // Fila 3: Cédula / Nombre
            //
            this.capCed.Dpi = 96F;
            this.capCed.Font = new DevExpress.Drawing.DXFont("Segoe UI", 8.5F, DevExpress.Drawing.DXFontStyle.Bold);
            this.capCed.ForeColor = System.Drawing.Color.FromArgb(102, 102, 102);
            this.capCed.LocationFloat = new DevExpress.Utils.PointFloat(0F, 52F);
            this.capCed.Name = "capCed";
            this.capCed.SizeF = new System.Drawing.SizeF(118F, 18F);
            this.capCed.Text = "Cédula";
            this.valCed.Dpi = 96F;
            this.valCed.ExpressionBindings.AddRange(new DevExpress.XtraReports.UI.ExpressionBinding[] {
                new DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[CIFormato]")});
            this.valCed.LocationFloat = new DevExpress.Utils.PointFloat(120F, 52F);
            this.valCed.Name = "valCed";
            this.valCed.SizeF = new System.Drawing.SizeF(110F, 18F);
            this.capNom.Dpi = 96F;
            this.capNom.Font = new DevExpress.Drawing.DXFont("Segoe UI", 8.5F, DevExpress.Drawing.DXFontStyle.Bold);
            this.capNom.ForeColor = System.Drawing.Color.FromArgb(102, 102, 102);
            this.capNom.LocationFloat = new DevExpress.Utils.PointFloat(238F, 52F);
            this.capNom.Name = "capNom";
            this.capNom.SizeF = new System.Drawing.SizeF(100F, 18F);
            this.capNom.Text = "Nombre";
            this.valNom.Dpi = 96F;
            this.valNom.ExpressionBindings.AddRange(new DevExpress.XtraReports.UI.ExpressionBinding[] {
                new DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[NombreTexto]")});
            this.valNom.LocationFloat = new DevExpress.Utils.PointFloat(340F, 52F);
            this.valNom.Name = "valNom";
            this.valNom.SizeF = new System.Drawing.SizeF(374F, 18F);
            //
            // Fila 4: Certificador
            //
            this.capCertd.Dpi = 96F;
            this.capCertd.Font = new DevExpress.Drawing.DXFont("Segoe UI", 8.5F, DevExpress.Drawing.DXFontStyle.Bold);
            this.capCertd.ForeColor = System.Drawing.Color.FromArgb(102, 102, 102);
            this.capCertd.LocationFloat = new DevExpress.Utils.PointFloat(0F, 74F);
            this.capCertd.Name = "capCertd";
            this.capCertd.SizeF = new System.Drawing.SizeF(118F, 18F);
            this.capCertd.Text = "Certificador";
            this.valCertd.Dpi = 96F;
            this.valCertd.ExpressionBindings.AddRange(new DevExpress.XtraReports.UI.ExpressionBinding[] {
                new DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[CertificadorTexto]")});
            this.valCertd.LocationFloat = new DevExpress.Utils.PointFloat(120F, 74F);
            this.valCertd.Name = "valCertd";
            this.valCertd.SizeF = new System.Drawing.SizeF(594F, 18F);
            //
            // Fila 5: Afección
            //
            this.capAfec.Dpi = 96F;
            this.capAfec.Font = new DevExpress.Drawing.DXFont("Segoe UI", 8.5F, DevExpress.Drawing.DXFontStyle.Bold);
            this.capAfec.ForeColor = System.Drawing.Color.FromArgb(102, 102, 102);
            this.capAfec.LocationFloat = new DevExpress.Utils.PointFloat(0F, 96F);
            this.capAfec.Name = "capAfec";
            this.capAfec.SizeF = new System.Drawing.SizeF(118F, 18F);
            this.capAfec.Text = "Afección";
            this.valAfec.Dpi = 96F;
            this.valAfec.ExpressionBindings.AddRange(new DevExpress.XtraReports.UI.ExpressionBinding[] {
                new DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[AfeccionTexto]")});
            this.valAfec.LocationFloat = new DevExpress.Utils.PointFloat(120F, 96F);
            this.valAfec.Name = "valAfec";
            this.valAfec.SizeF = new System.Drawing.SizeF(594F, 18F);
            //
            // Fila 6: Indicaciones
            //
            this.capInd.Dpi = 96F;
            this.capInd.Font = new DevExpress.Drawing.DXFont("Segoe UI", 8.5F, DevExpress.Drawing.DXFontStyle.Bold);
            this.capInd.ForeColor = System.Drawing.Color.FromArgb(102, 102, 102);
            this.capInd.LocationFloat = new DevExpress.Utils.PointFloat(0F, 118F);
            this.capInd.Name = "capInd";
            this.capInd.SizeF = new System.Drawing.SizeF(118F, 18F);
            this.capInd.Text = "Indicaciones";
            this.valInd.Dpi = 96F;
            this.valInd.ExpressionBindings.AddRange(new DevExpress.XtraReports.UI.ExpressionBinding[] {
                new DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[IndicacionesTexto]")});
            this.valInd.LocationFloat = new DevExpress.Utils.PointFloat(120F, 118F);
            this.valInd.Multiline = true;
            this.valInd.Name = "valInd";
            this.valInd.SizeF = new System.Drawing.SizeF(594F, 18F);
            //
            // ruleBlock
            //
            this.ruleBlock.Dpi = 96F;
            this.ruleBlock.ForeColor = System.Drawing.Color.FromArgb(210, 216, 232);
            this.ruleBlock.LocationFloat = new DevExpress.Utils.PointFloat(0F, 144F);
            this.ruleBlock.Name = "ruleBlock";
            this.ruleBlock.SizeF = new System.Drawing.SizeF(714F, 1F);
            //
            // objectDataSource1
            //
            this.objectDataSource1.DataSource = typeof(global::Sgpa.Web.Reporting.Predefinidos.CertificacionDetalleLinea);
            this.objectDataSource1.Name = "objectDataSource1";
            //
            // CertificacionDetalleReport
            //
            this.Bands.AddRange(new DevExpress.XtraReports.UI.Band[] {
                this.TopMargin, this.ReportHeader, this.Detail, this.BottomMargin});
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
        internal DevExpress.XtraReports.UI.XRPictureBox xrLogo;
        private DevExpress.XtraReports.UI.XRLabel xrTitulo;
        private DevExpress.XtraReports.UI.XRLine xrLinea;
        private DevExpress.XtraReports.UI.XRLabel capLlamado;
        private DevExpress.XtraReports.UI.XRLabel valLlamado;
        private DevExpress.XtraReports.UI.XRLabel capRecibo;
        private DevExpress.XtraReports.UI.XRLabel valRecibo;
        private DevExpress.XtraReports.UI.XRLabel capRecib;
        private DevExpress.XtraReports.UI.XRLabel valRecib;
        private DevExpress.XtraReports.UI.XRLabel capCert;
        private DevExpress.XtraReports.UI.XRLabel valCert;
        private DevExpress.XtraReports.UI.XRLabel capIni;
        private DevExpress.XtraReports.UI.XRLabel valIni;
        private DevExpress.XtraReports.UI.XRLabel capFin;
        private DevExpress.XtraReports.UI.XRLabel valFin;
        private DevExpress.XtraReports.UI.XRLabel capCed;
        private DevExpress.XtraReports.UI.XRLabel valCed;
        private DevExpress.XtraReports.UI.XRLabel capNom;
        private DevExpress.XtraReports.UI.XRLabel valNom;
        private DevExpress.XtraReports.UI.XRLabel capCertd;
        private DevExpress.XtraReports.UI.XRLabel valCertd;
        private DevExpress.XtraReports.UI.XRLabel capAfec;
        private DevExpress.XtraReports.UI.XRLabel valAfec;
        private DevExpress.XtraReports.UI.XRLabel capInd;
        private DevExpress.XtraReports.UI.XRLabel valInd;
        private DevExpress.XtraReports.UI.XRLine ruleBlock;
        private DevExpress.DataAccess.ObjectBinding.ObjectDataSource objectDataSource1;
    }
}
