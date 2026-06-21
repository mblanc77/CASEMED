using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using DevExpress.Persistent.Base;
using NewSgpa.Module.BusinessObjects.Base;

namespace NewSgpa.Module.BusinessObjects.Sgpa;

[DefaultClassOptions]
[NavigationItem("Parámetros")]
[DefaultProperty(nameof(Descrip))]
[Table("Mutualista")]
public class Mutualista : BaseLookupEntity
{
    [VisibleInListView(true)]
    public virtual int CodMutualista { get; set; }

    [StringLength(50)]
    public virtual string? Direccion { get; set; }

    [StringLength(25)]
    public virtual string? Telefono { get; set; }

    [StringLength(25)]
    public virtual string? Fax { get; set; }

    [StringLength(25)]
    public virtual string? EMail { get; set; }

    public virtual double? Cuota { get; set; }

    [StringLength(50)]
    public virtual string? PersonaContacto { get; set; }

    public virtual short? DiaPago { get; set; }

    public virtual int? CodFormaPago { get; set; }

    public virtual bool Ficticia { get; set; }

    public override string ToString() => $"{CodMutualista} - {Descrip}";
}

[DefaultClassOptions]
[NavigationItem("Parámetros")]
[DefaultProperty(nameof(Nombre))]
[Table("Empresa")]
public class Empresa : BaseEntity
{
    [VisibleInListView(true)]
    public virtual int CodEmpresa { get; set; }

    [StringLength(50)]
    public virtual string? Nombre { get; set; }

    [StringLength(50)]
    public virtual string? Direccion { get; set; }

    [StringLength(25)]
    public virtual string? Telefono { get; set; }

    [StringLength(25)]
    public virtual string? Fax { get; set; }

    [StringLength(25)]
    public virtual string? EMail { get; set; }

    public virtual float? AporteCasemed { get; set; }

    public virtual int? AporteAguinaldo { get; set; }

    [StringLength(50)]
    public virtual string? PersonaContacto { get; set; }

    [StringLength(255)]
    public virtual string? Autoridades { get; set; }

    public virtual int? CodRegimenAporte { get; set; }
    [ForeignKey(nameof(CodRegimenAporte))]
    public virtual RegimenAporte? RegimenAporte { get; set; }

    public virtual int? CodSituacionPago { get; set; }
    [ForeignKey(nameof(CodSituacionPago))]
    public virtual SituacionPago? SituacionPago { get; set; }

    public virtual bool Liquidar { get; set; }

    public virtual bool Ficticia { get; set; }

    public override string ToString() => $"{CodEmpresa} - {Nombre}";
}

[DefaultClassOptions]
[NavigationItem("Parámetros")]
[DefaultProperty(nameof(Descrip))]
[Table("AfeccionGrupo")]
public class AfeccionGrupo : BaseLookupEntity
{
    public virtual int CodAfeccionGrupo { get; set; }

    public virtual int? CodPatologia { get; set; }
    [ForeignKey(nameof(CodPatologia))]
    public virtual Patologia? Patologia { get; set; }
}

[DefaultClassOptions]
[NavigationItem("Parámetros")]
[DefaultProperty(nameof(Descrip))]
[Table("AfeccionTipo")]
public class AfeccionTipo : BaseEntity
{
    public virtual int CodAfeccionTipo { get; set; }

    [StringLength(200)]
    public virtual string? Descrip { get; set; }

    public virtual int? CodAfeccionGrupo { get; set; }
    [ForeignKey(nameof(CodAfeccionGrupo))]
    public virtual AfeccionGrupo? AfeccionGrupo { get; set; }

    public virtual int? CodDiameg { get; set; }

    public override string ToString() => Descrip ?? string.Empty;
}

[DefaultClassOptions]
[NavigationItem("Parámetros")]
[DefaultProperty(nameof(Descrip))]
[Table("Patologia")]
public class Patologia : BaseLookupEntity
{
    public virtual int CodPatologia { get; set; }
}

[DefaultClassOptions]
[NavigationItem("Parámetros")]
[DefaultProperty(nameof(Descrip))]
[Table("Certificador")]
public class Certificador : BaseEntity
{
    public virtual int CodCertificador { get; set; }

    [StringLength(50)]
    public virtual string? Descrip { get; set; }

    [StringLength(50)]
    public virtual string? Direccion { get; set; }

    [StringLength(25)]
    public virtual string? Telefono { get; set; }

    [StringLength(25)]
    public virtual string? Fax { get; set; }

    [StringLength(25)]
    public virtual string? Bip { get; set; }

    public virtual bool CobraLlamado { get; set; }

    public override string ToString() => $"{CodCertificador} - {Descrip}";
}

[DefaultClassOptions]
[NavigationItem("Parámetros")]
[DefaultProperty(nameof(Descrip))]
[Table("SalidaTipo")]
public class SalidaTipo : BaseLookupEntity
{
    public virtual int CodSalidaTipo { get; set; }
}

[DefaultClassOptions]
[NavigationItem("Parámetros")]
[DefaultProperty(nameof(Descrip))]
[Table("BajaMotivo")]
public class BajaMotivo : BaseLookupEntity
{
    public virtual int CodBajaMotivo { get; set; }
}

[DefaultClassOptions]
[NavigationItem("Parámetros")]
[DefaultProperty(nameof(Descripcion))]
[Table("Banco")]
public class Banco : BaseEntity
{
    public virtual int CodBanco { get; set; }

    [StringLength(50)]
    public virtual string? Descripcion { get; set; }

    public override string ToString() => $"{CodBanco} - {Descripcion}";
}

[DefaultClassOptions]
[NavigationItem("Parámetros")]
[DefaultProperty(nameof(Descrip))]
[Table("FormaPago")]
public class FormaPago : BaseLookupEntity
{
    public virtual int CodFormaPago { get; set; }
}

[DefaultClassOptions]
[NavigationItem("Parámetros")]
[DefaultProperty(nameof(Descrip))]
[Table("RegimenAporte")]
public class RegimenAporte : BaseLookupEntity
{
    public virtual int CodRegimenAporte { get; set; }

    public virtual int? Porcentaje { get; set; }
}

[DefaultClassOptions]
[NavigationItem("Parámetros")]
[DefaultProperty(nameof(Descrip))]
[Table("RegimenJubilatorio")]
public class RegimenJubilatorio : BaseLookupEntity
{
    public virtual byte CodRegimenJubilatorio { get; set; }
}

[DefaultClassOptions]
[NavigationItem("Parámetros")]
[DefaultProperty(nameof(Descrip))]
[Table("AporteTipo")]
public class AporteTipo : BaseEntity
{
    [StringLength(3)]
    public virtual string? CodAporteTipo { get; set; }

    [StringLength(50)]
    public virtual string? Descrip { get; set; }

    public override string ToString() => Descrip ?? string.Empty;
}

[DefaultClassOptions]
[NavigationItem("Parámetros")]
[DefaultProperty(nameof(Descrip))]
[Table("SituacionPago")]
public class SituacionPago : BaseLookupEntity
{
    public virtual int CodSituacionPago { get; set; }
}

[DefaultClassOptions]
[NavigationItem("Parámetros")]
[DefaultProperty(nameof(Descrip))]
[Table("SituacionMutual")]
public class SituacionMutual : BaseEntity
{
    [StringLength(2)]
    public virtual string? CodSituacionMutual { get; set; }

    [StringLength(50)]
    public virtual string? Descrip { get; set; }

    public virtual bool Pagar { get; set; }

    public override string ToString() => Descrip ?? string.Empty;
}

[DefaultClassOptions]
[NavigationItem("Parámetros")]
[DefaultProperty(nameof(Descrip))]
[Table("Departamento")]
public class Departamento : BaseEntity
{
    [StringLength(3)]
    public virtual string? CodDepartamento { get; set; }

    [StringLength(50)]
    public virtual string? Descrip { get; set; }

    public override string ToString() => Descrip ?? string.Empty;
}

[DefaultClassOptions]
[NavigationItem("Parámetros")]
[DefaultProperty(nameof(Descrip))]
[Table("Especialidad")]
public class Especialidad : BaseLookupEntity
{
    public virtual int CodEspecialidad { get; set; }
}

[DefaultClassOptions]
[NavigationItem("Parámetros")]
[DefaultProperty(nameof(Descrip))]
[Table("PrestacionTipo")]
public class PrestacionTipo : BaseEntity
{
    public virtual int CodPrestacionTipo { get; set; }

    [StringLength(50)]
    public virtual string? Descrip { get; set; }

    public virtual float? Importe { get; set; }

    [StringLength(3)]
    public virtual string? CodMoneda { get; set; }

    // Columnas del PrestacionTipo original (sgpaserv) que la migración había omitido (auditoría de columnas).
    public virtual DateTime? FechaVigencia { get; set; }
    public virtual double? ImporteTopeDISSE { get; set; }
    public virtual double? ImporteTopeCASEMED { get; set; }
    public virtual int? PeriodoRenovacion { get; set; }
    public virtual bool? Receta { get; set; }
    [Column(TypeName = "ntext")]
    public virtual string? Obs { get; set; }

    public override string ToString() => Descrip ?? string.Empty;
}

[DefaultClassOptions]
[NavigationItem("Parámetros")]
[DefaultProperty(nameof(Descrip))]
[Table("RecetaDistancia")]
public class RecetaDistancia : BaseEntity
{
    [StringLength(3)]
    public virtual string? CodRecetaDistancia { get; set; }

    [StringLength(50)]
    public virtual string? Descrip { get; set; }

    public override string ToString() => Descrip ?? string.Empty;
}

[NavigationItem("Parámetros")]
[Table("IMS")]
public class Ims : BaseEntity
{
    public virtual int? Mes { get; set; }
    public virtual int? Anio { get; set; }
    public virtual float? Importe { get; set; }
    public virtual int? AnioMes { get; set; }

    public override string ToString() => $"IMS {Anio}/{Mes}: {Importe}";
}

[NavigationItem("Parámetros")]
[Table("FranjaIRPF")]
public class FranjaIrpf : BaseEntity
{
    public virtual double? Desde { get; set; }
    public virtual double? Hasta { get; set; }
    public virtual double? Porcentaje { get; set; }

    public override string ToString() => $"IRPF {Desde}-{Hasta}: {Porcentaje}%";
}

[NavigationItem("Parámetros")]
[Table("InformeEstadistico")]
public class InformeEstadistico : BaseEntity
{
    public virtual int IdRpt { get; set; }

    [StringLength(50)]
    public virtual string? Grupo { get; set; }

    public virtual int? Orden { get; set; }

    [StringLength(255)]
    public virtual string? TituloPantalla { get; set; }

    [StringLength(255)]
    public virtual string? TituloRpt { get; set; }

    public virtual bool MesAnio { get; set; }
    public virtual bool Periodo { get; set; }
    public virtual bool Empresa { get; set; }
    public virtual bool Fecha { get; set; }
    public virtual bool GrupoEtario { get; set; }
    public virtual bool Patologia { get; set; }

    [Column(TypeName = "ntext")]
    public virtual string? Comentario { get; set; }

    public override string ToString() => TituloPantalla ?? string.Empty;
}

[DefaultClassOptions]
[NavigationItem("Parámetros")]
[DefaultProperty(nameof(Descrip))]
[Table("GrupoEtario")]
public class GrupoEtario : BaseEntity
{
    [Key]
    public virtual short EdadIni { get; set; }

    [StringLength(50)]
    public virtual string? Descrip { get; set; }

    public virtual short? EdadFin { get; set; }

    public override string ToString() => Descrip ?? $"{EdadIni}-{EdadFin}";
}

[NavigationItem("Parámetros")]
[Table("CertificacionesTmp")]
public class CertificacionesTmp : BaseEntity
{
    public virtual long? CI { get; set; }
    public virtual DateTime? FechaIni { get; set; }
    public virtual DateTime? FechaFin { get; set; }
    public virtual double? ImporteDeducible { get; set; }
    public virtual int? CodSalidaTipo { get; set; }
}
