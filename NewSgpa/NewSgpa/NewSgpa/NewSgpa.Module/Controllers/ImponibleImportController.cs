using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Actions;
using DevExpress.Persistent.Base;
using NewSgpa.Module.BusinessObjects;
using NewSgpa.Module.BusinessObjects.Sgpa;
using NewSgpa.Module.Services;

namespace NewSgpa.Module.Controllers;

/// <summary>
/// XAF Controller for Imponible (BPS Payroll) import operations.
/// Replaces VB6 frmCarHL (Carga Hoja de Liquidación) and related queries
/// 100_Insert_Imponible, 100_Update_Imponible, 100_Insert_NoCargadoHL.
/// </summary>
public class ImponibleImportController : ViewController<ListView>
{
    private readonly SimpleAction _importAction;

    public ImponibleImportController()
    {
        TargetObjectType = typeof(Imponible);

        _importAction = new SimpleAction(this, "ImportarHL", PredefinedCategory.RecordEdit)
        {
            Caption = "Importar Hoja de Liquidación",
            ImageName = "Action_Import",
            ToolTip = "Importar datos de BPS desde archivo de hoja de liquidación"
        };
        _importAction.Execute += ImportAction_Execute;
    }

    private async void ImportAction_Execute(object? sender, SimpleActionExecuteEventArgs e)
    {
        // TODO: Replace with proper file upload dialog for Blazor.
        // For now, show a message indicating the service is ready.
        // In production, the Blazor UI would provide a file upload component
        // that feeds the content into ImportarAsync.
        //
        // Example integration flow:
        //   1. User selects BPS file via Blazor file upload
        //   2. Read file content as string
        //   3. var service = new ImponibleImportService(dbContext);
        //   4. var records = service.ParseBpsFile(content, isNewFormat);
        //   5. var result = await service.ImportarAsync(records, codEmpresa, mes, anio, modo, usuario);
        //   6. Show result summary

        Application.ShowViewStrategy.ShowMessage(
            "Para importar, utilice la opción de carga de archivos BPS. " +
            "Formatos soportados: HL estándar (tipo 4) y nuevo formato (tipo 7).",
            InformationType.Info);
    }
}
