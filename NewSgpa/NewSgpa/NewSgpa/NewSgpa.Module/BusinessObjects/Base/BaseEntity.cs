using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.DC;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl.EF;

namespace NewSgpa.Module.BusinessObjects.Base;

/// <summary>
/// Base class for all migrated domain entities.
/// Provides common audit fields (Usr, Ts).
/// Each derived entity defines its own primary key.
/// </summary>
public abstract class BaseEntity : IXafEntityObject, IObjectSpaceLink
{
    [StringLength(8)]
    [VisibleInListView(false)]
    public virtual string? Usr { get; set; }

    [VisibleInListView(false)]
    public virtual DateTime? Ts { get; set; }

    #region IXafEntityObject

    void IXafEntityObject.OnCreated()
    {
        Ts = DateTime.Now;
        OnCreated();
    }

    protected virtual void OnCreated()
    {
    }

    void IXafEntityObject.OnLoaded()
    {
        OnLoaded();
    }

    protected virtual void OnLoaded()
    {
    }

    void IXafEntityObject.OnSaving()
    {
        OnSaving();
    }

    protected virtual void OnSaving()
    {
    }

    #endregion

    #region IObjectSpaceLink

    // XAF will set this, but for migrations we don't need it
    [NotMapped]
    IObjectSpace? IObjectSpaceLink.ObjectSpace
    {
        get { return null; }
        set { }
    }

    #endregion
}

/// <summary>
/// Base class for lookup/catalog entities that have a code-based natural key and a description.
/// </summary>
public abstract class BaseLookupEntity : BaseEntity
{
    [StringLength(50)]
    public virtual string? Descrip { get; set; }

    public override string ToString() => Descrip ?? string.Empty;
}
