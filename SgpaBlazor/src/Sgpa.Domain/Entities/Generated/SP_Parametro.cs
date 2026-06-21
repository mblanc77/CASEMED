using Sgpa.Domain.Metadata;

namespace Sgpa.Domain.Entities;

/// <summary>Entidad generada desde NewSgpa2. NO editar a mano (regenerar con Sgpa.CodeGen).</summary>
// Sin clave primaria — POCO de sólo lectura/consulta (no apto para SgpaCrudView).
[SgpaTable("SP_Parametro")]
public partial class SP_Parametro
{
    [SgpaColumn(Order = 1, MaxLength = 2)]
    public string? NroEmpresa { get; set; }

    [SgpaColumn(Order = 2)]
    public float? UR { get; set; }

    [SgpaColumn(Order = 3)]
    public float? Dolar { get; set; }

    [SgpaColumn(Order = 4)]
    public byte? Redondeo { get; set; }

    [SgpaColumn(Order = 5)]
    public float? TopeUR { get; set; }

    [SgpaColumn(Order = 6)]
    public float? PctPrestamo { get; set; }

    [SgpaColumn(Order = 7)]
    public short? MesesCalculo { get; set; }

    [SgpaColumn(Order = 8)]
    public short? MaxCuotas { get; set; }

    [SgpaColumn(Order = 9)]
    public short? DiasTolerancia { get; set; }

    [SgpaColumn(Order = 10)]
    public double? TopeSueldos { get; set; }

    [SgpaAudit(Kind = SgpaAuditKind.User)]
    public string? Usr { get; set; }

    [SgpaAudit(Kind = SgpaAuditKind.Timestamp)]
    public DateTime? Ts { get; set; }

}
