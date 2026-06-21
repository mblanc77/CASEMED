using Sgpa.Data.Configuracion;
using Xunit;

namespace Sgpa.Business.Tests;

/// <summary>Humanización del nombre físico de tabla (nombre amigable por defecto, sin alias del admin).</summary>
public class TablaDisplayNameTests
{
    [Theory]
    [InlineData("SituacionMutual", "Situacion Mutual")]
    [InlineData("RegimenJubilatorio", "Regimen Jubilatorio")]
    [InlineData("RetencionPrestamo", "Retencion Prestamo")]
    [InlineData("SubsidioCabezal", "Subsidio Cabezal")]
    [InlineData("SubsidioItemCod_Afiliado", "Subsidio Item Cod Afiliado")]
    [InlineData("Afiliado", "Afiliado")]   // una palabra → igual
    [InlineData("IRPF", "IRPF")]           // sigla → no se parte
    public void Humanizar_separa_en_palabras(string input, string esperado)
        => Assert.Equal(esperado, TablaConfigService.Humanizar(input));
}
