using System.Collections.Generic;
using DevExpress.Data.Filtering;
using Sgpa.Domain.Entities;
using Sgpa.Web.Components.Crud;
using Xunit;

namespace Sgpa.Web.Tests;

/// <summary>Round-trip de parámetros de filtro: enumerar → parametrizar → (serializar/parsear) → sustituir.</summary>
public class FiltroParametrosTests
{
    [Fact]
    public void Parametrizar_y_sustituir_columna_simple()
    {
        var crit = CriteriaOperator.Parse("[Nombres] = 'Juan'");

        var leaves = FiltroParametros.Enumerar(crit, typeof(Afiliado));
        Assert.Single(leaves);   // un solo valor parametrizable

        var (pc, defs) = FiltroParametros.Parametrizar(crit, typeof(Afiliado),
            new Dictionary<int, string> { [leaves[0].Indice] = "Nombre" });
        Assert.Single(defs);
        Assert.Equal("Nombre", defs[0].Nombre);

        // Round-trip por string (como se persiste) y el parámetro sobrevive.
        var reparsed = CriteriaOperator.Parse(pc!.ToString());
        Assert.Contains("Nombre", FiltroParametros.Nombres(reparsed));

        // Sustituir el parámetro por un valor concreto → ya no quedan parámetros y aparece el valor.
        var concreto = FiltroParametros.Sustituir(reparsed, new Dictionary<string, object?> { ["Nombre"] = "Pedro" });
        Assert.Empty(FiltroParametros.Nombres(concreto));
        Assert.Contains("Pedro", concreto!.ToString());
    }

    [Fact]
    public void Parametrizar_dentro_de_un_exists_de_coleccion()
    {
        // Afiliados con alguna certificación efectiva → el valor 'True' se vuelve parámetro.
        var crit = CriteriaOperator.Parse("Certificaciones[Efectiva = True]");

        var leaves = FiltroParametros.Enumerar(crit, typeof(Afiliado));
        Assert.Single(leaves);

        var (pc, defs) = FiltroParametros.Parametrizar(crit, typeof(Afiliado),
            new Dictionary<int, string> { [leaves[0].Indice] = "Efe" });
        Assert.Single(defs);

        var reparsed = CriteriaOperator.Parse(pc!.ToString());
        Assert.Contains("Efe", FiltroParametros.Nombres(reparsed));

        var concreto = FiltroParametros.Sustituir(reparsed, new Dictionary<string, object?> { ["Efe"] = false });
        Assert.Empty(FiltroParametros.Nombres(concreto));
    }

    [Fact]
    public void Etiqueta_con_espacios_genera_identificador_seguro_y_round_trip()
    {
        var crit = CriteriaOperator.Parse("Certificacion[[FechaCertificacion] > #2020-01-01#]");
        var leaves = FiltroParametros.Enumerar(crit, typeof(Afiliado));
        Assert.Single(leaves);

        // El usuario pone una etiqueta CON ESPACIOS (era lo que rompía el Parse).
        var (pc, defs) = FiltroParametros.Parametrizar(crit, typeof(Afiliado),
            new Dictionary<int, string> { [leaves[0].Indice] = "Fecha de certificación desde" });
        Assert.Single(defs);
        Assert.Equal("Fecha de certificación desde", defs[0].Prompt);     // etiqueta libre
        Assert.DoesNotContain(' ', defs[0].Nombre);                        // identificador sin espacios

        // Lo crítico: el criterio parametrizado hace round-trip por string sin romper el Parse.
        var reparsed = CriteriaOperator.Parse(pc!.ToString());
        Assert.Contains(defs[0].Nombre, FiltroParametros.Nombres(reparsed));
        var concreto = FiltroParametros.Sustituir(reparsed,
            new Dictionary<string, object?> { [defs[0].Nombre] = new System.DateTime(2024, 6, 1) });
        Assert.Empty(FiltroParametros.Nombres(concreto));
    }

    [Fact]
    public void Valores_no_marcados_quedan_fijos()
    {
        var crit = CriteriaOperator.Parse("[Nombres] = 'Juan' AND [CodMutualista] = 3");
        var leaves = FiltroParametros.Enumerar(crit, typeof(Afiliado));
        Assert.Equal(2, leaves.Count);

        // Sólo se parametriza el primero; el segundo queda fijo.
        var (pc, defs) = FiltroParametros.Parametrizar(crit, typeof(Afiliado),
            new Dictionary<int, string> { [leaves[0].Indice] = "Nombre" });
        Assert.Single(defs);
        Assert.Single(FiltroParametros.Nombres(pc));   // un solo parámetro
        Assert.Contains("3", pc!.ToString());          // el valor fijo sigue presente
    }
}
