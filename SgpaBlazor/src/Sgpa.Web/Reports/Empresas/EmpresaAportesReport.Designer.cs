namespace Sgpa.Web.Reports.Empresas
{
    partial class EmpresaAportesReport
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
            this.xrCodSub = new DevExpress.XtraReports.UI.XRLabel();
            this.hPeriodo = new DevExpress.XtraReports.UI.XRLabel();
            this.hAfiliados = new DevExpress.XtraReports.UI.XRLabel();
            this.hAporte = new DevExpress.XtraReports.UI.XRLabel();
            this.hAguinaldo = new DevExpress.XtraReports.UI.XRLabel();
            this.hTotal = new DevExpress.XtraReports.UI.XRLabel();
            this.ruleHead = new DevExpress.XtraReports.UI.XRLine();
            this.dPeriodo = new DevExpress.XtraReports.UI.XRLabel();
            this.dAfiliados = new DevExpress.XtraReports.UI.XRLabel();
            this.dAporte = new DevExpress.XtraReports.UI.XRLabel();
            this.dAguinaldo = new DevExpress.XtraReports.UI.XRLabel();
            this.dTotal = new DevExpress.XtraReports.UI.XRLabel();
            this.ruleFoot = new DevExpress.XtraReports.UI.XRLine();
            this.xrTotalLbl = new DevExpress.XtraReports.UI.XRLabel();
            this.xrTotalImp = new DevExpress.XtraReports.UI.XRLabel();
            this.pNombre = new DevExpress.XtraReports.Parameters.Parameter();
            this.pCod = new DevExpress.XtraReports.Parameters.Parameter();
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
                this.xrLogo, this.xrTitulo, this.xrLinea, this.xrNombre, this.xrCodSub,
                this.hPeriodo, this.hAfiliados, this.hAporte, this.hAguinaldo, this.hTotal, this.ruleHead});
            this.ReportHeader.Dpi = 96F;
            this.ReportHeader.HeightF = 140F;
            this.ReportHeader.Name = "ReportHeader";
            //
            // Detail
            //
            this.Detail.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
                this.dPeriodo, this.dAfiliados, this.dAporte, this.dAguinaldo, this.dTotal});
            this.Detail.Dpi = 96F;
            this.Detail.HeightF = 22F;
            this.Detail.Name = "Detail";
            //
            // ReportFooter
            //
            this.ReportFooter.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
                this.ruleFoot, this.xrTotalLbl, this.xrTotalImp});
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
            this.xrTitulo.Text = "Resumen de aportes";
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
            // xrCodSub
            //
            this.xrCodSub.Dpi = 96F;
            this.xrCodSub.ExpressionBindings.AddRange(new DevExpress.XtraReports.UI.ExpressionBinding[] {
                new DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "'Código ' + [Parameters.pCod]")});
            this.xrCodSub.Font = new DevExpress.Drawing.DXFont("Segoe UI", 9.5F);
            this.xrCodSub.ForeColor = System.Drawing.Color.FromArgb(102, 102, 102);
            this.xrCodSub.LocationFloat = new DevExpress.Utils.PointFloat(0F, 88F);
            this.xrCodSub.Name = "xrCodSub";
            this.xrCodSub.SizeF = new System.Drawing.SizeF(714F, 16F);
            //
            // hPeriodo
            //
            this.hPeriodo.Dpi = 96F;
            this.hPeriodo.Font = new DevExpress.Drawing.DXFont("Segoe UI", 9F, DevExpress.Drawing.DXFontStyle.Bold);
            this.hPeriodo.ForeColor = System.Drawing.Color.FromArgb(102, 102, 102);
            this.hPeriodo.LocationFloat = new DevExpress.Utils.PointFloat(0F, 116F);
            this.hPeriodo.Name = "hPeriodo";
            this.hPeriodo.SizeF = new System.Drawing.SizeF(100F, 18F);
            this.hPeriodo.Text = "Período";
            //
            // hAfiliados
            //
            this.hAfiliados.Dpi = 96F;
            this.hAfiliados.Font = new DevExpress.Drawing.DXFont("Segoe UI", 9F, DevExpress.Drawing.DXFontStyle.Bold);
            this.hAfiliados.ForeColor = System.Drawing.Color.FromArgb(102, 102, 102);
            this.hAfiliados.LocationFloat = new DevExpress.Utils.PointFloat(100F, 116F);
            this.hAfiliados.Name = "hAfiliados";
            this.hAfiliados.SizeF = new System.Drawing.SizeF(90F, 18F);
            this.hAfiliados.Text = "Afiliados";
            this.hAfiliados.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
            //
            // hAporte
            //
            this.hAporte.Dpi = 96F;
            this.hAporte.Font = new DevExpress.Drawing.DXFont("Segoe UI", 9F, DevExpress.Drawing.DXFontStyle.Bold);
            this.hAporte.ForeColor = System.Drawing.Color.FromArgb(102, 102, 102);
            this.hAporte.LocationFloat = new DevExpress.Utils.PointFloat(190F, 116F);
            this.hAporte.Name = "hAporte";
            this.hAporte.SizeF = new System.Drawing.SizeF(160F, 18F);
            this.hAporte.Text = "Aporte";
            this.hAporte.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
            //
            // hAguinaldo
            //
            this.hAguinaldo.Dpi = 96F;
            this.hAguinaldo.Font = new DevExpress.Drawing.DXFont("Segoe UI", 9F, DevExpress.Drawing.DXFontStyle.Bold);
            this.hAguinaldo.ForeColor = System.Drawing.Color.FromArgb(102, 102, 102);
            this.hAguinaldo.LocationFloat = new DevExpress.Utils.PointFloat(350F, 116F);
            this.hAguinaldo.Name = "hAguinaldo";
            this.hAguinaldo.SizeF = new System.Drawing.SizeF(160F, 18F);
            this.hAguinaldo.Text = "Aguinaldo";
            this.hAguinaldo.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
            //
            // hTotal
            //
            this.hTotal.Dpi = 96F;
            this.hTotal.Font = new DevExpress.Drawing.DXFont("Segoe UI", 9F, DevExpress.Drawing.DXFontStyle.Bold);
            this.hTotal.ForeColor = System.Drawing.Color.FromArgb(102, 102, 102);
            this.hTotal.LocationFloat = new DevExpress.Utils.PointFloat(510F, 116F);
            this.hTotal.Name = "hTotal";
            this.hTotal.SizeF = new System.Drawing.SizeF(204F, 18F);
            this.hTotal.Text = "Total";
            this.hTotal.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
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
            this.dPeriodo.SizeF = new System.Drawing.SizeF(100F, 20F);
            //
            // dAfiliados
            //
            this.dAfiliados.Borders = DevExpress.XtraPrinting.BorderSide.Bottom;
            this.dAfiliados.BorderColor = System.Drawing.Color.FromArgb(225, 228, 240);
            this.dAfiliados.Dpi = 96F;
            this.dAfiliados.ExpressionBindings.AddRange(new DevExpress.XtraReports.UI.ExpressionBinding[] {
                new DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[Afiliados]")});
            this.dAfiliados.LocationFloat = new DevExpress.Utils.PointFloat(100F, 1F);
            this.dAfiliados.Name = "dAfiliados";
            this.dAfiliados.SizeF = new System.Drawing.SizeF(90F, 20F);
            this.dAfiliados.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
            //
            // dAporte
            //
            this.dAporte.Borders = DevExpress.XtraPrinting.BorderSide.Bottom;
            this.dAporte.BorderColor = System.Drawing.Color.FromArgb(225, 228, 240);
            this.dAporte.Dpi = 96F;
            this.dAporte.ExpressionBindings.AddRange(new DevExpress.XtraReports.UI.ExpressionBinding[] {
                new DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[ImporteAporteFmt]")});
            this.dAporte.LocationFloat = new DevExpress.Utils.PointFloat(190F, 1F);
            this.dAporte.Name = "dAporte";
            this.dAporte.SizeF = new System.Drawing.SizeF(160F, 20F);
            this.dAporte.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
            //
            // dAguinaldo
            //
            this.dAguinaldo.Borders = DevExpress.XtraPrinting.BorderSide.Bottom;
            this.dAguinaldo.BorderColor = System.Drawing.Color.FromArgb(225, 228, 240);
            this.dAguinaldo.Dpi = 96F;
            this.dAguinaldo.ExpressionBindings.AddRange(new DevExpress.XtraReports.UI.ExpressionBinding[] {
                new DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[ImporteAguinaldoFmt]")});
            this.dAguinaldo.LocationFloat = new DevExpress.Utils.PointFloat(350F, 1F);
            this.dAguinaldo.Name = "dAguinaldo";
            this.dAguinaldo.SizeF = new System.Drawing.SizeF(160F, 20F);
            this.dAguinaldo.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
            //
            // dTotal
            //
            this.dTotal.Borders = DevExpress.XtraPrinting.BorderSide.Bottom;
            this.dTotal.BorderColor = System.Drawing.Color.FromArgb(225, 228, 240);
            this.dTotal.Dpi = 96F;
            this.dTotal.ExpressionBindings.AddRange(new DevExpress.XtraReports.UI.ExpressionBinding[] {
                new DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[TotalFmt]")});
            this.dTotal.Font = new DevExpress.Drawing.DXFont("Segoe UI", 9.5F, DevExpress.Drawing.DXFontStyle.Bold);
            this.dTotal.LocationFloat = new DevExpress.Utils.PointFloat(510F, 1F);
            this.dTotal.Name = "dTotal";
            this.dTotal.SizeF = new System.Drawing.SizeF(204F, 20F);
            this.dTotal.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
            //
            // ruleFoot
            //
            this.ruleFoot.Dpi = 96F;
            this.ruleFoot.ForeColor = System.Drawing.Color.FromArgb(28, 54, 128);
            this.ruleFoot.LocationFloat = new DevExpress.Utils.PointFloat(0F, 6F);
            this.ruleFoot.Name = "ruleFoot";
            this.ruleFoot.SizeF = new System.Drawing.SizeF(714F, 1F);
            //
            // xrTotalLbl
            //
            this.xrTotalLbl.Dpi = 96F;
            this.xrTotalLbl.ExpressionBindings.AddRange(new DevExpress.XtraReports.UI.ExpressionBinding[] {
                new DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "'Meses: ' + sumCount()")});
            this.xrTotalLbl.Font = new DevExpress.Drawing.DXFont("Segoe UI", 9F, DevExpress.Drawing.DXFontStyle.Bold);
            this.xrTotalLbl.LocationFloat = new DevExpress.Utils.PointFloat(0F, 12F);
            this.xrTotalLbl.Name = "xrTotalLbl";
            this.xrTotalLbl.SizeF = new System.Drawing.SizeF(190F, 16F);
            //
            // xrTotalImp
            //
            this.xrTotalImp.Dpi = 96F;
            this.xrTotalImp.ExpressionBindings.AddRange(new DevExpress.XtraReports.UI.ExpressionBinding[] {
                new DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "'Total $ ' + FormatString('{0:N2}', sumSum([ImporteAporte]) + sumSum([ImporteAguinaldo]))")});
            this.xrTotalImp.Font = new DevExpress.Drawing.DXFont("Segoe UI", 9.5F, DevExpress.Drawing.DXFontStyle.Bold);
            this.xrTotalImp.ForeColor = System.Drawing.Color.FromArgb(28, 54, 128);
            this.xrTotalImp.LocationFloat = new DevExpress.Utils.PointFloat(350F, 12F);
            this.xrTotalImp.Name = "xrTotalImp";
            this.xrTotalImp.SizeF = new System.Drawing.SizeF(364F, 16F);
            this.xrTotalImp.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
            //
            // pNombre
            //
            this.pNombre.Description = "pNombre";
            this.pNombre.Name = "pNombre";
            this.pNombre.Visible = false;
            //
            // pCod
            //
            this.pCod.Description = "pCod";
            this.pCod.Name = "pCod";
            this.pCod.Visible = false;
            //
            // objectDataSource1
            //
            this.objectDataSource1.DataSource = typeof(global::Sgpa.Web.Reporting.Predefinidos.EmpresaAporteLinea);
            this.objectDataSource1.Name = "objectDataSource1";
            //
            // EmpresaAportesReport
            //
            this.Bands.AddRange(new DevExpress.XtraReports.UI.Band[] {
                this.TopMargin, this.ReportHeader, this.Detail, this.ReportFooter, this.BottomMargin});
            this.ComponentStorage.AddRange(new System.ComponentModel.IComponent[] { this.objectDataSource1 });
            this.DataSource = this.objectDataSource1;
            this.Font = new DevExpress.Drawing.DXFont("Segoe UI", 9.5F);
            this.Margins = new DevExpress.Drawing.DXMargins(40F, 40F, 40F, 40F);
            this.PaperKind = DevExpress.Drawing.Printing.DXPaperKind.A4;
            this.Parameters.AddRange(new DevExpress.XtraReports.Parameters.Parameter[] { this.pNombre, this.pCod });
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
        private DevExpress.XtraReports.UI.XRLabel xrCodSub;
        private DevExpress.XtraReports.UI.XRLabel hPeriodo;
        private DevExpress.XtraReports.UI.XRLabel hAfiliados;
        private DevExpress.XtraReports.UI.XRLabel hAporte;
        private DevExpress.XtraReports.UI.XRLabel hAguinaldo;
        private DevExpress.XtraReports.UI.XRLabel hTotal;
        private DevExpress.XtraReports.UI.XRLine ruleHead;
        private DevExpress.XtraReports.UI.XRLabel dPeriodo;
        private DevExpress.XtraReports.UI.XRLabel dAfiliados;
        private DevExpress.XtraReports.UI.XRLabel dAporte;
        private DevExpress.XtraReports.UI.XRLabel dAguinaldo;
        private DevExpress.XtraReports.UI.XRLabel dTotal;
        private DevExpress.XtraReports.UI.XRLine ruleFoot;
        private DevExpress.XtraReports.UI.XRLabel xrTotalLbl;
        private DevExpress.XtraReports.UI.XRLabel xrTotalImp;
        private DevExpress.XtraReports.Parameters.Parameter pNombre;
        private DevExpress.XtraReports.Parameters.Parameter pCod;
        private DevExpress.DataAccess.ObjectBinding.ObjectDataSource objectDataSource1;
    }
}
