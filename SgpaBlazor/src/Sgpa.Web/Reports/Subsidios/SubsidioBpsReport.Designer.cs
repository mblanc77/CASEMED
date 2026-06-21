namespace Sgpa.Web.Reports.Subsidios
{
    partial class SubsidioBpsReport
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
            this.xrPeriodo = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLinea = new DevExpress.XtraReports.UI.XRLine();
            this.cPersonas = new DevExpress.XtraReports.UI.XRLabel();
            this.vPersonas = new DevExpress.XtraReports.UI.XRLabel();
            this.cGravado = new DevExpress.XtraReports.UI.XRLabel();
            this.vGravado = new DevExpress.XtraReports.UI.XRLabel();
            this.cTributos = new DevExpress.XtraReports.UI.XRLabel();
            this.vTributos = new DevExpress.XtraReports.UI.XRLabel();
            this.rule1 = new DevExpress.XtraReports.UI.XRLine();
            this.cObrero = new DevExpress.XtraReports.UI.XRLabel();
            this.vObrero = new DevExpress.XtraReports.UI.XRLabel();
            this.cPatronal = new DevExpress.XtraReports.UI.XRLabel();
            this.vPatronal = new DevExpress.XtraReports.UI.XRLabel();
            this.cObPat = new DevExpress.XtraReports.UI.XRLabel();
            this.vObPat = new DevExpress.XtraReports.UI.XRLabel();
            this.rule2 = new DevExpress.XtraReports.UI.XRLine();
            this.cMutual = new DevExpress.XtraReports.UI.XRLabel();
            this.vMutual = new DevExpress.XtraReports.UI.XRLabel();
            this.cTribMut = new DevExpress.XtraReports.UI.XRLabel();
            this.vTribMut = new DevExpress.XtraReports.UI.XRLabel();
            this.rule3 = new DevExpress.XtraReports.UI.XRLine();
            this.cTotTrib = new DevExpress.XtraReports.UI.XRLabel();
            this.vTotTrib = new DevExpress.XtraReports.UI.XRLabel();
            this.cTotal = new DevExpress.XtraReports.UI.XRLabel();
            this.vTotal = new DevExpress.XtraReports.UI.XRLabel();
            this.pPeriodo = new DevExpress.XtraReports.Parameters.Parameter();
            this.pPersonas = new DevExpress.XtraReports.Parameters.Parameter();
            this.pGravado = new DevExpress.XtraReports.Parameters.Parameter();
            this.pImpTributos = new DevExpress.XtraReports.Parameters.Parameter();
            this.pObrero = new DevExpress.XtraReports.Parameters.Parameter();
            this.pPatronal = new DevExpress.XtraReports.Parameters.Parameter();
            this.pObPat = new DevExpress.XtraReports.Parameters.Parameter();
            this.pMutual = new DevExpress.XtraReports.Parameters.Parameter();
            this.pTribMutual = new DevExpress.XtraReports.Parameters.Parameter();
            this.pTotTributos = new DevExpress.XtraReports.Parameters.Parameter();
            this.pTotalGeneral = new DevExpress.XtraReports.Parameters.Parameter();
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
            // Detail (vacío; el contenido va en ReportHeader, que imprime una vez sin data source)
            //
            this.Detail.Dpi = 96F;
            this.Detail.HeightF = 0F;
            this.Detail.Name = "Detail";
            //
            // ReportHeader
            //
            this.ReportHeader.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
                this.xrLogo, this.xrTitulo, this.xrPeriodo, this.xrLinea,
                this.cPersonas, this.vPersonas, this.cGravado, this.vGravado, this.cTributos, this.vTributos, this.rule1,
                this.cObrero, this.vObrero, this.cPatronal, this.vPatronal, this.cObPat, this.vObPat, this.rule2,
                this.cMutual, this.vMutual, this.cTribMut, this.vTribMut, this.rule3,
                this.cTotTrib, this.vTotTrib, this.cTotal, this.vTotal});
            this.ReportHeader.Dpi = 96F;
            this.ReportHeader.HeightF = 320F;
            this.ReportHeader.Name = "ReportHeader";
            //
            // xrLogo / xrTitulo / xrPeriodo / xrLinea
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
            this.xrTitulo.Text = "Obligación mensual (BPS)";
            this.xrTitulo.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter;
            this.xrPeriodo.Dpi = 96F;
            this.xrPeriodo.ExpressionBindings.AddRange(new DevExpress.XtraReports.UI.ExpressionBinding[] {
                new DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[Parameters.pPeriodo]")});
            this.xrPeriodo.Font = new DevExpress.Drawing.DXFont("Segoe UI", 10F);
            this.xrPeriodo.ForeColor = System.Drawing.Color.FromArgb(102, 102, 102);
            this.xrPeriodo.LocationFloat = new DevExpress.Utils.PointFloat(0F, 36F);
            this.xrPeriodo.Name = "xrPeriodo";
            this.xrPeriodo.SizeF = new System.Drawing.SizeF(714F, 18F);
            this.xrPeriodo.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter;
            this.xrLinea.Dpi = 96F;
            this.xrLinea.ForeColor = System.Drawing.Color.FromArgb(28, 54, 128);
            this.xrLinea.LocationFloat = new DevExpress.Utils.PointFloat(0F, 60F);
            this.xrLinea.Name = "xrLinea";
            this.xrLinea.SizeF = new System.Drawing.SizeF(714F, 2F);
            //
            // Fila 1: CANT. PERSONAS | MONTO GRAVADO $ | IMP. TRIBUTOS $   (cap Y=80, val Y=98)
            //
            this.cPersonas.Dpi = 96F;
            this.cPersonas.Font = new DevExpress.Drawing.DXFont("Segoe UI", 8.5F, DevExpress.Drawing.DXFontStyle.Bold);
            this.cPersonas.ForeColor = System.Drawing.Color.FromArgb(102, 102, 102);
            this.cPersonas.LocationFloat = new DevExpress.Utils.PointFloat(0F, 80F);
            this.cPersonas.Name = "cPersonas";
            this.cPersonas.SizeF = new System.Drawing.SizeF(238F, 16F);
            this.cPersonas.Text = "CANT. PERSONAS";
            this.vPersonas.Dpi = 96F;
            this.vPersonas.ExpressionBindings.AddRange(new DevExpress.XtraReports.UI.ExpressionBinding[] {
                new DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[Parameters.pPersonas]")});
            this.vPersonas.Font = new DevExpress.Drawing.DXFont("Segoe UI", 13F, DevExpress.Drawing.DXFontStyle.Bold);
            this.vPersonas.ForeColor = System.Drawing.Color.FromArgb(28, 54, 128);
            this.vPersonas.LocationFloat = new DevExpress.Utils.PointFloat(0F, 98F);
            this.vPersonas.Name = "vPersonas";
            this.vPersonas.SizeF = new System.Drawing.SizeF(224F, 24F);
            this.cGravado.Dpi = 96F;
            this.cGravado.Font = new DevExpress.Drawing.DXFont("Segoe UI", 8.5F, DevExpress.Drawing.DXFontStyle.Bold);
            this.cGravado.ForeColor = System.Drawing.Color.FromArgb(102, 102, 102);
            this.cGravado.LocationFloat = new DevExpress.Utils.PointFloat(238F, 80F);
            this.cGravado.Name = "cGravado";
            this.cGravado.SizeF = new System.Drawing.SizeF(238F, 16F);
            this.cGravado.Text = "MONTO GRAVADO $";
            this.vGravado.Dpi = 96F;
            this.vGravado.ExpressionBindings.AddRange(new DevExpress.XtraReports.UI.ExpressionBinding[] {
                new DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[Parameters.pGravado]")});
            this.vGravado.Font = new DevExpress.Drawing.DXFont("Segoe UI", 13F, DevExpress.Drawing.DXFontStyle.Bold);
            this.vGravado.ForeColor = System.Drawing.Color.FromArgb(28, 54, 128);
            this.vGravado.LocationFloat = new DevExpress.Utils.PointFloat(238F, 98F);
            this.vGravado.Name = "vGravado";
            this.vGravado.SizeF = new System.Drawing.SizeF(224F, 24F);
            this.cTributos.Dpi = 96F;
            this.cTributos.Font = new DevExpress.Drawing.DXFont("Segoe UI", 8.5F, DevExpress.Drawing.DXFontStyle.Bold);
            this.cTributos.ForeColor = System.Drawing.Color.FromArgb(102, 102, 102);
            this.cTributos.LocationFloat = new DevExpress.Utils.PointFloat(476F, 80F);
            this.cTributos.Name = "cTributos";
            this.cTributos.SizeF = new System.Drawing.SizeF(238F, 16F);
            this.cTributos.Text = "IMP. TRIBUTOS $";
            this.vTributos.Dpi = 96F;
            this.vTributos.ExpressionBindings.AddRange(new DevExpress.XtraReports.UI.ExpressionBinding[] {
                new DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[Parameters.pImpTributos]")});
            this.vTributos.Font = new DevExpress.Drawing.DXFont("Segoe UI", 13F, DevExpress.Drawing.DXFontStyle.Bold);
            this.vTributos.ForeColor = System.Drawing.Color.FromArgb(28, 54, 128);
            this.vTributos.LocationFloat = new DevExpress.Utils.PointFloat(476F, 98F);
            this.vTributos.Name = "vTributos";
            this.vTributos.SizeF = new System.Drawing.SizeF(238F, 24F);
            this.rule1.Dpi = 96F;
            this.rule1.ForeColor = System.Drawing.Color.FromArgb(210, 216, 232);
            this.rule1.LocationFloat = new DevExpress.Utils.PointFloat(0F, 128F);
            this.rule1.Name = "rule1";
            this.rule1.SizeF = new System.Drawing.SizeF(714F, 1F);
            //
            // Fila 2: OBRERO | PAT. | OB. Y PAT.   (cap Y=138, val Y=156)
            //
            this.cObrero.Dpi = 96F;
            this.cObrero.Font = new DevExpress.Drawing.DXFont("Segoe UI", 8.5F, DevExpress.Drawing.DXFontStyle.Bold);
            this.cObrero.ForeColor = System.Drawing.Color.FromArgb(102, 102, 102);
            this.cObrero.LocationFloat = new DevExpress.Utils.PointFloat(0F, 138F);
            this.cObrero.Name = "cObrero";
            this.cObrero.SizeF = new System.Drawing.SizeF(238F, 16F);
            this.cObrero.Text = "TOT. APORTE OBRERO $";
            this.vObrero.Dpi = 96F;
            this.vObrero.ExpressionBindings.AddRange(new DevExpress.XtraReports.UI.ExpressionBinding[] {
                new DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[Parameters.pObrero]")});
            this.vObrero.Font = new DevExpress.Drawing.DXFont("Segoe UI", 12F, DevExpress.Drawing.DXFontStyle.Bold);
            this.vObrero.LocationFloat = new DevExpress.Utils.PointFloat(0F, 156F);
            this.vObrero.Name = "vObrero";
            this.vObrero.SizeF = new System.Drawing.SizeF(224F, 22F);
            this.cPatronal.Dpi = 96F;
            this.cPatronal.Font = new DevExpress.Drawing.DXFont("Segoe UI", 8.5F, DevExpress.Drawing.DXFontStyle.Bold);
            this.cPatronal.ForeColor = System.Drawing.Color.FromArgb(102, 102, 102);
            this.cPatronal.LocationFloat = new DevExpress.Utils.PointFloat(238F, 138F);
            this.cPatronal.Name = "cPatronal";
            this.cPatronal.SizeF = new System.Drawing.SizeF(238F, 16F);
            this.cPatronal.Text = "TOT. APORTE PAT. $";
            this.vPatronal.Dpi = 96F;
            this.vPatronal.ExpressionBindings.AddRange(new DevExpress.XtraReports.UI.ExpressionBinding[] {
                new DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[Parameters.pPatronal]")});
            this.vPatronal.Font = new DevExpress.Drawing.DXFont("Segoe UI", 12F, DevExpress.Drawing.DXFontStyle.Bold);
            this.vPatronal.LocationFloat = new DevExpress.Utils.PointFloat(238F, 156F);
            this.vPatronal.Name = "vPatronal";
            this.vPatronal.SizeF = new System.Drawing.SizeF(224F, 22F);
            this.cObPat.Dpi = 96F;
            this.cObPat.Font = new DevExpress.Drawing.DXFont("Segoe UI", 8.5F, DevExpress.Drawing.DXFontStyle.Bold);
            this.cObPat.ForeColor = System.Drawing.Color.FromArgb(102, 102, 102);
            this.cObPat.LocationFloat = new DevExpress.Utils.PointFloat(476F, 138F);
            this.cObPat.Name = "cObPat";
            this.cObPat.SizeF = new System.Drawing.SizeF(238F, 16F);
            this.cObPat.Text = "TOT. AP. OB. Y PAT. $";
            this.vObPat.Dpi = 96F;
            this.vObPat.ExpressionBindings.AddRange(new DevExpress.XtraReports.UI.ExpressionBinding[] {
                new DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[Parameters.pObPat]")});
            this.vObPat.Font = new DevExpress.Drawing.DXFont("Segoe UI", 12F, DevExpress.Drawing.DXFontStyle.Bold);
            this.vObPat.LocationFloat = new DevExpress.Utils.PointFloat(476F, 156F);
            this.vObPat.Name = "vObPat";
            this.vObPat.SizeF = new System.Drawing.SizeF(238F, 22F);
            this.rule2.Dpi = 96F;
            this.rule2.ForeColor = System.Drawing.Color.FromArgb(210, 216, 232);
            this.rule2.LocationFloat = new DevExpress.Utils.PointFloat(0F, 184F);
            this.rule2.Name = "rule2";
            this.rule2.SizeF = new System.Drawing.SizeF(714F, 1F);
            //
            // Fila 3: APORTE MUTUAL | TRIB. MUTUAL (0,5%)   (cap Y=194, val Y=212)
            //
            this.cMutual.Dpi = 96F;
            this.cMutual.Font = new DevExpress.Drawing.DXFont("Segoe UI", 8.5F, DevExpress.Drawing.DXFontStyle.Bold);
            this.cMutual.ForeColor = System.Drawing.Color.FromArgb(102, 102, 102);
            this.cMutual.LocationFloat = new DevExpress.Utils.PointFloat(0F, 194F);
            this.cMutual.Name = "cMutual";
            this.cMutual.SizeF = new System.Drawing.SizeF(238F, 16F);
            this.cMutual.Text = "TOT. APORTE MUTUAL $";
            this.vMutual.Dpi = 96F;
            this.vMutual.ExpressionBindings.AddRange(new DevExpress.XtraReports.UI.ExpressionBinding[] {
                new DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[Parameters.pMutual]")});
            this.vMutual.Font = new DevExpress.Drawing.DXFont("Segoe UI", 12F, DevExpress.Drawing.DXFontStyle.Bold);
            this.vMutual.LocationFloat = new DevExpress.Utils.PointFloat(0F, 212F);
            this.vMutual.Name = "vMutual";
            this.vMutual.SizeF = new System.Drawing.SizeF(224F, 22F);
            this.cTribMut.Dpi = 96F;
            this.cTribMut.Font = new DevExpress.Drawing.DXFont("Segoe UI", 8.5F, DevExpress.Drawing.DXFontStyle.Bold);
            this.cTribMut.ForeColor = System.Drawing.Color.FromArgb(102, 102, 102);
            this.cTribMut.LocationFloat = new DevExpress.Utils.PointFloat(238F, 194F);
            this.cTribMut.Name = "cTribMut";
            this.cTribMut.SizeF = new System.Drawing.SizeF(238F, 16F);
            this.cTribMut.Text = "TRIB. MUTUAL (0,5%) $";
            this.vTribMut.Dpi = 96F;
            this.vTribMut.ExpressionBindings.AddRange(new DevExpress.XtraReports.UI.ExpressionBinding[] {
                new DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[Parameters.pTribMutual]")});
            this.vTribMut.Font = new DevExpress.Drawing.DXFont("Segoe UI", 12F, DevExpress.Drawing.DXFontStyle.Bold);
            this.vTribMut.LocationFloat = new DevExpress.Utils.PointFloat(238F, 212F);
            this.vTribMut.Name = "vTribMut";
            this.vTribMut.SizeF = new System.Drawing.SizeF(224F, 22F);
            this.rule3.Dpi = 96F;
            this.rule3.ForeColor = System.Drawing.Color.FromArgb(210, 216, 232);
            this.rule3.LocationFloat = new DevExpress.Utils.PointFloat(0F, 240F);
            this.rule3.Name = "rule3";
            this.rule3.SizeF = new System.Drawing.SizeF(714F, 1F);
            //
            // Fila 4: TOT. TRIBUTOS | TOTAL GENERAL   (cap Y=250, val Y=268)
            //
            this.cTotTrib.Dpi = 96F;
            this.cTotTrib.Font = new DevExpress.Drawing.DXFont("Segoe UI", 8.5F, DevExpress.Drawing.DXFontStyle.Bold);
            this.cTotTrib.ForeColor = System.Drawing.Color.FromArgb(102, 102, 102);
            this.cTotTrib.LocationFloat = new DevExpress.Utils.PointFloat(0F, 250F);
            this.cTotTrib.Name = "cTotTrib";
            this.cTotTrib.SizeF = new System.Drawing.SizeF(238F, 16F);
            this.cTotTrib.Text = "TOT. TRIBUTOS $";
            this.vTotTrib.Dpi = 96F;
            this.vTotTrib.ExpressionBindings.AddRange(new DevExpress.XtraReports.UI.ExpressionBinding[] {
                new DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[Parameters.pTotTributos]")});
            this.vTotTrib.Font = new DevExpress.Drawing.DXFont("Segoe UI", 12F, DevExpress.Drawing.DXFontStyle.Bold);
            this.vTotTrib.LocationFloat = new DevExpress.Utils.PointFloat(0F, 268F);
            this.vTotTrib.Name = "vTotTrib";
            this.vTotTrib.SizeF = new System.Drawing.SizeF(224F, 22F);
            this.cTotal.Dpi = 96F;
            this.cTotal.Font = new DevExpress.Drawing.DXFont("Segoe UI", 8.5F, DevExpress.Drawing.DXFontStyle.Bold);
            this.cTotal.ForeColor = System.Drawing.Color.FromArgb(28, 54, 128);
            this.cTotal.LocationFloat = new DevExpress.Utils.PointFloat(238F, 250F);
            this.cTotal.Name = "cTotal";
            this.cTotal.SizeF = new System.Drawing.SizeF(238F, 16F);
            this.cTotal.Text = "TOTAL GENERAL $";
            this.vTotal.Dpi = 96F;
            this.vTotal.ExpressionBindings.AddRange(new DevExpress.XtraReports.UI.ExpressionBinding[] {
                new DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[Parameters.pTotalGeneral]")});
            this.vTotal.Font = new DevExpress.Drawing.DXFont("Segoe UI", 14F, DevExpress.Drawing.DXFontStyle.Bold);
            this.vTotal.ForeColor = System.Drawing.Color.FromArgb(28, 54, 128);
            this.vTotal.LocationFloat = new DevExpress.Utils.PointFloat(238F, 268F);
            this.vTotal.Name = "vTotal";
            this.vTotal.SizeF = new System.Drawing.SizeF(238F, 26F);
            //
            // parámetros (todos string, ocultos)
            //
            this.pPeriodo.Name = "pPeriodo"; this.pPeriodo.Visible = false; this.pPeriodo.Description = "pPeriodo";
            this.pPersonas.Name = "pPersonas"; this.pPersonas.Visible = false; this.pPersonas.Description = "pPersonas";
            this.pGravado.Name = "pGravado"; this.pGravado.Visible = false; this.pGravado.Description = "pGravado";
            this.pImpTributos.Name = "pImpTributos"; this.pImpTributos.Visible = false; this.pImpTributos.Description = "pImpTributos";
            this.pObrero.Name = "pObrero"; this.pObrero.Visible = false; this.pObrero.Description = "pObrero";
            this.pPatronal.Name = "pPatronal"; this.pPatronal.Visible = false; this.pPatronal.Description = "pPatronal";
            this.pObPat.Name = "pObPat"; this.pObPat.Visible = false; this.pObPat.Description = "pObPat";
            this.pMutual.Name = "pMutual"; this.pMutual.Visible = false; this.pMutual.Description = "pMutual";
            this.pTribMutual.Name = "pTribMutual"; this.pTribMutual.Visible = false; this.pTribMutual.Description = "pTribMutual";
            this.pTotTributos.Name = "pTotTributos"; this.pTotTributos.Visible = false; this.pTotTributos.Description = "pTotTributos";
            this.pTotalGeneral.Name = "pTotalGeneral"; this.pTotalGeneral.Visible = false; this.pTotalGeneral.Description = "pTotalGeneral";
            //
            // SubsidioBpsReport
            //
            this.Bands.AddRange(new DevExpress.XtraReports.UI.Band[] {
                this.TopMargin, this.ReportHeader, this.Detail, this.BottomMargin});
            this.Font = new DevExpress.Drawing.DXFont("Segoe UI", 9.5F);
            this.Margins = new DevExpress.Drawing.DXMargins(40F, 40F, 40F, 40F);
            this.PaperKind = DevExpress.Drawing.Printing.DXPaperKind.A4;
            this.Parameters.AddRange(new DevExpress.XtraReports.Parameters.Parameter[] {
                this.pPeriodo, this.pPersonas, this.pGravado, this.pImpTributos, this.pObrero, this.pPatronal,
                this.pObPat, this.pMutual, this.pTribMutual, this.pTotTributos, this.pTotalGeneral});
            this.Version = "25.2";
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();
        }
        #endregion

        private DevExpress.XtraReports.UI.TopMarginBand TopMargin;
        private DevExpress.XtraReports.UI.BottomMarginBand BottomMargin;
        private DevExpress.XtraReports.UI.ReportHeaderBand ReportHeader;
        private DevExpress.XtraReports.UI.DetailBand Detail;
        internal DevExpress.XtraReports.UI.XRPictureBox xrLogo;
        private DevExpress.XtraReports.UI.XRLabel xrTitulo;
        private DevExpress.XtraReports.UI.XRLabel xrPeriodo;
        private DevExpress.XtraReports.UI.XRLine xrLinea;
        private DevExpress.XtraReports.UI.XRLabel cPersonas;
        private DevExpress.XtraReports.UI.XRLabel vPersonas;
        private DevExpress.XtraReports.UI.XRLabel cGravado;
        private DevExpress.XtraReports.UI.XRLabel vGravado;
        private DevExpress.XtraReports.UI.XRLabel cTributos;
        private DevExpress.XtraReports.UI.XRLabel vTributos;
        private DevExpress.XtraReports.UI.XRLine rule1;
        private DevExpress.XtraReports.UI.XRLabel cObrero;
        private DevExpress.XtraReports.UI.XRLabel vObrero;
        private DevExpress.XtraReports.UI.XRLabel cPatronal;
        private DevExpress.XtraReports.UI.XRLabel vPatronal;
        private DevExpress.XtraReports.UI.XRLabel cObPat;
        private DevExpress.XtraReports.UI.XRLabel vObPat;
        private DevExpress.XtraReports.UI.XRLine rule2;
        private DevExpress.XtraReports.UI.XRLabel cMutual;
        private DevExpress.XtraReports.UI.XRLabel vMutual;
        private DevExpress.XtraReports.UI.XRLabel cTribMut;
        private DevExpress.XtraReports.UI.XRLabel vTribMut;
        private DevExpress.XtraReports.UI.XRLine rule3;
        private DevExpress.XtraReports.UI.XRLabel cTotTrib;
        private DevExpress.XtraReports.UI.XRLabel vTotTrib;
        private DevExpress.XtraReports.UI.XRLabel cTotal;
        private DevExpress.XtraReports.UI.XRLabel vTotal;
        private DevExpress.XtraReports.Parameters.Parameter pPeriodo;
        private DevExpress.XtraReports.Parameters.Parameter pPersonas;
        private DevExpress.XtraReports.Parameters.Parameter pGravado;
        private DevExpress.XtraReports.Parameters.Parameter pImpTributos;
        private DevExpress.XtraReports.Parameters.Parameter pObrero;
        private DevExpress.XtraReports.Parameters.Parameter pPatronal;
        private DevExpress.XtraReports.Parameters.Parameter pObPat;
        private DevExpress.XtraReports.Parameters.Parameter pMutual;
        private DevExpress.XtraReports.Parameters.Parameter pTribMutual;
        private DevExpress.XtraReports.Parameters.Parameter pTotTributos;
        private DevExpress.XtraReports.Parameters.Parameter pTotalGeneral;
    }
}
