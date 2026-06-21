using Sgpa.Domain.Metadata;

namespace Sgpa.Domain.Entities;

/// <summary>Entidad generada desde NewSgpa2. NO editar a mano (regenerar con Sgpa.CodeGen).</summary>
// Sin clave primaria — POCO de sólo lectura/consulta (no apto para SgpaCrudView).
[SgpaTable("MaeFun")]
public partial class MaeFun
{
    [SgpaColumn(Order = 1)]
    public int? NroFun { get; set; }

    [SgpaColumn(Order = 2)]
    public int? NroCuenta { get; set; }

    [SgpaColumn(Order = 3, Required = true, MaxLength = 15)]
    public string? Apellido1 { get; set; }

    [SgpaColumn(Order = 4, MaxLength = 15)]
    public string? Apellido2 { get; set; }

    [SgpaColumn(Order = 5, Required = true, MaxLength = 15)]
    public string? Nombre1 { get; set; }

    [SgpaColumn(Order = 6, MaxLength = 15)]
    public string? Nombre2 { get; set; }

    [SgpaColumn(Order = 7)]
    public int? Cedula { get; set; }

    [SgpaColumn(Order = 8)]
    public short? Nacionalidad { get; set; }

    [SgpaColumn(Order = 9)]
    public DateTime? FecNac { get; set; }

    [SgpaColumn(Order = 10)]
    public short? EstCivil { get; set; }

    [SgpaColumn(Order = 11, MaxLength = 20)]
    public string? DirCalle { get; set; }

    [SgpaColumn(Order = 12, MaxLength = 4)]
    public string? DirPuerta { get; set; }

    [SgpaColumn(Order = 13, MaxLength = 2)]
    public string? DirBis { get; set; }

    [SgpaColumn(Order = 14, MaxLength = 2)]
    public string? DirPiso { get; set; }

    [SgpaColumn(Order = 15, MaxLength = 4)]
    public string? DirApto { get; set; }

    [SgpaColumn(Order = 16, MaxLength = 2)]
    public string? DirBloque { get; set; }

    [SgpaColumn(Order = 17, MaxLength = 30)]
    public string? DirLocal { get; set; }

    [SgpaColumn(Order = 18)]
    public int? CodCargo { get; set; }

    [SgpaColumn(Order = 19, MaxLength = 25)]
    public string? DesCargo { get; set; }

    [SgpaColumn(Order = 20, MaxLength = 8)]
    public string? Telefono { get; set; }

    [SgpaColumn(Order = 21)]
    public DateTime? FecIngreso { get; set; }

    [SgpaColumn(Order = 22)]
    public bool? InfDbla { get; set; }

    [SgpaColumn(Order = 23)]
    public DateTime? FecInfDbla { get; set; }

    [SgpaColumn(Order = 24)]
    public short? AsigCuenta { get; set; }

    [SgpaColumn(Order = 25)]
    public DateTime? FecAsigCta { get; set; }

}
