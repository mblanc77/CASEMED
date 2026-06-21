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
    public partial class AddTramiteCarneEstado
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
            tramiteCarneEstado = new SGPA.Server.Models.CMU.TramiteCarneEstado();
        }
        protected bool errorVisible;
        protected SGPA.Server.Models.CMU.TramiteCarneEstado tramiteCarneEstado;

        protected IEnumerable<SGPA.Server.Models.CMU.TramiteCarneEstadoCodigo> tramiteCarneEstadoCodigosForEstado;

        protected IEnumerable<SGPA.Server.Models.CMU.TramiteCarne> tramiteCarnesForTramite;


        protected int tramiteCarneEstadoCodigosForEstadoCount;
        protected SGPA.Server.Models.CMU.TramiteCarneEstadoCodigo tramiteCarneEstadoCodigosForEstadoValue;
        protected async Task tramiteCarneEstadoCodigosForEstadoLoadData(LoadDataArgs args)
        {
            try
            {
                var result = await CMUService.GetTramiteCarneEstadoCodigos(top: args.Top, skip: args.Skip, count:args.Top != null && args.Skip != null, filter: $"{args.Filter}", orderby: $"{args.OrderBy}");
                tramiteCarneEstadoCodigosForEstado = result.Value.AsODataEnumerable();
                tramiteCarneEstadoCodigosForEstadoCount = result.Count;

                if (!object.Equals(tramiteCarneEstado.Estado, null))
                {
                    var valueResult = await CMUService.GetTramiteCarneEstadoCodigos(filter: $"Id eq {tramiteCarneEstado.Estado}");
                    var firstItem = valueResult.Value.FirstOrDefault();
                    if (firstItem != null)
                    {
                        tramiteCarneEstadoCodigosForEstadoValue = firstItem;
                    }
                }

            }
            catch (System.Exception ex)
            {
                NotificationService.Notify(new NotificationMessage(){ Severity = NotificationSeverity.Error, Summary = $"Error", Detail = $"Unable to load TramiteCarneEstadoCodigo" });
            }
        }

        protected int tramiteCarnesForTramiteCount;
        protected SGPA.Server.Models.CMU.TramiteCarne tramiteCarnesForTramiteValue;
        protected async Task tramiteCarnesForTramiteLoadData(LoadDataArgs args)
        {
            try
            {
                var result = await CMUService.GetTramiteCarnes(top: args.Top, skip: args.Skip, count:args.Top != null && args.Skip != null, filter: $"{args.Filter}", orderby: $"{args.OrderBy}");
                tramiteCarnesForTramite = result.Value.AsODataEnumerable();
                tramiteCarnesForTramiteCount = result.Count;

                if (!object.Equals(tramiteCarneEstado.Tramite, null))
                {
                    var valueResult = await CMUService.GetTramiteCarnes(filter: $"OID eq {tramiteCarneEstado.Tramite}");
                    var firstItem = valueResult.Value.FirstOrDefault();
                    if (firstItem != null)
                    {
                        tramiteCarnesForTramiteValue = firstItem;
                    }
                }

            }
            catch (System.Exception ex)
            {
                NotificationService.Notify(new NotificationMessage(){ Severity = NotificationSeverity.Error, Summary = $"Error", Detail = $"Unable to load TramiteCarne" });
            }
        }
        protected async Task FormSubmit()
        {
            try
            {
                var result = await CMUService.CreateTramiteCarneEstado(tramiteCarneEstado);
                DialogService.Close(tramiteCarneEstado);
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