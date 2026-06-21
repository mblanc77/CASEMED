using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Actions;
using DevExpress.Persistent.Base;
using NewSgpa.Module.BusinessObjects.Prestamos;
using NewSgpa.Module.BusinessObjects.Sgpa;

namespace NewSgpa.Module.Controllers;

/// <summary>
/// XAF Controller for Afiliado detail operations.
/// Replaces VB6 frmABM_Afiliado toolbar buttons.
/// </summary>
public class AfiliadoDetailController : ViewController<DetailView>
{
    private readonly SimpleAction _verCertificacionesAction;
    private readonly SimpleAction _verPrestacionesAction;
    private readonly SimpleAction _verSubsidiosAction;
    private readonly SimpleAction _verReintegrosAction;
    private readonly SimpleAction _verPrestamosAction;

    public AfiliadoDetailController()
    {
        TargetObjectType = typeof(Afiliado);

        _verCertificacionesAction = new SimpleAction(this, "VerCertificaciones", PredefinedCategory.View)
        {
            Caption = "Certificaciones",
            ImageName = "BO_Audit_ChangeHistory",
            ToolTip = "Ver certificaciones del afiliado"
        };
        _verCertificacionesAction.Execute += (s, e) => NavigateToFiltered<Certificacion>();

        _verPrestacionesAction = new SimpleAction(this, "VerPrestaciones", PredefinedCategory.View)
        {
            Caption = "Prestaciones",
            ImageName = "BO_Invoice",
            ToolTip = "Ver prestaciones del afiliado"
        };
        _verPrestacionesAction.Execute += (s, e) => NavigateToFiltered<Prestacion>();

        _verSubsidiosAction = new SimpleAction(this, "VerSubsidios", PredefinedCategory.View)
        {
            Caption = "Subsidios",
            ImageName = "BO_Sale",
            ToolTip = "Ver subsidios del afiliado"
        };
        _verSubsidiosAction.Execute += (s, e) => NavigateToFiltered<SubsidioCabezal>();

        _verReintegrosAction = new SimpleAction(this, "VerReintegros", PredefinedCategory.View)
        {
            Caption = "Reintegros",
            ImageName = "BO_Contract",
            ToolTip = "Ver reintegros mutuales del afiliado"
        };
        _verReintegrosAction.Execute += (s, e) => NavigateToFiltered<ReintegroMutual>();

        _verPrestamosAction = new SimpleAction(this, "VerPrestamos", PredefinedCategory.View)
        {
            Caption = "Préstamos",
            ImageName = "BO_Sale",
            ToolTip = "Ver préstamos del afiliado"
        };
        _verPrestamosAction.Execute += (s, e) => NavigateToFiltered<SpPrestamo>();
    }

    private void NavigateToFiltered<T>() where T : class
    {
        var afiliado = View.CurrentObject as Afiliado;
        if (afiliado == null) return;

        var os = Application.CreateObjectSpace(typeof(T));
        var listViewId = Application.FindListViewId(typeof(T));
        var criteria = DevExpress.Data.Filtering.CriteriaOperator.Parse("CI = ?", afiliado.CI);
        var collectionSource = Application.CreateCollectionSource(os, typeof(T), listViewId);
        collectionSource.Criteria["FilterByCI"] = criteria;
        var listView = Application.CreateListView(listViewId, collectionSource, true);

        var showViewParameters = new ShowViewParameters(listView)
        {
            TargetWindow = TargetWindow.NewModalWindow
        };
        Application.ShowViewStrategy.ShowView(showViewParameters, new ShowViewSource(Frame, null));
    }
}
