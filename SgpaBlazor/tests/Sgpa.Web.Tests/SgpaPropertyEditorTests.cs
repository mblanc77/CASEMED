using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Bunit;
using DevExpress.Blazor;
using Microsoft.Extensions.DependencyInjection;
using Sgpa.Data.Crud;
using Sgpa.Domain.Entities;
using Sgpa.Domain.Metadata;
using Sgpa.Web.Components.Crud;

namespace Sgpa.Web.Tests;

/// <summary>
/// Verifica que el editor genérico de propiedad usa un combo (DxComboBox) para columnas FK
/// resueltas por convención, y el editor por tipo para las demás.
/// </summary>
public class SgpaPropertyEditorTests : TestContext
{
    private sealed class FakeLookups : ISgpaLookupService
    {
        public Task<IReadOnlyList<LookupItem>?> GetAsync(Type entityType, CancellationToken cancellationToken = default)
            => Task.FromResult<IReadOnlyList<LookupItem>?>(new List<LookupItem>
            {
                new() { Value = 25, Text = "Grupo A" },
                new() { Value = 75, Text = "Grupo B" },
            });

        public Task<IReadOnlyDictionary<string, string>> GetDescriptionsAsync(
            Type entityType, IReadOnlyCollection<object> keys, CancellationToken cancellationToken = default)
            => Task.FromResult<IReadOnlyDictionary<string, string>>(
                new Dictionary<string, string> { ["25"] = "Grupo A", ["75"] = "Grupo B" });
    }

    public SgpaPropertyEditorTests()
    {
        JSInterop.Mode = JSRuntimeMode.Loose;
        Services.AddDevExpressBlazor();
        Services.AddSingleton<ISgpaLookupService>(new FakeLookups());
    }

    [Fact]
    public void Columna_FK_renderiza_combo()
    {
        var meta = EntityMetadata.For<AfeccionTipo>();
        var col = meta.Columns.First(c => c.Name == "CodAfeccionGrupo"); // FK → AfeccionGrupo

        var cut = RenderComponent<SgpaPropertyEditor>(p => p
            .Add(x => x.Model, new AfeccionTipo())
            .Add(x => x.Column, col)
            .Add(x => x.Owner, meta));

        Assert.Single(cut.FindComponents<DxComboBox<LookupItem, object>>());
    }

    [Fact]
    public void Columna_no_FK_no_renderiza_combo()
    {
        var meta = EntityMetadata.For<AfeccionTipo>();
        var col = meta.Columns.First(c => c.Name == "Descrip"); // no es FK

        var cut = RenderComponent<SgpaPropertyEditor>(p => p
            .Add(x => x.Model, new AfeccionTipo())
            .Add(x => x.Column, col)
            .Add(x => x.Owner, meta));

        Assert.Empty(cut.FindComponents<DxComboBox<LookupItem, object>>());
    }
}
