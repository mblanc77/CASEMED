namespace Sgpa.Web.Reports.Prestamos
{
    partial class FichaReport
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
            this.pNum = new DevExpress.XtraReports.Parameters.Parameter();
            this.pFecha = new DevExpress.XtraReports.Parameters.Parameter();
            this.pCI = new DevExpress.XtraReports.Parameters.Parameter();
            this.pNombre = new DevExpress.XtraReports.Parameters.Parameter();
            this.pImponibles = new DevExpress.XtraReports.Parameters.Parameter();
            this.pMoneda = new DevExpress.XtraReports.Parameters.Parameter();
            this.pCuotas = new DevExpress.XtraReports.Parameters.Parameter();
            this.pValorCuota = new DevExpress.XtraReports.Parameters.Parameter();
            this.pTasa = new DevExpress.XtraReports.Parameters.Parameter();
            this.pMontoTotal = new DevExpress.XtraReports.Parameters.Parameter();
            this.TopMargin = new DevExpress.XtraReports.UI.TopMarginBand();
            this.BottomMargin = new DevExpress.XtraReports.UI.BottomMarginBand();
            this.ReportHeader = new DevExpress.XtraReports.UI.ReportHeaderBand();
            this.xrLogo = new DevExpress.XtraReports.UI.XRPictureBox();
            this.xrTitulo = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLinea = new DevExpress.XtraReports.UI.XRLine();
            this.xrNumLbl = new DevExpress.XtraReports.UI.XRLabel();
            this.xrNum = new DevExpress.XtraReports.UI.XRLabel();
            this.xrFechaLbl = new DevExpress.XtraReports.UI.XRLabel();
            this.xrFecha = new DevExpress.XtraReports.UI.XRLabel();
            this.xrCILbl = new DevExpress.XtraReports.UI.XRLabel();
            this.xrCI = new DevExpress.XtraReports.UI.XRLabel();
            this.xrImpLbl = new DevExpress.XtraReports.UI.XRLabel();
            this.xrImp = new DevExpress.XtraReports.UI.XRLabel();
            this.xrNomLbl = new DevExpress.XtraReports.UI.XRLabel();
            this.xrNom = new DevExpress.XtraReports.UI.XRLabel();
            this.xrMonLbl = new DevExpress.XtraReports.UI.XRLabel();
            this.xrMon = new DevExpress.XtraReports.UI.XRLabel();
            this.xrCuoLbl = new DevExpress.XtraReports.UI.XRLabel();
            this.xrCuo = new DevExpress.XtraReports.UI.XRLabel();
            this.xrValLbl = new DevExpress.XtraReports.UI.XRLabel();
            this.xrVal = new DevExpress.XtraReports.UI.XRLabel();
            this.xrTasaLbl = new DevExpress.XtraReports.UI.XRLabel();
            this.xrTasa = new DevExpress.XtraReports.UI.XRLabel();
            this.xrCuadroLbl = new DevExpress.XtraReports.UI.XRLabel();
            this.headerTable = new DevExpress.XtraReports.UI.XRTable();
            this.headerRow = new DevExpress.XtraReports.UI.XRTableRow();
            this.hN = new DevExpress.XtraReports.UI.XRTableCell();
            this.hSaldoIni = new DevExpress.XtraReports.UI.XRTableCell();
            this.hCuota = new DevExpress.XtraReports.UI.XRTableCell();
            this.hInteres = new DevExpress.XtraReports.UI.XRTableCell();
            this.hAmort = new DevExpress.XtraReports.UI.XRTableCell();
            this.hSaldo = new DevExpress.XtraReports.UI.XRTableCell();
            this.panelAfi = new DevExpress.XtraReports.UI.XRPanel();
            this.xrAfiTitulo = new DevExpress.XtraReports.UI.XRLabel();
            this.panelPre = new DevExpress.XtraReports.UI.XRPanel();
            this.xrPreTitulo = new DevExpress.XtraReports.UI.XRLabel();
            this.Detail = new DevExpress.XtraReports.UI.DetailBand();
            this.detailTable = new DevExpress.XtraReports.UI.XRTable();
            this.detailRow = new DevExpress.XtraReports.UI.XRTableRow();
            this.dN = new DevExpress.XtraReports.UI.XRTableCell();
            this.dMonto = new DevExpress.XtraReports.UI.XRTableCell();
            this.dCuota = new DevExpress.XtraReports.UI.XRTableCell();
            this.dInteres = new DevExpress.XtraReports.UI.XRTableCell();
            this.dAmort = new DevExpress.XtraReports.UI.XRTableCell();
            this.dSaldo = new DevExpress.XtraReports.UI.XRTableCell();
            this.ReportFooter = new DevExpress.XtraReports.UI.ReportFooterBand();
            this.totalTable = new DevExpress.XtraReports.UI.XRTable();
            this.totalRow = new DevExpress.XtraReports.UI.XRTableRow();
            this.tLbl = new DevExpress.XtraReports.UI.XRTableCell();
            this.tEmpty1 = new DevExpress.XtraReports.UI.XRTableCell();
            this.tCuota = new DevExpress.XtraReports.UI.XRTableCell();
            this.tInteres = new DevExpress.XtraReports.UI.XRTableCell();
            this.tAmort = new DevExpress.XtraReports.UI.XRTableCell();
            this.tEmpty2 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrMontoTotalLbl = new DevExpress.XtraReports.UI.XRLabel();
            this.xrMontoTotal = new DevExpress.XtraReports.UI.XRLabel();
            this.PageFooter = new DevExpress.XtraReports.UI.PageFooterBand();
            this.xrPie = new DevExpress.XtraReports.UI.XRLabel();
            this.xrPageInfo = new DevExpress.XtraReports.UI.XRPageInfo();
            this.objectDataSource1 = new DevExpress.DataAccess.ObjectBinding.ObjectDataSource(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.headerTable)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.detailTable)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.totalTable)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.objectDataSource1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            // 
            // pNum
            // 
            this.pNum.Name = "pNum";
            this.pNum.Type = typeof(int);
            this.pNum.Visible = false;
            // 
            // pFecha
            // 
            this.pFecha.Name = "pFecha";
            this.pFecha.Type = typeof(global::System.DateTime);
            this.pFecha.Visible = false;
            // 
            // pCI
            // 
            this.pCI.Name = "pCI";
            this.pCI.Type = typeof(long);
            this.pCI.Visible = false;
            // 
            // pNombre
            // 
            this.pNombre.Name = "pNombre";
            this.pNombre.Visible = false;
            // 
            // pImponibles
            // 
            this.pImponibles.Name = "pImponibles";
            this.pImponibles.Type = typeof(decimal);
            this.pImponibles.Visible = false;
            // 
            // pMoneda
            // 
            this.pMoneda.Name = "pMoneda";
            this.pMoneda.Visible = false;
            // 
            // pCuotas
            // 
            this.pCuotas.Name = "pCuotas";
            this.pCuotas.Type = typeof(int);
            this.pCuotas.Visible = false;
            // 
            // pValorCuota
            // 
            this.pValorCuota.Name = "pValorCuota";
            this.pValorCuota.Type = typeof(decimal);
            this.pValorCuota.Visible = false;
            // 
            // pTasa
            // 
            this.pTasa.Name = "pTasa";
            this.pTasa.Type = typeof(decimal);
            this.pTasa.Visible = false;
            // 
            // pMontoTotal
            // 
            this.pMontoTotal.Name = "pMontoTotal";
            this.pMontoTotal.Type = typeof(decimal);
            this.pMontoTotal.Visible = false;
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
            // ReportHeader
            // 
            this.ReportHeader.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrLogo,
            this.xrTitulo,
            this.xrLinea,
            this.xrNumLbl,
            this.xrNum,
            this.xrFechaLbl,
            this.xrFecha,
            this.xrCILbl,
            this.xrCI,
            this.xrImpLbl,
            this.xrImp,
            this.xrNomLbl,
            this.xrNom,
            this.xrMonLbl,
            this.xrMon,
            this.xrCuoLbl,
            this.xrCuo,
            this.xrValLbl,
            this.xrVal,
            this.xrTasa,
            this.xrCuadroLbl,
            this.headerTable,
            this.panelAfi,
            this.panelPre});
            this.ReportHeader.HeightF = 264.5833F;
            this.ReportHeader.Name = "ReportHeader";
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
            this.xrTitulo.LocationFloat = new DevExpress.Utils.PointFloat(0F, 8.333333F);
            this.xrTitulo.Name = "xrTitulo";
            this.xrTitulo.SizeF = new System.Drawing.SizeF(729.1667F, 27.08334F);
            this.xrTitulo.Text = "Ficha de préstamo";
            this.xrTitulo.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter;
            // 
            // xrLinea
            // 
            this.xrLinea.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(54)))), ((int)(((byte)(128)))));
            this.xrLinea.LocationFloat = new DevExpress.Utils.PointFloat(0F, 52.08333F);
            this.xrLinea.Name = "xrLinea";
            this.xrLinea.SizeF = new System.Drawing.SizeF(729.1667F, 2.083336F);
            // 
            // xrNumLbl
            // 
            this.xrNumLbl.Font = new DevExpress.Drawing.DXFont("Segoe UI", 8F);
            this.xrNumLbl.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(102)))), ((int)(((byte)(102)))), ((int)(((byte)(102)))));
            this.xrNumLbl.LocationFloat = new DevExpress.Utils.PointFloat(416.6667F, 6.25F);
            this.xrNumLbl.Name = "xrNumLbl";
            this.xrNumLbl.SizeF = new System.Drawing.SizeF(135.4167F, 16.66667F);
            this.xrNumLbl.Text = "N° de préstamo";
            this.xrNumLbl.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
            // 
            // xrNum
            // 
            this.xrNum.ExpressionBindings.AddRange(new DevExpress.XtraReports.UI.ExpressionBinding[] {
            new DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[Parameters.pNum]")});
            this.xrNum.Font = new DevExpress.Drawing.DXFont("Segoe UI", 11F, DevExpress.Drawing.DXFontStyle.Bold);
            this.xrNum.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(54)))), ((int)(((byte)(128)))));
            this.xrNum.LocationFloat = new DevExpress.Utils.PointFloat(557.2917F, 4.166667F);
            this.xrNum.Name = "xrNum";
            this.xrNum.SizeF = new System.Drawing.SizeF(171.875F, 20.83333F);
            this.xrNum.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
            // 
            // xrFechaLbl
            // 
            this.xrFechaLbl.Font = new DevExpress.Drawing.DXFont("Segoe UI", 8F);
            this.xrFechaLbl.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(102)))), ((int)(((byte)(102)))), ((int)(((byte)(102)))));
            this.xrFechaLbl.LocationFloat = new DevExpress.Utils.PointFloat(416.6667F, 27.08333F);
            this.xrFechaLbl.Name = "xrFechaLbl";
            this.xrFechaLbl.SizeF = new System.Drawing.SizeF(135.4167F, 16.66667F);
            this.xrFechaLbl.Text = "Fecha";
            this.xrFechaLbl.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
            // 
            // xrFecha
            // 
            this.xrFecha.ExpressionBindings.AddRange(new DevExpress.XtraReports.UI.ExpressionBinding[] {
            new DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[Parameters.pFecha]")});
            this.xrFecha.LocationFloat = new DevExpress.Utils.PointFloat(557.2917F, 25F);
            this.xrFecha.Name = "xrFecha";
            this.xrFecha.SizeF = new System.Drawing.SizeF(171.875F, 18.75F);
            this.xrFecha.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
            this.xrFecha.TextFormatString = "{0:dd/MM/yyyy}";
            // 
            // xrCILbl
            // 
            this.xrCILbl.Font = new DevExpress.Drawing.DXFont("Segoe UI", 8F);
            this.xrCILbl.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(102)))), ((int)(((byte)(102)))), ((int)(((byte)(102)))));
            this.xrCILbl.LocationFloat = new DevExpress.Utils.PointFloat(14.58333F, 95.83334F);
            this.xrCILbl.Name = "xrCILbl";
            this.xrCILbl.SizeF = new System.Drawing.SizeF(62.5F, 16.66666F);
            this.xrCILbl.Text = "C.I.";
            // 
            // xrCI
            // 
            this.xrCI.ExpressionBindings.AddRange(new DevExpress.XtraReports.UI.ExpressionBinding[] {
            new DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[Parameters.pCI]")});
            this.xrCI.Font = new DevExpress.Drawing.DXFont("Segoe UI", 9F, DevExpress.Drawing.DXFontStyle.Bold);
            this.xrCI.LocationFloat = new DevExpress.Utils.PointFloat(83.33334F, 95.83334F);
            this.xrCI.Name = "xrCI";
            this.xrCI.SizeF = new System.Drawing.SizeF(187.5F, 16.66666F);
            this.xrCI.TextFormatString = "{0:#,#}";
            // 
            // xrImpLbl
            // 
            this.xrImpLbl.Font = new DevExpress.Drawing.DXFont("Segoe UI", 8F);
            this.xrImpLbl.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(102)))), ((int)(((byte)(102)))), ((int)(((byte)(102)))));
            this.xrImpLbl.LocationFloat = new DevExpress.Utils.PointFloat(437.5F, 95.83334F);
            this.xrImpLbl.Name = "xrImpLbl";
            this.xrImpLbl.SizeF = new System.Drawing.SizeF(93.75F, 16.66666F);
            this.xrImpLbl.Text = "Imponibles";
            this.xrImpLbl.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
            // 
            // xrImp
            // 
            this.xrImp.ExpressionBindings.AddRange(new DevExpress.XtraReports.UI.ExpressionBinding[] {
            new DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[Parameters.pImponibles]")});
            this.xrImp.Font = new DevExpress.Drawing.DXFont("Segoe UI", 9F, DevExpress.Drawing.DXFontStyle.Bold);
            this.xrImp.LocationFloat = new DevExpress.Utils.PointFloat(536.4583F, 95.83334F);
            this.xrImp.Name = "xrImp";
            this.xrImp.SizeF = new System.Drawing.SizeF(182.2917F, 16.66666F);
            this.xrImp.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
            this.xrImp.TextFormatString = "$ {0:N2}";
            // 
            // xrNomLbl
            // 
            this.xrNomLbl.Font = new DevExpress.Drawing.DXFont("Segoe UI", 8F);
            this.xrNomLbl.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(102)))), ((int)(((byte)(102)))), ((int)(((byte)(102)))));
            this.xrNomLbl.LocationFloat = new DevExpress.Utils.PointFloat(14.58333F, 116.6667F);
            this.xrNomLbl.Name = "xrNomLbl";
            this.xrNomLbl.SizeF = new System.Drawing.SizeF(62.5F, 16.66666F);
            this.xrNomLbl.Text = "Nombre";
            // 
            // xrNom
            // 
            this.xrNom.ExpressionBindings.AddRange(new DevExpress.XtraReports.UI.ExpressionBinding[] {
            new DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[Parameters.pNombre]")});
            this.xrNom.Font = new DevExpress.Drawing.DXFont("Segoe UI", 9F, DevExpress.Drawing.DXFontStyle.Bold);
            this.xrNom.LocationFloat = new DevExpress.Utils.PointFloat(83.33334F, 116.6667F);
            this.xrNom.Name = "xrNom";
            this.xrNom.SizeF = new System.Drawing.SizeF(625F, 16.66666F);
            // 
            // xrMonLbl
            // 
            this.xrMonLbl.Font = new DevExpress.Drawing.DXFont("Segoe UI", 8F);
            this.xrMonLbl.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(102)))), ((int)(((byte)(102)))), ((int)(((byte)(102)))));
            this.xrMonLbl.LocationFloat = new DevExpress.Utils.PointFloat(12.5F, 181F);
            this.xrMonLbl.Name = "xrMonLbl";
            this.xrMonLbl.SizeF = new System.Drawing.SizeF(62.5F, 16.66666F);
            this.xrMonLbl.StylePriority.UseTextAlignment = false;
            this.xrMonLbl.Text = "Moneda";
            this.xrMonLbl.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
            // 
            // xrMon
            // 
            this.xrMon.ExpressionBindings.AddRange(new DevExpress.XtraReports.UI.ExpressionBinding[] {
            new DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[Parameters.pMoneda]")});
            this.xrMon.LocationFloat = new DevExpress.Utils.PointFloat(87.5F, 181F);
            this.xrMon.Name = "xrMon";
            this.xrMon.SizeF = new System.Drawing.SizeF(156.25F, 16.66666F);
            this.xrMon.StylePriority.UseTextAlignment = false;
            this.xrMon.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
            // 
            // xrCuoLbl
            // 
            this.xrCuoLbl.Font = new DevExpress.Drawing.DXFont("Segoe UI", 8F);
            this.xrCuoLbl.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(102)))), ((int)(((byte)(102)))), ((int)(((byte)(102)))));
            this.xrCuoLbl.LocationFloat = new DevExpress.Utils.PointFloat(262.5F, 181F);
            this.xrCuoLbl.Name = "xrCuoLbl";
            this.xrCuoLbl.SizeF = new System.Drawing.SizeF(52.08334F, 16.66666F);
            this.xrCuoLbl.StylePriority.UseTextAlignment = false;
            this.xrCuoLbl.Text = "Cuotas";
            this.xrCuoLbl.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
            // 
            // xrCuo
            // 
            this.xrCuo.ExpressionBindings.AddRange(new DevExpress.XtraReports.UI.ExpressionBinding[] {
            new DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[Parameters.pCuotas]")});
            this.xrCuo.LocationFloat = new DevExpress.Utils.PointFloat(312.5F, 181F);
            this.xrCuo.Name = "xrCuo";
            this.xrCuo.SizeF = new System.Drawing.SizeF(52.08334F, 16.66666F);
            this.xrCuo.StylePriority.UseTextAlignment = false;
            this.xrCuo.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
            // 
            // xrValLbl
            // 
            this.xrValLbl.Font = new DevExpress.Drawing.DXFont("Segoe UI", 8F);
            this.xrValLbl.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(102)))), ((int)(((byte)(102)))), ((int)(((byte)(102)))));
            this.xrValLbl.LocationFloat = new DevExpress.Utils.PointFloat(400F, 181F);
            this.xrValLbl.Name = "xrValLbl";
            this.xrValLbl.SizeF = new System.Drawing.SizeF(72.91666F, 16.66666F);
            this.xrValLbl.StylePriority.UseTextAlignment = false;
            this.xrValLbl.Text = "Valor cuota";
            this.xrValLbl.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
            // 
            // xrVal
            // 
            this.xrVal.ExpressionBindings.AddRange(new DevExpress.XtraReports.UI.ExpressionBinding[] {
            new DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[Parameters.pValorCuota]")});
            this.xrVal.Font = new DevExpress.Drawing.DXFont("Segoe UI", 9F, DevExpress.Drawing.DXFontStyle.Bold);
            this.xrVal.LocationFloat = new DevExpress.Utils.PointFloat(475F, 181F);
            this.xrVal.Name = "xrVal";
            this.xrVal.SizeF = new System.Drawing.SizeF(125F, 16.66666F);
            this.xrVal.StylePriority.UseTextAlignment = false;
            this.xrVal.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
            this.xrVal.TextFormatString = "$ {0:N2}";
            // 
            // xrTasaLbl
            // 
            this.xrTasaLbl.Font = new DevExpress.Drawing.DXFont("Segoe UI", 8F);
            this.xrTasaLbl.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(102)))), ((int)(((byte)(102)))), ((int)(((byte)(102)))));
            this.xrTasaLbl.LocationFloat = new DevExpress.Utils.PointFloat(600F, 31.66667F);
            this.xrTasaLbl.Name = "xrTasaLbl";
            this.xrTasaLbl.SizeF = new System.Drawing.SizeF(52.08331F, 16.66666F);
            this.xrTasaLbl.StylePriority.UseTextAlignment = false;
            this.xrTasaLbl.Text = "Tasa";
            this.xrTasaLbl.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
            // 
            // xrTasa
            // 
            this.xrTasa.ExpressionBindings.AddRange(new DevExpress.XtraReports.UI.ExpressionBinding[] {
            new DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[Parameters.pTasa]")});
            this.xrTasa.LocationFloat = new DevExpress.Utils.PointFloat(662.5F, 181F);
            this.xrTasa.Name = "xrTasa";
            this.xrTasa.SizeF = new System.Drawing.SizeF(67.70837F, 16.66666F);
            this.xrTasa.StylePriority.UseTextAlignment = false;
            this.xrTasa.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
            this.xrTasa.TextFormatString = "{0:N2} %";
            // 
            // xrCuadroLbl
            // 
            this.xrCuadroLbl.Font = new DevExpress.Drawing.DXFont("Segoe UI", 10F, DevExpress.Drawing.DXFontStyle.Bold);
            this.xrCuadroLbl.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(54)))), ((int)(((byte)(128)))));
            this.xrCuadroLbl.LocationFloat = new DevExpress.Utils.PointFloat(0F, 218.75F);
            this.xrCuadroLbl.Name = "xrCuadroLbl";
            this.xrCuadroLbl.SizeF = new System.Drawing.SizeF(729.1667F, 18.75F);
            this.xrCuadroLbl.Text = "Cuadro de amortización";
            // 
            // headerTable
            // 
            this.headerTable.Font = new DevExpress.Drawing.DXFont("Segoe UI", 8.5F, DevExpress.Drawing.DXFontStyle.Bold);
            this.headerTable.LocationFloat = new DevExpress.Utils.PointFloat(0F, 241.6667F);
            this.headerTable.Name = "headerTable";
            this.headerTable.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.headerRow});
            this.headerTable.SizeF = new System.Drawing.SizeF(729.1667F, 20.83333F);
            // 
            // headerRow
            // 
            this.headerRow.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.hN,
            this.hSaldoIni,
            this.hCuota,
            this.hInteres,
            this.hAmort,
            this.hSaldo});
            this.headerRow.Name = "headerRow";
            this.headerRow.Weight = 1D;
            // 
            // hN
            // 
            this.hN.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(54)))), ((int)(((byte)(128)))));
            this.hN.ForeColor = System.Drawing.Color.White;
            this.hN.Name = "hN";
            this.hN.Padding = new DevExpress.XtraPrinting.PaddingInfo(4F, 6F, 2F, 2F, 100F);
            this.hN.Text = "N°";
            this.hN.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.hN.Weight = 0.7D;
            // 
            // hSaldoIni
            // 
            this.hSaldoIni.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(54)))), ((int)(((byte)(128)))));
            this.hSaldoIni.ForeColor = System.Drawing.Color.White;
            this.hSaldoIni.Name = "hSaldoIni";
            this.hSaldoIni.Padding = new DevExpress.XtraPrinting.PaddingInfo(4F, 6F, 2F, 2F, 100F);
            this.hSaldoIni.Text = "Saldo inicial";
            this.hSaldoIni.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
            this.hSaldoIni.Weight = 1.4D;
            // 
            // hCuota
            // 
            this.hCuota.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(54)))), ((int)(((byte)(128)))));
            this.hCuota.ForeColor = System.Drawing.Color.White;
            this.hCuota.Name = "hCuota";
            this.hCuota.Padding = new DevExpress.XtraPrinting.PaddingInfo(4F, 6F, 2F, 2F, 100F);
            this.hCuota.Text = "Cuota";
            this.hCuota.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
            this.hCuota.Weight = 1.4D;
            // 
            // hInteres
            // 
            this.hInteres.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(54)))), ((int)(((byte)(128)))));
            this.hInteres.ForeColor = System.Drawing.Color.White;
            this.hInteres.Name = "hInteres";
            this.hInteres.Padding = new DevExpress.XtraPrinting.PaddingInfo(4F, 6F, 2F, 2F, 100F);
            this.hInteres.Text = "Interés";
            this.hInteres.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
            this.hInteres.Weight = 1.3D;
            // 
            // hAmort
            // 
            this.hAmort.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(54)))), ((int)(((byte)(128)))));
            this.hAmort.ForeColor = System.Drawing.Color.White;
            this.hAmort.Name = "hAmort";
            this.hAmort.Padding = new DevExpress.XtraPrinting.PaddingInfo(4F, 6F, 2F, 2F, 100F);
            this.hAmort.Text = "Amortización";
            this.hAmort.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
            this.hAmort.Weight = 1.4D;
            // 
            // hSaldo
            // 
            this.hSaldo.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(54)))), ((int)(((byte)(128)))));
            this.hSaldo.ForeColor = System.Drawing.Color.White;
            this.hSaldo.Name = "hSaldo";
            this.hSaldo.Padding = new DevExpress.XtraPrinting.PaddingInfo(4F, 6F, 2F, 2F, 100F);
            this.hSaldo.Text = "Saldo";
            this.hSaldo.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
            this.hSaldo.Weight = 1.4D;
            // 
            // panelAfi
            // 
            this.panelAfi.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(247)))), ((int)(((byte)(248)))), ((int)(((byte)(251)))));
            this.panelAfi.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(221)))), ((int)(((byte)(221)))), ((int)(((byte)(221)))));
            this.panelAfi.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.panelAfi.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrAfiTitulo});
            this.panelAfi.LocationFloat = new DevExpress.Utils.PointFloat(0F, 66.66666F);
            this.panelAfi.Name = "panelAfi";
            this.panelAfi.SizeF = new System.Drawing.SizeF(729.1667F, 72.91666F);
            // 
            // xrAfiTitulo
            // 
            this.xrAfiTitulo.Font = new DevExpress.Drawing.DXFont("Segoe UI", 9F, DevExpress.Drawing.DXFontStyle.Bold);
            this.xrAfiTitulo.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(54)))), ((int)(((byte)(128)))));
            this.xrAfiTitulo.LocationFloat = new DevExpress.Utils.PointFloat(8.333333F, 4.166667F);
            this.xrAfiTitulo.Name = "xrAfiTitulo";
            this.xrAfiTitulo.SizeF = new System.Drawing.SizeF(312.5F, 16.66667F);
            this.xrAfiTitulo.Text = "Datos del afiliado";
            // 
            // panelPre
            // 
            this.panelPre.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(247)))), ((int)(((byte)(248)))), ((int)(((byte)(251)))));
            this.panelPre.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(221)))), ((int)(((byte)(221)))), ((int)(((byte)(221)))));
            this.panelPre.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.panelPre.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrTasaLbl,
            this.xrPreTitulo});
            this.panelPre.LocationFloat = new DevExpress.Utils.PointFloat(0F, 150F);
            this.panelPre.Name = "panelPre";
            this.panelPre.SizeF = new System.Drawing.SizeF(729.1667F, 58.33333F);
            // 
            // xrPreTitulo
            // 
            this.xrPreTitulo.Font = new DevExpress.Drawing.DXFont("Segoe UI", 9F, DevExpress.Drawing.DXFontStyle.Bold);
            this.xrPreTitulo.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(54)))), ((int)(((byte)(128)))));
            this.xrPreTitulo.LocationFloat = new DevExpress.Utils.PointFloat(8.333333F, 4.166667F);
            this.xrPreTitulo.Name = "xrPreTitulo";
            this.xrPreTitulo.SizeF = new System.Drawing.SizeF(312.5F, 16.66667F);
            this.xrPreTitulo.Text = "Datos del préstamo";
            // 
            // Detail
            // 
            this.Detail.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.detailTable});
            this.Detail.HeightF = 18.75F;
            this.Detail.HierarchyPrintOptions.Indent = 20.83333F;
            this.Detail.Name = "Detail";
            // 
            // detailTable
            // 
            this.detailTable.Font = new DevExpress.Drawing.DXFont("Segoe UI", 8.5F);
            this.detailTable.LocationFloat = new DevExpress.Utils.PointFloat(0F, 0F);
            this.detailTable.Name = "detailTable";
            this.detailTable.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.detailRow});
            this.detailTable.SizeF = new System.Drawing.SizeF(729.1667F, 18.75F);
            // 
            // detailRow
            // 
            this.detailRow.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.dN,
            this.dMonto,
            this.dCuota,
            this.dInteres,
            this.dAmort,
            this.dSaldo});
            this.detailRow.Name = "detailRow";
            this.detailRow.Weight = 1D;
            // 
            // dN
            // 
            this.dN.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(221)))), ((int)(((byte)(221)))), ((int)(((byte)(221)))));
            this.dN.Borders = DevExpress.XtraPrinting.BorderSide.Bottom;
            this.dN.ExpressionBindings.AddRange(new DevExpress.XtraReports.UI.ExpressionBinding[] {
            new DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[NroCuota]")});
            this.dN.Name = "dN";
            this.dN.Padding = new DevExpress.XtraPrinting.PaddingInfo(4F, 6F, 1F, 1F, 100F);
            this.dN.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.dN.Weight = 0.7D;
            // 
            // dMonto
            // 
            this.dMonto.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(221)))), ((int)(((byte)(221)))), ((int)(((byte)(221)))));
            this.dMonto.Borders = DevExpress.XtraPrinting.BorderSide.Bottom;
            this.dMonto.ExpressionBindings.AddRange(new DevExpress.XtraReports.UI.ExpressionBinding[] {
            new DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[Monto]")});
            this.dMonto.Name = "dMonto";
            this.dMonto.Padding = new DevExpress.XtraPrinting.PaddingInfo(4F, 6F, 1F, 1F, 100F);
            this.dMonto.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
            this.dMonto.TextFormatString = "{0:N2}";
            this.dMonto.Weight = 1.4D;
            // 
            // dCuota
            // 
            this.dCuota.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(221)))), ((int)(((byte)(221)))), ((int)(((byte)(221)))));
            this.dCuota.Borders = DevExpress.XtraPrinting.BorderSide.Bottom;
            this.dCuota.ExpressionBindings.AddRange(new DevExpress.XtraReports.UI.ExpressionBinding[] {
            new DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[ImporteCuota]")});
            this.dCuota.Name = "dCuota";
            this.dCuota.Padding = new DevExpress.XtraPrinting.PaddingInfo(4F, 6F, 1F, 1F, 100F);
            this.dCuota.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
            this.dCuota.TextFormatString = "{0:N2}";
            this.dCuota.Weight = 1.4D;
            // 
            // dInteres
            // 
            this.dInteres.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(221)))), ((int)(((byte)(221)))), ((int)(((byte)(221)))));
            this.dInteres.Borders = DevExpress.XtraPrinting.BorderSide.Bottom;
            this.dInteres.ExpressionBindings.AddRange(new DevExpress.XtraReports.UI.ExpressionBinding[] {
            new DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[Interes]")});
            this.dInteres.Name = "dInteres";
            this.dInteres.Padding = new DevExpress.XtraPrinting.PaddingInfo(4F, 6F, 1F, 1F, 100F);
            this.dInteres.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
            this.dInteres.TextFormatString = "{0:N2}";
            this.dInteres.Weight = 1.3D;
            // 
            // dAmort
            // 
            this.dAmort.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(221)))), ((int)(((byte)(221)))), ((int)(((byte)(221)))));
            this.dAmort.Borders = DevExpress.XtraPrinting.BorderSide.Bottom;
            this.dAmort.ExpressionBindings.AddRange(new DevExpress.XtraReports.UI.ExpressionBinding[] {
            new DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[Amortizacion]")});
            this.dAmort.Name = "dAmort";
            this.dAmort.Padding = new DevExpress.XtraPrinting.PaddingInfo(4F, 6F, 1F, 1F, 100F);
            this.dAmort.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
            this.dAmort.TextFormatString = "{0:N2}";
            this.dAmort.Weight = 1.4D;
            // 
            // dSaldo
            // 
            this.dSaldo.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(221)))), ((int)(((byte)(221)))), ((int)(((byte)(221)))));
            this.dSaldo.Borders = DevExpress.XtraPrinting.BorderSide.Bottom;
            this.dSaldo.ExpressionBindings.AddRange(new DevExpress.XtraReports.UI.ExpressionBinding[] {
            new DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[Saldo]")});
            this.dSaldo.Name = "dSaldo";
            this.dSaldo.Padding = new DevExpress.XtraPrinting.PaddingInfo(4F, 6F, 1F, 1F, 100F);
            this.dSaldo.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
            this.dSaldo.TextFormatString = "{0:N2}";
            this.dSaldo.Weight = 1.4D;
            // 
            // ReportFooter
            // 
            this.ReportFooter.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.totalTable,
            this.xrMontoTotalLbl,
            this.xrMontoTotal});
            this.ReportFooter.HeightF = 75F;
            this.ReportFooter.Name = "ReportFooter";
            // 
            // totalTable
            // 
            this.totalTable.Font = new DevExpress.Drawing.DXFont("Segoe UI", 8.5F, DevExpress.Drawing.DXFontStyle.Bold);
            this.totalTable.LocationFloat = new DevExpress.Utils.PointFloat(0F, 0F);
            this.totalTable.Name = "totalTable";
            this.totalTable.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.totalRow});
            this.totalTable.SizeF = new System.Drawing.SizeF(729.1667F, 22.91667F);
            // 
            // totalRow
            // 
            this.totalRow.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.tLbl,
            this.tEmpty1,
            this.tCuota,
            this.tInteres,
            this.tAmort,
            this.tEmpty2});
            this.totalRow.Name = "totalRow";
            this.totalRow.Weight = 1D;
            // 
            // tLbl
            // 
            this.tLbl.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(54)))), ((int)(((byte)(128)))));
            this.tLbl.Borders = DevExpress.XtraPrinting.BorderSide.Top;
            this.tLbl.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(54)))), ((int)(((byte)(128)))));
            this.tLbl.Name = "tLbl";
            this.tLbl.Padding = new DevExpress.XtraPrinting.PaddingInfo(4F, 6F, 2F, 2F, 100F);
            this.tLbl.Text = "Totales";
            this.tLbl.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            this.tLbl.Weight = 0.7D;
            // 
            // tEmpty1
            // 
            this.tEmpty1.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(54)))), ((int)(((byte)(128)))));
            this.tEmpty1.Borders = DevExpress.XtraPrinting.BorderSide.Top;
            this.tEmpty1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(54)))), ((int)(((byte)(128)))));
            this.tEmpty1.Name = "tEmpty1";
            this.tEmpty1.Padding = new DevExpress.XtraPrinting.PaddingInfo(4F, 6F, 2F, 2F, 100F);
            this.tEmpty1.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
            this.tEmpty1.Weight = 1.4D;
            // 
            // tCuota
            // 
            this.tCuota.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(54)))), ((int)(((byte)(128)))));
            this.tCuota.Borders = DevExpress.XtraPrinting.BorderSide.Top;
            this.tCuota.ExpressionBindings.AddRange(new DevExpress.XtraReports.UI.ExpressionBinding[] {
            new DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "sumSum([ImporteCuota])")});
            this.tCuota.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(54)))), ((int)(((byte)(128)))));
            this.tCuota.Name = "tCuota";
            this.tCuota.Padding = new DevExpress.XtraPrinting.PaddingInfo(4F, 6F, 2F, 2F, 100F);
            this.tCuota.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
            this.tCuota.TextFormatString = "{0:N2}";
            this.tCuota.Weight = 1.4D;
            // 
            // tInteres
            // 
            this.tInteres.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(54)))), ((int)(((byte)(128)))));
            this.tInteres.Borders = DevExpress.XtraPrinting.BorderSide.Top;
            this.tInteres.ExpressionBindings.AddRange(new DevExpress.XtraReports.UI.ExpressionBinding[] {
            new DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "sumSum([Interes])")});
            this.tInteres.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(54)))), ((int)(((byte)(128)))));
            this.tInteres.Name = "tInteres";
            this.tInteres.Padding = new DevExpress.XtraPrinting.PaddingInfo(4F, 6F, 2F, 2F, 100F);
            this.tInteres.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
            this.tInteres.TextFormatString = "{0:N2}";
            this.tInteres.Weight = 1.3D;
            // 
            // tAmort
            // 
            this.tAmort.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(54)))), ((int)(((byte)(128)))));
            this.tAmort.Borders = DevExpress.XtraPrinting.BorderSide.Top;
            this.tAmort.ExpressionBindings.AddRange(new DevExpress.XtraReports.UI.ExpressionBinding[] {
            new DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "sumSum([Amortizacion])")});
            this.tAmort.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(54)))), ((int)(((byte)(128)))));
            this.tAmort.Name = "tAmort";
            this.tAmort.Padding = new DevExpress.XtraPrinting.PaddingInfo(4F, 6F, 2F, 2F, 100F);
            this.tAmort.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
            this.tAmort.TextFormatString = "{0:N2}";
            this.tAmort.Weight = 1.4D;
            // 
            // tEmpty2
            // 
            this.tEmpty2.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(54)))), ((int)(((byte)(128)))));
            this.tEmpty2.Borders = DevExpress.XtraPrinting.BorderSide.Top;
            this.tEmpty2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(54)))), ((int)(((byte)(128)))));
            this.tEmpty2.Name = "tEmpty2";
            this.tEmpty2.Padding = new DevExpress.XtraPrinting.PaddingInfo(4F, 6F, 2F, 2F, 100F);
            this.tEmpty2.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
            this.tEmpty2.Weight = 1.4D;
            // 
            // xrMontoTotalLbl
            // 
            this.xrMontoTotalLbl.Font = new DevExpress.Drawing.DXFont("Segoe UI", 10F, DevExpress.Drawing.DXFontStyle.Bold);
            this.xrMontoTotalLbl.LocationFloat = new DevExpress.Utils.PointFloat(0F, 41.66667F);
            this.xrMontoTotalLbl.Name = "xrMontoTotalLbl";
            this.xrMontoTotalLbl.SizeF = new System.Drawing.SizeF(312.5F, 18.75F);
            this.xrMontoTotalLbl.Text = "Monto total del préstamo";
            // 
            // xrMontoTotal
            // 
            this.xrMontoTotal.ExpressionBindings.AddRange(new DevExpress.XtraReports.UI.ExpressionBinding[] {
            new DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[Parameters.pMontoTotal]")});
            this.xrMontoTotal.Font = new DevExpress.Drawing.DXFont("Segoe UI", 13F, DevExpress.Drawing.DXFontStyle.Bold);
            this.xrMontoTotal.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(54)))), ((int)(((byte)(128)))));
            this.xrMontoTotal.LocationFloat = new DevExpress.Utils.PointFloat(500F, 39.58333F);
            this.xrMontoTotal.Name = "xrMontoTotal";
            this.xrMontoTotal.SizeF = new System.Drawing.SizeF(229.1667F, 22.91667F);
            this.xrMontoTotal.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
            this.xrMontoTotal.TextFormatString = "$ {0:N2}";
            // 
            // PageFooter
            // 
            this.PageFooter.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrPie,
            this.xrPageInfo});
            this.PageFooter.HeightF = 27.08333F;
            this.PageFooter.Name = "PageFooter";
            // 
            // xrPie
            // 
            this.xrPie.Font = new DevExpress.Drawing.DXFont("Segoe UI", 7F);
            this.xrPie.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(102)))), ((int)(((byte)(102)))), ((int)(((byte)(102)))));
            this.xrPie.LocationFloat = new DevExpress.Utils.PointFloat(0F, 6.25F);
            this.xrPie.Name = "xrPie";
            this.xrPie.SizeF = new System.Drawing.SizeF(312.5F, 14.58333F);
            this.xrPie.Text = "CASEMED — Sistema de Gestión";
            // 
            // xrPageInfo
            // 
            this.xrPageInfo.Font = new DevExpress.Drawing.DXFont("Segoe UI", 7F);
            this.xrPageInfo.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(102)))), ((int)(((byte)(102)))), ((int)(((byte)(102)))));
            this.xrPageInfo.LocationFloat = new DevExpress.Utils.PointFloat(520.8333F, 6.25F);
            this.xrPageInfo.Name = "xrPageInfo";
            this.xrPageInfo.SizeF = new System.Drawing.SizeF(208.3334F, 14.58333F);
            this.xrPageInfo.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
            this.xrPageInfo.TextFormatString = "Página {0} de {1}";
            // 
            // objectDataSource1
            // 
            this.objectDataSource1.DataSource = typeof(global::Sgpa.Web.Reporting.Predefinidos.PrestamoCuadroLinea);
            this.objectDataSource1.Name = "objectDataSource1";
            // 
            // FichaReport
            // 
            this.Bands.AddRange(new DevExpress.XtraReports.UI.Band[] {
            this.TopMargin,
            this.BottomMargin,
            this.ReportHeader,
            this.Detail,
            this.ReportFooter,
            this.PageFooter});
            this.ComponentStorage.AddRange(new System.ComponentModel.IComponent[] {
            this.objectDataSource1});
            this.DataSource = this.objectDataSource1;
            this.Font = new DevExpress.Drawing.DXFont("Segoe UI", 9F);
            this.Margins = new DevExpress.Drawing.DXMargins(40F, 40F, 41.66667F, 41.66667F);
            this.PageHeightF = 1169.291F;
            this.PageWidthF = 826.7717F;
            this.PaperKind = DevExpress.Drawing.Printing.DXPaperKind.A4;
            this.Parameters.AddRange(new DevExpress.XtraReports.Parameters.Parameter[] {
            this.pNum,
            this.pFecha,
            this.pCI,
            this.pNombre,
            this.pImponibles,
            this.pMoneda,
            this.pCuotas,
            this.pValorCuota,
            this.pTasa,
            this.pMontoTotal});
            this.Version = "25.2";
            ((System.ComponentModel.ISupportInitialize)(this.headerTable)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.detailTable)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.totalTable)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.objectDataSource1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();

        }
        #endregion

        private DevExpress.XtraReports.UI.TopMarginBand TopMargin;
        private DevExpress.XtraReports.UI.BottomMarginBand BottomMargin;
        private DevExpress.XtraReports.UI.ReportHeaderBand ReportHeader;
        private DevExpress.XtraReports.UI.DetailBand Detail;
        private DevExpress.XtraReports.UI.ReportFooterBand ReportFooter;
        private DevExpress.XtraReports.UI.PageFooterBand PageFooter;
        internal DevExpress.XtraReports.UI.XRPictureBox xrLogo;
        private DevExpress.XtraReports.UI.XRLabel xrTitulo;
        private DevExpress.XtraReports.UI.XRLine xrLinea;
        private DevExpress.XtraReports.UI.XRLabel xrNumLbl;
        private DevExpress.XtraReports.UI.XRLabel xrNum;
        private DevExpress.XtraReports.UI.XRLabel xrFechaLbl;
        private DevExpress.XtraReports.UI.XRLabel xrFecha;
        private DevExpress.XtraReports.UI.XRPanel panelAfi;
        private DevExpress.XtraReports.UI.XRLabel xrAfiTitulo;
        private DevExpress.XtraReports.UI.XRLabel xrCILbl;
        private DevExpress.XtraReports.UI.XRLabel xrCI;
        private DevExpress.XtraReports.UI.XRLabel xrImpLbl;
        private DevExpress.XtraReports.UI.XRLabel xrImp;
        private DevExpress.XtraReports.UI.XRLabel xrNomLbl;
        private DevExpress.XtraReports.UI.XRLabel xrNom;
        private DevExpress.XtraReports.UI.XRPanel panelPre;
        private DevExpress.XtraReports.UI.XRLabel xrPreTitulo;
        private DevExpress.XtraReports.UI.XRLabel xrMonLbl;
        private DevExpress.XtraReports.UI.XRLabel xrMon;
        private DevExpress.XtraReports.UI.XRLabel xrCuoLbl;
        private DevExpress.XtraReports.UI.XRLabel xrCuo;
        private DevExpress.XtraReports.UI.XRLabel xrValLbl;
        private DevExpress.XtraReports.UI.XRLabel xrVal;
        private DevExpress.XtraReports.UI.XRLabel xrTasaLbl;
        private DevExpress.XtraReports.UI.XRLabel xrTasa;
        private DevExpress.XtraReports.UI.XRLabel xrCuadroLbl;
        private DevExpress.XtraReports.UI.XRTable headerTable;
        private DevExpress.XtraReports.UI.XRTableRow headerRow;
        private DevExpress.XtraReports.UI.XRTableCell hN;
        private DevExpress.XtraReports.UI.XRTableCell hSaldoIni;
        private DevExpress.XtraReports.UI.XRTableCell hCuota;
        private DevExpress.XtraReports.UI.XRTableCell hInteres;
        private DevExpress.XtraReports.UI.XRTableCell hAmort;
        private DevExpress.XtraReports.UI.XRTableCell hSaldo;
        private DevExpress.XtraReports.UI.XRTable detailTable;
        private DevExpress.XtraReports.UI.XRTableRow detailRow;
        private DevExpress.XtraReports.UI.XRTableCell dN;
        private DevExpress.XtraReports.UI.XRTableCell dMonto;
        private DevExpress.XtraReports.UI.XRTableCell dCuota;
        private DevExpress.XtraReports.UI.XRTableCell dInteres;
        private DevExpress.XtraReports.UI.XRTableCell dAmort;
        private DevExpress.XtraReports.UI.XRTableCell dSaldo;
        private DevExpress.XtraReports.UI.XRTable totalTable;
        private DevExpress.XtraReports.UI.XRTableRow totalRow;
        private DevExpress.XtraReports.UI.XRTableCell tLbl;
        private DevExpress.XtraReports.UI.XRTableCell tEmpty1;
        private DevExpress.XtraReports.UI.XRTableCell tCuota;
        private DevExpress.XtraReports.UI.XRTableCell tInteres;
        private DevExpress.XtraReports.UI.XRTableCell tAmort;
        private DevExpress.XtraReports.UI.XRTableCell tEmpty2;
        private DevExpress.XtraReports.UI.XRLabel xrMontoTotalLbl;
        private DevExpress.XtraReports.UI.XRLabel xrMontoTotal;
        private DevExpress.XtraReports.UI.XRLabel xrPie;
        private DevExpress.XtraReports.UI.XRPageInfo xrPageInfo;
        private DevExpress.DataAccess.ObjectBinding.ObjectDataSource objectDataSource1;
        private DevExpress.XtraReports.Parameters.Parameter pNum;
        private DevExpress.XtraReports.Parameters.Parameter pFecha;
        private DevExpress.XtraReports.Parameters.Parameter pCI;
        private DevExpress.XtraReports.Parameters.Parameter pNombre;
        private DevExpress.XtraReports.Parameters.Parameter pImponibles;
        private DevExpress.XtraReports.Parameters.Parameter pMoneda;
        private DevExpress.XtraReports.Parameters.Parameter pCuotas;
        private DevExpress.XtraReports.Parameters.Parameter pValorCuota;
        private DevExpress.XtraReports.Parameters.Parameter pTasa;
        private DevExpress.XtraReports.Parameters.Parameter pMontoTotal;
    }
}
