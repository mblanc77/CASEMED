using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Actions;
using DevExpress.Persistent.Base;
using Microsoft.Extensions.DependencyInjection;
using NewSgpa.Module.BusinessObjects;
using NewSgpa.Module.BusinessObjects.Prestamos;
using NewSgpa.Module.Services;

namespace NewSgpa.Module.Controllers;

/// <summary>
/// XAF Controller for Préstamo business operations.
/// Replaces VB6 frmPrestamo toolbar + frmCancelarPrestamo + frmIngRefinanciacion.
///
/// Actions:
///   - Cancelar Préstamo (early cancellation)
///   - Anular Préstamo (void)
///   - Refinanciar (create new loan from remaining balance)
///   - Calcular Cuadro (compute amortization schedule)
/// </summary>
public class PrestamoAdminController : ViewController<DetailView>
{
    private readonly SimpleAction _cancelarAction;
    private readonly SimpleAction _anularAction;
    private readonly SimpleAction _refinanciarAction;
    private readonly SimpleAction _calcularCuadroAction;

    public PrestamoAdminController()
    {
        TargetObjectType = typeof(SpPrestamo);

        _cancelarAction = new SimpleAction(this, "CancelarPrestamo", PredefinedCategory.RecordEdit)
        {
            Caption = "Cancelar Préstamo",
            ConfirmationMessage = "¿Está seguro de cancelar anticipadamente este préstamo?",
            ImageName = "Action_Cancel",
            ToolTip = "Cancela el préstamo anticipadamente. Se generará una factura de cancelación."
        };
        _cancelarAction.Execute += CancelarAction_Execute;

        _anularAction = new SimpleAction(this, "AnularPrestamo", PredefinedCategory.RecordEdit)
        {
            Caption = "Anular Préstamo",
            ConfirmationMessage = "¿Está seguro de anular este préstamo? Esta acción no se puede deshacer.",
            ImageName = "Action_Delete",
            ToolTip = "Anula completamente el préstamo y todas sus facturas y cuotas."
        };
        _anularAction.Execute += AnularAction_Execute;

        _refinanciarAction = new SimpleAction(this, "RefinanciarPrestamo", PredefinedCategory.RecordEdit)
        {
            Caption = "Refinanciar",
            ImageName = "Action_Reload",
            ToolTip = "Refinancia el préstamo: cancela el actual y genera uno nuevo con el saldo."
        };
        _refinanciarAction.Execute += RefinanciarAction_Execute;

        _calcularCuadroAction = new SimpleAction(this, "CalcularCuadro", PredefinedCategory.RecordEdit)
        {
            Caption = "Calcular Cuadro",
            ImageName = "Action_SimpleAction",
            ToolTip = "Calcula el cuadro de amortización del préstamo."
        };
        _calcularCuadroAction.Execute += CalcularCuadroAction_Execute;
    }

    protected override void OnActivated()
    {
        base.OnActivated();
        UpdateActionStates();
        View.CurrentObjectChanged += (_, _) => UpdateActionStates();
    }

    private void UpdateActionStates()
    {
        var prestamo = View.CurrentObject as SpPrestamo;
        bool isActivo = prestamo?.CodPrestamoEstado == "act";
        _cancelarAction.Enabled["IsActivo"] = isActivo;
        _anularAction.Enabled["IsActivo"] = isActivo;
        _refinanciarAction.Enabled["IsActivo"] = isActivo;
    }

    private async void CancelarAction_Execute(object? sender, SimpleActionExecuteEventArgs e)
    {
        var prestamo = View.CurrentObject as SpPrestamo;
        if (prestamo == null) return;

        var dbContext = GetDbContext();
        if (dbContext == null) return;

        string usuario = SecuritySystem.CurrentUserName ?? "system";
        var service = new PrestamoAdminService(dbContext);

        try
        {
            double importe = await service.CalcularImporteCancelacionAsync(prestamo.IDPrestamo);

            bool success = await service.CancelarPrestamoAsync(prestamo.IDPrestamo, usuario);
            if (success)
            {
                Application.ShowViewStrategy.ShowMessage(
                    $"Préstamo #{prestamo.IDPrestamo} cancelado. Importe de cancelación: {importe:N2}",
                    InformationType.Success);
                View.ObjectSpace.Refresh();
            }
        }
        catch (Exception ex)
        {
            Application.ShowViewStrategy.ShowMessage(
                $"Error al cancelar: {ex.Message}", InformationType.Error);
        }
    }

    private async void AnularAction_Execute(object? sender, SimpleActionExecuteEventArgs e)
    {
        var prestamo = View.CurrentObject as SpPrestamo;
        if (prestamo == null) return;

        var dbContext = GetDbContext();
        if (dbContext == null) return;

        string usuario = SecuritySystem.CurrentUserName ?? "system";
        var service = new PrestamoAdminService(dbContext);

        try
        {
            bool success = await service.AnularPrestamoAsync(prestamo.IDPrestamo, usuario);
            if (success)
            {
                Application.ShowViewStrategy.ShowMessage(
                    $"Préstamo #{prestamo.IDPrestamo} anulado.", InformationType.Success);
                View.ObjectSpace.Refresh();
            }
        }
        catch (Exception ex)
        {
            Application.ShowViewStrategy.ShowMessage(
                $"Error al anular: {ex.Message}", InformationType.Error);
        }
    }

    private async void RefinanciarAction_Execute(object? sender, SimpleActionExecuteEventArgs e)
    {
        var prestamo = View.CurrentObject as SpPrestamo;
        if (prestamo == null) return;

        var dbContext = GetDbContext();
        if (dbContext == null) return;

        string usuario = SecuritySystem.CurrentUserName ?? "system";
        var service = new PrestamoAdminService(dbContext);

        try
        {
            // TODO: Prompt user for new cuotas count and whether to use same rate.
            // For now, use same cuotas and same rate as original.
            int cuotas = prestamo.Cuotas ?? 12;

            int nuevoId = await service.RefinanciarPrestamoAsync(
                prestamo.IDPrestamo, DateTime.Today, cuotas, true, usuario);

            Application.ShowViewStrategy.ShowMessage(
                $"Préstamo refinanciado. Nuevo préstamo: #{nuevoId}", InformationType.Success);
            View.ObjectSpace.Refresh();
        }
        catch (Exception ex)
        {
            Application.ShowViewStrategy.ShowMessage(
                $"Error al refinanciar: {ex.Message}", InformationType.Error);
        }
    }

    private void CalcularCuadroAction_Execute(object? sender, SimpleActionExecuteEventArgs e)
    {
        var prestamo = View.CurrentObject as SpPrestamo;
        if (prestamo == null) return;

        var service = new PrestamoAdminService(
            GetDbContext() ?? throw new InvalidOperationException());

        int cuotas = prestamo.Cuotas ?? 0;
        double monto = prestamo.Importe ?? 0;
        double tasa = prestamo.Moneda?.Tasa ?? 0;

        if (cuotas <= 0 || monto <= 0)
        {
            Application.ShowViewStrategy.ShowMessage(
                "El préstamo debe tener cuotas e importe definidos.", InformationType.Warning);
            return;
        }

        var cuadro = service.CalcularCuadroAmortizacion(cuotas, tasa, monto);
        Application.ShowViewStrategy.ShowMessage(
            $"Cuadro calculado: {cuadro.Count} cuotas. " +
            $"Cuota mensual: {cuadro[0].ImporteCuota:N2}",
            InformationType.Success);
    }

    private NewSgpaEFCoreDbContext? GetDbContext()
    {
        var os = Application.CreateObjectSpace(typeof(SpPrestamo));
        var dbContext = ((IObjectSpaceLink)os).ObjectSpace?.ServiceProvider?
            .GetRequiredService<NewSgpaEFCoreDbContext>();

        if (dbContext == null)
        {
            Application.ShowViewStrategy.ShowMessage(
                "Error: No se pudo obtener el contexto de datos.", InformationType.Error);
        }

        return dbContext;
    }
}

/// <summary>
/// XAF Controller for Préstamo list-level operations.
/// Shows summary info and tope calculation.
/// </summary>
public class PrestamoListController : ViewController<ListView>
{
    private readonly SimpleAction _calcularTopeAction;

    public PrestamoListController()
    {
        TargetObjectType = typeof(SpPrestamo);

        _calcularTopeAction = new SimpleAction(this, "CalcularTope", PredefinedCategory.Tools)
        {
            Caption = "Calcular Tope",
            ImageName = "Action_SimpleAction",
            ToolTip = "Calcula el monto máximo de préstamo para el afiliado seleccionado."
        };
        _calcularTopeAction.Execute += CalcularTopeAction_Execute;
    }

    private async void CalcularTopeAction_Execute(object? sender, SimpleActionExecuteEventArgs e)
    {
        var prestamo = View.SelectedObjects.Cast<SpPrestamo>().FirstOrDefault();
        if (prestamo?.CI == null || prestamo.CI == 0)
        {
            Application.ShowViewStrategy.ShowMessage(
                "Seleccione un préstamo con CI válida.", InformationType.Warning);
            return;
        }

        var os = Application.CreateObjectSpace(typeof(SpPrestamo));
        var dbContext = ((IObjectSpaceLink)os).ObjectSpace?.ServiceProvider?
            .GetRequiredService<NewSgpaEFCoreDbContext>();

        if (dbContext == null) return;

        var service = new PrestamoAdminService(dbContext);
        double tope = await service.CalcularTopePrestamoAsync(
            prestamo.CodMoneda ?? "", prestamo.CI ?? 0);
        double sueldos = await service.GetImporteSueldosAsync(prestamo.CI ?? 0);
        double abierto = await service.GetImportePrestamoAbiertoAsync(prestamo.CI ?? 0);

        Application.ShowViewStrategy.ShowMessage(
            $"CI {prestamo.CI}: Sueldos={sueldos:N2}, Abierto={abierto:N2}, Tope={tope:N2}",
            InformationType.Info);
    }
}
