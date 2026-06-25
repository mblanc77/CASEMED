using System;
using System.Linq;
using Sgpa.Business.Certificaciones;
using Sgpa.Domain.Entities;
using Xunit;

namespace Sgpa.Business.Tests;

/// <summary>Unit puro del validador síncrono de la certificación (FluentValidation).</summary>
public class CertificacionValidatorTests
{
    // Los avisos por días (ruleset "Avisos", async, consulta a base) no se ejecutan en Validate() por defecto, así
    // que el service con db nula nunca se toca: estos tests cubren sólo las reglas síncronas (fechas).
    private readonly CertificacionValidator _v = new(new CertificacionService(null!));

    [Fact]
    public void No_efectiva_no_exige_fechas()
        => Assert.True(_v.Validate(new Certificacion { Efectiva = false, CI = 13010559 }).IsValid);

    [Fact]
    public void Sin_cedula_da_error_aunque_no_sea_efectiva()
    {
        var r = _v.Validate(new Certificacion { Efectiva = false });
        Assert.False(r.IsValid);
        Assert.Contains(r.Errors, e => e.PropertyName == nameof(Certificacion.CI));
    }

    [Fact]
    public void Efectiva_sin_fechas_da_error_en_cada_fecha()
    {
        var r = _v.Validate(new Certificacion { Efectiva = true, CI = 13010559 });
        Assert.False(r.IsValid);
        Assert.Contains(r.Errors, e => e.PropertyName == nameof(Certificacion.FechaCertificacion));
        Assert.Contains(r.Errors, e => e.PropertyName == nameof(Certificacion.FechaIni));
        Assert.Contains(r.Errors, e => e.PropertyName == nameof(Certificacion.FechaFin));
    }

    [Fact]
    public void Efectiva_con_inicio_mayor_que_fin_da_error_en_FechaFin()
    {
        var r = _v.Validate(new Certificacion
        {
            Efectiva = true,
            FechaCertificacion = new DateTime(2026, 6, 1),
            FechaIni = new DateTime(2026, 6, 10),
            FechaFin = new DateTime(2026, 6, 5),
        });
        Assert.Contains(r.Errors, e => e.PropertyName == nameof(Certificacion.FechaFin));
    }

    [Fact]
    public void Efectiva_con_fechas_coherentes_es_valida()
        => Assert.True(_v.Validate(new Certificacion
        {
            Efectiva = true,
            CI = 13010559,
            FechaCertificacion = new DateTime(2026, 6, 1),
            FechaIni = new DateTime(2026, 6, 1),
            FechaFin = new DateTime(2026, 6, 10),
        }).IsValid);
}
