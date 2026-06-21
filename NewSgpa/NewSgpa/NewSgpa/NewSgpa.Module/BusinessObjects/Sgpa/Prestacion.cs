using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using DevExpress.Persistent.Base;
using NewSgpa.Module.BusinessObjects.Base;

namespace NewSgpa.Module.BusinessObjects.Sgpa;

[DefaultClassOptions]
[NavigationItem("Prestaciones")]
[Table("Prestacion")]
public class Prestacion : BaseEntity
{
    public virtual long? CI { get; set; }
    [ForeignKey(nameof(CI))]
    public virtual Afiliado? Afiliado { get; set; }

    public virtual DateTime? Fecha { get; set; }

    public virtual int? CodPrestacionTipo { get; set; }
    [ForeignKey(nameof(CodPrestacionTipo))]
    public virtual PrestacionTipo? PrestacionTipo { get; set; }

    public virtual int? CodFormaPago { get; set; }
    [ForeignKey(nameof(CodFormaPago))]
    public virtual FormaPago? FormaPago { get; set; }

    [StringLength(3)]
    public virtual string? Moneda { get; set; }

    public virtual double? Importe { get; set; }

    public virtual bool Boleta { get; set; }

    public virtual int? NroRecibo { get; set; }

    [Column(TypeName = "ntext")]
    public virtual string? Observaciones { get; set; }

    public override string ToString() => $"{CI} - {PrestacionTipo?.Descrip} ({Fecha:d})";
}

[DefaultClassOptions]
[NavigationItem("Prestaciones")]
[Table("Receta")]
public class Receta : BaseEntity
{
    public virtual long? CI { get; set; }
    [ForeignKey(nameof(CI))]
    public virtual Afiliado? Afiliado { get; set; }

    public virtual DateTime? Fecha { get; set; }

    public virtual int? CodPrestacionTipo { get; set; }
    [ForeignKey(nameof(CodPrestacionTipo))]
    public virtual PrestacionTipo? PrestacionTipo { get; set; }

    [StringLength(3)]
    public virtual string? CodRecetaDistancia { get; set; }
    [ForeignKey(nameof(CodRecetaDistancia))]
    public virtual RecetaDistancia? RecetaDistancia { get; set; }

    public virtual float? Esf_I { get; set; }
    public virtual float? Esf_D { get; set; }
    public virtual float? Cil_I { get; set; }
    public virtual float? Cil_D { get; set; }

    public override string ToString() => $"Receta {CI} ({Fecha:d})";
}
