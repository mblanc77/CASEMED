using System.Linq;
using Sgpa.Domain.Entities;
using Sgpa.Domain.Metadata;
using Xunit;

namespace Sgpa.Business.Tests;

/// <summary>Valida la resolución de FK por convención (EntityCatalog.LookupTargetFor) y DisplayColumn.</summary>
public class MetadataLookupTests
{
    [Fact]
    public void FK_a_catalogo_chico_resuelve_destino_y_descripcion()
    {
        var meta = EntityMetadata.For<AfeccionTipo>();
        var col = meta.Columns.First(c => c.Name == "CodAfeccionGrupo");
        var target = EntityCatalog.LookupTargetFor(col, meta);

        Assert.NotNull(target);
        Assert.Equal("AfeccionGrupo", target!.Table);
        Assert.NotNull(target.DisplayColumn);
        Assert.Equal("Descrip", target.DisplayColumn!.Name);
    }

    [Fact]
    public void Afiliado_CodMutualista_resuelve_a_Mutualista()
    {
        var meta = EntityMetadata.For<Afiliado>();
        var col = meta.Columns.First(c => c.Name == "CodMutualista");
        var target = EntityCatalog.LookupTargetFor(col, meta);

        Assert.NotNull(target);
        Assert.Equal("Mutualista", target!.Table);
        Assert.NotNull(target.DisplayColumn);
    }

    [Fact]
    public void La_propia_clave_no_es_lookup()
    {
        var meta = EntityMetadata.For<AfeccionGrupo>();
        Assert.Null(EntityCatalog.LookupTargetFor(meta.Keys[0], meta));
    }

    [Fact]
    public void Columna_sin_entidad_destino_no_es_lookup()
    {
        var meta = EntityMetadata.For<AfeccionGrupo>();
        var descrip = meta.Columns.First(c => c.Name == "Descrip");
        Assert.Null(EntityCatalog.LookupTargetFor(descrip, meta));
    }
}
