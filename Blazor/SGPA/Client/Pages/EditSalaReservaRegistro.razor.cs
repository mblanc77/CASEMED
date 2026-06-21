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
    public partial class EditSalaReservaRegistro
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
        public int Id { get; set; }

        protected override async Task OnInitializedAsync()
        {
            salaReservaRegistro = await CMUService.GetSalaReservaRegistroById(id:Id);
        }
        protected bool errorVisible;
        protected SGPA.Server.Models.CMU.SalaReservaRegistro salaReservaRegistro;

        protected IEnumerable<SGPA.Server.Models.CMU.SalaReserva> salaReservasForReserva;


        protected int salaReservasForReservaCount;
        protected SGPA.Server.Models.CMU.SalaReserva salaReservasForReservaValue;
        protected async Task salaReservasForReservaLoadData(LoadDataArgs args)
        {
            try
            {
                var result = await CMUService.GetSalaReservas(top: args.Top, skip: args.Skip, count:args.Top != null && args.Skip != null, filter: $"{args.Filter}", orderby: $"{args.OrderBy}");
                salaReservasForReserva = result.Value.AsODataEnumerable();
                salaReservasForReservaCount = result.Count;

                if (!object.Equals(salaReservaRegistro.Reserva, null))
                {
                    var valueResult = await CMUService.GetSalaReservas(filter: $"Id eq {salaReservaRegistro.Reserva}");
                    var firstItem = valueResult.Value.FirstOrDefault();
                    if (firstItem != null)
                    {
                        salaReservasForReservaValue = firstItem;
                    }
                }

            }
            catch (System.Exception ex)
            {
                NotificationService.Notify(new NotificationMessage(){ Severity = NotificationSeverity.Error, Summary = $"Error", Detail = $"Unable to load SalaReserva" });
            }
        }
        protected async Task FormSubmit()
        {
            try
            {
                var result = await CMUService.UpdateSalaReservaRegistro(id:Id, salaReservaRegistro);
                if (result.StatusCode == System.Net.HttpStatusCode.PreconditionFailed)
                {
                     hasChanges = true;
                     canEdit = false;
                     return;
                }
                DialogService.Close(salaReservaRegistro);
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

            salaReservaRegistro = await CMUService.GetSalaReservaRegistroById(id:Id);
        }
    }
}