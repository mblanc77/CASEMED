using System.Collections.ObjectModel;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using DevExpress.Persistent.Base;
using NewSgpa.Module.BusinessObjects.Base;

namespace NewSgpa.Module.BusinessObjects.Prestamos;

// === Lookup / Catalog tables for SP ===

[DefaultClassOptions]
[NavigationItem("Préstamos")]
[DefaultProperty(nameof(Descrip))]
[Table("SP_Moneda")]
public class SpMoneda : BaseEntity
{
    [StringLength(3)]
    public virtual string? CodMoneda { get; set; }

    [StringLength(50)]
    public virtual string? Descrip { get; set; }

    public virtual float? Tasa { get; set; }
    public virtual float? TasaMora { get; set; }
    public virtual float? TasaCambio { get; set; }

    [StringLength(2)]
    public virtual string? CodAbitab { get; set; }

    [StringLength(255)]
    public virtual string? DescripLarga { get; set; }

    public override string ToString() => Descrip ?? string.Empty;
}

[DefaultClassOptions]
[NavigationItem("Préstamos")]
[DefaultProperty(nameof(Descrip))]
[Table("SP_PrestamoEstado")]
public class SpPrestamoEstado : BaseEntity
{
    [StringLength(3)]
    public virtual string? CodPrestamoEstado { get; set; }

    [StringLength(50)]
    public virtual string? Descrip { get; set; }

    public virtual bool Fin { get; set; }

    public override string ToString() => Descrip ?? string.Empty;
}

[DefaultClassOptions]
[NavigationItem("Préstamos")]
[DefaultProperty(nameof(Descrip))]
[Table("SP_ItemPago")]
public class SpItemPago : BaseEntity
{
    [StringLength(3)]
    public virtual string? CodItemPago { get; set; }

    [StringLength(50)]
    public virtual string? Descrip { get; set; }

    public override string ToString() => Descrip ?? string.Empty;
}

[DefaultClassOptions]
[NavigationItem("Préstamos")]
[DefaultProperty(nameof(Descrip))]
[Table("SP_FacturaEstado")]
public class SpFacturaEstado : BaseEntity
{
    [StringLength(3)]
    public virtual string? CodFacturaEstado { get; set; }

    [StringLength(50)]
    public virtual string? Descrip { get; set; }

    public virtual bool Anulada { get; set; }

    public override string ToString() => Descrip ?? string.Empty;
}

[DefaultClassOptions]
[NavigationItem("Préstamos")]
[DefaultProperty(nameof(Descrip))]
[Table("SP_FacturaTipo")]
public class SpFacturaTipo : BaseEntity
{
    [StringLength(50)]
    public virtual string? CodFacturaTipo { get; set; }

    [StringLength(50)]
    public virtual string? Descrip { get; set; }

    public override string ToString() => Descrip ?? string.Empty;
}

[DefaultClassOptions]
[NavigationItem("Préstamos")]
[DefaultProperty(nameof(Descrip))]
[Table("SP_CuotaEstado")]
public class SpCuotaEstado : BaseEntity
{
    [StringLength(3)]
    public virtual string? CodCuotaEstado { get; set; }

    [StringLength(50)]
    public virtual string? Descrip { get; set; }

    public override string ToString() => Descrip ?? string.Empty;
}

[DefaultClassOptions]
[NavigationItem("Préstamos")]
[DefaultProperty(nameof(Descrip))]
[Table("SP_PagoOrigen")]
public class SpPagoOrigen : BaseEntity
{
    [StringLength(3)]
    public virtual string? CodPagoOrigen { get; set; }

    [StringLength(50)]
    public virtual string? Descrip { get; set; }

    public override string ToString() => Descrip ?? string.Empty;
}

[DefaultClassOptions]
[NavigationItem("Préstamos")]
[DefaultProperty(nameof(Descrip))]
[Table("SP_RetencionItemCod")]
public class SpRetencionItemCod : BaseEntity
{
    [StringLength(3)]
    public virtual string? CodRetencionItemCod { get; set; }

    [StringLength(50)]
    public virtual string? Descrip { get; set; }

    [StringLength(50)]
    public virtual string? TopeaImporte { get; set; }

    public override string ToString() => Descrip ?? string.Empty;
}

[Table("SP_CtrlPrestamoEstado")]
public class SpCtrlPrestamoEstado : BaseEntity
{
    [StringLength(3)]
    public virtual string? PrestamoEstadoAnt { get; set; }

    [StringLength(3)]
    public virtual string? PrestamoEstadoSig { get; set; }
}

// === Main transactional entities ===

[DefaultClassOptions]
[NavigationItem("Préstamos")]
[DefaultProperty(nameof(DisplayText))]
[Table("SP_Prestamo")]
public class SpPrestamo : BaseEntity
{
    public virtual int IDPrestamo { get; set; }

    public virtual long? CI { get; set; }

    public virtual DateTime? Fecha { get; set; }

    public virtual int? CodEmpresa { get; set; }

    [StringLength(3)]
    public virtual string? CodMoneda { get; set; }
    [ForeignKey(nameof(CodMoneda))]
    public virtual SpMoneda? Moneda { get; set; }

    public virtual float? Importe { get; set; }
    public virtual int? Cuotas { get; set; }
    public virtual float? ImporteCuota { get; set; }

    [StringLength(3)]
    public virtual string? CodPrestamoEstado { get; set; }
    [ForeignKey(nameof(CodPrestamoEstado))]
    public virtual SpPrestamoEstado? PrestamoEstado { get; set; }

    [Column(TypeName = "ntext")]
    public virtual string? Observaciones { get; set; }

    // Columnas del SP_Prestamo original (sp.mdb) que el workbench de préstamos necesita y que la
    // primera versión de la migración había omitido. Ver tools/sql/sp-prestamo-extend.sql (Blazor).
    [StringLength(3)]
    public virtual string? CodPrestamoTipo { get; set; }
    public virtual float? Tasa { get; set; }
    public virtual float? Saldo { get; set; }
    public virtual int? CuotasPagas { get; set; }
    public virtual DateTime? FechaCobro { get; set; }
    public virtual int? NroSerieCheque { get; set; }
    public virtual int? NroCheque { get; set; }
    public virtual float? TasaCambio { get; set; }
    public virtual float? Promedio { get; set; }
    [StringLength(50)]
    public virtual string? Banco { get; set; }
    [StringLength(50)]
    public virtual string? Sucursal { get; set; }
    [StringLength(50)]
    public virtual string? NroCta { get; set; }
    public virtual int? IDPrestamoRef { get; set; }

    // Navigation - ignored due to keyless related entities
    [NotMapped]
    public virtual IList<SpFactura> Facturas { get; set; } = new ObservableCollection<SpFactura>();
    [NotMapped]
    public virtual IList<SpCuota> CuotasCollection { get; set; } = new ObservableCollection<SpCuota>();
    [NotMapped]
    public virtual IList<SpCuadroAmortizacion> CuadroAmortizacion { get; set; } = new ObservableCollection<SpCuadroAmortizacion>();
    [NotMapped]
    public virtual IList<SpPrestamoComentario> Comentarios { get; set; } = new ObservableCollection<SpPrestamoComentario>();

    [NotMapped]
    public string DisplayText => $"Prest #{IDPrestamo} - CI:{CI}";

    public override string ToString() => DisplayText;
}

[Table("SP_PrestamoComentario")]
public class SpPrestamoComentario : BaseEntity
{
    public virtual int? IDPrestamo { get; set; }
    [ForeignKey(nameof(IDPrestamo))]
    public virtual SpPrestamo? Prestamo { get; set; }

    public virtual DateTime? Fecha { get; set; }

    [Column(TypeName = "ntext")]
    public virtual string? Observaciones { get; set; }
}

[Table("SP_AfiliadoComentario")]
public class SpAfiliadoComentario : BaseEntity
{
    public virtual long? CI { get; set; }

    public virtual DateTime? Fecha { get; set; }

    [Column(TypeName = "ntext")]
    public virtual string? Observaciones { get; set; }
}

[DefaultClassOptions]
[NavigationItem("Préstamos")]
[Table("SP_Factura")]
public class SpFactura : BaseEntity
{
    public virtual int IDFactura { get; set; }
    public virtual int? NroFactura { get; set; }

    [StringLength(2)]
    public virtual string? NroEmpresa { get; set; }

    public virtual int? IdPrestamo { get; set; }
    [ForeignKey(nameof(IdPrestamo))]
    public virtual SpPrestamo? Prestamo { get; set; }

    public virtual DateTime? FechaEmitida { get; set; }
    public virtual DateTime? FechaVencimiento { get; set; }
    public virtual DateTime? FechaPago { get; set; }

    [StringLength(3)]
    public virtual string? CodMoneda { get; set; }
    [ForeignKey(nameof(CodMoneda))]
    public virtual SpMoneda? Moneda { get; set; }

    public virtual float? Importe { get; set; }

    [StringLength(3)]
    public virtual string? CodFacturaEstado { get; set; }
    [ForeignKey(nameof(CodFacturaEstado))]
    public virtual SpFacturaEstado? FacturaEstado { get; set; }

    public virtual float? TasaCambio { get; set; }

    [StringLength(50)]
    public virtual string? CodigoBarra { get; set; }

    public virtual int? Impresiones { get; set; }
    public virtual float? ImpAmortizable { get; set; }
    public virtual float? ImpInteres { get; set; }

    [StringLength(50)]
    public virtual string? CodFacturaTipo { get; set; }

    // Navigation - ignored due to keyless detail entities
    [NotMapped]
    public virtual IList<SpFacturaDetalle> Detalles { get; set; } = new ObservableCollection<SpFacturaDetalle>();
    [NotMapped]
    public virtual IList<SpPago> Pagos { get; set; } = new ObservableCollection<SpPago>();

    public override string ToString() => $"Factura #{IDFactura}";
}

[Table("SP_FacturaDetalle")]
public class SpFacturaDetalle : BaseEntity
{
    public virtual int? IdFactura { get; set; }
    [ForeignKey(nameof(IdFactura))]
    public virtual SpFactura? Factura { get; set; }

    public virtual int? NroReng { get; set; }

    [StringLength(3)]
    public virtual string? CodItemPago { get; set; }
    [ForeignKey(nameof(CodItemPago))]
    public virtual SpItemPago? ItemPago { get; set; }

    [StringLength(50)]
    public virtual string? Descrip { get; set; }

    public virtual int? NroCuota { get; set; }
    public virtual float? Importe { get; set; }
}

[Table("SP_Cuota")]
public class SpCuota : BaseEntity
{
    public virtual int? IDPrestamo { get; set; }
    [ForeignKey(nameof(IDPrestamo))]
    public virtual SpPrestamo? Prestamo { get; set; }

    public virtual int? Nro { get; set; }
    public virtual DateTime? FechaVencimiento { get; set; }
    public virtual DateTime? FechaPago { get; set; }

    [StringLength(3)]
    public virtual string? CodItemPago { get; set; }

    public virtual float? Importe { get; set; }

    [StringLength(3)]
    public virtual string? CodMoneda { get; set; }

    [StringLength(3)]
    public virtual string? CodCuotaEstado { get; set; }
    [ForeignKey(nameof(CodCuotaEstado))]
    public virtual SpCuotaEstado? CuotaEstado { get; set; }
}

[Table("SP_CuadroAmortizacion")]
public class SpCuadroAmortizacion : BaseEntity
{
    public virtual int? IDPrestamo { get; set; }
    [ForeignKey(nameof(IDPrestamo))]
    public virtual SpPrestamo? Prestamo { get; set; }

    public virtual int? NroCuota { get; set; }
    public virtual float? Monto { get; set; }
    public virtual float? ImporteCuota { get; set; }
    public virtual float? Interes { get; set; }
    public virtual float? Amortizacion { get; set; }
    public virtual float? Saldo { get; set; }
}

[Table("SP_Pago")]
public class SpPago : BaseEntity
{
    public virtual int? IDFactura { get; set; }
    [ForeignKey(nameof(IDFactura))]
    public virtual SpFactura? Factura { get; set; }

    public virtual DateTime? Fecha { get; set; }
    public virtual float? Importe { get; set; }

    [StringLength(50)]
    public virtual string? CodSucursal { get; set; }

    public virtual float? TasaCambio { get; set; }

    [StringLength(3)]
    public virtual string? CodPagoOrigen { get; set; }
    [ForeignKey(nameof(CodPagoOrigen))]
    public virtual SpPagoOrigen? PagoOrigen { get; set; }
}

[Table("SP_Pago_ItemPago")]
public class SpPagoItemPago : BaseEntity
{
    public virtual int? IDFactura { get; set; }

    [StringLength(3)]
    public virtual string? CodItemPago { get; set; }

    public virtual int? NroCuota { get; set; }
    public virtual float? Importe { get; set; }
}

[Table("SP_RetencionPrestamo")]
public class SpRetencionPrestamo : BaseEntity
{
    public virtual int? IDPrestamo { get; set; }
    [ForeignKey(nameof(IDPrestamo))]
    public virtual SpPrestamo? Prestamo { get; set; }

    public virtual long? CI { get; set; }
    public virtual DateTime? Fecha { get; set; }
    public virtual int? CodEmpresa { get; set; }

    [StringLength(3)]
    public virtual string? CodMoneda { get; set; }

    public virtual float? Importe { get; set; }
    public virtual float? Saldo { get; set; }
    public virtual float? ImpPago { get; set; }
}

[Table("SP_Retencion")]
public class SpRetencion : BaseEntity
{
    public virtual int? IDPrestamo { get; set; }
    [ForeignKey(nameof(IDPrestamo))]
    public virtual SpPrestamo? Prestamo { get; set; }

    public virtual DateTime? Fecha { get; set; }
    public virtual float? TipoCambio { get; set; }
    public virtual float? Importe { get; set; }

    [Column(TypeName = "ntext")]
    public virtual string? Observaciones { get; set; }

    public virtual bool Directa { get; set; }
}

[Table("SP_RetencionAviso")]
public class SpRetencionAviso : BaseEntity
{
    public virtual int? IDPrestamo { get; set; }
    [ForeignKey(nameof(IDPrestamo))]
    public virtual SpPrestamo? Prestamo { get; set; }

    public virtual DateTime? Fecha { get; set; }

    [Column(TypeName = "ntext")]
    public virtual string? Comentario { get; set; }
}

[Table("SP_RetencionItem")]
public class SpRetencionItem : BaseEntity
{
    public virtual int? IDPrestamo { get; set; }

    public virtual DateTime? Fecha { get; set; }
    public virtual int? IDFactura { get; set; }

    [StringLength(3)]
    public virtual string? CodRetencionItemCod { get; set; }
    [ForeignKey(nameof(CodRetencionItemCod))]
    public virtual SpRetencionItemCod? RetencionItemCod { get; set; }

    public virtual float? Importe { get; set; }
}

[Table("SP_RetencionPago")]
public class SpRetencionPago : BaseEntity
{
    public virtual int? IDPrestamo { get; set; }
    [ForeignKey(nameof(IDPrestamo))]
    public virtual SpPrestamo? Prestamo { get; set; }

    public virtual DateTime? Fecha { get; set; }
    public virtual int? Mes { get; set; }
    public virtual int? Anio { get; set; }
    public virtual float? Importe { get; set; }
}

[Table("SP_ImpLiquido")]
public class SpImpLiquido : BaseEntity
{
    public virtual long? CI { get; set; }
    public virtual int? CodEmpresa { get; set; }
    public virtual DateTime? Fechaingreso { get; set; }
    public virtual byte? Mes { get; set; }
    public virtual int? Anio { get; set; }
    public virtual int? IdTrabaja { get; set; }
    public virtual double? Importe { get; set; }
    public virtual int? AnioMes { get; set; }
}

[Table("MapeoAbitab")]
public class MapeoAbitab : BaseEntity
{
    public virtual int? Inicio { get; set; }
    public virtual int? Largo { get; set; }

    [StringLength(50)]
    public virtual string? Campo { get; set; }

    public virtual bool CodigoBarra { get; set; }
}

[Table("ErrCargaAbitab")]
public class ErrCargaAbitab : BaseEntity
{
    public virtual DateTime? Fecha { get; set; }
    public virtual int? NroReng { get; set; }

    [StringLength(100)]
    public virtual string? Descrip { get; set; }
}

[Table("SP_PagoError")]
public class SpPagoError : BaseEntity
{
    public virtual int? IDFactura { get; set; }
    public virtual DateTime? Fecha { get; set; }
    public virtual float? Importe { get; set; }

    [StringLength(50)]
    public virtual string? CodSucursal { get; set; }

    public virtual float? TasaCambio { get; set; }

    [StringLength(3)]
    public virtual string? CodFacturaEstado { get; set; }
}

[Table("SP_PagoParcial")]
public class SpPagoParcial : BaseEntity
{
    public virtual int? IDPrestamo { get; set; }
    [ForeignKey(nameof(IDPrestamo))]
    public virtual SpPrestamo? Prestamo { get; set; }

    public virtual DateTime? Fecha { get; set; }
    public virtual float? Importe { get; set; }
    public virtual float? TasaCambio { get; set; }
}

[Table("SP_Parametro")]
public class SpParametro : BaseEntity
{
    [StringLength(2)]
    public virtual string? NroEmpresa { get; set; }

    public virtual float? UR { get; set; }
    public virtual float? Dolar { get; set; }
    public virtual byte? Redondeo { get; set; }
    public virtual float? TopeUR { get; set; }
    public virtual float? PctPrestamo { get; set; }
    public virtual short? MesesCalculo { get; set; }
    public virtual short? MaxCuotas { get; set; }
    public virtual short? DiasTolerancia { get; set; }
    public virtual double? TopeSueldos { get; set; }
}

[Table("SP_PrestamoTipo")]
public class SpPrestamoTipo : BaseEntity
{
    [StringLength(3)]
    public virtual string? CodPrestamoTipo { get; set; }

    [StringLength(50)]
    public virtual string? Descrip { get; set; }

    public virtual bool TopeaImporte { get; set; }
}

