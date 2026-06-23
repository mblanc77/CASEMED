using Sgpa.Domain.Metadata;

namespace Sgpa.Domain.Entities;

/// <summary>
/// Columnas del cabezal BPS (tabla <c>SubsidioCabezal_BPS</c>, 1:1 por IdSubsidio) expuestas en el listview
/// de subsidios. Se traen por la vista de lectura <c>dbo.SubsidioCabezalLista</c> (SubsidioCabezal LEFT JOIN
/// SubsidioCabezal_BPS): marcadas <see cref="SgpaColumnAttribute.Computed"/> para que el CRUD genérico las
/// LEA de la vista pero NO las persista (INSERT/UPDATE van a SubsidioCabezal). Así quedan ordenables,
/// filtrables, agrupables y <strong>totalizables</strong> server-side, igual que cualquier columna real.
/// </summary>
[SgpaReadSource("SubsidioCabezalLista")]
public partial class SubsidioCabezal
{
    [SgpaColumn(Order = 20, Computed = true, VisibleInDetail = false, DisplayFormat = "0", Caption = "Días BPS")]
    public int? DiasBPS { get; set; }

    [SgpaColumn(Order = 21, Computed = true, VisibleInDetail = false, DisplayFormat = "n2", Caption = "Líquido BPS")]
    public double? LiquidoBPS { get; set; }

    // En la tabla BPS la columna se llama LiquidoPagar; la vista la expone como LiquidoPagarBPS.
    [SgpaColumn(Order = 22, Computed = true, VisibleInDetail = false, DisplayFormat = "n2", Caption = "Líquido a pagar")]
    public double? LiquidoPagarBPS { get; set; }
}
