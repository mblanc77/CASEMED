using Sgpa.Domain.Metadata;

namespace Sgpa.Domain.Entities;

/// <summary>Entidad generada desde NewSgpa2. NO editar a mano (regenerar con Sgpa.CodeGen).</summary>
// Sin clave primaria — POCO de sólo lectura/consulta (no apto para SgpaCrudView).
[SgpaTable("CtaCteRet")]
public partial class CtaCteRet
{
    [SgpaColumn(Order = 1)]
    public int? IDPrestamo { get; set; }

    [SgpaColumn(Order = 2)]
    public double? Importe { get; set; }

    [SgpaColumn(Order = 3)]
    public double? cobros { get; set; }

    [SgpaColumn(Order = 4)]
    public double? saldo { get; set; }

}
