namespace Sgpa.Web.Reports.Certificaciones
{
    partial class CertificacionFichaReport
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
            this.GroupHeader1 = new DevExpress.XtraReports.UI.GroupHeaderBand();
            this.Detail = new DevExpress.XtraReports.UI.DetailBand();
            this.GroupFooter1 = new DevExpress.XtraReports.UI.GroupFooterBand();
            this.xrLogo = new DevExpress.XtraReports.UI.XRPictureBox();
            this.xrTitulo = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLinea = new DevExpress.XtraReports.UI.XRLine();
            this.gNombre = new DevExpress.XtraReports.UI.XRLabel();
            this.capCI = new DevExpress.XtraReports.UI.XRLabel();
            this.valCI = new DevExpress.XtraReports.UI.XRLabel();
            this.capFNac = new DevExpress.XtraReports.UI.XRLabel();
            this.valFNac = new DevExpress.XtraReports.UI.XRLabel();
            this.capMut = new DevExpress.XtraReports.UI.XRLabel();
            this.valMut = new DevExpress.XtraReports.UI.XRLabel();
            this.hAfec = new DevExpress.XtraReports.UI.XRLabel();
            this.hCant = new DevExpress.XtraReports.UI.XRLabel();
            this.hDias = new DevExpress.XtraReports.UI.XRLabel();
            this.ruleG = new DevExpress.XtraReports.UI.XRLine();
            this.dAfec = new DevExpress.XtraReports.UI.XRLabel();
            this.dCant = new DevExpress.XtraReports.UI.XRLabel();
            this.dDias = new DevExpress.XtraReports.UI.XRLabel();
            this.ruleFoot = new DevExpress.XtraReports.UI.XRLine();
            this.gTotalDias = new DevExpress.XtraReports.UI.XRLabel();
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
            // GroupHeader1 (cabecera + ficha del afiliado; una por página)
            //
            this.GroupHeader1.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
                this.xrLogo, this.xrTitulo, this.xrLinea, this.gNombre, this.capCI, this.valCI,
                this.capFNac, this.valFNac, this.capMut, this.valMut, this.hAfec, this.hCant, this.hDias, this.ruleG});
            this.GroupHeader1.Dpi = 96F;
            this.GroupHeader1.GroupFields.AddRange(new DevExpress.XtraReports.UI.GroupField[] {
                new DevExpress.XtraReports.UI.GroupField("CI", DevExpress.XtraReports.UI.XRColumnSortOrder.Ascending)});
            this.GroupHeader1.HeightF = 156F;
            this.GroupHeader1.Name = "GroupHeader1";
            this.GroupHeader1.PageBreak = DevExpress.XtraReports.UI.PageBreak.BeforeBand;
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
            this.GroupFooter1.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
                this.ruleFoot, this.gTotalDias});
            this.GroupFooter1.Dpi = 96F;
            this.GroupFooter1.HeightF = 30F;
            this.GroupFooter1.Name = "GroupFooter1";
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
            this.xrTitulo.Text = "Ficha de certificación";
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
            // gNombre
            //
            this.gNombre.Dpi = 96F;
            this.gNombre.ExpressionBindings.AddRange(new DevExpress.XtraReports.UI.ExpressionBinding[] {
                new DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[NombreTexto]")});
            this.gNombre.Font = new DevExpress.Drawing.DXFont("Segoe UI", 13F, DevExpress.Drawing.DXFontStyle.Bold);
            this.gNombre.ForeColor = System.Drawing.Color.FromArgb(28, 54, 128);
            this.gNombre.LocationFloat = new DevExpress.Utils.PointFloat(0F, 58F);
            this.gNombre.Name = "gNombre";
            this.gNombre.SizeF = new System.Drawing.SizeF(714F, 22F);
            //
            // capCI / valCI
            //
            this.capCI.Dpi = 96F;
            this.capCI.Font = new DevExpress.Drawing.DXFont("Segoe UI", 8.5F, DevExpress.Drawing.DXFontStyle.Bold);
            this.capCI.ForeColor = System.Drawing.Color.FromArgb(102, 102, 102);
            this.capCI.LocationFloat = new DevExpress.Utils.PointFloat(0F, 86F);
            this.capCI.Name = "capCI";
            this.capCI.SizeF = new System.Drawing.SizeF(70F, 18F);
            this.capCI.Text = "C.I.";
            this.valCI.Dpi = 96F;
            this.valCI.ExpressionBindings.AddRange(new DevExpress.XtraReports.UI.ExpressionBinding[] {
                new DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[CIFormato]")});
            this.valCI.LocationFloat = new DevExpress.Utils.PointFloat(74F, 86F);
            this.valCI.Name = "valCI";
            this.valCI.SizeF = new System.Drawing.SizeF(220F, 18F);
            //
            // capFNac / valFNac
            //
            this.capFNac.Dpi = 96F;
            this.capFNac.Font = new DevExpress.Drawing.DXFont("Segoe UI", 8.5F, DevExpress.Drawing.DXFontStyle.Bold);
            this.capFNac.ForeColor = System.Drawing.Color.FromArgb(102, 102, 102);
            this.capFNac.LocationFloat = new DevExpress.Utils.PointFloat(310F, 86F);
            this.capFNac.Name = "capFNac";
            this.capFNac.SizeF = new System.Drawing.SizeF(70F, 18F);
            this.capFNac.Text = "F. Nac.";
            this.valFNac.Dpi = 96F;
            this.valFNac.ExpressionBindings.AddRange(new DevExpress.XtraReports.UI.ExpressionBinding[] {
                new DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[FNacFmt]")});
            this.valFNac.LocationFloat = new DevExpress.Utils.PointFloat(384F, 86F);
            this.valFNac.Name = "valFNac";
            this.valFNac.SizeF = new System.Drawing.SizeF(160F, 18F);
            //
            // capMut / valMut
            //
            this.capMut.Dpi = 96F;
            this.capMut.Font = new DevExpress.Drawing.DXFont("Segoe UI", 8.5F, DevExpress.Drawing.DXFontStyle.Bold);
            this.capMut.ForeColor = System.Drawing.Color.FromArgb(102, 102, 102);
            this.capMut.LocationFloat = new DevExpress.Utils.PointFloat(0F, 108F);
            this.capMut.Name = "capMut";
            this.capMut.SizeF = new System.Drawing.SizeF(70F, 18F);
            this.capMut.Text = "Mutualista";
            this.valMut.Dpi = 96F;
            this.valMut.ExpressionBindings.AddRange(new DevExpress.XtraReports.UI.ExpressionBinding[] {
                new DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[MutualistaTexto]")});
            this.valMut.LocationFloat = new DevExpress.Utils.PointFloat(74F, 108F);
            this.valMut.Name = "valMut";
            this.valMut.SizeF = new System.Drawing.SizeF(470F, 18F);
            //
            // hAfec / hCant / hDias (cabecera de tabla)
            //
            this.hAfec.Dpi = 96F;
            this.hAfec.Font = new DevExpress.Drawing.DXFont("Segoe UI", 9F, DevExpress.Drawing.DXFontStyle.Bold);
            this.hAfec.ForeColor = System.Drawing.Color.FromArgb(102, 102, 102);
            this.hAfec.LocationFloat = new DevExpress.Utils.PointFloat(0F, 136F);
            this.hAfec.Name = "hAfec";
            this.hAfec.SizeF = new System.Drawing.SizeF(450F, 18F);
            this.hAfec.Text = "Tipo de afección";
            this.hCant.Dpi = 96F;
            this.hCant.Font = new DevExpress.Drawing.DXFont("Segoe UI", 9F, DevExpress.Drawing.DXFontStyle.Bold);
            this.hCant.ForeColor = System.Drawing.Color.FromArgb(102, 102, 102);
            this.hCant.LocationFloat = new DevExpress.Utils.PointFloat(450F, 136F);
            this.hCant.Name = "hCant";
            this.hCant.SizeF = new System.Drawing.SizeF(120F, 18F);
            this.hCant.Text = "Cantidad";
            this.hCant.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
            this.hDias.Dpi = 96F;
            this.hDias.Font = new DevExpress.Drawing.DXFont("Segoe UI", 9F, DevExpress.Drawing.DXFontStyle.Bold);
            this.hDias.ForeColor = System.Drawing.Color.FromArgb(102, 102, 102);
            this.hDias.LocationFloat = new DevExpress.Utils.PointFloat(570F, 136F);
            this.hDias.Name = "hDias";
            this.hDias.SizeF = new System.Drawing.SizeF(144F, 18F);
            this.hDias.Text = "Días";
            this.hDias.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
            //
            // ruleG
            //
            this.ruleG.Dpi = 96F;
            this.ruleG.ForeColor = System.Drawing.Color.FromArgb(28, 54, 128);
            this.ruleG.LocationFloat = new DevExpress.Utils.PointFloat(0F, 154F);
            this.ruleG.Name = "ruleG";
            this.ruleG.SizeF = new System.Drawing.SizeF(714F, 1F);
            //
            // dAfec / dCant / dDias
            //
            this.dAfec.Borders = DevExpress.XtraPrinting.BorderSide.Bottom;
            this.dAfec.BorderColor = System.Drawing.Color.FromArgb(225, 228, 240);
            this.dAfec.Dpi = 96F;
            this.dAfec.ExpressionBindings.AddRange(new DevExpress.XtraReports.UI.ExpressionBinding[] {
                new DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[AfeccionTexto]")});
            this.dAfec.LocationFloat = new DevExpress.Utils.PointFloat(0F, 1F);
            this.dAfec.Name = "dAfec";
            this.dAfec.SizeF = new System.Drawing.SizeF(450F, 18F);
            this.dCant.Borders = DevExpress.XtraPrinting.BorderSide.Bottom;
            this.dCant.BorderColor = System.Drawing.Color.FromArgb(225, 228, 240);
            this.dCant.Dpi = 96F;
            this.dCant.ExpressionBindings.AddRange(new DevExpress.XtraReports.UI.ExpressionBinding[] {
                new DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[Cantidad]")});
            this.dCant.LocationFloat = new DevExpress.Utils.PointFloat(450F, 1F);
            this.dCant.Name = "dCant";
            this.dCant.SizeF = new System.Drawing.SizeF(120F, 18F);
            this.dCant.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
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
            // ruleFoot
            //
            this.ruleFoot.Dpi = 96F;
            this.ruleFoot.ForeColor = System.Drawing.Color.FromArgb(28, 54, 128);
            this.ruleFoot.LocationFloat = new DevExpress.Utils.PointFloat(0F, 4F);
            this.ruleFoot.Name = "ruleFoot";
            this.ruleFoot.SizeF = new System.Drawing.SizeF(714F, 1F);
            //
            // gTotalDias
            //
            this.gTotalDias.Dpi = 96F;
            this.gTotalDias.ExpressionBindings.AddRange(new DevExpress.XtraReports.UI.ExpressionBinding[] {
                new DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "'Total de días: ' + sumSum([Dias])")});
            this.gTotalDias.Font = new DevExpress.Drawing.DXFont("Segoe UI", 9.5F, DevExpress.Drawing.DXFontStyle.Bold);
            this.gTotalDias.ForeColor = System.Drawing.Color.FromArgb(28, 54, 128);
            this.gTotalDias.LocationFloat = new DevExpress.Utils.PointFloat(0F, 10F);
            this.gTotalDias.Name = "gTotalDias";
            this.gTotalDias.SizeF = new System.Drawing.SizeF(714F, 16F);
            this.gTotalDias.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
            //
            // objectDataSource1
            //
            this.objectDataSource1.DataSource = typeof(global::Sgpa.Web.Reporting.Predefinidos.CertificacionAfeccionLinea);
            this.objectDataSource1.Name = "objectDataSource1";
            //
            // CertificacionFichaReport
            //
            this.Bands.AddRange(new DevExpress.XtraReports.UI.Band[] {
                this.TopMargin, this.GroupHeader1, this.Detail, this.GroupFooter1, this.BottomMargin});
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
        private DevExpress.XtraReports.UI.GroupHeaderBand GroupHeader1;
        private DevExpress.XtraReports.UI.DetailBand Detail;
        private DevExpress.XtraReports.UI.GroupFooterBand GroupFooter1;
        internal DevExpress.XtraReports.UI.XRPictureBox xrLogo;
        private DevExpress.XtraReports.UI.XRLabel xrTitulo;
        private DevExpress.XtraReports.UI.XRLine xrLinea;
        private DevExpress.XtraReports.UI.XRLabel gNombre;
        private DevExpress.XtraReports.UI.XRLabel capCI;
        private DevExpress.XtraReports.UI.XRLabel valCI;
        private DevExpress.XtraReports.UI.XRLabel capFNac;
        private DevExpress.XtraReports.UI.XRLabel valFNac;
        private DevExpress.XtraReports.UI.XRLabel capMut;
        private DevExpress.XtraReports.UI.XRLabel valMut;
        private DevExpress.XtraReports.UI.XRLabel hAfec;
        private DevExpress.XtraReports.UI.XRLabel hCant;
        private DevExpress.XtraReports.UI.XRLabel hDias;
        private DevExpress.XtraReports.UI.XRLine ruleG;
        private DevExpress.XtraReports.UI.XRLabel dAfec;
        private DevExpress.XtraReports.UI.XRLabel dCant;
        private DevExpress.XtraReports.UI.XRLabel dDias;
        private DevExpress.XtraReports.UI.XRLine ruleFoot;
        private DevExpress.XtraReports.UI.XRLabel gTotalDias;
        private DevExpress.DataAccess.ObjectBinding.ObjectDataSource objectDataSource1;
    }
}
