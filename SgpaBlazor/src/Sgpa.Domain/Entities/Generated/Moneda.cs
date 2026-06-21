using Sgpa.Domain.Metadata;

namespace Sgpa.Domain.Entities;

/// <summary>Entidad generada desde NewSgpa2. NO editar a mano (regenerar con Sgpa.CodeGen).</summary>
// Sin clave primaria — POCO de sólo lectura/consulta (no apto para SgpaCrudView).
[SgpaTable("Moneda")]
public partial class Moneda
{
    [SgpaColumn(Column = "Moneda", Order = 1, MaxLength = 3)]
    public string? Moneda_ { get; set; }

}
