using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.JSInterop;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Radzen;
using Radzen.Blazor;

namespace SgpaNew.Client.Pages
{
    public partial class AddEmpresa
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
        public SgpaService SgpaService { get; set; }

        protected override async Task OnInitializedAsync()
        {
            empresa = new SgpaNew.Server.Models.Sgpa.Empresa();
        }
        protected bool errorVisible;
        protected SgpaNew.Server.Models.Sgpa.Empresa empresa;

        protected IEnumerable<SgpaNew.Server.Models.Sgpa.RegimenAporte> regimenAportesForCodRegimenAporte;

        protected IEnumerable<SgpaNew.Server.Models.Sgpa.SituacionPago> situacionPagosForCodSituacionPago;


        protected int regimenAportesForCodRegimenAporteCount;
        protected SgpaNew.Server.Models.Sgpa.RegimenAporte regimenAportesForCodRegimenAporteValue;
        protected async Task regimenAportesForCodRegimenAporteLoadData(LoadDataArgs args)
        {
            try
            {
                var result = await SgpaService.GetRegimenAportes(top: args.Top, skip: args.Skip, count:args.Top != null && args.Skip != null, filter: $"contains(Descrip, '{(!string.IsNullOrEmpty(args.Filter) ? args.Filter : "")}')", orderby: $"{args.OrderBy}");
                regimenAportesForCodRegimenAporte = result.Value.AsODataEnumerable();
                regimenAportesForCodRegimenAporteCount = result.Count;

                if (!object.Equals(empresa.CodRegimenAporte, null))
                {
                    var valueResult = await SgpaService.GetRegimenAportes(filter: $"CodRegimenAporte eq {empresa.CodRegimenAporte}");
                    var firstItem = valueResult.Value.FirstOrDefault();
                    if (firstItem != null)
                    {
                        regimenAportesForCodRegimenAporteValue = firstItem;
                    }
                }

            }
            catch (System.Exception ex)
            {
                NotificationService.Notify(new NotificationMessage(){ Severity = NotificationSeverity.Error, Summary = $"Error", Detail = $"Unable to load RegimenAporte" });
            }
        }

        protected int situacionPagosForCodSituacionPagoCount;
        protected SgpaNew.Server.Models.Sgpa.SituacionPago situacionPagosForCodSituacionPagoValue;
        protected async Task situacionPagosForCodSituacionPagoLoadData(LoadDataArgs args)
        {
            try
            {
                var result = await SgpaService.GetSituacionPagos(top: args.Top, skip: args.Skip, count:args.Top != null && args.Skip != null, filter: $"contains(Descrip, '{(!string.IsNullOrEmpty(args.Filter) ? args.Filter : "")}')", orderby: $"{args.OrderBy}");
                situacionPagosForCodSituacionPago = result.Value.AsODataEnumerable();
                situacionPagosForCodSituacionPagoCount = result.Count;

                if (!object.Equals(empresa.CodSituacionPago, null))
                {
                    var valueResult = await SgpaService.GetSituacionPagos(filter: $"CodSituacionPago eq {empresa.CodSituacionPago}");
                    var firstItem = valueResult.Value.FirstOrDefault();
                    if (firstItem != null)
                    {
                        situacionPagosForCodSituacionPagoValue = firstItem;
                    }
                }

            }
            catch (System.Exception ex)
            {
                NotificationService.Notify(new NotificationMessage(){ Severity = NotificationSeverity.Error, Summary = $"Error", Detail = $"Unable to load SituacionPago" });
            }
        }
        protected async Task FormSubmit()
        {
            try
            {
                var result = await SgpaService.CreateEmpresa(empresa);
                DialogService.Close(empresa);
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

        [Inject]
        protected SecurityService Security { get; set; }
    }
}