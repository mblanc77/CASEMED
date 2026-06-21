using System.Drawing;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.ConditionalAppearance;
using DevExpress.ExpressApp.Editors;
using DevExpress.ExpressApp.Model;
using NewSgpa.Module.BusinessObjects.Sgpa;

namespace NewSgpa.Module.Controllers;

/// <summary>
/// Customizes list view filters for key entities.
/// Provides default filter criteria matching common VB6 usage patterns.
/// </summary>
public class DefaultFilterController : ViewController<ListView>
{
    protected override void OnActivated()
    {
        base.OnActivated();
        ApplyDefaultFilter();
    }

    private void ApplyDefaultFilter()
    {
        var typeName = View.ObjectTypeInfo?.Type?.Name;

        switch (typeName)
        {
            case nameof(SubsidioCabezal):
                // Default: show current year subsidios
                var currentYear = DateTime.Today.Year;
                if (string.IsNullOrEmpty(View.CollectionSource.Criteria?["DefaultYear"]?.ToString()))
                {
                    View.CollectionSource.Criteria["DefaultYear"] =
                        DevExpress.Data.Filtering.CriteriaOperator.Parse($"Anio = {currentYear}");
                }
                break;

            case nameof(Certificacion):
                // Default: show last 6 months of certifications
                var sixMonthsAgo = DateTime.Today.AddMonths(-6);
                if (string.IsNullOrEmpty(View.CollectionSource.Criteria?["DefaultPeriod"]?.ToString()))
                {
                    View.CollectionSource.Criteria["DefaultPeriod"] =
                        DevExpress.Data.Filtering.CriteriaOperator.Parse(
                            $"FechaIni >= #{sixMonthsAgo:yyyy-MM-dd}#");
                }
                break;

            case nameof(Imponible):
                // Default: show last 12 months
                var lastYear = DateTime.Today.AddMonths(-12);
                var currentPeriod = lastYear.Year * 100 + lastYear.Month;
                if (string.IsNullOrEmpty(View.CollectionSource.Criteria?["DefaultPeriod"]?.ToString()))
                {
                    View.CollectionSource.Criteria["DefaultPeriod"] =
                        DevExpress.Data.Filtering.CriteriaOperator.Parse(
                            $"AnioMes >= {currentPeriod}");
                }
                break;
        }
    }
}
