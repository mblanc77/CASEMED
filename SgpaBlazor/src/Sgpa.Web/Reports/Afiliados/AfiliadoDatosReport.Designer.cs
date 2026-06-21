namespace Sgpa.Web.Reports.Afiliados
{
    partial class AfiliadoDatosReport
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
            this.xrNombre = new DevExpress.XtraReports.UI.XRLabel();
            this.xrCISub = new DevExpress.XtraReports.UI.XRLabel();
            this.secPers = new DevExpress.XtraReports.UI.XRLabel();
            this.rulePers = new DevExpress.XtraReports.UI.XRLine();
            this.capNac = new DevExpress.XtraReports.UI.XRLabel();
            this.valNac = new DevExpress.XtraReports.UI.XRLabel();
            this.capSexo = new DevExpress.XtraReports.UI.XRLabel();
            this.valSexo = new DevExpress.XtraReports.UI.XRLabel();
            this.secMut = new DevExpress.XtraReports.UI.XRLabel();
            this.ruleMut = new DevExpress.XtraReports.UI.XRLine();
            this.capMut = new DevExpress.XtraReports.UI.XRLabel();
            this.valMut = new DevExpress.XtraReports.UI.XRLabel();
            this.capSocio = new DevExpress.XtraReports.UI.XRLabel();
            this.valSocio = new DevExpress.XtraReports.UI.XRLabel();
            this.capIng = new DevExpress.XtraReports.UI.XRLabel();
            this.valIng = new DevExpress.XtraReports.UI.XRLabel();
            this.capPaga = new DevExpress.XtraReports.UI.XRLabel();
            this.valPaga = new DevExpress.XtraReports.UI.XRLabel();
            this.capReg = new DevExpress.XtraReports.UI.XRLabel();
            this.valReg = new DevExpress.XtraReports.UI.XRLabel();
            this.secCont = new DevExpress.XtraReports.UI.XRLabel();
            this.ruleCont = new DevExpress.XtraReports.UI.XRLine();
            this.capDir = new DevExpress.XtraReports.UI.XRLabel();
            this.valDir = new DevExpress.XtraReports.UI.XRLabel();
            this.capDep = new DevExpress.XtraReports.UI.XRLabel();
            this.valDep = new DevExpress.XtraReports.UI.XRLabel();
            this.capTel = new DevExpress.XtraReports.UI.XRLabel();
            this.valTel = new DevExpress.XtraReports.UI.XRLabel();
            this.capMov = new DevExpress.XtraReports.UI.XRLabel();
            this.valMov = new DevExpress.XtraReports.UI.XRLabel();
            this.capMail = new DevExpress.XtraReports.UI.XRLabel();
            this.valMail = new DevExpress.XtraReports.UI.XRLabel();
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
                this.xrLogo, this.xrTitulo, this.xrLinea, this.xrNombre, this.xrCISub,
                this.secPers, this.rulePers, this.capNac, this.valNac, this.capSexo, this.valSexo,
                this.secMut, this.ruleMut, this.capMut, this.valMut, this.capSocio, this.valSocio,
                this.capIng, this.valIng, this.capPaga, this.valPaga, this.capReg, this.valReg,
                this.secCont, this.ruleCont, this.capDir, this.valDir, this.capDep, this.valDep,
                this.capTel, this.valTel, this.capMov, this.valMov, this.capMail, this.valMail});
            this.Detail.Dpi = 96F;
            this.Detail.HeightF = 520F;
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
            this.xrTitulo.LocationFloat = new DevExpress.Utils.PointFloat(0F, 8F);
            this.xrTitulo.Name = "xrTitulo";
            this.xrTitulo.SizeF = new System.Drawing.SizeF(714F, 26F);
            this.xrTitulo.Text = "Ficha del afiliado";
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
                new DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[NombreCompleto]")});
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
                new DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "'C.I. ' + [CIFormato]")});
            this.xrCISub.Font = new DevExpress.Drawing.DXFont("Segoe UI", 9.5F);
            this.xrCISub.ForeColor = System.Drawing.Color.FromArgb(102, 102, 102);
            this.xrCISub.LocationFloat = new DevExpress.Utils.PointFloat(0F, 88F);
            this.xrCISub.Name = "xrCISub";
            this.xrCISub.SizeF = new System.Drawing.SizeF(714F, 16F);
            //
            // secPers
            //
            this.secPers.Dpi = 96F;
            this.secPers.Font = new DevExpress.Drawing.DXFont("Segoe UI", 10.5F, DevExpress.Drawing.DXFontStyle.Bold);
            this.secPers.ForeColor = System.Drawing.Color.FromArgb(28, 54, 128);
            this.secPers.LocationFloat = new DevExpress.Utils.PointFloat(0F, 118F);
            this.secPers.Name = "secPers";
            this.secPers.SizeF = new System.Drawing.SizeF(714F, 16F);
            this.secPers.Text = "DATOS PERSONALES";
            //
            // rulePers
            //
            this.rulePers.Dpi = 96F;
            this.rulePers.ForeColor = System.Drawing.Color.FromArgb(210, 216, 232);
            this.rulePers.LocationFloat = new DevExpress.Utils.PointFloat(0F, 136F);
            this.rulePers.Name = "rulePers";
            this.rulePers.SizeF = new System.Drawing.SizeF(714F, 1F);
            //
            // capNac / valNac
            //
            this.capNac.Dpi = 96F;
            this.capNac.Font = new DevExpress.Drawing.DXFont("Segoe UI", 9F);
            this.capNac.ForeColor = System.Drawing.Color.FromArgb(102, 102, 102);
            this.capNac.LocationFloat = new DevExpress.Utils.PointFloat(0F, 142F);
            this.capNac.Name = "capNac";
            this.capNac.SizeF = new System.Drawing.SizeF(170F, 20F);
            this.capNac.Text = "Fecha de nacimiento";
            this.valNac.Dpi = 96F;
            this.valNac.ExpressionBindings.AddRange(new DevExpress.XtraReports.UI.ExpressionBinding[] {
                new DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[FechaNacFmt]")});
            this.valNac.LocationFloat = new DevExpress.Utils.PointFloat(176F, 142F);
            this.valNac.Name = "valNac";
            this.valNac.SizeF = new System.Drawing.SizeF(538F, 20F);
            //
            // capSexo / valSexo
            //
            this.capSexo.Dpi = 96F;
            this.capSexo.Font = new DevExpress.Drawing.DXFont("Segoe UI", 9F);
            this.capSexo.ForeColor = System.Drawing.Color.FromArgb(102, 102, 102);
            this.capSexo.LocationFloat = new DevExpress.Utils.PointFloat(0F, 164F);
            this.capSexo.Name = "capSexo";
            this.capSexo.SizeF = new System.Drawing.SizeF(170F, 20F);
            this.capSexo.Text = "Sexo";
            this.valSexo.Dpi = 96F;
            this.valSexo.ExpressionBindings.AddRange(new DevExpress.XtraReports.UI.ExpressionBinding[] {
                new DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[SexoTexto]")});
            this.valSexo.LocationFloat = new DevExpress.Utils.PointFloat(176F, 164F);
            this.valSexo.Name = "valSexo";
            this.valSexo.SizeF = new System.Drawing.SizeF(538F, 20F);
            //
            // secMut
            //
            this.secMut.Dpi = 96F;
            this.secMut.Font = new DevExpress.Drawing.DXFont("Segoe UI", 10.5F, DevExpress.Drawing.DXFontStyle.Bold);
            this.secMut.ForeColor = System.Drawing.Color.FromArgb(28, 54, 128);
            this.secMut.LocationFloat = new DevExpress.Utils.PointFloat(0F, 192F);
            this.secMut.Name = "secMut";
            this.secMut.SizeF = new System.Drawing.SizeF(714F, 16F);
            this.secMut.Text = "AFILIACIÓN MUTUAL";
            //
            // ruleMut
            //
            this.ruleMut.Dpi = 96F;
            this.ruleMut.ForeColor = System.Drawing.Color.FromArgb(210, 216, 232);
            this.ruleMut.LocationFloat = new DevExpress.Utils.PointFloat(0F, 210F);
            this.ruleMut.Name = "ruleMut";
            this.ruleMut.SizeF = new System.Drawing.SizeF(714F, 1F);
            //
            // capMut / valMut
            //
            this.capMut.Dpi = 96F;
            this.capMut.Font = new DevExpress.Drawing.DXFont("Segoe UI", 9F);
            this.capMut.ForeColor = System.Drawing.Color.FromArgb(102, 102, 102);
            this.capMut.LocationFloat = new DevExpress.Utils.PointFloat(0F, 216F);
            this.capMut.Name = "capMut";
            this.capMut.SizeF = new System.Drawing.SizeF(170F, 20F);
            this.capMut.Text = "Mutualista";
            this.valMut.Dpi = 96F;
            this.valMut.ExpressionBindings.AddRange(new DevExpress.XtraReports.UI.ExpressionBinding[] {
                new DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[MutualistaTexto]")});
            this.valMut.LocationFloat = new DevExpress.Utils.PointFloat(176F, 216F);
            this.valMut.Name = "valMut";
            this.valMut.SizeF = new System.Drawing.SizeF(538F, 20F);
            //
            // capSocio / valSocio
            //
            this.capSocio.Dpi = 96F;
            this.capSocio.Font = new DevExpress.Drawing.DXFont("Segoe UI", 9F);
            this.capSocio.ForeColor = System.Drawing.Color.FromArgb(102, 102, 102);
            this.capSocio.LocationFloat = new DevExpress.Utils.PointFloat(0F, 238F);
            this.capSocio.Name = "capSocio";
            this.capSocio.SizeF = new System.Drawing.SizeF(170F, 20F);
            this.capSocio.Text = "Nº de socio";
            this.valSocio.Dpi = 96F;
            this.valSocio.ExpressionBindings.AddRange(new DevExpress.XtraReports.UI.ExpressionBinding[] {
                new DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[NroSocioTexto]")});
            this.valSocio.LocationFloat = new DevExpress.Utils.PointFloat(176F, 238F);
            this.valSocio.Name = "valSocio";
            this.valSocio.SizeF = new System.Drawing.SizeF(538F, 20F);
            //
            // capIng / valIng
            //
            this.capIng.Dpi = 96F;
            this.capIng.Font = new DevExpress.Drawing.DXFont("Segoe UI", 9F);
            this.capIng.ForeColor = System.Drawing.Color.FromArgb(102, 102, 102);
            this.capIng.LocationFloat = new DevExpress.Utils.PointFloat(0F, 260F);
            this.capIng.Name = "capIng";
            this.capIng.SizeF = new System.Drawing.SizeF(170F, 20F);
            this.capIng.Text = "Fecha de ingreso";
            this.valIng.Dpi = 96F;
            this.valIng.ExpressionBindings.AddRange(new DevExpress.XtraReports.UI.ExpressionBinding[] {
                new DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[FechaIngMutFmt]")});
            this.valIng.LocationFloat = new DevExpress.Utils.PointFloat(176F, 260F);
            this.valIng.Name = "valIng";
            this.valIng.SizeF = new System.Drawing.SizeF(538F, 20F);
            //
            // capPaga / valPaga
            //
            this.capPaga.Dpi = 96F;
            this.capPaga.Font = new DevExpress.Drawing.DXFont("Segoe UI", 9F);
            this.capPaga.ForeColor = System.Drawing.Color.FromArgb(102, 102, 102);
            this.capPaga.LocationFloat = new DevExpress.Utils.PointFloat(0F, 282F);
            this.capPaga.Name = "capPaga";
            this.capPaga.SizeF = new System.Drawing.SizeF(170F, 20F);
            this.capPaga.Text = "Paga mutualista";
            this.valPaga.Dpi = 96F;
            this.valPaga.ExpressionBindings.AddRange(new DevExpress.XtraReports.UI.ExpressionBinding[] {
                new DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[PagaMutualistaTexto]")});
            this.valPaga.LocationFloat = new DevExpress.Utils.PointFloat(176F, 282F);
            this.valPaga.Name = "valPaga";
            this.valPaga.SizeF = new System.Drawing.SizeF(538F, 20F);
            //
            // capReg / valReg
            //
            this.capReg.Dpi = 96F;
            this.capReg.Font = new DevExpress.Drawing.DXFont("Segoe UI", 9F);
            this.capReg.ForeColor = System.Drawing.Color.FromArgb(102, 102, 102);
            this.capReg.LocationFloat = new DevExpress.Utils.PointFloat(0F, 304F);
            this.capReg.Name = "capReg";
            this.capReg.SizeF = new System.Drawing.SizeF(170F, 20F);
            this.capReg.Text = "Régimen jubilatorio";
            this.valReg.Dpi = 96F;
            this.valReg.ExpressionBindings.AddRange(new DevExpress.XtraReports.UI.ExpressionBinding[] {
                new DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[RegimenTexto]")});
            this.valReg.LocationFloat = new DevExpress.Utils.PointFloat(176F, 304F);
            this.valReg.Name = "valReg";
            this.valReg.SizeF = new System.Drawing.SizeF(538F, 20F);
            //
            // secCont
            //
            this.secCont.Dpi = 96F;
            this.secCont.Font = new DevExpress.Drawing.DXFont("Segoe UI", 10.5F, DevExpress.Drawing.DXFontStyle.Bold);
            this.secCont.ForeColor = System.Drawing.Color.FromArgb(28, 54, 128);
            this.secCont.LocationFloat = new DevExpress.Utils.PointFloat(0F, 332F);
            this.secCont.Name = "secCont";
            this.secCont.SizeF = new System.Drawing.SizeF(714F, 16F);
            this.secCont.Text = "CONTACTO";
            //
            // ruleCont
            //
            this.ruleCont.Dpi = 96F;
            this.ruleCont.ForeColor = System.Drawing.Color.FromArgb(210, 216, 232);
            this.ruleCont.LocationFloat = new DevExpress.Utils.PointFloat(0F, 350F);
            this.ruleCont.Name = "ruleCont";
            this.ruleCont.SizeF = new System.Drawing.SizeF(714F, 1F);
            //
            // capDir / valDir
            //
            this.capDir.Dpi = 96F;
            this.capDir.Font = new DevExpress.Drawing.DXFont("Segoe UI", 9F);
            this.capDir.ForeColor = System.Drawing.Color.FromArgb(102, 102, 102);
            this.capDir.LocationFloat = new DevExpress.Utils.PointFloat(0F, 356F);
            this.capDir.Name = "capDir";
            this.capDir.SizeF = new System.Drawing.SizeF(170F, 20F);
            this.capDir.Text = "Dirección";
            this.valDir.Dpi = 96F;
            this.valDir.ExpressionBindings.AddRange(new DevExpress.XtraReports.UI.ExpressionBinding[] {
                new DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[DireccionTexto]")});
            this.valDir.LocationFloat = new DevExpress.Utils.PointFloat(176F, 356F);
            this.valDir.Name = "valDir";
            this.valDir.SizeF = new System.Drawing.SizeF(538F, 20F);
            //
            // capDep / valDep
            //
            this.capDep.Dpi = 96F;
            this.capDep.Font = new DevExpress.Drawing.DXFont("Segoe UI", 9F);
            this.capDep.ForeColor = System.Drawing.Color.FromArgb(102, 102, 102);
            this.capDep.LocationFloat = new DevExpress.Utils.PointFloat(0F, 378F);
            this.capDep.Name = "capDep";
            this.capDep.SizeF = new System.Drawing.SizeF(170F, 20F);
            this.capDep.Text = "Departamento";
            this.valDep.Dpi = 96F;
            this.valDep.ExpressionBindings.AddRange(new DevExpress.XtraReports.UI.ExpressionBinding[] {
                new DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[DepartamentoTexto]")});
            this.valDep.LocationFloat = new DevExpress.Utils.PointFloat(176F, 378F);
            this.valDep.Name = "valDep";
            this.valDep.SizeF = new System.Drawing.SizeF(538F, 20F);
            //
            // capTel / valTel
            //
            this.capTel.Dpi = 96F;
            this.capTel.Font = new DevExpress.Drawing.DXFont("Segoe UI", 9F);
            this.capTel.ForeColor = System.Drawing.Color.FromArgb(102, 102, 102);
            this.capTel.LocationFloat = new DevExpress.Utils.PointFloat(0F, 400F);
            this.capTel.Name = "capTel";
            this.capTel.SizeF = new System.Drawing.SizeF(170F, 20F);
            this.capTel.Text = "Teléfono";
            this.valTel.Dpi = 96F;
            this.valTel.ExpressionBindings.AddRange(new DevExpress.XtraReports.UI.ExpressionBinding[] {
                new DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[TelefonoTexto]")});
            this.valTel.LocationFloat = new DevExpress.Utils.PointFloat(176F, 400F);
            this.valTel.Name = "valTel";
            this.valTel.SizeF = new System.Drawing.SizeF(538F, 20F);
            //
            // capMov / valMov
            //
            this.capMov.Dpi = 96F;
            this.capMov.Font = new DevExpress.Drawing.DXFont("Segoe UI", 9F);
            this.capMov.ForeColor = System.Drawing.Color.FromArgb(102, 102, 102);
            this.capMov.LocationFloat = new DevExpress.Utils.PointFloat(0F, 422F);
            this.capMov.Name = "capMov";
            this.capMov.SizeF = new System.Drawing.SizeF(170F, 20F);
            this.capMov.Text = "Móvil";
            this.valMov.Dpi = 96F;
            this.valMov.ExpressionBindings.AddRange(new DevExpress.XtraReports.UI.ExpressionBinding[] {
                new DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[MovilTexto]")});
            this.valMov.LocationFloat = new DevExpress.Utils.PointFloat(176F, 422F);
            this.valMov.Name = "valMov";
            this.valMov.SizeF = new System.Drawing.SizeF(538F, 20F);
            //
            // capMail / valMail
            //
            this.capMail.Dpi = 96F;
            this.capMail.Font = new DevExpress.Drawing.DXFont("Segoe UI", 9F);
            this.capMail.ForeColor = System.Drawing.Color.FromArgb(102, 102, 102);
            this.capMail.LocationFloat = new DevExpress.Utils.PointFloat(0F, 444F);
            this.capMail.Name = "capMail";
            this.capMail.SizeF = new System.Drawing.SizeF(170F, 20F);
            this.capMail.Text = "E-mail";
            this.valMail.Dpi = 96F;
            this.valMail.ExpressionBindings.AddRange(new DevExpress.XtraReports.UI.ExpressionBinding[] {
                new DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[EMailTexto]")});
            this.valMail.LocationFloat = new DevExpress.Utils.PointFloat(176F, 444F);
            this.valMail.Name = "valMail";
            this.valMail.SizeF = new System.Drawing.SizeF(538F, 20F);
            //
            // objectDataSource1
            //
            this.objectDataSource1.DataSource = typeof(global::Sgpa.Web.Reporting.Predefinidos.AfiliadoDatosData);
            this.objectDataSource1.Name = "objectDataSource1";
            //
            // AfiliadoDatosReport
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
        private DevExpress.XtraReports.UI.XRLabel xrNombre;
        private DevExpress.XtraReports.UI.XRLabel xrCISub;
        private DevExpress.XtraReports.UI.XRLabel secPers;
        private DevExpress.XtraReports.UI.XRLine rulePers;
        private DevExpress.XtraReports.UI.XRLabel capNac;
        private DevExpress.XtraReports.UI.XRLabel valNac;
        private DevExpress.XtraReports.UI.XRLabel capSexo;
        private DevExpress.XtraReports.UI.XRLabel valSexo;
        private DevExpress.XtraReports.UI.XRLabel secMut;
        private DevExpress.XtraReports.UI.XRLine ruleMut;
        private DevExpress.XtraReports.UI.XRLabel capMut;
        private DevExpress.XtraReports.UI.XRLabel valMut;
        private DevExpress.XtraReports.UI.XRLabel capSocio;
        private DevExpress.XtraReports.UI.XRLabel valSocio;
        private DevExpress.XtraReports.UI.XRLabel capIng;
        private DevExpress.XtraReports.UI.XRLabel valIng;
        private DevExpress.XtraReports.UI.XRLabel capPaga;
        private DevExpress.XtraReports.UI.XRLabel valPaga;
        private DevExpress.XtraReports.UI.XRLabel capReg;
        private DevExpress.XtraReports.UI.XRLabel valReg;
        private DevExpress.XtraReports.UI.XRLabel secCont;
        private DevExpress.XtraReports.UI.XRLine ruleCont;
        private DevExpress.XtraReports.UI.XRLabel capDir;
        private DevExpress.XtraReports.UI.XRLabel valDir;
        private DevExpress.XtraReports.UI.XRLabel capDep;
        private DevExpress.XtraReports.UI.XRLabel valDep;
        private DevExpress.XtraReports.UI.XRLabel capTel;
        private DevExpress.XtraReports.UI.XRLabel valTel;
        private DevExpress.XtraReports.UI.XRLabel capMov;
        private DevExpress.XtraReports.UI.XRLabel valMov;
        private DevExpress.XtraReports.UI.XRLabel capMail;
        private DevExpress.XtraReports.UI.XRLabel valMail;
        private DevExpress.DataAccess.ObjectBinding.ObjectDataSource objectDataSource1;
    }
}
