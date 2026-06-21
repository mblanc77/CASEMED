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
    public partial class EditAuditedObjectWeakReference
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
            auditedObjectWeakReference = await CMUService.GetAuditedObjectWeakReferenceByOid(oid:Oid);
        }
        protected bool errorVisible;
        protected SGPA.Server.Models.CMU.AuditedObjectWeakReference auditedObjectWeakReference;

        protected IEnumerable<SGPA.Server.Models.CMU.XpWeakReference> xpWeakReferencesForOid;


        protected int xpWeakReferencesForOidCount;
        protected SGPA.Server.Models.CMU.XpWeakReference xpWeakReferencesForOidValue;
        protected async Task xpWeakReferencesForOidLoadData(LoadDataArgs args)
        {
            try
            {
                var result = await CMUService.GetXpWeakReferences(top: args.Top, skip: args.Skip, count:args.Top != null && args.Skip != null, filter: $"{args.Filter}", orderby: $"{args.OrderBy}");
                xpWeakReferencesForOid = result.Value.AsODataEnumerable();
                xpWeakReferencesForOidCount = result.Count;

                if (!object.Equals(auditedObjectWeakReference.Oid, null))
                {
                    var valueResult = await CMUService.GetXpWeakReferences(filter: $"Oid eq {auditedObjectWeakReference.Oid}");
                    var firstItem = valueResult.Value.FirstOrDefault();
                    if (firstItem != null)
                    {
                        xpWeakReferencesForOidValue = firstItem;
                    }
                }

            }
            catch (System.Exception ex)
            {
                NotificationService.Notify(new NotificationMessage(){ Severity = NotificationSeverity.Error, Summary = $"Error", Detail = $"Unable to load XpweakReference" });
            }
        }
        protected async Task FormSubmit()
        {
            try
            {
                var result = await CMUService.UpdateAuditedObjectWeakReference(oid:Oid, auditedObjectWeakReference);
                if (result.StatusCode == System.Net.HttpStatusCode.PreconditionFailed)
                {
                     hasChanges = true;
                     canEdit = false;
                     return;
                }
                DialogService.Close(auditedObjectWeakReference);
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

            auditedObjectWeakReference = await CMUService.GetAuditedObjectWeakReferenceByOid(oid:Oid);
        }
    }
}