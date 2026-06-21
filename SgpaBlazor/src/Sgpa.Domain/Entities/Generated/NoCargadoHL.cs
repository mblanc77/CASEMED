using Sgpa.Domain.Metadata;

namespace Sgpa.Domain.Entities;

/// <summary>Entidad generada desde NewSgpa2. NO editar a mano (regenerar con Sgpa.CodeGen).</summary>
// Clave compuesta: CI, CodEmpresa, Mes, Anio.
[SgpaTable("NoCargadoHL")]
public partial class NoCargadoHL
{
    [SgpaColumn(Order = 1, Required = true)]
    [SgpaKey]
    public long CI { get; set; }

    [SgpaColumn(Order = 2, Required = true)]
    [SgpaKey]
    public int CodEmpresa { get; set; }

    [SgpaColumn(Order = 3, Required = true)]
    [SgpaKey]
    public int Mes { get; set; }

    [SgpaColumn(Order = 4, Required = true)]
    [SgpaKey]
    public int Anio { get; set; }

    [SgpaColumn(Order = 5, MaxLength = 50)]
    public string? Apellido1 { get; set; }

    [SgpaColumn(Order = 6, MaxLength = 50)]
    public string? Apellido2 { get; set; }

    [SgpaColumn(Order = 7, MaxLength = 50)]
    public string? Nombres { get; set; }

    [SgpaAudit(Kind = SgpaAuditKind.User)]
    public string? Usr { get; set; }

    [SgpaAudit(Kind = SgpaAuditKind.Timestamp)]
    public DateTime? Ts { get; set; }

}
