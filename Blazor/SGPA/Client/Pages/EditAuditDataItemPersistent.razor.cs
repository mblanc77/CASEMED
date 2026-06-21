using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.JSInterop;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Radzen;
using Radzen.Blazor;

namespace SGPA.Client.Pages
{
    public partial class EditAuditDataItemPersistent
    {
        [Inject]
        protected IJSRuntime JSRuntime { get; set; }

        [Inject]
        protected NavigationManager NavigationManager { get; set; }

        [Inject]
        protected DialogService DialogService { get; set; }

        [Inject]
        protected TooltipService TooltipService { get; set; }

        [Inject]
        protected ContextMenuService ContextMenuService { get; set; }

        [Inject]
        protected NotificationService NotificationService { get; set; }
        [Inject]
        public CMUService CMUService { get; set; }

        [Parameter]
        public Guid Oid { get; set; }

        protected override async Task OnInitializedAsync()
        {
            auditDataItemPersistent = await CMUService.GetAuditDataItemPersistentByOid(oid:Oid);
        }
        protected bool errorVisible;
        protected SGPA.Server.Models.CMU.AuditDataItemPersistent auditDataItemPersistent;

        protected IEnumerable<SGPA.Server.Models.CMU.AuditedObjectWeakReference> auditedObjectWeakReferencesForAuditedObject;

        protected IEnumerable<SGPA.Server.Models.CMU.XpWeakReference> xpWeakReferencesForNewObject;

        protected IEnumerable<SGPA.Server.Models.CMU.XpWeakReference> xpWeakReferencesForOldObject;


        protected int auditedObjectWeakReferencesForAuditedObjectCount;
        protected SGPA.Server.Models.CMU.AuditedObjectWeakReference auditedObjectWeakReferencesForAuditedObjectValue;
        protected async Task auditedObjectWeakReferencesForAuditedObjectLoadData(LoadDataArgs args)
        {
            try
            {
                var result = await CMUService.GetAuditedObjectWeakReferences(top: args.Top, skip: args.Skip, count:args.Top != null && args.Skip != null, filter: $"{args.Filter}", orderby: $"{args.OrderBy}");
                auditedObjectWeakReferencesForAuditedObject = result.Value.AsODataEnumerable();
                auditedObjectWeakReferencesForAuditedObjectCount = result.Count;

                if (!object.Equals(auditDataItemPersistent.AuditedObject, null))
                {
                    var valueResult = await CMUService.GetAuditedObjectWeakReferences(filter: $"Oid eq {auditDataItemPersistent.AuditedObject}");
                    var firstItem = valueResult.Value.FirstOrDefault();
                    if (firstItem != null)
                    {
                        auditedObjectWeakReferencesForAuditedObjectValue = firstItem;
                    }
                }

            }
            catch (System.Exception ex)
            {
                NotificationService.Notify(new NotificationMessage(){ Severity = NotificationSeverity.Error, Summary = $"Error", Detail = $"Unable to load AuditedObjectWeakReference" });
            }
        }

        protected int xpWeakReferencesForNewObjectCount;
        protected SGPA.Server.Models.CMU.XpWeakReference xpWeakReferencesForNewObjectValue;
        protected async Task xpWeakReferencesForNewObjectLoadData(LoadDataArgs args)
        {
            try
            {
                var result = await CMUService.GetXpWeakReferences(top: args.Top, skip: args.Skip, count:args.Top != null && args.Skip != null, filter: $"{args.Filter}", orderby: $"{args.OrderBy}");
                xpWeakReferencesForNewObject = result.Value.AsODataEnumerable();
                xpWeakReferencesForNewObjectCount = result.Count;

                if (!object.Equals(auditDataItemPersistent.NewObject, null))
                {
                    var valueResult = await CMUService.GetXpWeakReferences(filter: $"Oid eq {auditDataItemPersistent.NewObject}");
                    var firstItem = valueResult.Value.FirstOrDefault();
                    if (firstItem != null)
                    {
                        xpWeakReferencesForNewObjectValue = firstItem;
                    }
                }

            }
            catch (System.Exception ex)
            {
                NotificationService.Notify(new NotificationMessage(){ Severity = NotificationSeverity.Error, Summary = $"Error", Detail = $"Unable to load XpweakReference" });
            }
        }

        protected int xpWeakReferencesForOldObjectCount;
        protected SGPA.Server.Models.CMU.XpWeakReference xpWeakReferencesForOldObjectValue;
        protected async Task xpWeakReferencesForOldObjectLoadData(LoadDataArgs args)
        {
            try
            {
                var result = await CMUService.GetXpWeakReferences(top: args.Top, skip: args.Skip, count:args.Top != null && args.Skip != null, filter: $"{args.Filter}", orderby: $"{args.OrderBy}");
                xpWeakReferencesForOldObject = result.Value.AsODataEnumerable();
                xpWeakReferencesForOldObjectCount = result.Count;

                if (!object.Equals(auditDataItemPersistent.OldObject, null))
                {
                    var valueResult = await CMUService.GetXpWeakReferences(filter: $"Oid eq {auditDataItemPersistent.OldObject}");
                    var firstItem = valueResult.Value.FirstOrDefault();
                    if (firstItem != null)
                    {
                        xpWeakReferencesForOldObjectValue = firstItem;
                    }
                }

            }
            catch (System.Exception ex)
            {
                NotificationService.Notify(new NotificationMessage(){ Severity = NotificationSeverity.Error, Summary = $"Error", Detail = $"Unable to load XpweakReference1" });
            }
        }
        protected async Task FormSubmit()
        {
            try
            {
                var result = await CMUService.UpdateAuditDataItemPersistent(oid:Oid, auditDataItemPersistent);
                if (result.StatusCode == System.Net.HttpStatusCode.PreconditionFailed)
                {
                     hasChanges = true;
                     canEdit = false;
                     return;
                }
                DialogService.Close(auditDataItemPersistent);
            }
            catch (Exception ex)
            {
                errorVisible = true;
            }
        }

        protected async Task CancelButtonClick(MouseEventArgs args)
        {
            DialogService.Close(null);
        }


        protected bool hasChanges = false;
        protected bool canEdit = true;


        protected async Task ReloadButtonClick(MouseEventArgs args)
        {
            hasChanges = false;
            canEdit = true;

            auditDataItemPersistent = await CMUService.GetAuditDataItemPersistentByOid(oid:Oid);
        }
    }
}