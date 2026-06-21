using DevExpress.ExpressApp;
using NewSgpa.Module.BusinessObjects.Base;

namespace NewSgpa.Module.Controllers;

/// <summary>
/// Auto-fills Usr and Ts audit fields when creating or saving records.
/// Replaces VB6 pattern: rsXxx!Usr = oUsr.Login / rsXxx!Ts = Now.
/// </summary>
public class AuditFieldController : ViewController
{
    public AuditFieldController()
    {
        TargetObjectType = typeof(BaseEntity);
    }

    protected override void OnActivated()
    {
        base.OnActivated();
        ObjectSpace.ObjectChanged += ObjectSpace_ObjectChanged;
        ObjectSpace.Committing += ObjectSpace_Committing;
    }

    protected override void OnDeactivated()
    {
        ObjectSpace.ObjectChanged -= ObjectSpace_ObjectChanged;
        ObjectSpace.Committing -= ObjectSpace_Committing;
        base.OnDeactivated();
    }

    private void ObjectSpace_ObjectChanged(object? sender, ObjectChangedEventArgs e)
    {
        if (e.Object is BaseEntity entity && ObjectSpace.IsNewObject(entity) && string.IsNullOrEmpty(entity.Usr))
        {
            entity.Usr = GetCurrentUser();
            entity.Ts = DateTime.Now;
        }
    }

    private void ObjectSpace_Committing(object? sender, System.ComponentModel.CancelEventArgs e)
    {
        var modified = ObjectSpace.ModifiedObjects;
        if (modified == null) return;

        foreach (var obj in modified)
        {
            if (obj is BaseEntity entity)
            {
                entity.Usr = GetCurrentUser();
                entity.Ts = DateTime.Now;
            }
        }
    }

    private string GetCurrentUser()
    {
        var userName = SecuritySystem.CurrentUserName;
        // Truncate to 8 chars to match Access field length
        return string.IsNullOrEmpty(userName) ? "system" : userName[..Math.Min(8, userName.Length)];
    }
}
