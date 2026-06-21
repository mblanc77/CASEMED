using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Actions;
using DevExpress.Persistent.Base;
using Microsoft.Extensions.DependencyInjection;
using NewSgpa.Module.BusinessObjects;
using NewSgpa.Module.BusinessObjects.Sgpa;
using NewSgpa.Module.Services;

namespace NewSgpa.Module.Controllers;

/// <summary>
/// XAF Controller for AdPreJub (Adelanto Pre-Jubilatorio) operations.
/// Replaces VB6 frmAdPreJub toolbar button "generar".
/// </summary>
public class AdPreJubController : ViewController<ListView>
{
    private readonly SimpleAction _generarPagosAction;

    public AdPreJubController()
    {
        TargetObjectType = typeof(AdPreJub);

        _generarPagosAction = new SimpleAction(this, "GenerarPagosPreJub", PredefinedCategory.RecordEdit)
        {
            Caption = "Generar Pagos",
            ConfirmationMessage = "¿Generar pagos pre-jubilatorios para el mes actual? " +
                                  "Se eliminarán los pagos previos del mismo período.",
            ImageName = "Action_Grant",
            ToolTip = "Genera los pagos mensuales para todos los afiliados pre-jubilatorios activos."
        };
        _generarPagosAction.Execute += GenerarPagosAction_Execute;
    }

    private async void GenerarPagosAction_Execute(object? sender, SimpleActionExecuteEventArgs e)
    {
        var os = Application.CreateObjectSpace(typeof(AdPreJub));
        var dbContext = ((IObjectSpaceLink)os).ObjectSpace?.ServiceProvider?
            .GetRequiredService<NewSgpaEFCoreDbContext>();

        if (dbContext == null)
        {
            Application.ShowViewStrategy.ShowMessage(
                "Error: No se pudo obtener el contexto de datos.",
                InformationType.Error);
            return;
        }

        var now = DateTime.Today;
        string usuario = SecuritySystem.CurrentUserName ?? "system";
        var service = new AdPreJubService(dbContext);

        try
        {
            int count = await service.GenerarPagosAsync(now.Month, now.Year, usuario);
            Application.ShowViewStrategy.ShowMessage(
                $"Pagos generados para {count} afiliados ({now.Month:D2}/{now.Year}).",
                InformationType.Success);
            View.ObjectSpace.Refresh();
        }
        catch (Exception ex)
        {
            Application.ShowViewStrategy.ShowMessage(
                $"Error al generar pagos: {ex.Message}", InformationType.Error);
        }
    }
}
