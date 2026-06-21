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
    public partial class EditRol
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
            rol = await CMUService.GetRolByOid(oid:Oid);
        }
        protected bool errorVisible;
        protected SGPA.Server.Models.CMU.Rol rol;

        protected IEnumerable<SGPA.Server.Models.CMU.SecuritySystemRole> securitySystemRolesForOid;


        protected int securitySystemRolesForOidCount;
        protected SGPA.Server.Models.CMU.SecuritySystemRole securitySystemRolesForOidValue;
        protected async Task securitySystemRolesForOidLoadData(LoadDataArgs args)
        {
            try
            {
                var result = await CMUService.GetSecuritySystemRoles(top: args.Top, skip: args.Skip, count:args.Top != null && args.Skip != null, filter: $"{args.Filter}", orderby: $"{args.OrderBy}");
                securitySystemRolesForOid = result.Value.AsODataEnumerable();
                securitySystemRolesForOidCount = result.Count;

                if (!object.Equals(rol.Oid, null))
                {
                    var valueResult = await CMUService.GetSecuritySystemRoles(filter: $"Oid eq {rol.Oid}");
                    var firstItem = valueResult.Value.FirstOrDefault();
                    if (firstItem != null)
                    {
                        securitySystemRolesForOidValue = firstItem;
                    }
                }

            }
            catch (System.Exception ex)
            {
                NotificationService.Notify(new NotificationMessage(){ Severity = NotificationSeverity.Error, Summary = $"Error", Detail = $"Unable to load SecuritySystemRole" });
            }
        }
        protected async Task FormSubmit()
        {
            try
            {
                var result = await CMUService.UpdateRol(oid:Oid, rol);
                if (result.StatusCode == System.Net.HttpStatusCode.PreconditionFailed)
                {
                     hasChanges = true;
                     canEdit = false;
                     return;
                }
                DialogService.Close(rol);
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

            rol = await CMUService.GetRolByOid(oid:Oid);
        }
    }
}