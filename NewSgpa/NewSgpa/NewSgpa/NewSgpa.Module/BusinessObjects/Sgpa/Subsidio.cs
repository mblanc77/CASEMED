using System.Collections.ObjectModel;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using DevExpress.ExpressApp.ConditionalAppearance;
using DevExpress.Persistent.Base;
using NewSgpa.Module.BusinessObjects.Base;

namespace NewSgpa.Module.BusinessObjects.Sgpa;

[DefaultClassOptions]
[NavigationItem("Subsidios")]
[DefaultProperty(nameof(DisplayText))]
[Table("SubsidioCabezal")]
[Appearance("SubsidioLiquidado", TargetItems = "*", Criteria = "Liquidar = true",
    FontColor = "DarkGreen", Context = "ListView")]
[Appearance("SubsidioSimulado", TargetItems = "*", Criteria = "Liquidar = false",
    FontColor = "Gray", FontStyle = DevExpress.Drawing.DXFontStyle.Italic, Context = "ListView")]
public class SubsidioCabezal : BaseEntity
{
    public virtual int IdSubsidio { get; set; }

    public virtual byte? Mes { get; set; }
    public virtual int? Anio { get; set; }

    public virtual long? CI { get; set; }
    [ForeignKey(nameof(CI))]
    public virtual Afiliado? Afiliado { get; set; }

    public virtual bool Liquidar { get; set; }
    public virtual float? ValorJornal { get; set; }
    public virtual int? Dias { get; set; }
    public virtual double? ImpNominal { get; set; }
    public virtual double? ImpAguinaldo { get; set; }
    public virtual double? ImpLiquido { get; set; }
    public virtual int? NroRecibo { get; set; }
    public virtual DateTime? FechaPago { get; set; }

    public virtual int? CodBanco { get; set; }
    [ForeignKey(nameof(CodBanco))]
    public virtual Banco? Banco { get; set; }

    [StringLength(50)]
    public virtual string? NroCuenta { get; set; }

    // Navigation - ignored due to keyless related entities
    [NotMapped]
    public virtual IList<SubsidioEnfermedad> Enfermedades { get; set; } = new ObservableCollection<SubsidioEnfermedad>();
    [NotMapped]
    public virtual IList<SubsidioCabezalEmpresa> Empresas { get; set; } = new ObservableCollection<SubsidioCabezalEmpresa>();
    [NotMapped]
    public virtual IList<SubsidioItem> Items { get; set; } = new ObservableCollection<SubsidioItem>();
    [NotMapped]
    public virtual SubsidioCabezalBps? Bps { get; set; }

    [NotMapped]
    public string DisplayText => $"Sub {IdSubsidio} - {CI} ({Anio}/{Mes})";

    public override string ToString() => DisplayText;
}

[Table("SubsidioEnfermedad")]
public class SubsidioEnfermedad : BaseEntity
{
    public virtual int? IdSubsidio { get; set; }
    [ForeignKey(nameof(IdSubsidio))]
    public virtual SubsidioCabezal? SubsidioCabezal { get; set; }

    public virtual DateTime? FechaIni { get; set; }
    public virtual DateTime? FechaFin { get; set; }
    public virtual DateTime? FechaIniSubsidio { get; set; }
    public virtual DateTime? FechaFinSubsidio { get; set; }

    // Columnas del SubsidioEnfermedad original que la migración había omitido (auditoría de columnas).
    public virtual int? NroLlamado { get; set; }
    public virtual byte? Dias { get; set; }
    public virtual double? Importe { get; set; }
}

[Table("SubsidioCabezalEmpresa")]
public class SubsidioCabezalEmpresa : BaseEntity
{
    public virtual int? IdSubsidio { get; set; }
    [ForeignKey(nameof(IdSubsidio))]
    public virtual SubsidioCabezal? SubsidioCabezal { get; set; }

    public virtual int? CodEmpresa { get; set; }
    [ForeignKey(nameof(CodEmpresa))]
    public virtual Empresa? Empresa { get; set; }

    public virtual float? ValorJornal { get; set; }
    public virtual int? Dias { get; set; }
    public virtual double? ImpNominal { get; set; }
    public virtual double? ImpAguinaldo { get; set; }
    public virtual double? ImpLiquido { get; set; }
}

[Table("SubsidioCabezal_BPS")]
public class SubsidioCabezalBps : BaseEntity
{
    public virtual int? IdSubsidio { get; set; }
    [ForeignKey(nameof(IdSubsidio))]
    public virtual SubsidioCabezal? SubsidioCabezal { get; set; }

    public virtual int? DiasBPS { get; set; }
    public virtual double? LiquidoBPS { get; set; }
    public virtual double? AguinaldoBPS { get; set; }
    public virtual double? LiquidoPagar { get; set; }
}

[Table("SubsidioItem")]
public class SubsidioItem : BaseEntity
{
    public virtual int? IdSubsidio { get; set; }
    [ForeignKey(nameof(IdSubsidio))]
    public virtual SubsidioCabezal? SubsidioCabezal { get; set; }

    public virtual int? CodSubsidioItemCod { get; set; }
    [ForeignKey(nameof(CodSubsidioItemCod))]
    public virtual SubsidioItemCod? SubsidioItemCod { get; set; }

    public virtual float? Importe { get; set; }

    // Columna del SubsidioItem original que la migración había omitido (auditoría de columnas).
    public virtual bool? AbiEmp { get; set; }
}

[Table("SubsidioImponible")]
public class SubsidioImponible : BaseEntity
{
    public virtual int? IdSubsidio { get; set; }
    public virtual byte? Mes { get; set; }
    public virtual int? Anio { get; set; }
    public virtual int? CodEmpresa { get; set; }
    public virtual int? Dias { get; set; }
    public virtual double? Importe { get; set; }

    [StringLength(8)]
    public virtual string? Usr { get; set; }

    public virtual DateTime? Ts { get; set; }
}

[DefaultClassOptions]
[NavigationItem("Parámetros")]
[DefaultProperty(nameof(Descrip))]
[Table("SubsidioItemCod")]
public class SubsidioItemCod : BaseEntity
{
    public virtual int CodSubsidioItemCod { get; set; }

    [StringLength(50)]
    public virtual string? Descrip { get; set; }

    [StringLength(1)]
    public virtual string? Tipo { get; set; }

    [StringLength(50)]
    public virtual string? ValorTipo { get; set; }

    public virtual bool? Comparar { get; set; }
    public virtual byte? CompararContra { get; set; }
    public virtual double? Valor { get; set; }

    [StringLength(1)]
    public virtual string? TipoComp { get; set; }

    public virtual int? Orden { get; set; }
    public virtual int? Signo { get; set; }

    [StringLength(5)]
    public virtual string? Operador { get; set; }

    public virtual double? ValorMin { get; set; }
    public virtual double? ValorMax { get; set; }
    public virtual bool Procesar { get; set; }
    public virtual DateTime? FechaVigencia { get; set; }
    public virtual DateTime? FechaBaja { get; set; }
    public virtual bool AperturaXEmpresa { get; set; }
    public virtual bool ModificaNominal { get; set; }

    public override string ToString() => Descrip ?? string.Empty;
}

[Table("SubsidioItemCod_Afiliado")]
public class SubsidioItemCodAfiliado : BaseEntity
{
    public virtual int? SubItmCodAfiId { get; set; }

    public virtual int? CodSubsidioItemCod { get; set; }
    [ForeignKey(nameof(CodSubsidioItemCod))]
    public virtual SubsidioItemCod? SubsidioItemCod { get; set; }

    public virtual long? CI { get; set; }
    [ForeignKey(nameof(CI))]
    public virtual Afiliado? Afiliado { get; set; }

    public virtual double? Valor { get; set; }
    public virtual DateTime? Vigencia { get; set; }
}

[Table("SubsidioItemEmpresa")]
public class SubsidioItemEmpresa : BaseEntity
{
    public virtual int? IdSubsidio { get; set; }
    [ForeignKey(nameof(IdSubsidio))]
    public virtual SubsidioCabezal? SubsidioCabezal { get; set; }

    public virtual int? CodSubsidioItemCod { get; set; }
    [ForeignKey(nameof(CodSubsidioItemCod))]
    public virtual SubsidioItemCod? SubsidioItemCod { get; set; }

    public virtual int? CodEmpresa { get; set; }
    [ForeignKey(nameof(CodEmpresa))]
    public virtual Empresa? Empresa { get; set; }

    public virtual float? Importe { get; set; }
}
