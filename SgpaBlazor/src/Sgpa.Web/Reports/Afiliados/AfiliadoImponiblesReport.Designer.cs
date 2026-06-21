namespace Sgpa.Web.Reports.Afiliados
{
    partial class AfiliadoImponiblesReport
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
            this.hPeriodo = new DevExpress.XtraReports.UI.XRLabel();
            this.hEmpresa = new DevExpress.XtraReports.UI.XRLabel();
            this.hConcepto = new DevExpress.XtraReports.UI.XRLabel();
            this.hDias = new DevExpress.XtraReports.UI.XRLabel();
            this.hImporte = new DevExpress.XtraReports.UI.XRLabel();
            this.ruleHead = new DevExpress.XtraReports.UI.XRLine();
            this.dPeriodo = new DevExpress.XtraReports.UI.XRLabel();
            this.dEmpresa = new DevExpress.XtraReports.UI.XRLabel();
            this.dConcepto = new DevExpress.XtraReports.UI.XRLabel();
            this.dDias = new DevExpress.XtraReports.UI.XRLabel();
            this.dImporte = new DevExpress.XtraReports.UI.XRLabel();
            this.ruleFoot = new DevExpress.XtraReports.UI.XRLine();
            this.xrTotal = new DevExpress.XtraReports.UI.XRLabel();
            this.xrTotalImp = new DevExpress.XtraReports.UI.XRLabel();
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
                this.hPeriodo, this.hEmpresa, this.hConcepto, this.hDias, this.hImporte, this.ruleHead});
            this.ReportHeader.Dpi = 96F;
            this.ReportHeader.HeightF = 140F;
            this.ReportHeader.Name = "ReportHeader";
            //
            // Detail
            //
            this.Detail.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
                this.dPeriodo, this.dEmpresa, this.dConcepto, this.dDias, this.dImporte});
            this.Detail.Dpi = 96F;
            this.Detail.HeightF = 22F;
            this.Detail.Name = "Detail";
            //
            // ReportFooter
            //
            this.ReportFooter.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
                this.ruleFoot, this.xrTotal, this.xrTotalImp});
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
            this.xrTitulo.Text = "Imponibles del afiliado";
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
            // hPeriodo
            //
            this.hPeriodo.Dpi = 96F;
            this.hPeriodo.Font = new DevExpress.Drawing.DXFont("Segoe UI", 9F, DevExpress.Drawing.DXFontStyle.Bold);
            this.hPeriodo.ForeColor = System.Drawing.Color.FromArgb(102, 102, 102);
            this.hPeriodo.LocationFloat = new DevExpress.Utils.PointFloat(0F, 116F);
            this.hPeriodo.Name = "hPeriodo";
            this.hPeriodo.SizeF = new System.Drawing.SizeF(80F, 18F);
            this.hPeriodo.Text = "Período";
            //
            // hEmpresa
            //
            this.hEmpresa.Dpi = 96F;
            this.hEmpresa.Font = new DevExpress.Drawing.DXFont("Segoe UI", 9F, DevExpress.Drawing.DXFontStyle.Bold);
            this.hEmpresa.ForeColor = System.Drawing.Color.FromArgb(102, 102, 102);
            this.hEmpresa.LocationFloat = new DevExpress.Utils.PointFloat(84F, 116F);
            this.hEmpresa.Name = "hEmpresa";
            this.hEmpresa.SizeF = new System.Drawing.SizeF(260F, 18F);
            this.hEmpresa.Text = "Empresa";
            //
            // hConcepto
            //
            this.hConcepto.Dpi = 96F;
            this.hConcepto.Font = new DevExpress.Drawing.DXFont("Segoe UI", 9F, DevExpress.Drawing.DXFontStyle.Bold);
            this.hConcepto.ForeColor = System.Drawing.Color.FromArgb(102, 102, 102);
            this.hConcepto.LocationFloat = new DevExpress.Utils.PointFloat(348F, 116F);
            this.hConcepto.Name = "hConcepto";
            this.hConcepto.SizeF = new System.Drawing.SizeF(130F, 18F);
            this.hConcepto.Text = "Concepto";
            //
            // hDias
            //
            this.hDias.Dpi = 96F;
            this.hDias.Font = new DevExpress.Drawing.DXFont("Segoe UI", 9F, DevExpress.Drawing.DXFontStyle.Bold);
            this.hDias.ForeColor = System.Drawing.Color.FromArgb(102, 102, 102);
            this.hDias.LocationFloat = new DevExpress.Utils.PointFloat(482F, 116F);
            this.hDias.Name = "hDias";
            this.hDias.SizeF = new System.Drawing.SizeF(60F, 18F);
            this.hDias.Text = "Días";
            this.hDias.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
            //
            // hImporte
            //
            this.hImporte.Dpi = 96F;
            this.hImporte.Font = new DevExpress.Drawing.DXFont("Segoe UI", 9F, DevExpress.Drawing.DXFontStyle.Bold);
            this.hImporte.ForeColor = System.Drawing.Color.FromArgb(102, 102, 102);
            this.hImporte.LocationFloat = new DevExpress.Utils.PointFloat(546F, 116F);
            this.hImporte.Name = "hImporte";
            this.hImporte.SizeF = new System.Drawing.SizeF(168F, 18F);
            this.hImporte.Text = "Importe";
            this.hImporte.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
            //
            // ruleHead
            //
            this.ruleHead.Dpi = 96F;
            this.ruleHead.ForeColor = System.Drawing.Color.FromArgb(28, 54, 128);
            this.ruleHead.LocationFloat = new DevExpress.Utils.PointFloat(0F, 136F);
            this.ruleHead.Name = "ruleHead";
            this.ruleHead.SizeF = new System.Drawing.SizeF(714F, 1F);
            //
            // dPeriodo
            //
            this.dPeriodo.Borders = DevExpress.XtraPrinting.BorderSide.Bottom;
            this.dPeriodo.BorderColor = System.Drawing.Color.FromArgb(225, 228, 240);
            this.dPeriodo.Dpi = 96F;
            this.dPeriodo.ExpressionBindings.AddRange(new DevExpress.XtraReports.UI.ExpressionBinding[] {
                new DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[PeriodoFmt]")});
            this.dPeriodo.LocationFloat = new DevExpress.Utils.PointFloat(0F, 1F);
            this.dPeriodo.Name = "dPeriodo";
            this.dPeriodo.SizeF = new System.Drawing.SizeF(80F, 20F);
            //
            // dEmpresa
            //
            this.dEmpresa.Borders = DevExpress.XtraPrinting.BorderSide.Bottom;
            this.dEmpresa.BorderColor = System.Drawing.Color.FromArgb(225, 228, 240);
            this.dEmpresa.Dpi = 96F;
            this.dEmpresa.ExpressionBindings.AddRange(new DevExpress.XtraReports.UI.ExpressionBinding[] {
                new DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[EmpresaTexto]")});
            this.dEmpresa.LocationFloat = new DevExpress.Utils.PointFloat(84F, 1F);
            this.dEmpresa.Name = "dEmpresa";
            this.dEmpresa.SizeF = new System.Drawing.SizeF(260F, 20F);
            //
            // dConcepto
            //
            this.dConcepto.Borders = DevExpress.XtraPrinting.BorderSide.Bottom;
            this.dConcepto.BorderColor = System.Drawing.Color.FromArgb(225, 228, 240);
            this.dConcepto.Dpi = 96F;
            this.dConcepto.ExpressionBindings.AddRange(new DevExpress.XtraReports.UI.ExpressionBinding[] {
                new DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[ConceptoTexto]")});
            this.dConcepto.LocationFloat = new DevExpress.Utils.PointFloat(348F, 1F);
            this.dConcepto.Name = "dConcepto";
            this.dConcepto.SizeF = new System.Drawing.SizeF(130F, 20F);
            //
            // dDias
            //
            this.dDias.Borders = DevExpress.XtraPrinting.BorderSide.Bottom;
            this.dDias.BorderColor = System.Drawing.Color.FromArgb(225, 228, 240);
            this.dDias.Dpi = 96F;
            this.dDias.ExpressionBindings.AddRange(new DevExpress.XtraReports.UI.ExpressionBinding[] {
                new DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[DiasTrabajados]")});
            this.dDias.LocationFloat = new DevExpress.Utils.PointFloat(482F, 1F);
            this.dDias.Name = "dDias";
            this.dDias.SizeF = new System.Drawing.SizeF(60F, 20F);
            this.dDias.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
            //
            // dImporte
            //
            this.dImporte.Borders = DevExpress.XtraPrinting.BorderSide.Bottom;
            this.dImporte.BorderColor = System.Drawing.Color.FromArgb(225, 228, 240);
            this.dImporte.Dpi = 96F;
            this.dImporte.ExpressionBindings.AddRange(new DevExpress.XtraReports.UI.ExpressionBinding[] {
                new DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[ImporteFmt]")});
            this.dImporte.LocationFloat = new DevExpress.Utils.PointFloat(546F, 1F);
            this.dImporte.Name = "dImporte";
            this.dImporte.SizeF = new System.Drawing.SizeF(168F, 20F);
            this.dImporte.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
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
                new DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "'Total de registros: ' + sumCount()")});
            this.xrTotal.Font = new DevExpress.Drawing.DXFont("Segoe UI", 9F, DevExpress.Drawing.DXFontStyle.Bold);
            this.xrTotal.LocationFloat = new DevExpress.Utils.PointFloat(0F, 12F);
            this.xrTotal.Name = "xrTotal";
            this.xrTotal.SizeF = new System.Drawing.SizeF(360F, 16F);
            //
            // xrTotalImp
            //
            this.xrTotalImp.Dpi = 96F;
            this.xrTotalImp.ExpressionBindings.AddRange(new DevExpress.XtraReports.UI.ExpressionBinding[] {
                new DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "'Total $ ' + FormatString('{0:N2}', sumSum([Importe]))")});
            this.xrTotalImp.Font = new DevExpress.Drawing.DXFont("Segoe UI", 9F, DevExpress.Drawing.DXFontStyle.Bold);
            this.xrTotalImp.ForeColor = System.Drawing.Color.FromArgb(28, 54, 128);
            this.xrTotalImp.LocationFloat = new DevExpress.Utils.PointFloat(434F, 12F);
            this.xrTotalImp.Name = "xrTotalImp";
            this.xrTotalImp.SizeF = new System.Drawing.SizeF(280F, 16F);
            this.xrTotalImp.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
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
            this.objectDataSource1.DataSource = typeof(global::Sgpa.Web.Reporting.Predefinidos.AfiliadoImponibleLinea);
            this.objectDataSource1.Name = "objectDataSource1";
            //
            // AfiliadoImponiblesReport
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
        private DevExpress.XtraReports.UI.XRLabel hPeriodo;
        private DevExpress.XtraReports.UI.XRLabel hEmpresa;
        private DevExpress.XtraReports.UI.XRLabel hConcepto;
        private DevExpress.XtraReports.UI.XRLabel hDias;
        private DevExpress.XtraReports.UI.XRLabel hImporte;
        private DevExpress.XtraReports.UI.XRLine ruleHead;
        private DevExpress.XtraReports.UI.XRLabel dPeriodo;
        private DevExpress.XtraReports.UI.XRLabel dEmpresa;
        private DevExpress.XtraReports.UI.XRLabel dConcepto;
        private DevExpress.XtraReports.UI.XRLabel dDias;
        private DevExpress.XtraReports.UI.XRLabel dImporte;
        private DevExpress.XtraReports.UI.XRLine ruleFoot;
        private DevExpress.XtraReports.UI.XRLabel xrTotal;
        private DevExpress.XtraReports.UI.XRLabel xrTotalImp;
        private DevExpress.XtraReports.Parameters.Parameter pNombre;
        private DevExpress.XtraReports.Parameters.Parameter pCI;
        private DevExpress.DataAccess.ObjectBinding.ObjectDataSource objectDataSource1;
    }
}
