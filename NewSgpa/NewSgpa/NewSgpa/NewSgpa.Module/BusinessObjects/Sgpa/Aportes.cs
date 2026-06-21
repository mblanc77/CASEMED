using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using DevExpress.Persistent.Base;
using NewSgpa.Module.BusinessObjects.Base;

namespace NewSgpa.Module.BusinessObjects.Sgpa;

[DefaultClassOptions]
[NavigationItem("Aportes")]
[Table("Imponible")]
public class Imponible : BaseEntity
{
    public virtual long? CI { get; set; }
    [ForeignKey(nameof(CI))]
    public virtual Afiliado? Afiliado { get; set; }

    public virtual int? CodEmpresa { get; set; }
    [ForeignKey(nameof(CodEmpresa))]
    public virtual Empresa? Empresa { get; set; }

    public virtual DateTime? Fechaingreso { get; set; }

    public virtual byte? Mes { get; set; }
    public virtual int? Anio { get; set; }

    [StringLength(3)]
    public virtual string? Concepto { get; set; }

    public virtual int? IdTrabaja { get; set; }

    public virtual int? DiasTrabajados { get; set; }

    public virtual double? Importe { get; set; }

    public virtual int? AnioMes { get; set; }

    public override string ToString() => $"Imp {CI} - {Anio}/{Mes}: {Importe}";
}

[DefaultClassOptions]
[NavigationItem("Aportes")]
[Table("EmpresaPago")]
public class EmpresaPago : BaseEntity
{
    public virtual int? CodEmpresa { get; set; }
    [ForeignKey(nameof(CodEmpresa))]
    public virtual Empresa? Empresa { get; set; }

    public virtual int? Mes { get; set; }
    public virtual int? Anio { get; set; }
    public virtual int? Importe { get; set; }

    public override string ToString() => $"Pago {Empresa?.Nombre} {Anio}/{Mes}";
}

[DefaultClassOptions]
[NavigationItem("Aportes")]
[Table("ReintegroMutual")]
public class ReintegroMutual : BaseEntity
{
    public virtual long? CI { get; set; }
    [ForeignKey(nameof(CI))]
    public virtual Afiliado? Afiliado { get; set; }

    public virtual int? Mes { get; set; }
    public virtual int? Anio { get; set; }
    public virtual DateTime? Fecha { get; set; }

    public virtual int? CodMutualista { get; set; }
    [ForeignKey(nameof(CodMutualista))]
    public virtual Mutualista? Mutualista { get; set; }

    public virtual float? Importe { get; set; }

    [Column(TypeName = "ntext")]
    public virtual string? Observaciones { get; set; }
}
