using Sgpa.Domain.Metadata;

namespace Sgpa.Domain.Entities;

/// <summary>Entidad generada desde NewSgpa2. NO editar a mano (regenerar con Sgpa.CodeGen).</summary>
// Clave compuesta: CodEmpresa, Mes, Anio.
[SgpaTable("EmpresaPago")]
public partial class EmpresaPago
{
    [SgpaColumn(Order = 1, Required = true)]
    [SgpaKey]
    public int CodEmpresa { get; set; }

    [SgpaColumn(Order = 2, Required = true)]
    [SgpaKey]
    public int Mes { get; set; }

    [SgpaColumn(Order = 3, Required = true)]
    [SgpaKey]
    public int Anio { get; set; }

    [SgpaColumn(Order = 4)]
    public int? Importe { get; set; }

    [SgpaAudit(Kind = SgpaAuditKind.User)]
    public string? Usr { get; set; }

    [SgpaAudit(Kind = SgpaAuditKind.Timestamp)]
    public DateTime? Ts { get; set; }

}
