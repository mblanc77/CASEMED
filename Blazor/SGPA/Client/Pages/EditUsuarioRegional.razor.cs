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
    public partial class EditUsuarioRegional
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
            usuarioRegional = await CMUService.GetUsuarioRegionalByOid(oid:Oid);
        }
        protected bool errorVisible;
        protected SGPA.Server.Models.CMU.UsuarioRegional usuarioRegional;

        protected IEnumerable<SGPA.Server.Models.CMU.Usuario> usuariosForOid;

        protected IEnumerable<SGPA.Server.Models.CMU.Regional> regionalsForRegional;


        protected int usuariosForOidCount;
        protected SGPA.Server.Models.CMU.Usuario usuariosForOidValue;
        protected async Task usuariosForOidLoadData(LoadDataArgs args)
        {
            try
            {
                var result = await CMUService.GetUsuarios(top: args.Top, skip: args.Skip, count:args.Top != null && args.Skip != null, filter: $"{args.Filter}", orderby: $"{args.OrderBy}");
                usuariosForOid = result.Value.AsODataEnumerable();
                usuariosForOidCount = result.Count;

                if (!object.Equals(usuarioRegional.Oid, null))
                {
                    var valueResult = await CMUService.GetUsuarios(filter: $"Oid eq {usuarioRegional.Oid}");
                    var firstItem = valueResult.Value.FirstOrDefault();
                    if (firstItem != null)
                    {
                        usuariosForOidValue = firstItem;
                    }
                }

            }
            catch (System.Exception ex)
            {
                NotificationService.Notify(new NotificationMessage(){ Severity = NotificationSeverity.Error, Summary = $"Error", Detail = $"Unable to load Usuario" });
            }
        }

        protected int regionalsForRegionalCount;
        protected SGPA.Server.Models.CMU.Regional regionalsForRegionalValue;
        protected async Task regionalsForRegionalLoadData(LoadDataArgs args)
        {
            try
            {
                var result = await CMUService.GetRegionals(top: args.Top, skip: args.Skip, count:args.Top != null && args.Skip != null, filter: $"{args.Filter}", orderby: $"{args.OrderBy}");
                regionalsForRegional = result.Value.AsODataEnumerable();
                regionalsForRegionalCount = result.Count;

                if (!object.Equals(usuarioRegional.Regional, null))
                {
                    var valueResult = await CMUService.GetRegionals(filter: $"Id eq {usuarioRegional.Regional}");
                    var firstItem = valueResult.Value.FirstOrDefault();
                    if (firstItem != null)
                    {
                        regionalsForRegionalValue = firstItem;
                    }
                }

            }
            catch (System.Exception ex)
            {
                NotificationService.Notify(new NotificationMessage(){ Severity = NotificationSeverity.Error, Summary = $"Error", Detail = $"Unable to load Regional1" });
            }
        }
        protected async Task FormSubmit()
        {
            try
            {
                var result = await CMUService.UpdateUsuarioRegional(oid:Oid, usuarioRegional);
                if (result.StatusCode == System.Net.HttpStatusCode.PreconditionFailed)
                {
                     hasChanges = true;
                     canEdit = false;
                     return;
                }
                DialogService.Close(usuarioRegional);
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

            usuarioRegional = await CMUService.GetUsuarioRegionalByOid(oid:Oid);
        }
    }
}