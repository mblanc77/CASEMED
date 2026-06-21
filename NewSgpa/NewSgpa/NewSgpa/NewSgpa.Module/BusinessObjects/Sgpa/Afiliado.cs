using System.Collections.ObjectModel;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using DevExpress.ExpressApp.ConditionalAppearance;
using DevExpress.Persistent.Base;
using NewSgpa.Module.BusinessObjects.Base;

namespace NewSgpa.Module.BusinessObjects.Sgpa;

[DefaultClassOptions]
[NavigationItem("Afiliados")]
[DefaultProperty(nameof(DescAfiliado))]
[Table("Afiliado")]
[Appearance("AfiliadoBajaMutualista", TargetItems = "*",
    Criteria = "FechaBajaMutualista Is Not Null",
    FontColor = "Gray", FontStyle = DevExpress.Drawing.DXFontStyle.Strikeout, Context = "ListView")]
public class Afiliado : BaseEntity
{
    [VisibleInListView(true)]
    public virtual long CI { get; set; }

    [StringLength(50)]
    public virtual string? Nombres { get; set; }

    [StringLength(30)]
    public virtual string? Apellido1 { get; set; }

    [StringLength(30)]
    public virtual string? Apellido2 { get; set; }

    public virtual DateTime? FechaNacimiento { get; set; }

    [StringLength(1)]
    public virtual string? Sexo { get; set; }

    [StringLength(100)]
    public virtual string? Direccion { get; set; }

    [StringLength(25)]
    public virtual string? Telefono { get; set; }

    [StringLength(100)]
    public virtual string? EMail { get; set; }

    [StringLength(50)]
    public virtual string? Movil { get; set; }

    public virtual int? CodMutualista { get; set; }
    [ForeignKey(nameof(CodMutualista))]
    public virtual Mutualista? Mutualista { get; set; }

    public virtual DateTime? FechaIngMutualista { get; set; }

    public virtual DateTime? FechaBajaMutualista { get; set; }

    [StringLength(12)]
    public virtual string? NroSocioMutualista { get; set; }

    public virtual byte? CodRegimenJubilatorio { get; set; }
    [ForeignKey(nameof(CodRegimenJubilatorio))]
    public virtual RegimenJubilatorio? RegimenJubilatorio { get; set; }

    [StringLength(3)]
    public virtual string? CodDepartamento { get; set; }
    [ForeignKey(nameof(CodDepartamento))]
    public virtual Departamento? Departamento { get; set; }

    public virtual bool PagaMutualista { get; set; }

    [StringLength(2)]
    public virtual string? CodSituacionMutual { get; set; }
    [ForeignKey(nameof(CodSituacionMutual))]
    public virtual SituacionMutual? SituacionMutual { get; set; }

    public virtual int? CodBanco { get; set; }
    [ForeignKey(nameof(CodBanco))]
    public virtual Banco? Banco { get; set; }

    [StringLength(50)]
    public virtual string? NroCuenta { get; set; }

    [StringLength(50)]
    public virtual string? NroFunCuenta { get; set; }

    // Navigation collections - ignored due to keyless related entities
    [NotMapped]
    public virtual IList<AfiliadoApunte> Apuntes { get; set; } = new ObservableCollection<AfiliadoApunte>();
    [NotMapped]
    public virtual IList<AfiliadoEspecialidad> Especialidades { get; set; } = new ObservableCollection<AfiliadoEspecialidad>();
    [NotMapped]
    public virtual IList<Trabaja> Empleos { get; set; } = new ObservableCollection<Trabaja>();
    [NotMapped]
    public virtual IList<Certificacion> Certificaciones { get; set; } = new ObservableCollection<Certificacion>();
    [NotMapped]
    public virtual IList<Prestacion> Prestaciones { get; set; } = new ObservableCollection<Prestacion>();
    [NotMapped]
    public virtual IList<Receta> Recetas { get; set; } = new ObservableCollection<Receta>();

    [NotMapped]
    public string DescAfiliado => $"{Apellido1} {Apellido2}, {Nombres}".Trim().Trim(',');

    public override string ToString() => $"{CI} - {DescAfiliado}";
}

[DefaultClassOptions]
[NavigationItem("Afiliados")]
[Table("AfiliadoApunte")]
public class AfiliadoApunte : BaseEntity
{
    public virtual long? CI { get; set; }
    [ForeignKey(nameof(CI))]
    public virtual Afiliado? Afiliado { get; set; }

    public virtual DateTime? Fecha { get; set; }

    [Column(TypeName = "ntext")]
    public virtual string? Descrip { get; set; }

    public override string ToString() => $"{Fecha:d} - {Descrip?.Substring(0, Math.Min(50, Descrip?.Length ?? 0))}";
}

[Table("AfiliadoEspecialidad")]
public class AfiliadoEspecialidad : BaseEntity
{
    public virtual long? CI { get; set; }
    [ForeignKey(nameof(CI))]
    public virtual Afiliado? Afiliado { get; set; }

    public virtual int? CodEspecialidad { get; set; }
    [ForeignKey(nameof(CodEspecialidad))]
    public virtual Especialidad? Especialidad { get; set; }
}

[DefaultClassOptions]
[NavigationItem("Afiliados")]
[Table("Trabaja")]
[Appearance("TrabajaInactivo", TargetItems = "*",
    Criteria = "FechaBaja Is Not Null",
    FontColor = "IndianRed", FontStyle = DevExpress.Drawing.DXFontStyle.Strikeout, Context = "ListView")]
public class Trabaja : BaseEntity
{
    public virtual int? IdTrabaja { get; set; }

    public virtual long? CI { get; set; }
    [ForeignKey(nameof(CI))]
    public virtual Afiliado? Afiliado { get; set; }

    public virtual int? CodEmpresa { get; set; }
    [ForeignKey(nameof(CodEmpresa))]
    public virtual Empresa? Empresa { get; set; }

    public virtual DateTime? FechaIngreso { get; set; }

    public virtual DateTime? FechaBaja { get; set; }

    public virtual int? CodBajaMotivo { get; set; }
    [ForeignKey(nameof(CodBajaMotivo))]
    public virtual BajaMotivo? BajaMotivo { get; set; }

    [StringLength(20)]
    public virtual string? NroFichaEmpresa { get; set; }

    public virtual DateTime? FechaIngCasemed { get; set; }

    [NotMapped]
    public bool Activo => FechaBaja == null;

    public override string ToString() => $"{Afiliado?.DescAfiliado} en {Empresa?.Nombre}";
}

[Table("Cuenta")]
public class Cuenta : BaseEntity
{
    public virtual long? CI { get; set; }
    [ForeignKey(nameof(CI))]
    public virtual Afiliado? Afiliado { get; set; }

    public virtual int? CodBanco { get; set; }
    [ForeignKey(nameof(CodBanco))]
    public virtual Banco? Banco { get; set; }

    [StringLength(50)]
    public virtual string? NroCuenta { get; set; }
}

[NavigationItem("Afiliados")]
[Table("AdPreJub")]
public class AdPreJub : BaseEntity
{
    public virtual long? CI { get; set; }
    [ForeignKey(nameof(CI))]
    public virtual Afiliado? Afiliado { get; set; }

    public virtual DateTime? FechaPresentacion { get; set; }
    public virtual int? ImporteMensual { get; set; }
    public virtual DateTime? FechaJubilacion { get; set; }

    [Column(TypeName = "ntext")]
    public virtual string? Observaciones { get; set; }
}

[Table("AdPreJubPago")]
public class AdPreJubPago : BaseEntity
{
    public virtual long? CI { get; set; }
    [ForeignKey(nameof(CI))]
    public virtual Afiliado? Afiliado { get; set; }

    public virtual int? Mes { get; set; }
    public virtual int? Anio { get; set; }
    public virtual DateTime? Fecha { get; set; }
    public virtual float? Importe { get; set; }

    [Column(TypeName = "ntext")]
    public virtual string? Observaciones { get; set; }
}
