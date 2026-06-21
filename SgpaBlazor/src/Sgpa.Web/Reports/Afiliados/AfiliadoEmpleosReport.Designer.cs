namespace Sgpa.Web.Reports.Afiliados
{
    partial class AfiliadoEmpleosReport
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
            this.hCodEmp = new DevExpress.XtraReports.UI.XRLabel();
            this.hEmpresa = new DevExpress.XtraReports.UI.XRLabel();
            this.hIngreso = new DevExpress.XtraReports.UI.XRLabel();
            this.hCasemed = new DevExpress.XtraReports.UI.XRLabel();
            this.hBaja = new DevExpress.XtraReports.UI.XRLabel();
            this.hEstado = new DevExpress.XtraReports.UI.XRLabel();
            this.ruleHead = new DevExpress.XtraReports.UI.XRLine();
            this.dCodEmp = new DevExpress.XtraReports.UI.XRLabel();
            this.dEmpresa = new DevExpress.XtraReports.UI.XRLabel();
            this.dIngreso = new DevExpress.XtraReports.UI.XRLabel();
            this.dCasemed = new DevExpress.XtraReports.UI.XRLabel();
            this.dBaja = new DevExpress.XtraReports.UI.XRLabel();
            this.dEstado = new DevExpress.XtraReports.UI.XRLabel();
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
                this.hCodEmp, this.hEmpresa, this.hIngreso, this.hCasemed, this.hBaja, this.hEstado, this.ruleHead});
            this.ReportHeader.Dpi = 96F;
            this.ReportHeader.HeightF = 140F;
            this.ReportHeader.Name = "ReportHeader";
            //
            // Detail
            //
            this.Detail.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
                this.dCodEmp, this.dEmpresa, this.dIngreso, this.dCasemed, this.dBaja, this.dEstado});
            this.Detail.Dpi = 96F;
            this.Detail.HeightF = 24F;
            this.Detail.Name = "Detail";
            //
            // ReportFooter
            //
            this.ReportFooter.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
                this.ruleFoot, this.xrTotal});
            this.ReportFooter.Dpi = 96F;
            this.ReportFooter.HeightF = 44F;
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
            this.xrTitulo.Text = "Empleos del afiliado";
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
            // hCodEmp
            //
            this.hCodEmp.Dpi = 96F;
            this.hCodEmp.Font = new DevExpress.Drawing.DXFont("Segoe UI", 9F, DevExpress.Drawing.DXFontStyle.Bold);
            this.hCodEmp.ForeColor = System.Drawing.Color.FromArgb(102, 102, 102);
            this.hCodEmp.LocationFloat = new DevExpress.Utils.PointFloat(0F, 116F);
            this.hCodEmp.Name = "hCodEmp";
            this.hCodEmp.SizeF = new System.Drawing.SizeF(50F, 18F);
            this.hCodEmp.Text = "Cód.";
            //
            // hEmpresa
            //
            this.hEmpresa.Dpi = 96F;
            this.hEmpresa.Font = new DevExpress.Drawing.DXFont("Segoe UI", 9F, DevExpress.Drawing.DXFontStyle.Bold);
            this.hEmpresa.ForeColor = System.Drawing.Color.FromArgb(102, 102, 102);
            this.hEmpresa.LocationFloat = new DevExpress.Utils.PointFloat(54F, 116F);
            this.hEmpresa.Name = "hEmpresa";
            this.hEmpresa.SizeF = new System.Drawing.SizeF(236F, 18F);
            this.hEmpresa.Text = "Empresa";
            //
            // hIngreso
            //
            this.hIngreso.Dpi = 96F;
            this.hIngreso.Font = new DevExpress.Drawing.DXFont("Segoe UI", 9F, DevExpress.Drawing.DXFontStyle.Bold);
            this.hIngreso.ForeColor = System.Drawing.Color.FromArgb(102, 102, 102);
            this.hIngreso.LocationFloat = new DevExpress.Utils.PointFloat(290F, 116F);
            this.hIngreso.Name = "hIngreso";
            this.hIngreso.SizeF = new System.Drawing.SizeF(96F, 18F);
            this.hIngreso.Text = "Ingreso";
            //
            // hCasemed
            //
            this.hCasemed.Dpi = 96F;
            this.hCasemed.Font = new DevExpress.Drawing.DXFont("Segoe UI", 9F, DevExpress.Drawing.DXFontStyle.Bold);
            this.hCasemed.ForeColor = System.Drawing.Color.FromArgb(102, 102, 102);
            this.hCasemed.LocationFloat = new DevExpress.Utils.PointFloat(386F, 116F);
            this.hCasemed.Name = "hCasemed";
            this.hCasemed.SizeF = new System.Drawing.SizeF(116F, 18F);
            this.hCasemed.Text = "Ing. CASEMED";
            //
            // hBaja
            //
            this.hBaja.Dpi = 96F;
            this.hBaja.Font = new DevExpress.Drawing.DXFont("Segoe UI", 9F, DevExpress.Drawing.DXFontStyle.Bold);
            this.hBaja.ForeColor = System.Drawing.Color.FromArgb(102, 102, 102);
            this.hBaja.LocationFloat = new DevExpress.Utils.PointFloat(502F, 116F);
            this.hBaja.Name = "hBaja";
            this.hBaja.SizeF = new System.Drawing.SizeF(96F, 18F);
            this.hBaja.Text = "Baja";
            //
            // hEstado
            //
            this.hEstado.Dpi = 96F;
            this.hEstado.Font = new DevExpress.Drawing.DXFont("Segoe UI", 9F, DevExpress.Drawing.DXFontStyle.Bold);
            this.hEstado.ForeColor = System.Drawing.Color.FromArgb(102, 102, 102);
            this.hEstado.LocationFloat = new DevExpress.Utils.PointFloat(598F, 116F);
            this.hEstado.Name = "hEstado";
            this.hEstado.SizeF = new System.Drawing.SizeF(116F, 18F);
            this.hEstado.Text = "Estado";
            //
            // ruleHead
            //
            this.ruleHead.Dpi = 96F;
            this.ruleHead.ForeColor = System.Drawing.Color.FromArgb(28, 54, 128);
            this.ruleHead.LocationFloat = new DevExpress.Utils.PointFloat(0F, 136F);
            this.ruleHead.Name = "ruleHead";
            this.ruleHead.SizeF = new System.Drawing.SizeF(714F, 1F);
            //
            // dCodEmp
            //
            this.dCodEmp.Borders = DevExpress.XtraPrinting.BorderSide.Bottom;
            this.dCodEmp.BorderColor = System.Drawing.Color.FromArgb(225, 228, 240);
            this.dCodEmp.Dpi = 96F;
            this.dCodEmp.ExpressionBindings.AddRange(new DevExpress.XtraReports.UI.ExpressionBinding[] {
                new DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[CodEmpresa]")});
            this.dCodEmp.LocationFloat = new DevExpress.Utils.PointFloat(0F, 2F);
            this.dCodEmp.Name = "dCodEmp";
            this.dCodEmp.SizeF = new System.Drawing.SizeF(50F, 20F);
            //
            // dEmpresa
            //
            this.dEmpresa.Borders = DevExpress.XtraPrinting.BorderSide.Bottom;
            this.dEmpresa.BorderColor = System.Drawing.Color.FromArgb(225, 228, 240);
            this.dEmpresa.Dpi = 96F;
            this.dEmpresa.ExpressionBindings.AddRange(new DevExpress.XtraReports.UI.ExpressionBinding[] {
                new DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[DescEmpresa]")});
            this.dEmpresa.LocationFloat = new DevExpress.Utils.PointFloat(54F, 2F);
            this.dEmpresa.Name = "dEmpresa";
            this.dEmpresa.SizeF = new System.Drawing.SizeF(236F, 20F);
            //
            // dIngreso
            //
            this.dIngreso.Borders = DevExpress.XtraPrinting.BorderSide.Bottom;
            this.dIngreso.BorderColor = System.Drawing.Color.FromArgb(225, 228, 240);
            this.dIngreso.Dpi = 96F;
            this.dIngreso.ExpressionBindings.AddRange(new DevExpress.XtraReports.UI.ExpressionBinding[] {
                new DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[FechaIngresoFmt]")});
            this.dIngreso.LocationFloat = new DevExpress.Utils.PointFloat(290F, 2F);
            this.dIngreso.Name = "dIngreso";
            this.dIngreso.SizeF = new System.Drawing.SizeF(96F, 20F);
            //
            // dCasemed
            //
            this.dCasemed.Borders = DevExpress.XtraPrinting.BorderSide.Bottom;
            this.dCasemed.BorderColor = System.Drawing.Color.FromArgb(225, 228, 240);
            this.dCasemed.Dpi = 96F;
            this.dCasemed.ExpressionBindings.AddRange(new DevExpress.XtraReports.UI.ExpressionBinding[] {
                new DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[FechaIngCasemedFmt]")});
            this.dCasemed.LocationFloat = new DevExpress.Utils.PointFloat(386F, 2F);
            this.dCasemed.Name = "dCasemed";
            this.dCasemed.SizeF = new System.Drawing.SizeF(116F, 20F);
            //
            // dBaja
            //
            this.dBaja.Borders = DevExpress.XtraPrinting.BorderSide.Bottom;
            this.dBaja.BorderColor = System.Drawing.Color.FromArgb(225, 228, 240);
            this.dBaja.Dpi = 96F;
            this.dBaja.ExpressionBindings.AddRange(new DevExpress.XtraReports.UI.ExpressionBinding[] {
                new DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[FechaBajaFmt]")});
            this.dBaja.LocationFloat = new DevExpress.Utils.PointFloat(502F, 2F);
            this.dBaja.Name = "dBaja";
            this.dBaja.SizeF = new System.Drawing.SizeF(96F, 20F);
            //
            // dEstado
            //
            this.dEstado.Borders = DevExpress.XtraPrinting.BorderSide.Bottom;
            this.dEstado.BorderColor = System.Drawing.Color.FromArgb(225, 228, 240);
            this.dEstado.Dpi = 96F;
            this.dEstado.ExpressionBindings.AddRange(new DevExpress.XtraReports.UI.ExpressionBinding[] {
                new DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[EstadoTexto]")});
            this.dEstado.LocationFloat = new DevExpress.Utils.PointFloat(598F, 2F);
            this.dEstado.Name = "dEstado";
            this.dEstado.SizeF = new System.Drawing.SizeF(116F, 20F);
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
                new DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "'Total de empleos: ' + sumCount()")});
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
            this.objectDataSource1.DataSource = typeof(global::Sgpa.Web.Reporting.Predefinidos.AfiliadoEmpleoLinea);
            this.objectDataSource1.Name = "objectDataSource1";
            //
            // AfiliadoEmpleosReport
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
        private DevExpress.XtraReports.UI.XRLabel hCodEmp;
        private DevExpress.XtraReports.UI.XRLabel hEmpresa;
        private DevExpress.XtraReports.UI.XRLabel hIngreso;
        private DevExpress.XtraReports.UI.XRLabel hCasemed;
        private DevExpress.XtraReports.UI.XRLabel hBaja;
        private DevExpress.XtraReports.UI.XRLabel hEstado;
        private DevExpress.XtraReports.UI.XRLine ruleHead;
        private DevExpress.XtraReports.UI.XRLabel dCodEmp;
        private DevExpress.XtraReports.UI.XRLabel dEmpresa;
        private DevExpress.XtraReports.UI.XRLabel dIngreso;
        private DevExpress.XtraReports.UI.XRLabel dCasemed;
        private DevExpress.XtraReports.UI.XRLabel dBaja;
        private DevExpress.XtraReports.UI.XRLabel dEstado;
        private DevExpress.XtraReports.UI.XRLine ruleFoot;
        private DevExpress.XtraReports.UI.XRLabel xrTotal;
        private DevExpress.XtraReports.Parameters.Parameter pNombre;
        private DevExpress.XtraReports.Parameters.Parameter pCI;
        private DevExpress.DataAccess.ObjectBinding.ObjectDataSource objectDataSource1;
    }
}
