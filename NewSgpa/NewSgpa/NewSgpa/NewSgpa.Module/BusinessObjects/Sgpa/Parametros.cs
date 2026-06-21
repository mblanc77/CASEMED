using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using DevExpress.Persistent.Base;
using NewSgpa.Module.BusinessObjects.Base;

namespace NewSgpa.Module.BusinessObjects.Sgpa;

/// <summary>
/// System-wide parameters for subsidio calculations.
/// Single-row table storing SMN, TopeJubilatorio, etc.
/// </summary>
[NavigationItem("Parámetros")]
[Table("Parametros")]
public class Parametros : BaseEntity
{
    public virtual double? SMN { get; set; }
    public virtual float? TopeJubilatorio { get; set; }
    public virtual float? TopePrima { get; set; }
    public virtual float? UR { get; set; }
    public virtual float? PctAdPreJub { get; set; }
    public virtual double? BCP { get; set; }
    public virtual double? TopeLiquidoBPS { get; set; }
    public virtual double? PctBPS { get; set; }

    public override string ToString() => $"Parámetros (SMN={SMN})";
}

/// <summary>
/// Prima por fallecimiento.
/// </summary>
[DefaultClassOptions]
[NavigationItem("Afiliados")]
[Table("PrimaFallecimiento")]
public class PrimaFallecimiento : BaseEntity
{
    public virtual long? CI { get; set; }
    [ForeignKey(nameof(CI))]
    public virtual Afiliado? Afiliado { get; set; }

    public virtual DateTime? FechaFirma { get; set; }
    public virtual DateTime? FechaFallecimiento { get; set; }
    public virtual double? Importe { get; set; }
    public virtual DateTime? FechaPago { get; set; }

    [Column(TypeName = "ntext")]
    public virtual string? Observaciones { get; set; }
}

/// <summary>
/// Afiliados no cargados en la hoja de liquidación.
/// Used by BPS import process.
/// </summary>
[Table("NoCargadoHL")]
public class NoCargadoHL : BaseEntity
{
    public virtual long? CI { get; set; }

    [StringLength(50)]
    public virtual string? Apellido1 { get; set; }

    [StringLength(50)]
    public virtual string? Apellido2 { get; set; }

    [StringLength(50)]
    public virtual string? Nombres { get; set; }

    public virtual int? CodEmpresa { get; set; }
    public virtual int? Mes { get; set; }
    public virtual int? Anio { get; set; }
}
