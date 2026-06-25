using Sgpa.Business.Afiliados;
using Sgpa.Business.Reintegros;
using Sgpa.Domain.Entities;
using Xunit;

namespace Sgpa.Business.Tests;

/// <summary>Unit puro del validador del reintegro mutual (FluentValidation).</summary>
public class ReintegroMutualValidatorTests
{
    // Los avisos (ruleset "Avisos", async, consulta a base) no se ejecutan en Validate() por defecto, así que el
    // service con db nula nunca se toca: estos tests cubren sólo las reglas síncronas (mes/año).
    private readonly ReintegroMutualValidator _v = new(new ReintegroService(null!), new AfiliadoService(null!));

    [Fact]
    public void Mes_y_anio_validos_es_valido()
        => Assert.True(_v.Validate(new ReintegroMutual { CI = 12345678, Mes = 6, Anio = 2026 }).IsValid);

    [Theory]
    [InlineData(0)]
    [InlineData(13)]
    public void Mes_fuera_de_1_a_12_da_error(int mes)
    {
        var r = _v.Validate(new ReintegroMutual { Mes = mes, Anio = 2026 });
        Assert.Contains(r.Errors, e => e.PropertyName == nameof(ReintegroMutual.Mes));
    }

    [Fact]
    public void Anio_cero_da_error()
        => Assert.Contains(_v.Validate(new ReintegroMutual { Mes = 6, Anio = 0 }).Errors,
            e => e.PropertyName == nameof(ReintegroMutual.Anio));
}
