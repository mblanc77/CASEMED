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
    public partial class AddTramiteCarneEstadoWorkFlow
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
            tramiteCarneEstadoWorkFlow = new SGPA.Server.Models.CMU.TramiteCarneEstadoWorkFlow();
        }
        protected bool errorVisible;
        protected SGPA.Server.Models.CMU.TramiteCarneEstadoWorkFlow tramiteCarneEstadoWorkFlow;

        protected IEnumerable<SGPA.Server.Models.CMU.TramiteCarneEstadoCodigo> tramiteCarneEstadoCodigosForEstadoActual;

        protected IEnumerable<SGPA.Server.Models.CMU.TramiteCarneEstadoCodigo> tramiteCarneEstadoCodigosForEstadoSiguiente;


        protected int tramiteCarneEstadoCodigosForEstadoActualCount;
        protected SGPA.Server.Models.CMU.TramiteCarneEstadoCodigo tramiteCarneEstadoCodigosForEstadoActualValue;
        protected async Task tramiteCarneEstadoCodigosForEstadoActualLoadData(LoadDataArgs args)
        {
            try
            {
                var result = await CMUService.GetTramiteCarneEstadoCodigos(top: args.Top, skip: args.Skip, count:args.Top != null && args.Skip != null, filter: $"{args.Filter}", orderby: $"{args.OrderBy}");
                tramiteCarneEstadoCodigosForEstadoActual = result.Value.AsODataEnumerable();
                tramiteCarneEstadoCodigosForEstadoActualCount = result.Count;

                if (!object.Equals(tramiteCarneEstadoWorkFlow.EstadoActual, null))
                {
                    var valueResult = await CMUService.GetTramiteCarneEstadoCodigos(filter: $"Id eq {tramiteCarneEstadoWorkFlow.EstadoActual}");
                    var firstItem = valueResult.Value.FirstOrDefault();
                    if (firstItem != null)
                    {
                        tramiteCarneEstadoCodigosForEstadoActualValue = firstItem;
                    }
                }

            }
            catch (System.Exception ex)
            {
                NotificationService.Notify(new NotificationMessage(){ Severity = NotificationSeverity.Error, Summary = $"Error", Detail = $"Unable to load TramiteCarneEstadoCodigo" });
            }
        }

        protected int tramiteCarneEstadoCodigosForEstadoSiguienteCount;
        protected SGPA.Server.Models.CMU.TramiteCarneEstadoCodigo tramiteCarneEstadoCodigosForEstadoSiguienteValue;
        protected async Task tramiteCarneEstadoCodigosForEstadoSiguienteLoadData(LoadDataArgs args)
        {
            try
            {
                var result = await CMUService.GetTramiteCarneEstadoCodigos(top: args.Top, skip: args.Skip, count:args.Top != null && args.Skip != null, filter: $"{args.Filter}", orderby: $"{args.OrderBy}");
                tramiteCarneEstadoCodigosForEstadoSiguiente = result.Value.AsODataEnumerable();
                tramiteCarneEstadoCodigosForEstadoSiguienteCount = result.Count;

                if (!object.Equals(tramiteCarneEstadoWorkFlow.EstadoSiguiente, null))
                {
                    var valueResult = await CMUService.GetTramiteCarneEstadoCodigos(filter: $"Id eq {tramiteCarneEstadoWorkFlow.EstadoSiguiente}");
                    var firstItem = valueResult.Value.FirstOrDefault();
                    if (firstItem != null)
                    {
                        tramiteCarneEstadoCodigosForEstadoSiguienteValue = firstItem;
                    }
                }

            }
            catch (System.Exception ex)
            {
                NotificationService.Notify(new NotificationMessage(){ Severity = NotificationSeverity.Error, Summary = $"Error", Detail = $"Unable to load TramiteCarneEstadoCodigo1" });
            }
        }
        protected async Task FormSubmit()
        {
            try
            {
                var result = await CMUService.CreateTramiteCarneEstadoWorkFlow(tramiteCarneEstadoWorkFlow);
                DialogService.Close(tramiteCarneEstadoWorkFlow);
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