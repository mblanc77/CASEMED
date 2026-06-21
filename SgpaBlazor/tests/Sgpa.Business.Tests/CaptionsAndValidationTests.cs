using System.Linq;
using Sgpa.Domain.Entities;
using Sgpa.Domain.Metadata;
using Xunit;

namespace Sgpa.Business.Tests;

/// <summary>Captions en español (SpanishCaptions) y validación dirigida por metadata (MetadataValidation).</summary>
public class CaptionsAndValidationTests
{
    [Theory]
    [InlineData("CodMutualista", "Código mutualista")]
    [InlineData("FechaIngMutualista", "Fecha ingreso mutualista")]
    [InlineData("CI", "Cédula")]
    [InlineData("ImpLiquido", "Importe líquido")]
    [InlineData("LiquidoBPS", "Líquido BPS")]   // acrónimo se respeta
    [InlineData("NroCuenta", "Nº cuenta")]
    [InlineData("Apellido1", "Apellido 1")]     // se separa el dígito
    [InlineData("Telefono", "Teléfono")]        // acento
    public void Humanize_genera_caption_en_espanol(string input, string expected)
        => Assert.Equal(expected, SpanishCaptions.Humanize(input));

    [Fact]
    public void Metadata_usa_caption_humanizado_cuando_no_hay_override()
    {
        var meta = EntityMetadata.For<Afiliado>();
        var col = meta.Columns.First(c => c.Name == "CodMutualista");
        Assert.Equal("Código mutualista", col.Caption);
    }

    [Fact]
    public void Generador_marca_required_y_maxlength_desde_el_catalogo()
    {
        var meta = EntityMetadata.For<MaeFun>();
        var apellido = meta.Columns.First(c => c.Name == "Apellido1");
        Assert.True(apellido.Required);
        Assert.Equal(15, apellido.MaxLength);
    }

    [Fact]
    public void Validacion_reporta_requerido_vacio()
    {
        var meta = EntityMetadata.For<MaeFun>();
        var model = new MaeFun(); // Apellido1/Nombre1 = null (requeridos)

        var errors = MetadataValidation.Validate(meta, model, isNew: true);

        Assert.Contains(errors, e => e.Column.Name == "Apellido1");
    }

    [Fact]
    public void Validacion_reporta_largo_excedido()
    {
        var meta = EntityMetadata.For<MaeFun>();
        var model = new MaeFun { Apellido1 = new string('X', 20), Nombre1 = "Ok" }; // MaxLength=15

        var errors = MetadataValidation.Validate(meta, model, isNew: true);

        var err = Assert.Single(errors, e => e.Column.Name == "Apellido1");
        Assert.Contains("15", err.Message);
    }

    [Fact]
    public void Validacion_ok_cuando_se_cumplen_las_reglas()
    {
        var meta = EntityMetadata.For<MaeFun>();
        var model = new MaeFun { Apellido1 = "Pérez", Nombre1 = "Juan" };

        var errors = MetadataValidation.Validate(meta, model, isNew: true);

        // No debe haber errores de Apellido1/Nombre1 (los demás campos requeridos son de valor, ya presentes).
        Assert.DoesNotContain(errors, e => e.Column.Name is "Apellido1" or "Nombre1");
    }
}
