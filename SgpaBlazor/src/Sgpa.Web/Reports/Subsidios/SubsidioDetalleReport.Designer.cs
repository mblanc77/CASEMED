namespace Sgpa.Web.Reports.Subsidios
{
    partial class SubsidioDetalleReport
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
            this.hEmp = new DevExpress.XtraReports.UI.XRLabel();
            this.hMes = new DevExpress.XtraReports.UI.XRLabel();
            this.hAnio = new DevExpress.XtraReports.UI.XRLabel();
            this.hDias = new DevExpress.XtraReports.UI.XRLabel();
            this.hImp = new DevExpress.XtraReports.UI.XRLabel();
            this.ruleG = new DevExpress.XtraReports.UI.XRLine();
            this.dEmp = new DevExpress.XtraReports.UI.XRLabel();
            this.dMes = new DevExpress.XtraReports.UI.XRLabel();
            this.dAnio = new DevExpress.XtraReports.UI.XRLabel();
            this.dDias = new DevExpress.XtraReports.UI.XRLabel();
            this.dImp = new DevExpress.XtraReports.UI.XRLabel();
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
                this.gAfiliado, this.hEmp, this.hMes, this.hAnio, this.hDias, this.hImp, this.ruleG});
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
                this.dEmp, this.dMes, this.dAnio, this.dDias, this.dImp});
            this.Detail.Dpi = 96F;
            this.Detail.HeightF = 18F;
            this.Detail.Name = "Detail";
            //
            // GroupFooter1
            //
            this.GroupFooter1.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] { this.gSubtotal });
            this.GroupFooter1.Dpi = 96F;
            this.GroupFooter1.HeightF = 24F;
            this.GroupFooter1.Name = "GroupFooter1";
            //
            // ReportFooter
            //
            this.ReportFooter.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] { this.ruleFoot, this.xrTotal });
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
            this.xrTitulo.Text = "Detalle de liquidación";
            this.xrTitulo.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter;
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
                new DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[AfiliadoEncabezado]")});
            this.gAfiliado.Font = new DevExpress.Drawing.DXFont("Segoe UI", 11F, DevExpress.Drawing.DXFontStyle.Bold);
            this.gAfiliado.ForeColor = System.Drawing.Color.FromArgb(28, 54, 128);
            this.gAfiliado.LocationFloat = new DevExpress.Utils.PointFloat(0F, 2F);
            this.gAfiliado.Name = "gAfiliado";
            this.gAfiliado.SizeF = new System.Drawing.SizeF(714F, 20F);
            //
            // Cabeceras (Y=26)
            //
            this.hEmp.Dpi = 96F;
            this.hEmp.Font = new DevExpress.Drawing.DXFont("Segoe UI", 9F, DevExpress.Drawing.DXFontStyle.Bold);
            this.hEmp.ForeColor = System.Drawing.Color.FromArgb(102, 102, 102);
            this.hEmp.LocationFloat = new DevExpress.Utils.PointFloat(0F, 26F);
            this.hEmp.Name = "hEmp";
            this.hEmp.SizeF = new System.Drawing.SizeF(320F, 18F);
            this.hEmp.Text = "Empresa";
            this.hMes.Dpi = 96F;
            this.hMes.Font = new DevExpress.Drawing.DXFont("Segoe UI", 9F, DevExpress.Drawing.DXFontStyle.Bold);
            this.hMes.ForeColor = System.Drawing.Color.FromArgb(102, 102, 102);
            this.hMes.LocationFloat = new DevExpress.Utils.PointFloat(320F, 26F);
            this.hMes.Name = "hMes";
            this.hMes.SizeF = new System.Drawing.SizeF(80F, 18F);
            this.hMes.Text = "Mes";
            this.hMes.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
            this.hAnio.Dpi = 96F;
            this.hAnio.Font = new DevExpress.Drawing.DXFont("Segoe UI", 9F, DevExpress.Drawing.DXFontStyle.Bold);
            this.hAnio.ForeColor = System.Drawing.Color.FromArgb(102, 102, 102);
            this.hAnio.LocationFloat = new DevExpress.Utils.PointFloat(400F, 26F);
            this.hAnio.Name = "hAnio";
            this.hAnio.SizeF = new System.Drawing.SizeF(80F, 18F);
            this.hAnio.Text = "Año";
            this.hAnio.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
            this.hDias.Dpi = 96F;
            this.hDias.Font = new DevExpress.Drawing.DXFont("Segoe UI", 9F, DevExpress.Drawing.DXFontStyle.Bold);
            this.hDias.ForeColor = System.Drawing.Color.FromArgb(102, 102, 102);
            this.hDias.LocationFloat = new DevExpress.Utils.PointFloat(480F, 26F);
            this.hDias.Name = "hDias";
            this.hDias.SizeF = new System.Drawing.SizeF(90F, 18F);
            this.hDias.Text = "Días";
            this.hDias.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
            this.hImp.Dpi = 96F;
            this.hImp.Font = new DevExpress.Drawing.DXFont("Segoe UI", 9F, DevExpress.Drawing.DXFontStyle.Bold);
            this.hImp.ForeColor = System.Drawing.Color.FromArgb(102, 102, 102);
            this.hImp.LocationFloat = new DevExpress.Utils.PointFloat(570F, 26F);
            this.hImp.Name = "hImp";
            this.hImp.SizeF = new System.Drawing.SizeF(144F, 18F);
            this.hImp.Text = "Importe";
            this.hImp.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
            //
            // ruleG
            //
            this.ruleG.Dpi = 96F;
            this.ruleG.ForeColor = System.Drawing.Color.FromArgb(28, 54, 128);
            this.ruleG.LocationFloat = new DevExpress.Utils.PointFloat(0F, 44F);
            this.ruleG.Name = "ruleG";
            this.ruleG.SizeF = new System.Drawing.SizeF(714F, 1F);
            //
            // Detalle
            //
            this.dEmp.Borders = DevExpress.XtraPrinting.BorderSide.Bottom;
            this.dEmp.BorderColor = System.Drawing.Color.FromArgb(225, 228, 240);
            this.dEmp.Dpi = 96F;
            this.dEmp.ExpressionBindings.AddRange(new DevExpress.XtraReports.UI.ExpressionBinding[] {
                new DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[EmpresaTexto]")});
            this.dEmp.LocationFloat = new DevExpress.Utils.PointFloat(0F, 0F);
            this.dEmp.Name = "dEmp";
            this.dEmp.SizeF = new System.Drawing.SizeF(320F, 18F);
            this.dMes.Borders = DevExpress.XtraPrinting.BorderSide.Bottom;
            this.dMes.BorderColor = System.Drawing.Color.FromArgb(225, 228, 240);
            this.dMes.Dpi = 96F;
            this.dMes.ExpressionBindings.AddRange(new DevExpress.XtraReports.UI.ExpressionBinding[] {
                new DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[Mes]")});
            this.dMes.LocationFloat = new DevExpress.Utils.PointFloat(320F, 0F);
            this.dMes.Name = "dMes";
            this.dMes.SizeF = new System.Drawing.SizeF(80F, 18F);
            this.dMes.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
            this.dAnio.Borders = DevExpress.XtraPrinting.BorderSide.Bottom;
            this.dAnio.BorderColor = System.Drawing.Color.FromArgb(225, 228, 240);
            this.dAnio.Dpi = 96F;
            this.dAnio.ExpressionBindings.AddRange(new DevExpress.XtraReports.UI.ExpressionBinding[] {
                new DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[Anio]")});
            this.dAnio.LocationFloat = new DevExpress.Utils.PointFloat(400F, 0F);
            this.dAnio.Name = "dAnio";
            this.dAnio.SizeF = new System.Drawing.SizeF(80F, 18F);
            this.dAnio.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
            this.dDias.Borders = DevExpress.XtraPrinting.BorderSide.Bottom;
            this.dDias.BorderColor = System.Drawing.Color.FromArgb(225, 228, 240);
            this.dDias.Dpi = 96F;
            this.dDias.ExpressionBindings.AddRange(new DevExpress.XtraReports.UI.ExpressionBinding[] {
                new DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[Dias]")});
            this.dDias.LocationFloat = new DevExpress.Utils.PointFloat(480F, 0F);
            this.dDias.Name = "dDias";
            this.dDias.SizeF = new System.Drawing.SizeF(90F, 18F);
            this.dDias.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
            this.dImp.Borders = DevExpress.XtraPrinting.BorderSide.Bottom;
            this.dImp.BorderColor = System.Drawing.Color.FromArgb(225, 228, 240);
            this.dImp.Dpi = 96F;
            this.dImp.ExpressionBindings.AddRange(new DevExpress.XtraReports.UI.ExpressionBinding[] {
                new DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[ImporteFmt]")});
            this.dImp.LocationFloat = new DevExpress.Utils.PointFloat(570F, 0F);
            this.dImp.Name = "dImp";
            this.dImp.SizeF = new System.Drawing.SizeF(144F, 18F);
            this.dImp.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
            //
            // gSubtotal
            //
            this.gSubtotal.Dpi = 96F;
            this.gSubtotal.ExpressionBindings.AddRange(new DevExpress.XtraReports.UI.ExpressionBinding[] {
                new DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "'Subtotal $ ' + FormatString('{0:N2}', sumSum([Importe]))")});
            this.gSubtotal.Font = new DevExpress.Drawing.DXFont("Segoe UI", 8.5F, DevExpress.Drawing.DXFontStyle.Bold);
            this.gSubtotal.ForeColor = System.Drawing.Color.FromArgb(102, 102, 102);
            this.gSubtotal.LocationFloat = new DevExpress.Utils.PointFloat(0F, 4F);
            this.gSubtotal.Name = "gSubtotal";
            this.gSubtotal.SizeF = new System.Drawing.SizeF(714F, 16F);
            this.gSubtotal.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
            //
            // ruleFoot / xrTotal
            //
            this.ruleFoot.Dpi = 96F;
            this.ruleFoot.ForeColor = System.Drawing.Color.FromArgb(28, 54, 128);
            this.ruleFoot.LocationFloat = new DevExpress.Utils.PointFloat(0F, 6F);
            this.ruleFoot.Name = "ruleFoot";
            this.ruleFoot.SizeF = new System.Drawing.SizeF(714F, 1F);
            this.xrTotal.Dpi = 96F;
            this.xrTotal.ExpressionBindings.AddRange(new DevExpress.XtraReports.UI.ExpressionBinding[] {
                new DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "'Total general $ ' + FormatString('{0:N2}', sumSum([Importe]))")});
            this.xrTotal.Font = new DevExpress.Drawing.DXFont("Segoe UI", 9F, DevExpress.Drawing.DXFontStyle.Bold);
            this.xrTotal.ForeColor = System.Drawing.Color.FromArgb(28, 54, 128);
            this.xrTotal.LocationFloat = new DevExpress.Utils.PointFloat(0F, 12F);
            this.xrTotal.Name = "xrTotal";
            this.xrTotal.SizeF = new System.Drawing.SizeF(714F, 16F);
            this.xrTotal.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
            //
            // objectDataSource1
            //
            this.objectDataSource1.DataSource = typeof(global::Sgpa.Web.Reporting.Predefinidos.SubsidioDetalleLinea);
            this.objectDataSource1.Name = "objectDataSource1";
            //
            // SubsidioDetalleReport
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
        private DevExpress.XtraReports.UI.XRLabel hEmp;
        private DevExpress.XtraReports.UI.XRLabel hMes;
        private DevExpress.XtraReports.UI.XRLabel hAnio;
        private DevExpress.XtraReports.UI.XRLabel hDias;
        private DevExpress.XtraReports.UI.XRLabel hImp;
        private DevExpress.XtraReports.UI.XRLine ruleG;
        private DevExpress.XtraReports.UI.XRLabel dEmp;
        private DevExpress.XtraReports.UI.XRLabel dMes;
        private DevExpress.XtraReports.UI.XRLabel dAnio;
        private DevExpress.XtraReports.UI.XRLabel dDias;
        private DevExpress.XtraReports.UI.XRLabel dImp;
        private DevExpress.XtraReports.UI.XRLabel gSubtotal;
        private DevExpress.XtraReports.UI.XRLine ruleFoot;
        private DevExpress.XtraReports.UI.XRLabel xrTotal;
        private DevExpress.DataAccess.ObjectBinding.ObjectDataSource objectDataSource1;
    }
}
