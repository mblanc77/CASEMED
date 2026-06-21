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
    public partial class EditColegiadoDebitoBancarioAsociado
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
            colegiadoDebitoBancarioAsociado = await CMUService.GetColegiadoDebitoBancarioAsociadoById(id:Id);
        }
        protected bool errorVisible;
        protected SGPA.Server.Models.CMU.ColegiadoDebitoBancarioAsociado colegiadoDebitoBancarioAsociado;

        protected IEnumerable<SGPA.Server.Models.CMU.AgenteCobranzaDebito> agenteCobranzaDebitosForAgenteDebito;

        protected IEnumerable<SGPA.Server.Models.CMU.Colegiado> colegiadosForColegiado;


        protected int agenteCobranzaDebitosForAgenteDebitoCount;
        protected SGPA.Server.Models.CMU.AgenteCobranzaDebito agenteCobranzaDebitosForAgenteDebitoValue;
        protected async Task agenteCobranzaDebitosForAgenteDebitoLoadData(LoadDataArgs args)
        {
            try
            {
                var result = await CMUService.GetAgenteCobranzaDebitos(top: args.Top, skip: args.Skip, count:args.Top != null && args.Skip != null, filter: $"{args.Filter}", orderby: $"{args.OrderBy}");
                agenteCobranzaDebitosForAgenteDebito = result.Value.AsODataEnumerable();
                agenteCobranzaDebitosForAgenteDebitoCount = result.Count;

                if (!object.Equals(colegiadoDebitoBancarioAsociado.AgenteDebito, null))
                {
                    var valueResult = await CMUService.GetAgenteCobranzaDebitos(filter: $"Id eq {colegiadoDebitoBancarioAsociado.AgenteDebito}");
                    var firstItem = valueResult.Value.FirstOrDefault();
                    if (firstItem != null)
                    {
                        agenteCobranzaDebitosForAgenteDebitoValue = firstItem;
                    }
                }

            }
            catch (System.Exception ex)
            {
                NotificationService.Notify(new NotificationMessage(){ Severity = NotificationSeverity.Error, Summary = $"Error", Detail = $"Unable to load AgenteCobranzaDebito" });
            }
        }

        protected int colegiadosForColegiadoCount;
        protected SGPA.Server.Models.CMU.Colegiado colegiadosForColegiadoValue;
        protected async Task colegiadosForColegiadoLoadData(LoadDataArgs args)
        {
            try
            {
                var result = await CMUService.GetColegiados(top: args.Top, skip: args.Skip, count:args.Top != null && args.Skip != null, filter: $"{args.Filter}", orderby: $"{args.OrderBy}");
                colegiadosForColegiado = result.Value.AsODataEnumerable();
                colegiadosForColegiadoCount = result.Count;

                if (!object.Equals(colegiadoDebitoBancarioAsociado.Colegiado, null))
                {
                    var valueResult = await CMUService.GetColegiados(filter: $"Documento eq {colegiadoDebitoBancarioAsociado.Colegiado}");
                    var firstItem = valueResult.Value.FirstOrDefault();
                    if (firstItem != null)
                    {
                        colegiadosForColegiadoValue = firstItem;
                    }
                }

            }
            catch (System.Exception ex)
            {
                NotificationService.Notify(new NotificationMessage(){ Severity = NotificationSeverity.Error, Summary = $"Error", Detail = $"Unable to load Colegiado1" });
            }
        }
        protected async Task FormSubmit()
        {
            try
            {
                var result = await CMUService.UpdateColegiadoDebitoBancarioAsociado(id:Id, colegiadoDebitoBancarioAsociado);
                if (result.StatusCode == System.Net.HttpStatusCode.PreconditionFailed)
                {
                     hasChanges = true;
                     canEdit = false;
                     return;
                }
                DialogService.Close(colegiadoDebitoBancarioAsociado);
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

            colegiadoDebitoBancarioAsociado = await CMUService.GetColegiadoDebitoBancarioAsociadoById(id:Id);
        }
    }
}