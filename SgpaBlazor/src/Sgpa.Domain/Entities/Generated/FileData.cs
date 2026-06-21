using Sgpa.Domain.Metadata;

namespace Sgpa.Domain.Entities;

/// <summary>Entidad generada desde NewSgpa2. NO editar a mano (regenerar con Sgpa.CodeGen).</summary>
[SgpaTable("FileData")]
public partial class FileData
{
    [SgpaColumn(Order = 1, Required = true)]
    [SgpaKey]
    public Guid ID { get; set; }

    [SgpaColumn(Order = 2, Required = true)]
    public int Size { get; set; }

    [SgpaColumn(Order = 3)]
    public string? FileName { get; set; }

    [SgpaColumn(Order = 4)]
    public byte[]? Content { get; set; }

}
