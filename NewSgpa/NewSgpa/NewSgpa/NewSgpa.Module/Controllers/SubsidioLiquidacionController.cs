using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Actions;
using DevExpress.Persistent.Base;
using NewSgpa.Module.BusinessObjects.Sgpa;
using NewSgpa.Module.BusinessObjects;
using NewSgpa.Module.Services;
using Microsoft.Extensions.DependencyInjection;

namespace NewSgpa.Module.Controllers;

/// <summary>
/// XAF Controller for subsidio liquidation operations.
/// Replaces VB6 frmLiquidaSubsidio toolbar buttons:
///   - Liquidar / Simular
///   - Cargar Liquidación BPS
///   - Exportar (NBC, BROU, BPS)
/// </summary>
public class SubsidioLiquidacionController : ViewController<ListView>
{
    private readonly SimpleAction _liquidarAction;
    private readonly SimpleAction _simularAction;
    private readonly SimpleAction _cargarBpsAction;
    private readonly SimpleAction _exportNbcAction;
    private readonly SimpleAction _exportBrouAction;
    private readonly SimpleAction _exportBpsAction;

    public SubsidioLiquidacionController()
    {
        TargetObjectType = typeof(SubsidioCabezal);

        _liquidarAction = new SimpleAction(this, "LiquidarSubsidio", PredefinedCategory.RecordEdit)
        {
            Caption = "Liquidar Subsidios",
            ConfirmationMessage = "¿Está seguro de ejecutar la liquidación definitiva?",
            ImageName = "Action_Grant",
            ToolTip = "Ejecuta la liquidación de subsidios para el período seleccionado"
        };
        _liquidarAction.Execute += LiquidarAction_Execute;

        _simularAction = new SimpleAction(this, "SimularSubsidio", PredefinedCategory.RecordEdit)
        {
            Caption = "Simular Liquidación",
            ImageName = "Action_Debug_Start",
            ToolTip = "Simula la liquidación sin generar recibos"
        };
        _simularAction.Execute += SimularAction_Execute;

        _cargarBpsAction = new SimpleAction(this, "CargarBPS", PredefinedCategory.RecordEdit)
        {
            Caption = "Cargar BPS",
            ImageName = "Action_Reload",
            ToolTip = "Recalcular datos BPS para el período actual"
        };
        _cargarBpsAction.Execute += CargarBpsAction_Execute;

        _exportNbcAction = new SimpleAction(this, "ExportarNBC", PredefinedCategory.Export)
        {
            Caption = "Exportar NBC",
            ImageName = "Action_Export",
            ToolTip = "Generar archivo de pago para Nuevo Banco Comercial"
        };
        _exportNbcAction.Execute += ExportNbcAction_Execute;

        _exportBrouAction = new SimpleAction(this, "ExportarBROU", PredefinedCategory.Export)
        {
            Caption = "Exportar BROU",
            ImageName = "Action_Export",
            ToolTip = "Generar archivo de pago para BROU"
        };
        _exportBrouAction.Execute += ExportBrouAction_Execute;

        _exportBpsAction = new SimpleAction(this, "ExportarBPS", PredefinedCategory.Export)
        {
            Caption = "Exportar BPS",
            ImageName = "Action_Export",
            ToolTip = "Generar archivo de exportación BPS"
        };
        _exportBpsAction.Execute += ExportBpsAction_Execute;
    }

    private async void LiquidarAction_Execute(object? sender, SimpleActionExecuteEventArgs e)
    {
        await ExecuteLiquidacion(true);
    }

    private async void SimularAction_Execute(object? sender, SimpleActionExecuteEventArgs e)
    {
        await ExecuteLiquidacion(false);
    }

    private async void CargarBpsAction_Execute(object? sender, SimpleActionExecuteEventArgs e)
    {
        var dbContext = GetDbContext();
        if (dbContext == null) return;

        var now = DateTime.Today;
        var service = new SubsidioLiquidacionService(dbContext);

        try
        {
            int count = await service.CargarLiquidacionBpsAsync(now.Month, now.Year, true);
            Application.ShowViewStrategy.ShowMessage(
                $"Datos BPS cargados para {count} subsidios.",
                InformationType.Success);
            View.ObjectSpace.Refresh();
        }
        catch (Exception ex)
        {
            Application.ShowViewStrategy.ShowMessage(
                $"Error al cargar BPS: {ex.Message}", InformationType.Error);
        }
    }

    private async void ExportNbcAction_Execute(object? sender, SimpleActionExecuteEventArgs e)
    {
        var dbContext = GetDbContext();
        if (dbContext == null) return;

        var now = DateTime.Today;
        var service = new ExportService(dbContext);
        var data = await service.GenerarArchivoNbcAsync(now.Month, now.Year, true, now);

        ShowExportResult("NBC", data);
    }

    private async void ExportBrouAction_Execute(object? sender, SimpleActionExecuteEventArgs e)
    {
        var dbContext = GetDbContext();
        if (dbContext == null) return;

        var now = DateTime.Today;
        var service = new ExportService(dbContext);
        var data = await service.GenerarArchivoBrouAsync(now.Month, now.Year, true, now);

        ShowExportResult("BROU", data);
    }

    private async void ExportBpsAction_Execute(object? sender, SimpleActionExecuteEventArgs e)
    {
        var dbContext = GetDbContext();
        if (dbContext == null) return;

        var now = DateTime.Today;
        var service = new ExportService(dbContext);
        var data = await service.GenerarArchivoBpsAsync(now.Month, now.Year, true);

        ShowExportResult("BPS", data);
    }

    private async Task ExecuteLiquidacion(bool liquidar)
    {
        var dbContext = GetDbContext();
        if (dbContext == null) return;

        // TODO: Get period from user input dialog
        // For now, use current month
        var now = DateTime.Today;
        int mes = now.Year * 100 + now.Month;
        string usuario = SecuritySystem.CurrentUserName ?? "system";

        var service = new SubsidioLiquidacionService(dbContext);

        try
        {
            // Consistency check before liquidation
            await service.VerificarConsistenciaAsync(now.Year, now.Month);
        }
        catch (InvalidOperationException ex)
        {
            Application.ShowViewStrategy.ShowMessage(
                $"Error de consistencia: {ex.Message}", InformationType.Error);
            return;
        }

        var result = await service.LiquidarAsync(mes, liquidar, null, liquidar, usuario);

        if (result.Success)
        {
            Application.ShowViewStrategy.ShowMessage(
                $"Liquidación completada. Procesados: {result.Processed} afiliados.",
                InformationType.Success);
            View.ObjectSpace.Refresh();
        }
        else
        {
            Application.ShowViewStrategy.ShowMessage(
                $"Error en liquidación: {result.ErrorMessage}. Última CI: {result.LastCIProcessed}",
                InformationType.Error);
        }
    }

    private NewSgpaEFCoreDbContext? GetDbContext()
    {
        var os = Application.CreateObjectSpace(typeof(SubsidioCabezal));
        var dbContext = ((IObjectSpaceLink)os).ObjectSpace?.ServiceProvider?
            .GetRequiredService<NewSgpaEFCoreDbContext>();

        if (dbContext == null)
        {
            Application.ShowViewStrategy.ShowMessage(
                "Error: No se pudo obtener el contexto de datos.",
                InformationType.Error);
        }

        return dbContext;
    }

    private void ShowExportResult(string tipo, byte[] data)
    {
        if (data.Length > 0)
        {
            // TODO: Integrate with Blazor file download mechanism
            Application.ShowViewStrategy.ShowMessage(
                $"Archivo {tipo} generado ({data.Length:N0} bytes). " +
                "Utilice la función de descarga del navegador.",
                InformationType.Success);
        }
        else
        {
            Application.ShowViewStrategy.ShowMessage(
                $"No se encontraron datos para exportar {tipo}.",
                InformationType.Warning);
        }
    }
}
