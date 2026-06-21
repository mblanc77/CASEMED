using System.Linq;
using Sgpa.Business.Afiliados;
using Sgpa.Domain.Entities;
using Xunit;

namespace Sgpa.Business.Tests;

/// <summary>Unit puro del validador de negocio del afiliado (FluentValidation).</summary>
public class AfiliadoValidatorTests
{
    private readonly AfiliadoValidator _v = new();

    private static Afiliado Valido(long ci) => new() { CI = ci, Nombres = "Juan", Apellido1 = "Pérez" };

    [Theory]
    [InlineData(1_234_567)]    // 7 dígitos
    [InlineData(12_345_678)]   // 8 dígitos
    [InlineData(123_456_789)]  // 9 dígitos
    public void Afiliado_completo_y_CI_en_rango_es_valido(long ci)
        => Assert.True(_v.Validate(Valido(ci)).IsValid);

    [Theory]
    [InlineData(0)]           // vacío
    [InlineData(123_456)]     // 6 dígitos
    [InlineData(1_000_000_000)] // 10 dígitos
    public void CI_fuera_de_rango_da_error_en_el_campo_CI(long ci)
    {
        var result = _v.Validate(Valido(ci));
        Assert.False(result.IsValid);
        Assert.Contains(result.Errors, e => e.PropertyName == nameof(Afiliado.CI));
    }

    [Fact]
    public void Nombre_y_primer_apellido_son_obligatorios()
    {
        var result = _v.Validate(new Afiliado { CI = 12_345_678 });
        Assert.False(result.IsValid);
        Assert.Contains(result.Errors, e => e.PropertyName == nameof(Afiliado.Nombres));
        Assert.Contains(result.Errors, e => e.PropertyName == nameof(Afiliado.Apellido1));
    }
}
