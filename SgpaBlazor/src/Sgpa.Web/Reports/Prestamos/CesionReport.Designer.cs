namespace Sgpa.Web.Reports.Prestamos
{
    partial class CesionReport
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
            this.xrIntro = new DevExpress.XtraReports.UI.XRLabel();
            this.xrP1Lbl = new DevExpress.XtraReports.UI.XRLabel();
            this.xrP1 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrP2Lbl = new DevExpress.XtraReports.UI.XRLabel();
            this.xrP2 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrP3Lbl = new DevExpress.XtraReports.UI.XRLabel();
            this.xrP3 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrP4Lbl = new DevExpress.XtraReports.UI.XRLabel();
            this.xrP4 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrP5Lbl = new DevExpress.XtraReports.UI.XRLabel();
            this.xrP5 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrP6Lbl = new DevExpress.XtraReports.UI.XRLabel();
            this.xrP6 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrConstancia = new DevExpress.XtraReports.UI.XRLabel();
            this.xrFirmaCedente = new DevExpress.XtraReports.UI.XRLine();
            this.xrFirmaCedenteLbl = new DevExpress.XtraReports.UI.XRLabel();
            this.xrFirmaCasemed = new DevExpress.XtraReports.UI.XRLine();
            this.xrFirmaCasemedLbl = new DevExpress.XtraReports.UI.XRLabel();
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
                this.xrLogo, this.xrTitulo, this.xrLinea, this.xrIntro, this.xrP1Lbl, this.xrP1, this.xrP2Lbl,
                this.xrP2, this.xrP3Lbl, this.xrP3, this.xrP4Lbl, this.xrP4, this.xrP5Lbl, this.xrP5, this.xrP6Lbl,
                this.xrP6, this.xrConstancia, this.xrFirmaCedente, this.xrFirmaCedenteLbl, this.xrFirmaCasemed, this.xrFirmaCasemedLbl});
            this.Detail.Dpi = 96F;
            this.Detail.HeightF = 660F;
            this.Detail.Name = "Detail";
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
            this.xrTitulo.LocationFloat = new DevExpress.Utils.PointFloat(0F, 10F);
            this.xrTitulo.Name = "xrTitulo";
            this.xrTitulo.SizeF = new System.Drawing.SizeF(714F, 26F);
            this.xrTitulo.Text = "Cesión de créditos";
            this.xrTitulo.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter;
            this.xrLinea.Dpi = 96F;
            this.xrLinea.ForeColor = System.Drawing.Color.FromArgb(28, 54, 128);
            this.xrLinea.LocationFloat = new DevExpress.Utils.PointFloat(0F, 46F);
            this.xrLinea.Name = "xrLinea";
            this.xrLinea.SizeF = new System.Drawing.SizeF(714F, 2F);
            //
            // xrIntro
            //
            this.xrIntro.Dpi = 96F;
            this.xrIntro.ExpressionBindings.AddRange(new DevExpress.XtraReports.UI.ExpressionBinding[] {
                new DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "'En la ciudad de Montevideo, el día ' + [FechaLarga] + '. PRESENTES: POR UNA PARTE: ' + [NombreCompleto] + ', titular de la C.I. ' + [CIFormato] + ', con domicilio en ' + [Direccion] + ' (en adelante el cedente) y POR OTRA PARTE, Dr. José Luis Iraola y la Cra. Luisa Otero en representación de la Caja de Auxilio y Seguro Convencional Médico (CASEMED), con domicilio en Arenal Grande 1676, acuerdan lo siguiente:'")});
            this.xrIntro.LocationFloat = new DevExpress.Utils.PointFloat(0F, 70F);
            this.xrIntro.Multiline = true;
            this.xrIntro.Name = "xrIntro";
            this.xrIntro.SizeF = new System.Drawing.SizeF(714F, 60F);
            //
            // PRIMERO
            //
            this.xrP1Lbl.Dpi = 96F;
            this.xrP1Lbl.Font = new DevExpress.Drawing.DXFont("Segoe UI", 9.5F, DevExpress.Drawing.DXFontStyle.Bold);
            this.xrP1Lbl.ForeColor = System.Drawing.Color.FromArgb(28, 54, 128);
            this.xrP1Lbl.LocationFloat = new DevExpress.Utils.PointFloat(0F, 138F);
            this.xrP1Lbl.Name = "xrP1Lbl";
            this.xrP1Lbl.SizeF = new System.Drawing.SizeF(90F, 16F);
            this.xrP1Lbl.Text = "PRIMERO:";
            this.xrP1.Dpi = 96F;
            this.xrP1.ExpressionBindings.AddRange(new DevExpress.XtraReports.UI.ExpressionBinding[] {
                new DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "'I) El cedente es titular de un crédito contra el fideicomiso denominado \"FIDEICOMISO SALARIAL PARA LOS TRABAJADORES DEL CASMU IAMPP SIN FINES DE LUCRO\". II) El cedente declara que a la fecha el crédito de referencia asciende a la suma de $..........................., el cual se encuentra libre de gravámenes o afectaciones de cualquier tipo. III) En el día de la fecha CASEMED abonó al cedente un préstamo por la suma de $ ' + [ImporteFmt] + ', lo que se documenta por separado.'")});
            this.xrP1.LocationFloat = new DevExpress.Utils.PointFloat(94F, 138F);
            this.xrP1.Multiline = true;
            this.xrP1.Name = "xrP1";
            this.xrP1.SizeF = new System.Drawing.SizeF(620F, 76F);
            //
            // SEGUNDO
            //
            this.xrP2Lbl.Dpi = 96F;
            this.xrP2Lbl.Font = new DevExpress.Drawing.DXFont("Segoe UI", 9.5F, DevExpress.Drawing.DXFontStyle.Bold);
            this.xrP2Lbl.ForeColor = System.Drawing.Color.FromArgb(28, 54, 128);
            this.xrP2Lbl.LocationFloat = new DevExpress.Utils.PointFloat(0F, 222F);
            this.xrP2Lbl.Name = "xrP2Lbl";
            this.xrP2Lbl.SizeF = new System.Drawing.SizeF(90F, 16F);
            this.xrP2Lbl.Text = "SEGUNDO:";
            this.xrP2.Dpi = 96F;
            this.xrP2.LocationFloat = new DevExpress.Utils.PointFloat(94F, 222F);
            this.xrP2.Multiline = true;
            this.xrP2.Name = "xrP2";
            this.xrP2.SizeF = new System.Drawing.SizeF(620F, 44F);
            this.xrP2.Text = "El cedente cede a favor de CASEMED, libre de afectaciones o gravámenes, quien en tal concepto adquiere, la totalidad de su crédito contra el fideicomiso referido, hasta la total cancelación del crédito otorgado por Casemed.";
            //
            // TERCERO
            //
            this.xrP3Lbl.Dpi = 96F;
            this.xrP3Lbl.Font = new DevExpress.Drawing.DXFont("Segoe UI", 9.5F, DevExpress.Drawing.DXFontStyle.Bold);
            this.xrP3Lbl.ForeColor = System.Drawing.Color.FromArgb(28, 54, 128);
            this.xrP3Lbl.LocationFloat = new DevExpress.Utils.PointFloat(0F, 274F);
            this.xrP3Lbl.Name = "xrP3Lbl";
            this.xrP3Lbl.SizeF = new System.Drawing.SizeF(90F, 16F);
            this.xrP3Lbl.Text = "TERCERO:";
            this.xrP3.Dpi = 96F;
            this.xrP3.LocationFloat = new DevExpress.Utils.PointFloat(94F, 274F);
            this.xrP3.Multiline = true;
            this.xrP3.Name = "xrP3";
            this.xrP3.SizeF = new System.Drawing.SizeF(620F, 64F);
            this.xrP3.Text = "En señal de tradición el cedente coloca al cesionario en su mismo lugar, grado y prelación respecto de los derechos cedidos y lo faculta a usar de los mismos a su vista y paciencia y a ejercer todas las acciones que le competen para el cobro del crédito. El cedente asegura la existencia y legitimidad de los derechos cedidos y se obliga a no gravarlos o cederlos de manera alguna que pueda afectar los derechos cedidos.";
            //
            // CUARTO
            //
            this.xrP4Lbl.Dpi = 96F;
            this.xrP4Lbl.Font = new DevExpress.Drawing.DXFont("Segoe UI", 9.5F, DevExpress.Drawing.DXFontStyle.Bold);
            this.xrP4Lbl.ForeColor = System.Drawing.Color.FromArgb(28, 54, 128);
            this.xrP4Lbl.LocationFloat = new DevExpress.Utils.PointFloat(0F, 346F);
            this.xrP4Lbl.Name = "xrP4Lbl";
            this.xrP4Lbl.SizeF = new System.Drawing.SizeF(90F, 16F);
            this.xrP4Lbl.Text = "CUARTO:";
            this.xrP4.Dpi = 96F;
            this.xrP4.LocationFloat = new DevExpress.Utils.PointFloat(94F, 346F);
            this.xrP4.Multiline = true;
            this.xrP4.Name = "xrP4";
            this.xrP4.SizeF = new System.Drawing.SizeF(620F, 56F);
            this.xrP4.Text = "Se acuerda que esta cesión se notifique al fiduciario del fideicomiso \"FIDEICOMISO SALARIAL PARA LOS TRABAJADORES DEL CASMU IAMPP SIN FINES DE LUCRO\", absteniéndose el cedente a partir de la fecha a hacer cobro de naturaleza alguna contra el fideicomiso, hasta la total extinción de la obligación referida en el numeral III de la cláusula primera.";
            //
            // QUINTO
            //
            this.xrP5Lbl.Dpi = 96F;
            this.xrP5Lbl.Font = new DevExpress.Drawing.DXFont("Segoe UI", 9.5F, DevExpress.Drawing.DXFontStyle.Bold);
            this.xrP5Lbl.ForeColor = System.Drawing.Color.FromArgb(28, 54, 128);
            this.xrP5Lbl.LocationFloat = new DevExpress.Utils.PointFloat(0F, 410F);
            this.xrP5Lbl.Name = "xrP5Lbl";
            this.xrP5Lbl.SizeF = new System.Drawing.SizeF(90F, 16F);
            this.xrP5Lbl.Text = "QUINTO:";
            this.xrP5.Dpi = 96F;
            this.xrP5.LocationFloat = new DevExpress.Utils.PointFloat(94F, 410F);
            this.xrP5.Multiline = true;
            this.xrP5.Name = "xrP5";
            this.xrP5.SizeF = new System.Drawing.SizeF(620F, 30F);
            this.xrP5.Text = "Se pacta el telegrama colacionado como forma de notificación entre las partes.";
            //
            // SEXTO
            //
            this.xrP6Lbl.Dpi = 96F;
            this.xrP6Lbl.Font = new DevExpress.Drawing.DXFont("Segoe UI", 9.5F, DevExpress.Drawing.DXFontStyle.Bold);
            this.xrP6Lbl.ForeColor = System.Drawing.Color.FromArgb(28, 54, 128);
            this.xrP6Lbl.LocationFloat = new DevExpress.Utils.PointFloat(0F, 448F);
            this.xrP6Lbl.Name = "xrP6Lbl";
            this.xrP6Lbl.SizeF = new System.Drawing.SizeF(90F, 16F);
            this.xrP6Lbl.Text = "SEXTO:";
            this.xrP6.Dpi = 96F;
            this.xrP6.LocationFloat = new DevExpress.Utils.PointFloat(94F, 448F);
            this.xrP6.Multiline = true;
            this.xrP6.Name = "xrP6";
            this.xrP6.SizeF = new System.Drawing.SizeF(620F, 36F);
            this.xrP6.Text = "Las partes constituyen domicilios especiales a todos los efectos a que dé lugar el presente contrato en los declarados como suyos en la comparecencia.";
            //
            // Constancia
            //
            this.xrConstancia.Dpi = 96F;
            this.xrConstancia.LocationFloat = new DevExpress.Utils.PointFloat(0F, 492F);
            this.xrConstancia.Multiline = true;
            this.xrConstancia.Name = "xrConstancia";
            this.xrConstancia.SizeF = new System.Drawing.SizeF(714F, 36F);
            this.xrConstancia.Text = "Para constancia se suscriben tres ejemplares de igual tenor en el lugar y fecha indicados, uno para cada parte y el tercero para el fiduciario.";
            //
            // Firmas
            //
            this.xrFirmaCedente.Dpi = 96F;
            this.xrFirmaCedente.LocationFloat = new DevExpress.Utils.PointFloat(60F, 600F);
            this.xrFirmaCedente.Name = "xrFirmaCedente";
            this.xrFirmaCedente.SizeF = new System.Drawing.SizeF(220F, 2F);
            this.xrFirmaCedenteLbl.Dpi = 96F;
            this.xrFirmaCedenteLbl.Font = new DevExpress.Drawing.DXFont("Segoe UI", 8F);
            this.xrFirmaCedenteLbl.ForeColor = System.Drawing.Color.FromArgb(102, 102, 102);
            this.xrFirmaCedenteLbl.LocationFloat = new DevExpress.Utils.PointFloat(60F, 603F);
            this.xrFirmaCedenteLbl.Name = "xrFirmaCedenteLbl";
            this.xrFirmaCedenteLbl.SizeF = new System.Drawing.SizeF(220F, 14F);
            this.xrFirmaCedenteLbl.Text = "EL CEDENTE";
            this.xrFirmaCedenteLbl.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter;
            this.xrFirmaCasemed.Dpi = 96F;
            this.xrFirmaCasemed.LocationFloat = new DevExpress.Utils.PointFloat(434F, 600F);
            this.xrFirmaCasemed.Name = "xrFirmaCasemed";
            this.xrFirmaCasemed.SizeF = new System.Drawing.SizeF(220F, 2F);
            this.xrFirmaCasemedLbl.Dpi = 96F;
            this.xrFirmaCasemedLbl.Font = new DevExpress.Drawing.DXFont("Segoe UI", 8F);
            this.xrFirmaCasemedLbl.ForeColor = System.Drawing.Color.FromArgb(102, 102, 102);
            this.xrFirmaCasemedLbl.LocationFloat = new DevExpress.Utils.PointFloat(434F, 603F);
            this.xrFirmaCasemedLbl.Name = "xrFirmaCasemedLbl";
            this.xrFirmaCasemedLbl.SizeF = new System.Drawing.SizeF(220F, 14F);
            this.xrFirmaCasemedLbl.Text = "POR CASEMED";
            this.xrFirmaCasemedLbl.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter;
            //
            // objectDataSource1
            //
            this.objectDataSource1.DataSource = typeof(global::Sgpa.Web.Reporting.Predefinidos.PrestamoValeData);
            this.objectDataSource1.Name = "objectDataSource1";
            //
            // CesionReport
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
        private DevExpress.XtraReports.UI.XRLabel xrIntro;
        private DevExpress.XtraReports.UI.XRLabel xrP1Lbl;
        private DevExpress.XtraReports.UI.XRLabel xrP1;
        private DevExpress.XtraReports.UI.XRLabel xrP2Lbl;
        private DevExpress.XtraReports.UI.XRLabel xrP2;
        private DevExpress.XtraReports.UI.XRLabel xrP3Lbl;
        private DevExpress.XtraReports.UI.XRLabel xrP3;
        private DevExpress.XtraReports.UI.XRLabel xrP4Lbl;
        private DevExpress.XtraReports.UI.XRLabel xrP4;
        private DevExpress.XtraReports.UI.XRLabel xrP5Lbl;
        private DevExpress.XtraReports.UI.XRLabel xrP5;
        private DevExpress.XtraReports.UI.XRLabel xrP6Lbl;
        private DevExpress.XtraReports.UI.XRLabel xrP6;
        private DevExpress.XtraReports.UI.XRLabel xrConstancia;
        private DevExpress.XtraReports.UI.XRLine xrFirmaCedente;
        private DevExpress.XtraReports.UI.XRLabel xrFirmaCedenteLbl;
        private DevExpress.XtraReports.UI.XRLine xrFirmaCasemed;
        private DevExpress.XtraReports.UI.XRLabel xrFirmaCasemedLbl;
        private DevExpress.DataAccess.ObjectBinding.ObjectDataSource objectDataSource1;
    }
}
