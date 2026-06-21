namespace Sgpa.Web.Reports.Subsidios
{
    partial class SubsidioReciboReport
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
            this.Detail = new DevExpress.XtraReports.UI.DetailBand();
            this.xrLogo = new DevExpress.XtraReports.UI.XRPictureBox();
            this.xrTitulo = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLinea = new DevExpress.XtraReports.UI.XRLine();
            this.xrReciboLbl = new DevExpress.XtraReports.UI.XRLabel();
            this.xrRecibo = new DevExpress.XtraReports.UI.XRLabel();
            this.xrMesLbl = new DevExpress.XtraReports.UI.XRLabel();
            this.xrMes = new DevExpress.XtraReports.UI.XRLabel();
            this.hNombre = new DevExpress.XtraReports.UI.XRLabel();
            this.hCI = new DevExpress.XtraReports.UI.XRLabel();
            this.xrNombre = new DevExpress.XtraReports.UI.XRLabel();
            this.xrCI = new DevExpress.XtraReports.UI.XRLabel();
            this.ruleB = new DevExpress.XtraReports.UI.XRLine();
            this.hDescrip = new DevExpress.XtraReports.UI.XRLabel();
            this.xrDescrip = new DevExpress.XtraReports.UI.XRLabel();
            this.ruleC = new DevExpress.XtraReports.UI.XRLine();
            this.xrImporteLbl = new DevExpress.XtraReports.UI.XRLabel();
            this.xrImporte = new DevExpress.XtraReports.UI.XRLabel();
            this.xrTransfLbl = new DevExpress.XtraReports.UI.XRLabel();
            this.xrFechaPagoLbl = new DevExpress.XtraReports.UI.XRLabel();
            this.xrFechaPago = new DevExpress.XtraReports.UI.XRLabel();
            this.xrDeposito = new DevExpress.XtraReports.UI.XRLabel();
            this.xrRuc = new DevExpress.XtraReports.UI.XRLabel();
            this.xrBps = new DevExpress.XtraReports.UI.XRLabel();
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
            // Detail (un recibo por página)
            //
            this.Detail.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
                this.xrLogo, this.xrTitulo, this.xrLinea, this.xrReciboLbl, this.xrRecibo, this.xrMesLbl, this.xrMes,
                this.hNombre, this.hCI, this.xrNombre, this.xrCI, this.ruleB, this.hDescrip, this.xrDescrip,
                this.ruleC, this.xrImporteLbl, this.xrImporte, this.xrTransfLbl, this.xrFechaPagoLbl, this.xrFechaPago,
                this.xrDeposito, this.xrRuc, this.xrBps});
            this.Detail.Dpi = 96F;
            this.Detail.HeightF = 650F;
            this.Detail.Name = "Detail";
            this.Detail.PageBreak = DevExpress.XtraReports.UI.PageBreak.AfterBand;
            //
            // xrLogo / xrTitulo / xrLinea
            //
            this.xrLogo.Dpi = 96F;
            this.xrLogo.LocationFloat = new DevExpress.Utils.PointFloat(0F, 0F);
            this.xrLogo.Name = "xrLogo";
            this.xrLogo.SizeF = new System.Drawing.SizeF(150F, 44F);
            this.xrLogo.Sizing = DevExpress.XtraPrinting.ImageSizeMode.ZoomImage;
            this.xrTitulo.Dpi = 96F;
            this.xrTitulo.Font = new DevExpress.Drawing.DXFont("Segoe UI", 14F, DevExpress.Drawing.DXFontStyle.Bold);
            this.xrTitulo.ForeColor = System.Drawing.Color.FromArgb(28, 54, 128);
            this.xrTitulo.LocationFloat = new DevExpress.Utils.PointFloat(0F, 56F);
            this.xrTitulo.Name = "xrTitulo";
            this.xrTitulo.SizeF = new System.Drawing.SizeF(714F, 24F);
            this.xrTitulo.Text = "Complemento de subsidio por enfermedad";
            this.xrTitulo.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter;
            this.xrLinea.Dpi = 96F;
            this.xrLinea.ForeColor = System.Drawing.Color.FromArgb(28, 54, 128);
            this.xrLinea.LocationFloat = new DevExpress.Utils.PointFloat(0F, 84F);
            this.xrLinea.Name = "xrLinea";
            this.xrLinea.SizeF = new System.Drawing.SizeF(714F, 2F);
            //
            // Recibo Nº + mes
            //
            this.xrReciboLbl.Dpi = 96F;
            this.xrReciboLbl.Font = new DevExpress.Drawing.DXFont("Segoe UI", 9F, DevExpress.Drawing.DXFontStyle.Bold);
            this.xrReciboLbl.ForeColor = System.Drawing.Color.FromArgb(102, 102, 102);
            this.xrReciboLbl.LocationFloat = new DevExpress.Utils.PointFloat(420F, 100F);
            this.xrReciboLbl.Name = "xrReciboLbl";
            this.xrReciboLbl.SizeF = new System.Drawing.SizeF(110F, 18F);
            this.xrReciboLbl.Text = "Recibo Nº:";
            this.xrReciboLbl.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
            this.xrRecibo.Dpi = 96F;
            this.xrRecibo.ExpressionBindings.AddRange(new DevExpress.XtraReports.UI.ExpressionBinding[] {
                new DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[NroReciboFmt]")});
            this.xrRecibo.Font = new DevExpress.Drawing.DXFont("Segoe UI", 12F, DevExpress.Drawing.DXFontStyle.Bold);
            this.xrRecibo.ForeColor = System.Drawing.Color.FromArgb(28, 54, 128);
            this.xrRecibo.LocationFloat = new DevExpress.Utils.PointFloat(534F, 98F);
            this.xrRecibo.Name = "xrRecibo";
            this.xrRecibo.SizeF = new System.Drawing.SizeF(180F, 20F);
            this.xrMesLbl.Dpi = 96F;
            this.xrMesLbl.Font = new DevExpress.Drawing.DXFont("Segoe UI", 9F);
            this.xrMesLbl.LocationFloat = new DevExpress.Utils.PointFloat(300F, 128F);
            this.xrMesLbl.Name = "xrMesLbl";
            this.xrMesLbl.SizeF = new System.Drawing.SizeF(180F, 18F);
            this.xrMesLbl.Text = "Correspondiente al mes de:";
            this.xrMesLbl.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
            this.xrMes.Dpi = 96F;
            this.xrMes.ExpressionBindings.AddRange(new DevExpress.XtraReports.UI.ExpressionBinding[] {
                new DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[MesTexto]")});
            this.xrMes.Font = new DevExpress.Drawing.DXFont("Segoe UI", 10F, DevExpress.Drawing.DXFontStyle.Bold);
            this.xrMes.LocationFloat = new DevExpress.Utils.PointFloat(486F, 128F);
            this.xrMes.Name = "xrMes";
            this.xrMes.SizeF = new System.Drawing.SizeF(228F, 18F);
            //
            // Nombre / C.I.
            //
            this.hNombre.Dpi = 96F;
            this.hNombre.Font = new DevExpress.Drawing.DXFont("Segoe UI", 8.5F, DevExpress.Drawing.DXFontStyle.Bold);
            this.hNombre.ForeColor = System.Drawing.Color.FromArgb(102, 102, 102);
            this.hNombre.LocationFloat = new DevExpress.Utils.PointFloat(0F, 176F);
            this.hNombre.Name = "hNombre";
            this.hNombre.SizeF = new System.Drawing.SizeF(440F, 16F);
            this.hNombre.Text = "NOMBRE Y APELLIDO";
            this.hCI.Dpi = 96F;
            this.hCI.Font = new DevExpress.Drawing.DXFont("Segoe UI", 8.5F, DevExpress.Drawing.DXFontStyle.Bold);
            this.hCI.ForeColor = System.Drawing.Color.FromArgb(102, 102, 102);
            this.hCI.LocationFloat = new DevExpress.Utils.PointFloat(480F, 176F);
            this.hCI.Name = "hCI";
            this.hCI.SizeF = new System.Drawing.SizeF(234F, 16F);
            this.hCI.Text = "C. I.";
            this.xrNombre.Dpi = 96F;
            this.xrNombre.ExpressionBindings.AddRange(new DevExpress.XtraReports.UI.ExpressionBinding[] {
                new DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[NombreTexto]")});
            this.xrNombre.Font = new DevExpress.Drawing.DXFont("Segoe UI", 11F, DevExpress.Drawing.DXFontStyle.Bold);
            this.xrNombre.LocationFloat = new DevExpress.Utils.PointFloat(0F, 194F);
            this.xrNombre.Name = "xrNombre";
            this.xrNombre.SizeF = new System.Drawing.SizeF(470F, 20F);
            this.xrCI.Dpi = 96F;
            this.xrCI.ExpressionBindings.AddRange(new DevExpress.XtraReports.UI.ExpressionBinding[] {
                new DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[CIFormato]")});
            this.xrCI.Font = new DevExpress.Drawing.DXFont("Segoe UI", 11F, DevExpress.Drawing.DXFontStyle.Bold);
            this.xrCI.LocationFloat = new DevExpress.Utils.PointFloat(480F, 194F);
            this.xrCI.Name = "xrCI";
            this.xrCI.SizeF = new System.Drawing.SizeF(234F, 20F);
            //
            // ruleB / Descripción
            //
            this.ruleB.Dpi = 96F;
            this.ruleB.ForeColor = System.Drawing.Color.FromArgb(210, 216, 232);
            this.ruleB.LocationFloat = new DevExpress.Utils.PointFloat(0F, 222F);
            this.ruleB.Name = "ruleB";
            this.ruleB.SizeF = new System.Drawing.SizeF(714F, 1F);
            this.hDescrip.Dpi = 96F;
            this.hDescrip.Font = new DevExpress.Drawing.DXFont("Segoe UI", 8.5F, DevExpress.Drawing.DXFontStyle.Bold);
            this.hDescrip.ForeColor = System.Drawing.Color.FromArgb(102, 102, 102);
            this.hDescrip.LocationFloat = new DevExpress.Utils.PointFloat(0F, 232F);
            this.hDescrip.Name = "hDescrip";
            this.hDescrip.SizeF = new System.Drawing.SizeF(440F, 16F);
            this.hDescrip.Text = "DESCRIPCIÓN";
            this.xrDescrip.Dpi = 96F;
            this.xrDescrip.ExpressionBindings.AddRange(new DevExpress.XtraReports.UI.ExpressionBinding[] {
                new DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "'Retribución por el período de enfermedad:  ' + [PeriodoTexto]")});
            this.xrDescrip.Dpi = 96F;
            this.xrDescrip.Font = new DevExpress.Drawing.DXFont("Segoe UI", 10F);
            this.xrDescrip.LocationFloat = new DevExpress.Utils.PointFloat(0F, 254F);
            this.xrDescrip.Multiline = true;
            this.xrDescrip.Name = "xrDescrip";
            this.xrDescrip.SizeF = new System.Drawing.SizeF(714F, 20F);
            //
            // ruleC / Importe
            //
            this.ruleC.Dpi = 96F;
            this.ruleC.ForeColor = System.Drawing.Color.FromArgb(28, 54, 128);
            this.ruleC.LocationFloat = new DevExpress.Utils.PointFloat(0F, 452F);
            this.ruleC.Name = "ruleC";
            this.ruleC.SizeF = new System.Drawing.SizeF(714F, 1F);
            this.xrImporteLbl.Dpi = 96F;
            this.xrImporteLbl.Font = new DevExpress.Drawing.DXFont("Segoe UI", 11F, DevExpress.Drawing.DXFontStyle.Bold);
            this.xrImporteLbl.LocationFloat = new DevExpress.Utils.PointFloat(0F, 468F);
            this.xrImporteLbl.Name = "xrImporteLbl";
            this.xrImporteLbl.SizeF = new System.Drawing.SizeF(440F, 22F);
            this.xrImporteLbl.Text = "IMPORTE A COBRAR EN PESOS URUGUAYOS:";
            this.xrImporte.Dpi = 96F;
            this.xrImporte.ExpressionBindings.AddRange(new DevExpress.XtraReports.UI.ExpressionBinding[] {
                new DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "'$ ' + [ImporteFmt]")});
            this.xrImporte.Font = new DevExpress.Drawing.DXFont("Segoe UI", 13F, DevExpress.Drawing.DXFontStyle.Bold);
            this.xrImporte.ForeColor = System.Drawing.Color.FromArgb(28, 54, 128);
            this.xrImporte.LocationFloat = new DevExpress.Utils.PointFloat(450F, 466F);
            this.xrImporte.Name = "xrImporte";
            this.xrImporte.SizeF = new System.Drawing.SizeF(264F, 24F);
            this.xrImporte.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
            //
            // Transferencia / Fecha de pago
            //
            this.xrTransfLbl.Dpi = 96F;
            this.xrTransfLbl.Font = new DevExpress.Drawing.DXFont("Segoe UI", 9F, DevExpress.Drawing.DXFontStyle.Bold);
            this.xrTransfLbl.ForeColor = System.Drawing.Color.FromArgb(102, 102, 102);
            this.xrTransfLbl.LocationFloat = new DevExpress.Utils.PointFloat(0F, 510F);
            this.xrTransfLbl.Name = "xrTransfLbl";
            this.xrTransfLbl.SizeF = new System.Drawing.SizeF(240F, 18F);
            this.xrTransfLbl.Text = "TRANSFERENCIA";
            this.xrFechaPagoLbl.Dpi = 96F;
            this.xrFechaPagoLbl.Font = new DevExpress.Drawing.DXFont("Segoe UI", 9F, DevExpress.Drawing.DXFontStyle.Bold);
            this.xrFechaPagoLbl.ForeColor = System.Drawing.Color.FromArgb(102, 102, 102);
            this.xrFechaPagoLbl.LocationFloat = new DevExpress.Utils.PointFloat(420F, 510F);
            this.xrFechaPagoLbl.Name = "xrFechaPagoLbl";
            this.xrFechaPagoLbl.SizeF = new System.Drawing.SizeF(140F, 18F);
            this.xrFechaPagoLbl.Text = "FECHA DE PAGO:";
            this.xrFechaPagoLbl.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
            this.xrFechaPago.Dpi = 96F;
            this.xrFechaPago.ExpressionBindings.AddRange(new DevExpress.XtraReports.UI.ExpressionBinding[] {
                new DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[FechaPagoFmt]")});
            this.xrFechaPago.Font = new DevExpress.Drawing.DXFont("Segoe UI", 10F, DevExpress.Drawing.DXFontStyle.Bold);
            this.xrFechaPago.LocationFloat = new DevExpress.Utils.PointFloat(564F, 510F);
            this.xrFechaPago.Name = "xrFechaPago";
            this.xrFechaPago.SizeF = new System.Drawing.SizeF(150F, 18F);
            //
            // Datos fijos: depósito / RUC / BPS
            //
            this.xrDeposito.Dpi = 96F;
            this.xrDeposito.Font = new DevExpress.Drawing.DXFont("Segoe UI", 9F);
            this.xrDeposito.LocationFloat = new DevExpress.Utils.PointFloat(0F, 548F);
            this.xrDeposito.Name = "xrDeposito";
            this.xrDeposito.SizeF = new System.Drawing.SizeF(640F, 18F);
            this.xrDeposito.Text = "Importe depositado en: Scotiabank 2201000000260478703";
            this.xrRuc.Dpi = 96F;
            this.xrRuc.Font = new DevExpress.Drawing.DXFont("Segoe UI", 9F);
            this.xrRuc.LocationFloat = new DevExpress.Utils.PointFloat(0F, 574F);
            this.xrRuc.Name = "xrRuc";
            this.xrRuc.SizeF = new System.Drawing.SizeF(300F, 18F);
            this.xrRuc.Text = "Nº RUC: 214680530013";
            this.xrBps.Dpi = 96F;
            this.xrBps.Font = new DevExpress.Drawing.DXFont("Segoe UI", 9F);
            this.xrBps.LocationFloat = new DevExpress.Utils.PointFloat(0F, 594F);
            this.xrBps.Name = "xrBps";
            this.xrBps.SizeF = new System.Drawing.SizeF(300F, 18F);
            this.xrBps.Text = "Nº BPS: 3712028";
            //
            // objectDataSource1
            //
            this.objectDataSource1.DataSource = typeof(global::Sgpa.Web.Reporting.Predefinidos.SubsidioReciboData);
            this.objectDataSource1.Name = "objectDataSource1";
            //
            // SubsidioReciboReport
            //
            this.Bands.AddRange(new DevExpress.XtraReports.UI.Band[] { this.TopMargin, this.Detail, this.BottomMargin });
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
        private DevExpress.XtraReports.UI.DetailBand Detail;
        internal DevExpress.XtraReports.UI.XRPictureBox xrLogo;
        private DevExpress.XtraReports.UI.XRLabel xrTitulo;
        private DevExpress.XtraReports.UI.XRLine xrLinea;
        private DevExpress.XtraReports.UI.XRLabel xrReciboLbl;
        private DevExpress.XtraReports.UI.XRLabel xrRecibo;
        private DevExpress.XtraReports.UI.XRLabel xrMesLbl;
        private DevExpress.XtraReports.UI.XRLabel xrMes;
        private DevExpress.XtraReports.UI.XRLabel hNombre;
        private DevExpress.XtraReports.UI.XRLabel hCI;
        private DevExpress.XtraReports.UI.XRLabel xrNombre;
        private DevExpress.XtraReports.UI.XRLabel xrCI;
        private DevExpress.XtraReports.UI.XRLine ruleB;
        private DevExpress.XtraReports.UI.XRLabel hDescrip;
        private DevExpress.XtraReports.UI.XRLabel xrDescrip;
        private DevExpress.XtraReports.UI.XRLine ruleC;
        private DevExpress.XtraReports.UI.XRLabel xrImporteLbl;
        private DevExpress.XtraReports.UI.XRLabel xrImporte;
        private DevExpress.XtraReports.UI.XRLabel xrTransfLbl;
        private DevExpress.XtraReports.UI.XRLabel xrFechaPagoLbl;
        private DevExpress.XtraReports.UI.XRLabel xrFechaPago;
        private DevExpress.XtraReports.UI.XRLabel xrDeposito;
        private DevExpress.XtraReports.UI.XRLabel xrRuc;
        private DevExpress.XtraReports.UI.XRLabel xrBps;
        private DevExpress.DataAccess.ObjectBinding.ObjectDataSource objectDataSource1;
    }
}
