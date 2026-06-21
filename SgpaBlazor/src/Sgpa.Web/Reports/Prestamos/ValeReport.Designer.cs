namespace Sgpa.Web.Reports.Prestamos
{
    partial class ValeReport
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ValeReport));
            this.TopMargin = new DevExpress.XtraReports.UI.TopMarginBand();
            this.BottomMargin = new DevExpress.XtraReports.UI.BottomMarginBand();
            this.Detail = new DevExpress.XtraReports.UI.DetailBand();
            this.xrLogo = new DevExpress.XtraReports.UI.XRPictureBox();
            this.xrTitulo = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLinea = new DevExpress.XtraReports.UI.XRLine();
            this.xrPor = new DevExpress.XtraReports.UI.XRLabel();
            this.xrVale = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLetras = new DevExpress.XtraReports.UI.XRLabel();
            this.xrCuerpo = new DevExpress.XtraReports.UI.XRLabel();
            this.xrMora = new DevExpress.XtraReports.UI.XRLabel();
            this.xrJuris = new DevExpress.XtraReports.UI.XRLabel();
            this.xrFecha = new DevExpress.XtraReports.UI.XRLabel();
            this.xrDeudorLbl = new DevExpress.XtraReports.UI.XRLabel();
            this.xrDeudor = new DevExpress.XtraReports.UI.XRLabel();
            this.xrDomicilioLbl = new DevExpress.XtraReports.UI.XRLabel();
            this.xrDomicilio = new DevExpress.XtraReports.UI.XRLabel();
            this.xrFirmaLbl = new DevExpress.XtraReports.UI.XRLabel();
            this.xrDocLbl = new DevExpress.XtraReports.UI.XRLabel();
            this.xrDoc = new DevExpress.XtraReports.UI.XRLabel();
            this.objectDataSource1 = new DevExpress.DataAccess.ObjectBinding.ObjectDataSource(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.objectDataSource1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            // 
            // TopMargin
            // 
            this.TopMargin.HeightF = 41.66667F;
            this.TopMargin.Name = "TopMargin";
            // 
            // BottomMargin
            // 
            this.BottomMargin.HeightF = 41.66667F;
            this.BottomMargin.Name = "BottomMargin";
            // 
            // Detail
            // 
            this.Detail.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrLogo,
            this.xrTitulo,
            this.xrLinea,
            this.xrPor,
            this.xrVale,
            this.xrLetras,
            this.xrCuerpo,
            this.xrMora,
            this.xrJuris,
            this.xrFecha,
            this.xrDeudorLbl,
            this.xrDeudor,
            this.xrDomicilioLbl,
            this.xrDomicilio,
            this.xrFirmaLbl,
            this.xrDocLbl,
            this.xrDoc});
            this.Detail.HeightF = 675.8929F;
            this.Detail.HierarchyPrintOptions.Indent = 20.83333F;
            this.Detail.Name = "Detail";
            // 
            // xrLogo
            // 
            this.xrLogo.LocationFloat = new DevExpress.Utils.PointFloat(0F, 0F);
            this.xrLogo.Name = "xrLogo";
            this.xrLogo.SizeF = new System.Drawing.SizeF(156.25F, 45.83333F);
            this.xrLogo.Sizing = DevExpress.XtraPrinting.ImageSizeMode.ZoomImage;
            // 
            // xrTitulo
            // 
            this.xrTitulo.Font = new DevExpress.Drawing.DXFont("Segoe UI", 15F, DevExpress.Drawing.DXFontStyle.Bold);
            this.xrTitulo.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(54)))), ((int)(((byte)(128)))));
            this.xrTitulo.LocationFloat = new DevExpress.Utils.PointFloat(0F, 10.41667F);
            this.xrTitulo.Name = "xrTitulo";
            this.xrTitulo.SizeF = new System.Drawing.SizeF(743.75F, 27.08333F);
            this.xrTitulo.Text = "Vale por el préstamo";
            this.xrTitulo.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter;
            // 
            // xrLinea
            // 
            this.xrLinea.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(54)))), ((int)(((byte)(128)))));
            this.xrLinea.LocationFloat = new DevExpress.Utils.PointFloat(0F, 47.91667F);
            this.xrLinea.Name = "xrLinea";
            this.xrLinea.SizeF = new System.Drawing.SizeF(743.75F, 2.083332F);
            // 
            // xrPor
            // 
            this.xrPor.AllowMarkupText = true;
            this.xrPor.ExpressionBindings.AddRange(new DevExpress.XtraReports.UI.ExpressionBinding[] {
            new DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "\'POR:   $ <u>\' + [ImporteTotalFmt] + \'</u>\'")});
            this.xrPor.Font = new DevExpress.Drawing.DXFont("Segoe UI", 11F, DevExpress.Drawing.DXFontStyle.Bold);
            this.xrPor.LocationFloat = new DevExpress.Utils.PointFloat(0F, 81.25F);
            this.xrPor.Name = "xrPor";
            this.xrPor.SizeF = new System.Drawing.SizeF(312.5F, 18.75F);
            // 
            // xrVale
            // 
            this.xrVale.AllowMarkupText = true;
            this.xrVale.ExpressionBindings.AddRange(new DevExpress.XtraReports.UI.ExpressionBinding[] {
            new DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "\'VALE: por la cantidad de <b><u>\' + [MonedaTexto] + \'</u></b>\'")});
            this.xrVale.LocationFloat = new DevExpress.Utils.PointFloat(0F, 116.6667F);
            this.xrVale.Name = "xrVale";
            this.xrVale.SizeF = new System.Drawing.SizeF(299.7024F, 16.66666F);
            // 
            // xrLetras
            // 
            this.xrLetras.ExpressionBindings.AddRange(new DevExpress.XtraReports.UI.ExpressionBinding[] {
            new DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[ImporteTotalLetras]")});
            this.xrLetras.Font = new DevExpress.Drawing.DXFont("Segoe UI", 9.5F, DevExpress.Drawing.DXFontStyle.Bold);
            this.xrLetras.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(54)))), ((int)(((byte)(128)))));
            this.xrLetras.LocationFloat = new DevExpress.Utils.PointFloat(299.7024F, 116.6667F);
            this.xrLetras.Name = "xrLetras";
            this.xrLetras.SizeF = new System.Drawing.SizeF(444.0476F, 16.66666F);
            // 
            // xrCuerpo
            // 
            this.xrCuerpo.AllowMarkupText = true;
            this.xrCuerpo.ExpressionBindings.AddRange(new DevExpress.XtraReports.UI.ExpressionBinding[] {
            new DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", resources.GetString("xrCuerpo.ExpressionBindings"))});
            this.xrCuerpo.LineSpacing = 1.2F;
            this.xrCuerpo.LocationFloat = new DevExpress.Utils.PointFloat(0F, 138.3334F);
            this.xrCuerpo.Multiline = true;
            this.xrCuerpo.Name = "xrCuerpo";
            this.xrCuerpo.SizeF = new System.Drawing.SizeF(743.75F, 70.16667F);
            // 
            // xrMora
            // 
            this.xrMora.AllowMarkupText = true;
            this.xrMora.ExpressionBindings.AddRange(new DevExpress.XtraReports.UI.ExpressionBinding[] {
            new DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", resources.GetString("xrMora.ExpressionBindings"))});
            this.xrMora.LineSpacing = 1.2F;
            this.xrMora.LocationFloat = new DevExpress.Utils.PointFloat(0F, 217.5F);
            this.xrMora.Multiline = true;
            this.xrMora.Name = "xrMora";
            this.xrMora.SizeF = new System.Drawing.SizeF(744.75F, 55.16666F);
            // 
            // xrJuris
            // 
            this.xrJuris.LineSpacing = 1.2F;
            this.xrJuris.LocationFloat = new DevExpress.Utils.PointFloat(0F, 283.6667F);
            this.xrJuris.Multiline = true;
            this.xrJuris.Name = "xrJuris";
            this.xrJuris.SizeF = new System.Drawing.SizeF(743.75F, 110F);
            this.xrJuris.Text = resources.GetString("xrJuris.Text");
            // 
            // xrFecha
            // 
            this.xrFecha.AllowMarkupText = true;
            this.xrFecha.ExpressionBindings.AddRange(new DevExpress.XtraReports.UI.ExpressionBinding[] {
            new DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "\'Montevideo, <b><u>\' + [FechaLarga] + \'</u></b> .-\'")});
            this.xrFecha.LocationFloat = new DevExpress.Utils.PointFloat(0F, 495.3096F);
            this.xrFecha.Name = "xrFecha";
            this.xrFecha.SizeF = new System.Drawing.SizeF(743.75F, 16.66666F);
            this.xrFecha.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
            // 
            // xrDeudorLbl
            // 
            this.xrDeudorLbl.Font = new DevExpress.Drawing.DXFont("Segoe UI", 9F, DevExpress.Drawing.DXFontStyle.Bold);
            this.xrDeudorLbl.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(102)))), ((int)(((byte)(102)))), ((int)(((byte)(102)))));
            this.xrDeudorLbl.LocationFloat = new DevExpress.Utils.PointFloat(0F, 534.6429F);
            this.xrDeudorLbl.Name = "xrDeudorLbl";
            this.xrDeudorLbl.SizeF = new System.Drawing.SizeF(114.5833F, 16.66666F);
            this.xrDeudorLbl.Text = "DEUDOR:";
            // 
            // xrDeudor
            // 
            this.xrDeudor.ExpressionBindings.AddRange(new DevExpress.XtraReports.UI.ExpressionBinding[] {
            new DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[NombreCompleto]")});
            this.xrDeudor.LocationFloat = new DevExpress.Utils.PointFloat(114.5833F, 534.6429F);
            this.xrDeudor.Name = "xrDeudor";
            this.xrDeudor.SizeF = new System.Drawing.SizeF(629.1667F, 16.66666F);
            // 
            // xrDomicilioLbl
            // 
            this.xrDomicilioLbl.Font = new DevExpress.Drawing.DXFont("Segoe UI", 9F, DevExpress.Drawing.DXFontStyle.Bold);
            this.xrDomicilioLbl.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(102)))), ((int)(((byte)(102)))), ((int)(((byte)(102)))));
            this.xrDomicilioLbl.LocationFloat = new DevExpress.Utils.PointFloat(0F, 567.9762F);
            this.xrDomicilioLbl.Name = "xrDomicilioLbl";
            this.xrDomicilioLbl.SizeF = new System.Drawing.SizeF(114.5833F, 16.66666F);
            this.xrDomicilioLbl.Text = "DOMICILIO:";
            // 
            // xrDomicilio
            // 
            this.xrDomicilio.ExpressionBindings.AddRange(new DevExpress.XtraReports.UI.ExpressionBinding[] {
            new DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[Direccion]")});
            this.xrDomicilio.LocationFloat = new DevExpress.Utils.PointFloat(114.5833F, 567.9762F);
            this.xrDomicilio.Name = "xrDomicilio";
            this.xrDomicilio.SizeF = new System.Drawing.SizeF(629.1667F, 16.66666F);
            // 
            // xrFirmaLbl
            // 
            this.xrFirmaLbl.Font = new DevExpress.Drawing.DXFont("Segoe UI", 9F, DevExpress.Drawing.DXFontStyle.Bold);
            this.xrFirmaLbl.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(102)))), ((int)(((byte)(102)))), ((int)(((byte)(102)))));
            this.xrFirmaLbl.LocationFloat = new DevExpress.Utils.PointFloat(0F, 607.5596F);
            this.xrFirmaLbl.Name = "xrFirmaLbl";
            this.xrFirmaLbl.SizeF = new System.Drawing.SizeF(312.5F, 16.66663F);
            this.xrFirmaLbl.Text = "FIRMA Y CONTRAFIRMA:";
            // 
            // xrDocLbl
            // 
            this.xrDocLbl.Font = new DevExpress.Drawing.DXFont("Segoe UI", 9F, DevExpress.Drawing.DXFontStyle.Bold);
            this.xrDocLbl.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(102)))), ((int)(((byte)(102)))), ((int)(((byte)(102)))));
            this.xrDocLbl.LocationFloat = new DevExpress.Utils.PointFloat(0F, 649.2262F);
            this.xrDocLbl.Name = "xrDocLbl";
            this.xrDocLbl.SizeF = new System.Drawing.SizeF(229.1667F, 16.66669F);
            this.xrDocLbl.Text = "DOCUMENTO DE IDENTIDAD:";
            // 
            // xrDoc
            // 
            this.xrDoc.ExpressionBindings.AddRange(new DevExpress.XtraReports.UI.ExpressionBinding[] {
            new DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[CIFormato]")});
            this.xrDoc.LocationFloat = new DevExpress.Utils.PointFloat(239.5833F, 649.2262F);
            this.xrDoc.Name = "xrDoc";
            this.xrDoc.SizeF = new System.Drawing.SizeF(208.3333F, 16.66669F);
            // 
            // objectDataSource1
            // 
            this.objectDataSource1.DataSource = typeof(global::Sgpa.Web.Reporting.Predefinidos.PrestamoValeData);
            this.objectDataSource1.Name = "objectDataSource1";
            // 
            // ValeReport
            // 
            this.Bands.AddRange(new DevExpress.XtraReports.UI.Band[] {
            this.TopMargin,
            this.BottomMargin,
            this.Detail});
            this.ComponentStorage.AddRange(new System.ComponentModel.IComponent[] {
            this.objectDataSource1});
            this.DataSource = this.objectDataSource1;
            this.Font = new DevExpress.Drawing.DXFont("Segoe UI", 9.5F);
            this.Margins = new DevExpress.Drawing.DXMargins(40F, 40F, 41.66667F, 41.66667F);
            this.PageHeightF = 1169.291F;
            this.PageWidthF = 826.7717F;
            this.PaperKind = DevExpress.Drawing.Printing.DXPaperKind.A4;
            this.Version = "25.2";
            this.BeforePrint += new DevExpress.XtraReports.UI.BeforePrintEventHandler(this.ValeReport_BeforePrint);
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
        private DevExpress.XtraReports.UI.XRLabel xrPor;
        private DevExpress.XtraReports.UI.XRLabel xrVale;
        private DevExpress.XtraReports.UI.XRLabel xrLetras;
        private DevExpress.XtraReports.UI.XRLabel xrCuerpo;
        private DevExpress.XtraReports.UI.XRLabel xrMora;
        private DevExpress.XtraReports.UI.XRLabel xrJuris;
        private DevExpress.XtraReports.UI.XRLabel xrFecha;
        private DevExpress.XtraReports.UI.XRLabel xrDeudorLbl;
        private DevExpress.XtraReports.UI.XRLabel xrDeudor;
        private DevExpress.XtraReports.UI.XRLabel xrDomicilioLbl;
        private DevExpress.XtraReports.UI.XRLabel xrDomicilio;
        private DevExpress.XtraReports.UI.XRLabel xrFirmaLbl;
        private DevExpress.XtraReports.UI.XRLabel xrDocLbl;
        private DevExpress.XtraReports.UI.XRLabel xrDoc;
        private DevExpress.DataAccess.ObjectBinding.ObjectDataSource objectDataSource1;
    }
}
