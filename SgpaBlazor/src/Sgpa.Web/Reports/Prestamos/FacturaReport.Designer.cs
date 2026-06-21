namespace Sgpa.Web.Reports.Prestamos
{
    partial class FacturaReport
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
            DevExpress.XtraPrinting.BarCode.Code128Generator code128Generator1 = new DevExpress.XtraPrinting.BarCode.Code128Generator();
            this.TopMargin = new DevExpress.XtraReports.UI.TopMarginBand();
            this.BottomMargin = new DevExpress.XtraReports.UI.BottomMarginBand();
            this.Detail = new DevExpress.XtraReports.UI.DetailBand();
            this.xrLogo = new DevExpress.XtraReports.UI.XRPictureBox();
            this.xrFacturaLbl = new DevExpress.XtraReports.UI.XRLabel();
            this.xrNroFactura = new DevExpress.XtraReports.UI.XRLabel();
            this.panelInfo = new DevExpress.XtraReports.UI.XRPanel();
            this.xrNombreLbl = new DevExpress.XtraReports.UI.XRLabel();
            this.xrNombre = new DevExpress.XtraReports.UI.XRLabel();
            this.xrCILbl = new DevExpress.XtraReports.UI.XRLabel();
            this.xrCI = new DevExpress.XtraReports.UI.XRLabel();
            this.xrDirLbl = new DevExpress.XtraReports.UI.XRLabel();
            this.xrDir = new DevExpress.XtraReports.UI.XRLabel();
            this.xrFEmiLbl = new DevExpress.XtraReports.UI.XRLabel();
            this.xrFEmi = new DevExpress.XtraReports.UI.XRLabel();
            this.xrFVtoLbl = new DevExpress.XtraReports.UI.XRLabel();
            this.xrFVto = new DevExpress.XtraReports.UI.XRLabel();
            this.xrRefHdr = new DevExpress.XtraReports.UI.XRLabel();
            this.xrImpHdr = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLineRef = new DevExpress.XtraReports.UI.XRLine();
            this.xrDescrip = new DevExpress.XtraReports.UI.XRLabel();
            this.xrImporte = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLineTotal = new DevExpress.XtraReports.UI.XRLine();
            this.xrTotalLbl = new DevExpress.XtraReports.UI.XRLabel();
            this.xrTotal = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLegal = new DevExpress.XtraReports.UI.XRLabel();
            this.xrTasaLbl = new DevExpress.XtraReports.UI.XRLabel();
            this.xrTasa = new DevExpress.XtraReports.UI.XRLabel();
            this.xrCut = new DevExpress.XtraReports.UI.XRLine();
            this.xrTalonTitulo = new DevExpress.XtraReports.UI.XRLabel();
            this.xrBarcode = new DevExpress.XtraReports.UI.XRBarCode();
            this.xrTNombre = new DevExpress.XtraReports.UI.XRLabel();
            this.xrTDoc = new DevExpress.XtraReports.UI.XRLabel();
            this.xrTImporte = new DevExpress.XtraReports.UI.XRLabel();
            this.xrTFechas = new DevExpress.XtraReports.UI.XRLabel();
            this.objectDataSource1 = new DevExpress.DataAccess.ObjectBinding.ObjectDataSource(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.objectDataSource1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            //
            // Margins
            //
            this.TopMargin.Dpi = 96F; this.TopMargin.HeightF = 40F; this.TopMargin.Name = "TopMargin";
            this.BottomMargin.Dpi = 96F; this.BottomMargin.HeightF = 40F; this.BottomMargin.Name = "BottomMargin";
            //
            // Detail (una factura + talón por cuota; salto de página tras cada una)
            //
            this.Detail.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
                this.xrLogo, this.xrFacturaLbl, this.xrNroFactura, this.panelInfo, this.xrRefHdr, this.xrImpHdr,
                this.xrLineRef, this.xrDescrip, this.xrImporte, this.xrLineTotal, this.xrTotalLbl, this.xrTotal,
                this.xrLegal, this.xrTasaLbl, this.xrTasa, this.xrCut, this.xrTalonTitulo, this.xrBarcode,
                this.xrTNombre, this.xrTDoc, this.xrTImporte, this.xrTFechas});
            this.Detail.Dpi = 96F;
            this.Detail.HeightF = 700F;
            this.Detail.Name = "Detail";
            this.Detail.PageBreak = DevExpress.XtraReports.UI.PageBreak.AfterBand;
            //
            // xrLogo / Factura Nº
            //
            this.xrLogo.Dpi = 96F;
            this.xrLogo.LocationFloat = new DevExpress.Utils.PointFloat(0F, 0F);
            this.xrLogo.Name = "xrLogo";
            this.xrLogo.SizeF = new System.Drawing.SizeF(150F, 44F);
            this.xrLogo.Sizing = DevExpress.XtraPrinting.ImageSizeMode.ZoomImage;
            this.xrFacturaLbl.Dpi = 96F;
            this.xrFacturaLbl.Font = new DevExpress.Drawing.DXFont("Segoe UI", 13F, DevExpress.Drawing.DXFontStyle.Bold);
            this.xrFacturaLbl.ForeColor = System.Drawing.Color.FromArgb(28, 54, 128);
            this.xrFacturaLbl.LocationFloat = new DevExpress.Utils.PointFloat(380F, 10F);
            this.xrFacturaLbl.Name = "xrFacturaLbl";
            this.xrFacturaLbl.SizeF = new System.Drawing.SizeF(150F, 24F);
            this.xrFacturaLbl.Text = "Factura Nº";
            this.xrFacturaLbl.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
            this.xrNroFactura.Dpi = 96F;
            this.xrNroFactura.ExpressionBindings.AddRange(new DevExpress.XtraReports.UI.ExpressionBinding[] {
                new DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[NroFacturaFmt]")});
            this.xrNroFactura.Font = new DevExpress.Drawing.DXFont("Segoe UI", 13F, DevExpress.Drawing.DXFontStyle.Bold);
            this.xrNroFactura.LocationFloat = new DevExpress.Utils.PointFloat(540F, 10F);
            this.xrNroFactura.Name = "xrNroFactura";
            this.xrNroFactura.SizeF = new System.Drawing.SizeF(160F, 24F);
            this.xrNroFactura.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
            //
            // panelInfo (borde, SIN relleno -> no tapa los labels; labels como hijos)
            //
            this.panelInfo.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
                this.xrNombreLbl, this.xrNombre, this.xrCILbl, this.xrCI, this.xrDirLbl, this.xrDir,
                this.xrFEmiLbl, this.xrFEmi, this.xrFVtoLbl, this.xrFVto});
            this.panelInfo.Borders = DevExpress.XtraPrinting.BorderSide.All;
            this.panelInfo.BorderColor = System.Drawing.Color.FromArgb(204, 204, 204);
            this.panelInfo.Dpi = 96F;
            this.panelInfo.LocationFloat = new DevExpress.Utils.PointFloat(0F, 64F);
            this.panelInfo.Name = "panelInfo";
            this.panelInfo.SizeF = new System.Drawing.SizeF(700F, 110F);
            this.xrNombreLbl.Dpi = 96F; this.xrNombreLbl.ForeColor = System.Drawing.Color.FromArgb(102, 102, 102); this.xrNombreLbl.LocationFloat = new DevExpress.Utils.PointFloat(14F, 14F); this.xrNombreLbl.Name = "xrNombreLbl"; this.xrNombreLbl.SizeF = new System.Drawing.SizeF(60F, 16F); this.xrNombreLbl.Text = "Nombre";
            this.xrNombre.Dpi = 96F; this.xrNombre.Font = new DevExpress.Drawing.DXFont("Segoe UI", 9F, DevExpress.Drawing.DXFontStyle.Bold); this.xrNombre.LocationFloat = new DevExpress.Utils.PointFloat(80F, 14F); this.xrNombre.Name = "xrNombre"; this.xrNombre.SizeF = new System.Drawing.SizeF(420F, 16F); this.xrNombre.ExpressionBindings.AddRange(new DevExpress.XtraReports.UI.ExpressionBinding[] { new DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[NombreCompleto]") });
            this.xrCILbl.Dpi = 96F; this.xrCILbl.ForeColor = System.Drawing.Color.FromArgb(102, 102, 102); this.xrCILbl.LocationFloat = new DevExpress.Utils.PointFloat(510F, 14F); this.xrCILbl.Name = "xrCILbl"; this.xrCILbl.SizeF = new System.Drawing.SizeF(40F, 16F); this.xrCILbl.Text = "C.I.";
            this.xrCI.Dpi = 96F; this.xrCI.Font = new DevExpress.Drawing.DXFont("Segoe UI", 9F, DevExpress.Drawing.DXFontStyle.Bold); this.xrCI.LocationFloat = new DevExpress.Utils.PointFloat(550F, 14F); this.xrCI.Name = "xrCI"; this.xrCI.SizeF = new System.Drawing.SizeF(136F, 16F); this.xrCI.TextFormatString = "{0:#,#}"; this.xrCI.ExpressionBindings.AddRange(new DevExpress.XtraReports.UI.ExpressionBinding[] { new DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[CI]") });
            this.xrDirLbl.Dpi = 96F; this.xrDirLbl.ForeColor = System.Drawing.Color.FromArgb(102, 102, 102); this.xrDirLbl.LocationFloat = new DevExpress.Utils.PointFloat(14F, 44F); this.xrDirLbl.Name = "xrDirLbl"; this.xrDirLbl.SizeF = new System.Drawing.SizeF(60F, 16F); this.xrDirLbl.Text = "Dirección";
            this.xrDir.Dpi = 96F; this.xrDir.LocationFloat = new DevExpress.Utils.PointFloat(80F, 44F); this.xrDir.Name = "xrDir"; this.xrDir.SizeF = new System.Drawing.SizeF(606F, 16F); this.xrDir.ExpressionBindings.AddRange(new DevExpress.XtraReports.UI.ExpressionBinding[] { new DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[Direccion]") });
            this.xrFEmiLbl.Dpi = 96F; this.xrFEmiLbl.ForeColor = System.Drawing.Color.FromArgb(102, 102, 102); this.xrFEmiLbl.LocationFloat = new DevExpress.Utils.PointFloat(14F, 76F); this.xrFEmiLbl.Name = "xrFEmiLbl"; this.xrFEmiLbl.SizeF = new System.Drawing.SizeF(110F, 16F); this.xrFEmiLbl.Text = "Fecha de Emisión";
            this.xrFEmi.Dpi = 96F; this.xrFEmi.LocationFloat = new DevExpress.Utils.PointFloat(130F, 76F); this.xrFEmi.Name = "xrFEmi"; this.xrFEmi.SizeF = new System.Drawing.SizeF(120F, 16F); this.xrFEmi.TextFormatString = "{0:dd/MM/yyyy}"; this.xrFEmi.ExpressionBindings.AddRange(new DevExpress.XtraReports.UI.ExpressionBinding[] { new DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[FechaEmitida]") });
            this.xrFVtoLbl.Dpi = 96F; this.xrFVtoLbl.ForeColor = System.Drawing.Color.FromArgb(102, 102, 102); this.xrFVtoLbl.LocationFloat = new DevExpress.Utils.PointFloat(360F, 76F); this.xrFVtoLbl.Name = "xrFVtoLbl"; this.xrFVtoLbl.SizeF = new System.Drawing.SizeF(150F, 16F); this.xrFVtoLbl.Text = "Fecha de Vencimiento";
            this.xrFVto.Dpi = 96F; this.xrFVto.LocationFloat = new DevExpress.Utils.PointFloat(516F, 76F); this.xrFVto.Name = "xrFVto"; this.xrFVto.SizeF = new System.Drawing.SizeF(120F, 16F); this.xrFVto.TextFormatString = "{0:dd/MM/yyyy}"; this.xrFVto.ExpressionBindings.AddRange(new DevExpress.XtraReports.UI.ExpressionBinding[] { new DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[FechaVencimiento]") });
            //
            // REFERENCIA / IMPORTE + detalle
            //
            this.xrRefHdr.Dpi = 96F; this.xrRefHdr.Font = new DevExpress.Drawing.DXFont("Segoe UI", 8.5F, DevExpress.Drawing.DXFontStyle.Bold); this.xrRefHdr.ForeColor = System.Drawing.Color.FromArgb(102, 102, 102); this.xrRefHdr.LocationFloat = new DevExpress.Utils.PointFloat(0F, 196F); this.xrRefHdr.Name = "xrRefHdr"; this.xrRefHdr.SizeF = new System.Drawing.SizeF(400F, 16F); this.xrRefHdr.Text = "REFERENCIA";
            this.xrImpHdr.Dpi = 96F; this.xrImpHdr.Font = new DevExpress.Drawing.DXFont("Segoe UI", 8.5F, DevExpress.Drawing.DXFontStyle.Bold); this.xrImpHdr.ForeColor = System.Drawing.Color.FromArgb(102, 102, 102); this.xrImpHdr.LocationFloat = new DevExpress.Utils.PointFloat(500F, 196F); this.xrImpHdr.Name = "xrImpHdr"; this.xrImpHdr.SizeF = new System.Drawing.SizeF(200F, 16F); this.xrImpHdr.Text = "IMPORTE"; this.xrImpHdr.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
            this.xrLineRef.Dpi = 96F; this.xrLineRef.ForeColor = System.Drawing.Color.FromArgb(204, 204, 204); this.xrLineRef.LocationFloat = new DevExpress.Utils.PointFloat(0F, 214F); this.xrLineRef.Name = "xrLineRef"; this.xrLineRef.SizeF = new System.Drawing.SizeF(700F, 1F);
            this.xrDescrip.Dpi = 96F; this.xrDescrip.LocationFloat = new DevExpress.Utils.PointFloat(14F, 222F); this.xrDescrip.Name = "xrDescrip"; this.xrDescrip.SizeF = new System.Drawing.SizeF(440F, 16F); this.xrDescrip.ExpressionBindings.AddRange(new DevExpress.XtraReports.UI.ExpressionBinding[] { new DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[Descrip]") });
            this.xrImporte.Dpi = 96F; this.xrImporte.LocationFloat = new DevExpress.Utils.PointFloat(500F, 222F); this.xrImporte.Name = "xrImporte"; this.xrImporte.SizeF = new System.Drawing.SizeF(200F, 16F); this.xrImporte.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight; this.xrImporte.ExpressionBindings.AddRange(new DevExpress.XtraReports.UI.ExpressionBinding[] { new DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[ImporteFmt]") });
            this.xrLineTotal.Dpi = 96F; this.xrLineTotal.ForeColor = System.Drawing.Color.FromArgb(204, 204, 204); this.xrLineTotal.LocationFloat = new DevExpress.Utils.PointFloat(0F, 300F); this.xrLineTotal.Name = "xrLineTotal"; this.xrLineTotal.SizeF = new System.Drawing.SizeF(700F, 1F);
            this.xrTotalLbl.Dpi = 96F; this.xrTotalLbl.Font = new DevExpress.Drawing.DXFont("Segoe UI", 10F, DevExpress.Drawing.DXFontStyle.Bold); this.xrTotalLbl.LocationFloat = new DevExpress.Utils.PointFloat(300F, 312F); this.xrTotalLbl.Name = "xrTotalLbl"; this.xrTotalLbl.SizeF = new System.Drawing.SizeF(200F, 18F); this.xrTotalLbl.Text = "Total a pagar $"; this.xrTotalLbl.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
            this.xrTotal.Dpi = 96F; this.xrTotal.Font = new DevExpress.Drawing.DXFont("Segoe UI", 12F, DevExpress.Drawing.DXFontStyle.Bold); this.xrTotal.ForeColor = System.Drawing.Color.FromArgb(28, 54, 128); this.xrTotal.LocationFloat = new DevExpress.Utils.PointFloat(510F, 311F); this.xrTotal.Name = "xrTotal"; this.xrTotal.SizeF = new System.Drawing.SizeF(190F, 20F); this.xrTotal.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight; this.xrTotal.ExpressionBindings.AddRange(new DevExpress.XtraReports.UI.ExpressionBinding[] { new DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[ImporteFmt]") });
            this.xrLegal.Dpi = 96F; this.xrLegal.Font = new DevExpress.Drawing.DXFont("Segoe UI", 7.5F); this.xrLegal.ForeColor = System.Drawing.Color.FromArgb(102, 102, 102); this.xrLegal.LocationFloat = new DevExpress.Utils.PointFloat(0F, 356F); this.xrLegal.Multiline = true; this.xrLegal.Name = "xrLegal"; this.xrLegal.SizeF = new System.Drawing.SizeF(700F, 44F); this.xrLegal.Text = "Este documento adquiere valor de recibo solamente si posee autentificación de cobro. Su pago no cancela adeudos anteriores. En caso de incumplimiento en la fecha de pago, esta factura devengará el máximo interés legal.";
            this.xrTasaLbl.Dpi = 96F; this.xrTasaLbl.ForeColor = System.Drawing.Color.FromArgb(102, 102, 102); this.xrTasaLbl.LocationFloat = new DevExpress.Utils.PointFloat(0F, 416F); this.xrTasaLbl.Name = "xrTasaLbl"; this.xrTasaLbl.SizeF = new System.Drawing.SizeF(90F, 16F); this.xrTasaLbl.Text = "Tasa efectiva";
            this.xrTasa.Dpi = 96F; this.xrTasa.Font = new DevExpress.Drawing.DXFont("Segoe UI", 9F, DevExpress.Drawing.DXFontStyle.Bold); this.xrTasa.LocationFloat = new DevExpress.Utils.PointFloat(95F, 416F); this.xrTasa.Name = "xrTasa"; this.xrTasa.SizeF = new System.Drawing.SizeF(120F, 16F); this.xrTasa.TextFormatString = "{0:N2} % anual"; this.xrTasa.ExpressionBindings.AddRange(new DevExpress.XtraReports.UI.ExpressionBinding[] { new DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[Tasa]") });
            //
            // Línea de corte + TALÓN
            //
            this.xrCut.Dpi = 96F; this.xrCut.ForeColor = System.Drawing.Color.FromArgb(153, 153, 153); this.xrCut.LineStyle = DevExpress.Drawing.DXDashStyle.Dash; this.xrCut.LocationFloat = new DevExpress.Utils.PointFloat(0F, 470F); this.xrCut.Name = "xrCut"; this.xrCut.SizeF = new System.Drawing.SizeF(700F, 2F);
            this.xrTalonTitulo.Dpi = 96F; this.xrTalonTitulo.Font = new DevExpress.Drawing.DXFont("Segoe UI", 9F, DevExpress.Drawing.DXFontStyle.Bold); this.xrTalonTitulo.ForeColor = System.Drawing.Color.FromArgb(102, 102, 102); this.xrTalonTitulo.LocationFloat = new DevExpress.Utils.PointFloat(0F, 482F); this.xrTalonTitulo.Name = "xrTalonTitulo"; this.xrTalonTitulo.SizeF = new System.Drawing.SizeF(700F, 16F); this.xrTalonTitulo.Text = "TALÓN DE COBRANZA"; this.xrTalonTitulo.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter;
            this.xrBarcode.Dpi = 96F;
            this.xrBarcode.ExpressionBindings.AddRange(new DevExpress.XtraReports.UI.ExpressionBinding[] {
                new DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[CodigoBarra]")});
            this.xrBarcode.LocationFloat = new DevExpress.Utils.PointFloat(170F, 506F);
            this.xrBarcode.Module = 1.5F;
            this.xrBarcode.Name = "xrBarcode";
            this.xrBarcode.Padding = new DevExpress.XtraPrinting.PaddingInfo(4, 4, 4, 4, 96F);
            this.xrBarcode.ShowText = true;
            this.xrBarcode.SizeF = new System.Drawing.SizeF(360F, 70F);
            this.xrBarcode.Symbology = code128Generator1;
            this.xrTNombre.Dpi = 96F; this.xrTNombre.LocationFloat = new DevExpress.Utils.PointFloat(0F, 590F); this.xrTNombre.Name = "xrTNombre"; this.xrTNombre.SizeF = new System.Drawing.SizeF(700F, 16F); this.xrTNombre.ExpressionBindings.AddRange(new DevExpress.XtraReports.UI.ExpressionBinding[] { new DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "'Nombre: ' + [NombreCompleto] + '     C.I. ' + [CIFormato]") });
            this.xrTDoc.Dpi = 96F; this.xrTDoc.LocationFloat = new DevExpress.Utils.PointFloat(0F, 612F); this.xrTDoc.Name = "xrTDoc"; this.xrTDoc.SizeF = new System.Drawing.SizeF(400F, 16F); this.xrTDoc.ExpressionBindings.AddRange(new DevExpress.XtraReports.UI.ExpressionBinding[] { new DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "'Documento: Factura Nº ' + [NroFacturaFmt]") });
            this.xrTImporte.Dpi = 96F; this.xrTImporte.Font = new DevExpress.Drawing.DXFont("Segoe UI", 9F, DevExpress.Drawing.DXFontStyle.Bold); this.xrTImporte.LocationFloat = new DevExpress.Utils.PointFloat(400F, 612F); this.xrTImporte.Name = "xrTImporte"; this.xrTImporte.SizeF = new System.Drawing.SizeF(300F, 16F); this.xrTImporte.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight; this.xrTImporte.ExpressionBindings.AddRange(new DevExpress.XtraReports.UI.ExpressionBinding[] { new DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "'Importe $ ' + [ImporteFmt]") });
            this.xrTFechas.Dpi = 96F; this.xrTFechas.ForeColor = System.Drawing.Color.FromArgb(102, 102, 102); this.xrTFechas.LocationFloat = new DevExpress.Utils.PointFloat(0F, 634F); this.xrTFechas.Name = "xrTFechas"; this.xrTFechas.SizeF = new System.Drawing.SizeF(700F, 16F); this.xrTFechas.ExpressionBindings.AddRange(new DevExpress.XtraReports.UI.ExpressionBinding[] { new DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "'Emisión: ' + FormatString('{0:dd/MM/yyyy}', [FechaEmitida]) + '     Vencimiento: ' + FormatString('{0:dd/MM/yyyy}', [FechaVencimiento])") });
            //
            // objectDataSource1
            //
            this.objectDataSource1.DataSource = typeof(global::Sgpa.Web.Reporting.Predefinidos.FacturaLinea);
            this.objectDataSource1.Name = "objectDataSource1";
            //
            // FacturaReport
            //
            this.Bands.AddRange(new DevExpress.XtraReports.UI.Band[] { this.TopMargin, this.BottomMargin, this.Detail });
            this.ComponentStorage.AddRange(new System.ComponentModel.IComponent[] { this.objectDataSource1 });
            this.DataSource = this.objectDataSource1;
            this.Font = new DevExpress.Drawing.DXFont("Segoe UI", 9F);
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
        private DevExpress.XtraReports.UI.XRLabel xrFacturaLbl;
        private DevExpress.XtraReports.UI.XRLabel xrNroFactura;
        private DevExpress.XtraReports.UI.XRPanel panelInfo;
        private DevExpress.XtraReports.UI.XRLabel xrNombreLbl;
        private DevExpress.XtraReports.UI.XRLabel xrNombre;
        private DevExpress.XtraReports.UI.XRLabel xrCILbl;
        private DevExpress.XtraReports.UI.XRLabel xrCI;
        private DevExpress.XtraReports.UI.XRLabel xrDirLbl;
        private DevExpress.XtraReports.UI.XRLabel xrDir;
        private DevExpress.XtraReports.UI.XRLabel xrFEmiLbl;
        private DevExpress.XtraReports.UI.XRLabel xrFEmi;
        private DevExpress.XtraReports.UI.XRLabel xrFVtoLbl;
        private DevExpress.XtraReports.UI.XRLabel xrFVto;
        private DevExpress.XtraReports.UI.XRLabel xrRefHdr;
        private DevExpress.XtraReports.UI.XRLabel xrImpHdr;
        private DevExpress.XtraReports.UI.XRLine xrLineRef;
        private DevExpress.XtraReports.UI.XRLabel xrDescrip;
        private DevExpress.XtraReports.UI.XRLabel xrImporte;
        private DevExpress.XtraReports.UI.XRLine xrLineTotal;
        private DevExpress.XtraReports.UI.XRLabel xrTotalLbl;
        private DevExpress.XtraReports.UI.XRLabel xrTotal;
        private DevExpress.XtraReports.UI.XRLabel xrLegal;
        private DevExpress.XtraReports.UI.XRLabel xrTasaLbl;
        private DevExpress.XtraReports.UI.XRLabel xrTasa;
        private DevExpress.XtraReports.UI.XRLine xrCut;
        private DevExpress.XtraReports.UI.XRLabel xrTalonTitulo;
        private DevExpress.XtraReports.UI.XRBarCode xrBarcode;
        private DevExpress.XtraReports.UI.XRLabel xrTNombre;
        private DevExpress.XtraReports.UI.XRLabel xrTDoc;
        private DevExpress.XtraReports.UI.XRLabel xrTImporte;
        private DevExpress.XtraReports.UI.XRLabel xrTFechas;
        private DevExpress.DataAccess.ObjectBinding.ObjectDataSource objectDataSource1;
    }
}
