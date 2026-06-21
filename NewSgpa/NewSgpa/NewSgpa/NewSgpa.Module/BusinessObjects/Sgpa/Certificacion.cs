using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using DevExpress.ExpressApp.ConditionalAppearance;
using DevExpress.Persistent.Base;
using NewSgpa.Module.BusinessObjects.Base;

namespace NewSgpa.Module.BusinessObjects.Sgpa;

[DefaultClassOptions]
[NavigationItem("Certificaciones")]
[Table("Certificacion")]
[Appearance("CertificacionNoEfectiva", TargetItems = "*",
    Criteria = "Efectiva = false",
    FontColor = "Gray", FontStyle = DevExpress.Drawing.DXFontStyle.Italic, Context = "ListView")]
public class Certificacion : BaseEntity
{
    public virtual int? NroLlamado { get; set; }

    public virtual long? CI { get; set; }
    [ForeignKey(nameof(CI))]
    public virtual Afiliado? Afiliado { get; set; }

    public virtual int? NroRecibo { get; set; }

    public virtual DateTime? FechaRecibido { get; set; }
    public virtual DateTime? FechaCertificacion { get; set; }
    public virtual DateTime? FechaIni { get; set; }
    public virtual DateTime? FechaFin { get; set; }

    public virtual int? CodAfeccionTipo { get; set; }
    [ForeignKey(nameof(CodAfeccionTipo))]
    public virtual AfeccionTipo? AfeccionTipo { get; set; }

    public virtual int? CodCertificador { get; set; }
    [ForeignKey(nameof(CodCertificador))]
    public virtual Certificador? Certificador { get; set; }

    public virtual int? CodSalidaTipo { get; set; }
    [ForeignKey(nameof(CodSalidaTipo))]
    public virtual SalidaTipo? SalidaTipo { get; set; }

    public virtual bool Efectiva { get; set; }

    [Column(TypeName = "ntext")]
    public virtual string? Indicaciones { get; set; }

    public virtual double? ImporteDeducible { get; set; }

    [Display(Name = "Trabaja")]
    [Column("Trabaja")]
    public virtual bool TrabajaDuranteCertificacion { get; set; }

    public override string ToString() => $"Cert {CI} ({FechaIni:d} - {FechaFin:d})";
}

[Table("CertificacionProrroga")]
public class CertificacionProrroga : BaseEntity
{
    public virtual long? CI { get; set; }
    [ForeignKey(nameof(CI))]
    public virtual Afiliado? Afiliado { get; set; }

    public virtual DateTime? Fecha { get; set; }
    public virtual int? Dias { get; set; }

    [Column(TypeName = "ntext")]
    public virtual string? Observaciones { get; set; }
}
