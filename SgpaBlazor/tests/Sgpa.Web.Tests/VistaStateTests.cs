using DevExpress.Blazor;
using Sgpa.Web.Components.Crud;
using Xunit;

namespace Sgpa.Web.Tests;

/// <summary>Serialización del estado de personalización de la grilla (sumarios administrados por la app).</summary>
public class VistaStateTests
{
    [Fact]
    public void Summaries_sobreviven_el_round_trip_json()
    {
        var state = new VistaState
        {
            Summaries =
            {
                new SummaryDef { Column = "Importe", Type = GridSummaryItemType.Sum },
                new SummaryDef { Column = "CI", Type = GridSummaryItemType.Count },
            }
        };

        var back = VistaState.FromJson(state.ToJson());

        Assert.NotNull(back);
        Assert.Equal(2, back!.Summaries.Count);
        Assert.Equal("Importe", back.Summaries[0].Column);
        Assert.Equal(GridSummaryItemType.Sum, back.Summaries[0].Type);
        Assert.Equal(GridSummaryItemType.Count, back.Summaries[1].Type);
    }

    [Fact]
    public void FromJson_tolera_nulo_vacio_y_basura()
    {
        Assert.Null(VistaState.FromJson(null));
        Assert.Null(VistaState.FromJson(""));
        Assert.Null(VistaState.FromJson("no es json"));
    }
}
