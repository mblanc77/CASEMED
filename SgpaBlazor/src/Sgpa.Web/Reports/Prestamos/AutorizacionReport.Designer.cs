namespace Sgpa.Web.Reports.Prestamos
{
    partial class AutorizacionReport
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
            this.xrSocio = new DevExpress.XtraReports.UI.XRLabel();
            this.xrPrestamoLbl = new DevExpress.XtraReports.UI.XRLabel();
            this.xrPrestamo = new DevExpress.XtraReports.UI.XRLabel();
            this.xrFecha = new DevExpress.XtraReports.UI.XRLabel();
            this.xrSr = new DevExpress.XtraReports.UI.XRLabel();
            this.xrConsid = new DevExpress.XtraReports.UI.XRLabel();
            this.xrIntro = new DevExpress.XtraReports.UI.XRLabel();
            this.xrNum1 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrCl1 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrNum2 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrCl2 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrNum3 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrCl3 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrNum4 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrCl4 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrCierre = new DevExpress.XtraReports.UI.XRLabel();
            this.xrFirmaLinea = new DevExpress.XtraReports.UI.XRLine();
            this.xrFirma = new DevExpress.XtraReports.UI.XRLabel();
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
            // Detail
            //
            this.Detail.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
                this.xrLogo, this.xrTitulo, this.xrLinea, this.xrSocio, this.xrPrestamoLbl, this.xrPrestamo,
                this.xrFecha, this.xrSr, this.xrConsid, this.xrIntro, this.xrNum1, this.xrCl1, this.xrNum2,
                this.xrCl2, this.xrNum3, this.xrCl3, this.xrNum4, this.xrCl4, this.xrCierre, this.xrFirmaLinea, this.xrFirma});
            this.Detail.Dpi = 96F;
            this.Detail.HeightF = 690F;
            this.Detail.Name = "Detail";
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
            this.xrTitulo.LocationFloat = new DevExpress.Utils.PointFloat(0F, 10F);
            this.xrTitulo.Name = "xrTitulo";
            this.xrTitulo.SizeF = new System.Drawing.SizeF(714F, 26F);
            this.xrTitulo.Text = "Autorización de descuento";
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
            // xrSocio
            //
            this.xrSocio.Dpi = 96F;
            this.xrSocio.ExpressionBindings.AddRange(new DevExpress.XtraReports.UI.ExpressionBinding[] {
                new DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "'SOCIO:  ' + [NombreCompleto] + ';  C.I. ' + [CIFormato]")});
            this.xrSocio.Font = new DevExpress.Drawing.DXFont("Segoe UI", 10F, DevExpress.Drawing.DXFontStyle.Bold);
            this.xrSocio.LocationFloat = new DevExpress.Utils.PointFloat(0F, 70F);
            this.xrSocio.Name = "xrSocio";
            this.xrSocio.SizeF = new System.Drawing.SizeF(714F, 18F);
            //
            // xrPrestamoLbl
            //
            this.xrPrestamoLbl.Dpi = 96F;
            this.xrPrestamoLbl.Font = new DevExpress.Drawing.DXFont("Segoe UI", 8F);
            this.xrPrestamoLbl.ForeColor = System.Drawing.Color.FromArgb(102, 102, 102);
            this.xrPrestamoLbl.LocationFloat = new DevExpress.Utils.PointFloat(484F, 96F);
            this.xrPrestamoLbl.Name = "xrPrestamoLbl";
            this.xrPrestamoLbl.SizeF = new System.Drawing.SizeF(130F, 16F);
            this.xrPrestamoLbl.Text = "PRÉSTAMO Nº";
            this.xrPrestamoLbl.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
            //
            // xrPrestamo
            //
            this.xrPrestamo.Dpi = 96F;
            this.xrPrestamo.ExpressionBindings.AddRange(new DevExpress.XtraReports.UI.ExpressionBinding[] {
                new DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[IDPrestamo]")});
            this.xrPrestamo.Font = new DevExpress.Drawing.DXFont("Segoe UI", 11F, DevExpress.Drawing.DXFontStyle.Bold);
            this.xrPrestamo.ForeColor = System.Drawing.Color.FromArgb(28, 54, 128);
            this.xrPrestamo.LocationFloat = new DevExpress.Utils.PointFloat(619F, 94F);
            this.xrPrestamo.Name = "xrPrestamo";
            this.xrPrestamo.SizeF = new System.Drawing.SizeF(95F, 18F);
            this.xrPrestamo.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
            //
            // xrFecha
            //
            this.xrFecha.Dpi = 96F;
            this.xrFecha.ExpressionBindings.AddRange(new DevExpress.XtraReports.UI.ExpressionBinding[] {
                new DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "'Montevideo, ' + [FechaLarga] + '.'")});
            this.xrFecha.Dpi = 96F;
            this.xrFecha.LocationFloat = new DevExpress.Utils.PointFloat(394F, 124F);
            this.xrFecha.Name = "xrFecha";
            this.xrFecha.SizeF = new System.Drawing.SizeF(320F, 16F);
            this.xrFecha.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
            //
            // xrSr
            //
            this.xrSr.Dpi = 96F;
            this.xrSr.LocationFloat = new DevExpress.Utils.PointFloat(0F, 124F);
            this.xrSr.Multiline = true;
            this.xrSr.Name = "xrSr";
            this.xrSr.SizeF = new System.Drawing.SizeF(200F, 52F);
            this.xrSr.Text = "Sr.\r\nGerente de\r\nCASEMED";
            //
            // xrConsid
            //
            this.xrConsid.Dpi = 96F;
            this.xrConsid.LocationFloat = new DevExpress.Utils.PointFloat(0F, 188F);
            this.xrConsid.Name = "xrConsid";
            this.xrConsid.SizeF = new System.Drawing.SizeF(714F, 16F);
            this.xrConsid.Text = "De mi consideración:";
            //
            // xrIntro
            //
            this.xrIntro.Dpi = 96F;
            this.xrIntro.LocationFloat = new DevExpress.Utils.PointFloat(0F, 214F);
            this.xrIntro.Multiline = true;
            this.xrIntro.Name = "xrIntro";
            this.xrIntro.SizeF = new System.Drawing.SizeF(714F, 18F);
            this.xrIntro.Text = "En virtud de lo dispuesto por el Reglamento de Préstamos vigente que conozco, expreso que:";
            //
            // cláusulas (números + textos)
            //
            this.xrNum1.Dpi = 96F;
            this.xrNum1.Font = new DevExpress.Drawing.DXFont("Segoe UI", 9.5F, DevExpress.Drawing.DXFontStyle.Bold);
            this.xrNum1.LocationFloat = new DevExpress.Utils.PointFloat(0F, 244F);
            this.xrNum1.Name = "xrNum1";
            this.xrNum1.SizeF = new System.Drawing.SizeF(28F, 16F);
            this.xrNum1.Text = "1.";
            this.xrCl1.Dpi = 96F;
            this.xrCl1.LocationFloat = new DevExpress.Utils.PointFloat(34F, 244F);
            this.xrCl1.Multiline = true;
            this.xrCl1.Name = "xrCl1";
            this.xrCl1.SizeF = new System.Drawing.SizeF(680F, 56F);
            this.xrCl1.Text = "Sin perjuicio del sistema de cobranza contratado, autorizo a CASEMED, siempre que esta Institución lo estimara conveniente, a debitar en cuenta bancaria las cuotas del préstamo de referencia y sus ilíquidos, comprometiéndome a brindar toda la información que me solicitare.";
            this.xrNum2.Dpi = 96F;
            this.xrNum2.Font = new DevExpress.Drawing.DXFont("Segoe UI", 9.5F, DevExpress.Drawing.DXFontStyle.Bold);
            this.xrNum2.LocationFloat = new DevExpress.Utils.PointFloat(0F, 310F);
            this.xrNum2.Name = "xrNum2";
            this.xrNum2.SizeF = new System.Drawing.SizeF(28F, 16F);
            this.xrNum2.Text = "2.";
            this.xrCl2.Dpi = 96F;
            this.xrCl2.LocationFloat = new DevExpress.Utils.PointFloat(34F, 310F);
            this.xrCl2.Multiline = true;
            this.xrCl2.Name = "xrCl2";
            this.xrCl2.SizeF = new System.Drawing.SizeF(680F, 104F);
            this.xrCl2.Text = "De igual manera, autorizo a que los eventuales saldos impagos que hubiere sean descontados de los haberes que tuviere a mi favor en cualquiera de las instituciones incorporadas a CASEMED, por cualquier concepto, incluyendo la liquidación final en caso de egreso. Autorizo a que las cuotas que fueren canceladas a través de débito en cuentas en pesos uruguayos, así como las que se retuvieran de haberes laborales, se acrediten, para el caso de que el préstamo fuere en moneda extranjera, al tipo de cambio vendedor de la institución financiera de que se trate o al tipo de cambio vendedor en pizarra del Banco de la República Oriental del Uruguay, correspondiente al cierre del día de ingreso efectivo a CASEMED.";
            this.xrNum3.Dpi = 96F;
            this.xrNum3.Font = new DevExpress.Drawing.DXFont("Segoe UI", 9.5F, DevExpress.Drawing.DXFontStyle.Bold);
            this.xrNum3.LocationFloat = new DevExpress.Utils.PointFloat(0F, 424F);
            this.xrNum3.Name = "xrNum3";
            this.xrNum3.SizeF = new System.Drawing.SizeF(28F, 16F);
            this.xrNum3.Text = "3.";
            this.xrCl3.Dpi = 96F;
            this.xrCl3.LocationFloat = new DevExpress.Utils.PointFloat(34F, 424F);
            this.xrCl3.Multiline = true;
            this.xrCl3.Name = "xrCl3";
            this.xrCl3.SizeF = new System.Drawing.SizeF(680F, 48F);
            this.xrCl3.Text = "Los saldos exigibles y pendientes de pago que hubiere serán compensados automáticamente, en el monto concurrente, con las sumas que, por cualquier concepto, pudiere haber generado con cargo a CASEMED.";
            this.xrNum4.Dpi = 96F;
            this.xrNum4.Font = new DevExpress.Drawing.DXFont("Segoe UI", 9.5F, DevExpress.Drawing.DXFontStyle.Bold);
            this.xrNum4.LocationFloat = new DevExpress.Utils.PointFloat(0F, 482F);
            this.xrNum4.Name = "xrNum4";
            this.xrNum4.SizeF = new System.Drawing.SizeF(28F, 16F);
            this.xrNum4.Text = "4.";
            this.xrCl4.Dpi = 96F;
            this.xrCl4.LocationFloat = new DevExpress.Utils.PointFloat(34F, 482F);
            this.xrCl4.Multiline = true;
            this.xrCl4.Name = "xrCl4";
            this.xrCl4.SizeF = new System.Drawing.SizeF(680F, 40F);
            this.xrCl4.Text = "Todas las referencias precedentes a CASEMED deberán entenderse hechas, asimismo, a cualquier entidad que la sucediera a cualquier título.";
            //
            // xrCierre
            //
            this.xrCierre.Dpi = 96F;
            this.xrCierre.LocationFloat = new DevExpress.Utils.PointFloat(0F, 538F);
            this.xrCierre.Name = "xrCierre";
            this.xrCierre.SizeF = new System.Drawing.SizeF(714F, 18F);
            this.xrCierre.Text = "Sin otro particular, saluda a usted atentamente,";
            //
            // xrFirmaLinea / xrFirma
            //
            this.xrFirmaLinea.Dpi = 96F;
            this.xrFirmaLinea.LocationFloat = new DevExpress.Utils.PointFloat(247F, 648F);
            this.xrFirmaLinea.Name = "xrFirmaLinea";
            this.xrFirmaLinea.SizeF = new System.Drawing.SizeF(220F, 2F);
            this.xrFirma.Dpi = 96F;
            this.xrFirma.Font = new DevExpress.Drawing.DXFont("Segoe UI", 8F);
            this.xrFirma.ForeColor = System.Drawing.Color.FromArgb(102, 102, 102);
            this.xrFirma.LocationFloat = new DevExpress.Utils.PointFloat(247F, 651F);
            this.xrFirma.Name = "xrFirma";
            this.xrFirma.SizeF = new System.Drawing.SizeF(220F, 14F);
            this.xrFirma.Text = "FIRMA";
            this.xrFirma.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter;
            //
            // objectDataSource1
            //
            this.objectDataSource1.DataSource = typeof(global::Sgpa.Web.Reporting.Predefinidos.PrestamoAutorizacionData);
            this.objectDataSource1.Name = "objectDataSource1";
            //
            // AutorizacionReport
            //
            this.Bands.AddRange(new DevExpress.XtraReports.UI.Band[] { this.TopMargin, this.BottomMargin, this.Detail });
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
        private DevExpress.XtraReports.UI.XRLabel xrSocio;
        private DevExpress.XtraReports.UI.XRLabel xrPrestamoLbl;
        private DevExpress.XtraReports.UI.XRLabel xrPrestamo;
        private DevExpress.XtraReports.UI.XRLabel xrFecha;
        private DevExpress.XtraReports.UI.XRLabel xrSr;
        private DevExpress.XtraReports.UI.XRLabel xrConsid;
        private DevExpress.XtraReports.UI.XRLabel xrIntro;
        private DevExpress.XtraReports.UI.XRLabel xrNum1;
        private DevExpress.XtraReports.UI.XRLabel xrCl1;
        private DevExpress.XtraReports.UI.XRLabel xrNum2;
        private DevExpress.XtraReports.UI.XRLabel xrCl2;
        private DevExpress.XtraReports.UI.XRLabel xrNum3;
        private DevExpress.XtraReports.UI.XRLabel xrCl3;
        private DevExpress.XtraReports.UI.XRLabel xrNum4;
        private DevExpress.XtraReports.UI.XRLabel xrCl4;
        private DevExpress.XtraReports.UI.XRLabel xrCierre;
        private DevExpress.XtraReports.UI.XRLine xrFirmaLinea;
        private DevExpress.XtraReports.UI.XRLabel xrFirma;
        private DevExpress.DataAccess.ObjectBinding.ObjectDataSource objectDataSource1;
    }
}
