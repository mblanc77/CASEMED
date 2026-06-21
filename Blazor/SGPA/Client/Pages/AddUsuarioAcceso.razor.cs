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
    public partial class AddUsuarioAcceso
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

        protected override async Task OnInitializedAsync()
        {
            usuarioAcceso = new SGPA.Server.Models.CMU.UsuarioAcceso();
        }
        protected bool errorVisible;
        protected SGPA.Server.Models.CMU.UsuarioAcceso usuarioAcceso;

        protected IEnumerable<SGPA.Server.Models.CMU.Usuario> usuariosForUsuario;


        protected int usuariosForUsuarioCount;
        protected SGPA.Server.Models.CMU.Usuario usuariosForUsuarioValue;
        protected async Task usuariosForUsuarioLoadData(LoadDataArgs args)
        {
            try
            {
                var result = await CMUService.GetUsuarios(top: args.Top, skip: args.Skip, count:args.Top != null && args.Skip != null, filter: $"{args.Filter}", orderby: $"{args.OrderBy}");
                usuariosForUsuario = result.Value.AsODataEnumerable();
                usuariosForUsuarioCount = result.Count;

                if (!object.Equals(usuarioAcceso.Usuario, null))
                {
                    var valueResult = await CMUService.GetUsuarios(filter: $"Oid eq {usuarioAcceso.Usuario}");
                    var firstItem = valueResult.Value.FirstOrDefault();
                    if (firstItem != null)
                    {
                        usuariosForUsuarioValue = firstItem;
                    }
                }

            }
            catch (System.Exception ex)
            {
                NotificationService.Notify(new NotificationMessage(){ Severity = NotificationSeverity.Error, Summary = $"Error", Detail = $"Unable to load Usuario1" });
            }
        }
        protected async Task FormSubmit()
        {
            try
            {
                var result = await CMUService.CreateUsuarioAcceso(usuarioAcceso);
                DialogService.Close(usuarioAcceso);
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
    }
}