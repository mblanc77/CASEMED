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
    public partial class AddRegistroColegiadoNotificacion
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
            registroColegiadoNotificacion = new SGPA.Server.Models.CMU.RegistroColegiadoNotificacion();
        }
        protected bool errorVisible;
        protected SGPA.Server.Models.CMU.RegistroColegiadoNotificacion registroColegiadoNotificacion;

        protected IEnumerable<SGPA.Server.Models.CMU.RegistroColegiado> registroColegiadosForRegistroColegiado;


        protected int registroColegiadosForRegistroColegiadoCount;
        protected SGPA.Server.Models.CMU.RegistroColegiado registroColegiadosForRegistroColegiadoValue;
        protected async Task registroColegiadosForRegistroColegiadoLoadData(LoadDataArgs args)
        {
            try
            {
                var result = await CMUService.GetRegistroColegiados(top: args.Top, skip: args.Skip, count:args.Top != null && args.Skip != null, filter: $"{args.Filter}", orderby: $"{args.OrderBy}");
                registroColegiadosForRegistroColegiado = result.Value.AsODataEnumerable();
                registroColegiadosForRegistroColegiadoCount = result.Count;

                if (!object.Equals(registroColegiadoNotificacion.RegistroColegiado, null))
                {
                    var valueResult = await CMUService.GetRegistroColegiados(filter: $"OID eq {registroColegiadoNotificacion.RegistroColegiado}");
                    var firstItem = valueResult.Value.FirstOrDefault();
                    if (firstItem != null)
                    {
                        registroColegiadosForRegistroColegiadoValue = firstItem;
                    }
                }

            }
            catch (System.Exception ex)
            {
                NotificationService.Notify(new NotificationMessage(){ Severity = NotificationSeverity.Error, Summary = $"Error", Detail = $"Unable to load RegistroColegiado1" });
            }
        }
        protected async Task FormSubmit()
        {
            try
            {
                var result = await CMUService.CreateRegistroColegiadoNotificacion(registroColegiadoNotificacion);
                DialogService.Close(registroColegiadoNotificacion);
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