using System.Linq;
using Sgpa.Domain.Entities;
using Sgpa.Domain.Metadata;
using Xunit;

namespace Sgpa.Business.Tests;

/// <summary>La decisión "por tabla" de edición inline (EditRow) vs popup para el CRUD genérico.</summary>
public class EntityCatalogTests
{
    [Fact]
    public void Lookup_resuelve_FK_que_es_parte_de_clave_compuesta_en_grilla_y_formulario()
    {
        // Trabaja.CodEmpresa es parte de la clave primaria COMPUESTA y a la vez FK a Empresa: tanto la grilla
        // (mostrar nombre) como el formulario (combo para elegir empresa) deben resolverla.
        var meta = EntityMetadata.For<Trabaja>();
        var col = meta.Columns.First(c => c.Name == "CodEmpresa");

        Assert.Equal("Empresa", EntityCatalog.LookupDisplayTargetFor(col, meta)?.Table);
        Assert.Equal("Empresa", EntityCatalog.LookupTargetFor(col, meta)?.Table);
    }

    [Fact]
    public void Lookup_no_resuelve_la_identidad_propia_de_la_entidad()
    {
        // En Empresa, CodEmpresa es su clave SIMPLE (identidad): no es combo en el formulario ni se resuelve a sí misma.
        var meta = EntityMetadata.For<Empresa>();
        var col = meta.Columns.First(c => c.Name == "CodEmpresa");
        Assert.Null(EntityCatalog.LookupTargetFor(col, meta));
        Assert.Null(EntityCatalog.LookupDisplayTargetFor(col, meta));
    }

}
