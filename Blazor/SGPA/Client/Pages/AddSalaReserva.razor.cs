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
    public partial class AddSalaReserva
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
            salaReserva = new SGPA.Server.Models.CMU.SalaReserva();
        }
        protected bool errorVisible;
        protected SGPA.Server.Models.CMU.SalaReserva salaReserva;

        protected IEnumerable<SGPA.Server.Models.CMU.MyFileDatum> myFileDataForFolleto;

        protected IEnumerable<SGPA.Server.Models.CMU.SalaOrganizador> salaOrganizadorsForOrganizador;

        protected IEnumerable<SGPA.Server.Models.CMU.SalaCmu> salaCmusForSala;


        protected int myFileDataForFolletoCount;
        protected SGPA.Server.Models.CMU.MyFileDatum myFileDataForFolletoValue;
        protected async Task myFileDataForFolletoLoadData(LoadDataArgs args)
        {
            try
            {
                var result = await CMUService.GetMyFileData(top: args.Top, skip: args.Skip, count:args.Top != null && args.Skip != null, filter: $"{args.Filter}", orderby: $"{args.OrderBy}");
                myFileDataForFolleto = result.Value.AsODataEnumerable();
                myFileDataForFolletoCount = result.Count;

                if (!object.Equals(salaReserva.Folleto, null))
                {
                    var valueResult = await CMUService.GetMyFileData(filter: $"Oid eq {salaReserva.Folleto}");
                    var firstItem = valueResult.Value.FirstOrDefault();
                    if (firstItem != null)
                    {
                        myFileDataForFolletoValue = firstItem;
                    }
                }

            }
            catch (System.Exception ex)
            {
                NotificationService.Notify(new NotificationMessage(){ Severity = NotificationSeverity.Error, Summary = $"Error", Detail = $"Unable to load MyFileDatum" });
            }
        }

        protected int salaOrganizadorsForOrganizadorCount;
        protected SGPA.Server.Models.CMU.SalaOrganizador salaOrganizadorsForOrganizadorValue;
        protected async Task salaOrganizadorsForOrganizadorLoadData(LoadDataArgs args)
        {
            try
            {
                var result = await CMUService.GetSalaOrganizadors(top: args.Top, skip: args.Skip, count:args.Top != null && args.Skip != null, filter: $"{args.Filter}", orderby: $"{args.OrderBy}");
                salaOrganizadorsForOrganizador = result.Value.AsODataEnumerable();
                salaOrganizadorsForOrganizadorCount = result.Count;

                if (!object.Equals(salaReserva.Organizador, null))
                {
                    var valueResult = await CMUService.GetSalaOrganizadors(filter: $"Id eq {salaReserva.Organizador}");
                    var firstItem = valueResult.Value.FirstOrDefault();
                    if (firstItem != null)
                    {
                        salaOrganizadorsForOrganizadorValue = firstItem;
                    }
                }

            }
            catch (System.Exception ex)
            {
                NotificationService.Notify(new NotificationMessage(){ Severity = NotificationSeverity.Error, Summary = $"Error", Detail = $"Unable to load SalaOrganizador" });
            }
        }

        protected int salaCmusForSalaCount;
        protected SGPA.Server.Models.CMU.SalaCmu salaCmusForSalaValue;
        protected async Task salaCmusForSalaLoadData(LoadDataArgs args)
        {
            try
            {
                var result = await CMUService.GetSalaCmus(top: args.Top, skip: args.Skip, count:args.Top != null && args.Skip != null, filter: $"{args.Filter}", orderby: $"{args.OrderBy}");
                salaCmusForSala = result.Value.AsODataEnumerable();
                salaCmusForSalaCount = result.Count;

                if (!object.Equals(salaReserva.Sala, null))
                {
                    var valueResult = await CMUService.GetSalaCmus(filter: $"Id eq {salaReserva.Sala}");
                    var firstItem = valueResult.Value.FirstOrDefault();
                    if (firstItem != null)
                    {
                        salaCmusForSalaValue = firstItem;
                    }
                }

            }
            catch (System.Exception ex)
            {
                NotificationService.Notify(new NotificationMessage(){ Severity = NotificationSeverity.Error, Summary = $"Error", Detail = $"Unable to load SalaCmu" });
            }
        }
        protected async Task FormSubmit()
        {
            try
            {
                var result = await CMUService.CreateSalaReserva(salaReserva);
                DialogService.Close(salaReserva);
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