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
    public partial class EditUsuarioInstitucion
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
            usuarioInstitucion = await CMUService.GetUsuarioInstitucionByOid(oid:Oid);
        }
        protected bool errorVisible;
        protected SGPA.Server.Models.CMU.UsuarioInstitucion usuarioInstitucion;

        protected IEnumerable<SGPA.Server.Models.CMU.AgenteCobranza> agenteCobranzasForInstitucion;

        protected IEnumerable<SGPA.Server.Models.CMU.Usuario> usuariosForOid;


        protected int agenteCobranzasForInstitucionCount;
        protected SGPA.Server.Models.CMU.AgenteCobranza agenteCobranzasForInstitucionValue;
        protected async Task agenteCobranzasForInstitucionLoadData(LoadDataArgs args)
        {
            try
            {
                var result = await CMUService.GetAgenteCobranzas(top: args.Top, skip: args.Skip, count:args.Top != null && args.Skip != null, filter: $"{args.Filter}", orderby: $"{args.OrderBy}");
                agenteCobranzasForInstitucion = result.Value.AsODataEnumerable();
                agenteCobranzasForInstitucionCount = result.Count;

                if (!object.Equals(usuarioInstitucion.Institucion, null))
                {
                    var valueResult = await CMUService.GetAgenteCobranzas(filter: $"Id eq {usuarioInstitucion.Institucion}");
                    var firstItem = valueResult.Value.FirstOrDefault();
                    if (firstItem != null)
                    {
                        agenteCobranzasForInstitucionValue = firstItem;
                    }
                }

            }
            catch (System.Exception ex)
            {
                NotificationService.Notify(new NotificationMessage(){ Severity = NotificationSeverity.Error, Summary = $"Error", Detail = $"Unable to load AgenteCobranza" });
            }
        }

        protected int usuariosForOidCount;
        protected SGPA.Server.Models.CMU.Usuario usuariosForOidValue;
        protected async Task usuariosForOidLoadData(LoadDataArgs args)
        {
            try
            {
                var result = await CMUService.GetUsuarios(top: args.Top, skip: args.Skip, count:args.Top != null && args.Skip != null, filter: $"{args.Filter}", orderby: $"{args.OrderBy}");
                usuariosForOid = result.Value.AsODataEnumerable();
                usuariosForOidCount = result.Count;

                if (!object.Equals(usuarioInstitucion.Oid, null))
                {
                    var valueResult = await CMUService.GetUsuarios(filter: $"Oid eq {usuarioInstitucion.Oid}");
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
        protected async Task FormSubmit()
        {
            try
            {
                var result = await CMUService.UpdateUsuarioInstitucion(oid:Oid, usuarioInstitucion);
                if (result.StatusCode == System.Net.HttpStatusCode.PreconditionFailed)
                {
                     hasChanges = true;
                     canEdit = false;
                     return;
                }
                DialogService.Close(usuarioInstitucion);
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

            usuarioInstitucion = await CMUService.GetUsuarioInstitucionByOid(oid:Oid);
        }
    }
}