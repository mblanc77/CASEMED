using System;
using System.Linq;
using System.Threading.Tasks;
using Sgpa.Business.Afiliados;
using Sgpa.Data;
using Sgpa.Data.Connection;
using Sgpa.Domain.Entities;
using Xunit;

namespace Sgpa.Business.Tests;

/// <summary>
/// Reglas del adelanto prejubilatorio (port de AbmAfili.AdPreJubOk). Las reglas síncronas (cédula, fecha de
/// presentación) se cubren puras; los chequeos de elegibilidad van contra la base (sólo lectura).
/// </summary>
public class AdPreJubValidatorTests
{
    private const string ConnectionString =
        "Data Source=localhost;Integrated Security=True;TrustServerCertificate=True;Encrypt=True;Initial Catalog=NewSgpa2";

    // Los avisos async (ruleset "Avisos") no corren en Validate() por defecto: con db nula no se tocan.
    private readonly AdPreJubValidator _v = new(new AfiliadoService(null!), new AdPreJubService(null!));

    [Fact]
    public void Falta_cedula_y_fecha_da_error()
    {
        var r = _v.Validate(new AdPreJub());
        Assert.Contains(r.Errors, e => e.PropertyName == nameof(AdPreJub.CI));
        Assert.Contains(r.Errors, e => e.PropertyName == nameof(AdPreJub.FechaPresentacion));
    }

    [Fact]
    public void Con_cedula_y_fecha_pasa_las_reglas_sincronas()
        => Assert.True(_v.Validate(new AdPreJub { CI = 13010559, FechaPresentacion = new DateTime(2026, 6, 1) }).IsValid);

    private static AdPreJubService NewService() => new(new DbExecutor(new SqlDbConnectionFactory(ConnectionString)));

    [Fact]   // valida que las funciones portadas 460_* existan y las consultas sean válidas
    public async Task Elegibilidad_de_cedula_inexistente_es_negativa()
    {
        var s = NewService();
        Assert.False(await s.TieneRegimenJubilatorioAsync(999_999_999));
        Assert.False(await s.TieneTrabajoActivoAsync(999_999_999));
        Assert.False(await s.TuvoSubsidioEnfermedadAsync(999_999_999));
    }
}
