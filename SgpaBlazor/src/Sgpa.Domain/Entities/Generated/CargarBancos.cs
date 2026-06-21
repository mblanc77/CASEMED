using Sgpa.Domain.Metadata;

namespace Sgpa.Domain.Entities;

/// <summary>Entidad generada desde NewSgpa2. NO editar a mano (regenerar con Sgpa.CodeGen).</summary>
// Sin clave primaria — POCO de sólo lectura/consulta (no apto para SgpaCrudView).
[SgpaTable("CargarBancos")]
public partial class CargarBancos
{
    [SgpaColumn(Order = 1)]
    public int? Id { get; set; }

    [SgpaColumn(Order = 2)]
    public double? CI { get; set; }

    [SgpaColumn(Order = 3)]
    public double? Mes { get; set; }

    [SgpaColumn(Order = 4, MaxLength = 255)]
    public string? Nombres { get; set; }

    [SgpaColumn(Order = 5, MaxLength = 255)]
    public string? Apellido1 { get; set; }

    [SgpaColumn(Order = 6)]
    public double? Reliquidación { get; set; }

    [SgpaColumn(Order = 7, MaxLength = 255)]
    public string? Banco { get; set; }

    [SgpaColumn(Order = 8, MaxLength = 255)]
    public string? NroCuenta { get; set; }

    [SgpaColumn(Order = 9)]
    public double? CodBanco { get; set; }

}
