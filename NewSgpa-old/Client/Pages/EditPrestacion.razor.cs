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
    public partial class EditPrestacion
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

        [Parameter]
        public int PrestacionId { get; set; }

        protected override async Task OnInitializedAsync()
        {
            prestacion = await SgpaService.GetPrestacionByPrestacionId(prestacionId:PrestacionId);
        }
        protected bool errorVisible;
        protected SgpaNew.Server.Models.Sgpa.Prestacion prestacion;

        protected IEnumerable<SgpaNew.Server.Models.Sgpa.Afiliado> afiliadosForCI;

        protected IEnumerable<SgpaNew.Server.Models.Sgpa.PrestacionTipo> prestacionTiposForCodPrestacionTipo;

        protected IEnumerable<SgpaNew.Server.Models.Sgpa.Monedum> monedaForMoneda;


        protected int afiliadosForCICount;
        protected SgpaNew.Server.Models.Sgpa.Afiliado afiliadosForCIValue;
        protected async Task afiliadosForCILoadData(LoadDataArgs args)
        {
            try
            {
                var result = await SgpaService.GetAfiliados(top: args.Top, skip: args.Skip, count:args.Top != null && args.Skip != null, filter: $"contains(Nombres, '{(!string.IsNullOrEmpty(args.Filter) ? args.Filter : "")}')", orderby: $"{args.OrderBy}");
                afiliadosForCI = result.Value.AsODataEnumerable();
                afiliadosForCICount = result.Count;

                if (!object.Equals(prestacion.CI, null))
                {
                    var valueResult = await SgpaService.GetAfiliados(filter: $"CI eq {prestacion.CI}");
                    var firstItem = valueResult.Value.FirstOrDefault();
                    if (firstItem != null)
                    {
                        afiliadosForCIValue = firstItem;
                    }
                }

            }
            catch (System.Exception ex)
            {
                NotificationService.Notify(new NotificationMessage(){ Severity = NotificationSeverity.Error, Summary = $"Error", Detail = $"Unable to load Afiliado" });
            }
        }

        protected int prestacionTiposForCodPrestacionTipoCount;
        protected SgpaNew.Server.Models.Sgpa.PrestacionTipo prestacionTiposForCodPrestacionTipoValue;
        protected async Task prestacionTiposForCodPrestacionTipoLoadData(LoadDataArgs args)
        {
            try
            {
                var result = await SgpaService.GetPrestacionTipos(top: args.Top, skip: args.Skip, count:args.Top != null && args.Skip != null, filter: $"contains(Descrip, '{(!string.IsNullOrEmpty(args.Filter) ? args.Filter : "")}')", orderby: $"{args.OrderBy}");
                prestacionTiposForCodPrestacionTipo = result.Value.AsODataEnumerable();
                prestacionTiposForCodPrestacionTipoCount = result.Count;

                if (!object.Equals(prestacion.CodPrestacionTipo, null))
                {
                    var valueResult = await SgpaService.GetPrestacionTipos(filter: $"CodPrestacionTipo eq {prestacion.CodPrestacionTipo}");
                    var firstItem = valueResult.Value.FirstOrDefault();
                    if (firstItem != null)
                    {
                        prestacionTiposForCodPrestacionTipoValue = firstItem;
                    }
                }

            }
            catch (System.Exception ex)
            {
                NotificationService.Notify(new NotificationMessage(){ Severity = NotificationSeverity.Error, Summary = $"Error", Detail = $"Unable to load PrestacionTipo" });
            }
        }

        protected int monedaForMonedaCount;
        protected SgpaNew.Server.Models.Sgpa.Monedum monedaForMonedaValue;
        protected async Task monedaForMonedaLoadData(LoadDataArgs args)
        {
            try
            {
                var result = await SgpaService.GetMoneda(top: args.Top, skip: args.Skip, count:args.Top != null && args.Skip != null, filter: $"contains(Moneda1, '{(!string.IsNullOrEmpty(args.Filter) ? args.Filter : "")}')", orderby: $"{args.OrderBy}");
                monedaForMoneda = result.Value.AsODataEnumerable();
                monedaForMonedaCount = result.Count;

                if (!object.Equals(prestacion.Moneda, null))
                {
                    var valueResult = await SgpaService.GetMoneda(filter: $"Moneda1 eq '{prestacion.Moneda}'");
                    var firstItem = valueResult.Value.FirstOrDefault();
                    if (firstItem != null)
                    {
                        monedaForMonedaValue = firstItem;
                    }
                }

            }
            catch (System.Exception ex)
            {
                NotificationService.Notify(new NotificationMessage(){ Severity = NotificationSeverity.Error, Summary = $"Error", Detail = $"Unable to load Monedum" });
            }
        }
        protected async Task FormSubmit()
        {
            try
            {
                var result = await SgpaService.UpdatePrestacion(prestacionId:PrestacionId, prestacion);
                if (result.StatusCode == System.Net.HttpStatusCode.PreconditionFailed)
                {
                     hasChanges = true;
                     canEdit = false;
                     return;
                }
                DialogService.Close(prestacion);
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


        protected async Task ReloadButtonClick(MouseEventArgs args)
        {
            hasChanges = false;
            canEdit = true;

            prestacion = await SgpaService.GetPrestacionByPrestacionId(prestacionId:PrestacionId);
        }
    }
}