using System;
using System.Linq;
using Sgpa.Business.Afiliados;
using Sgpa.Domain.Entities;
using Xunit;

namespace Sgpa.Business.Tests;

/// <summary>Unit puro del validador de la prima por fallecimiento (al menos una de las dos fechas).</summary>
public class PrimaFallecimientoValidatorTests
{
    private readonly PrimaFallecimientoValidator _v = new();

    [Fact]
    public void Sin_ninguna_fecha_es_invalido()
    {
        var r = _v.Validate(new PrimaFallecimiento { CI = 12345678 });
        Assert.False(r.IsValid);
        Assert.Contains(r.Errors, e => e.PropertyName == "Fechas");
    }

    [Fact]
    public void Con_fecha_de_firma_es_valido()
        => Assert.True(_v.Validate(new PrimaFallecimiento { FechaFirma = new DateTime(2026, 6, 1) }).IsValid);

    [Fact]
    public void Con_fecha_de_fallecimiento_es_valido()
        => Assert.True(_v.Validate(new PrimaFallecimiento { FechaFallecimiento = new DateTime(2026, 6, 1) }).IsValid);
}
