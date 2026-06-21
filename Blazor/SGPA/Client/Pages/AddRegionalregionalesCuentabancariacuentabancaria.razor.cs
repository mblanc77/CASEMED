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
    public partial class AddRegionalregionalesCuentabancariacuentabancaria
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
            regionalregionalesCuentabancariacuentabancaria = new SGPA.Server.Models.CMU.RegionalregionalesCuentabancariacuentabancaria();
        }
        protected bool errorVisible;
        protected SGPA.Server.Models.CMU.RegionalregionalesCuentabancariacuentabancaria regionalregionalesCuentabancariacuentabancaria;

        protected IEnumerable<SGPA.Server.Models.CMU.CuentaBancarium> cuentaBancariaForCuentaBancarias;

        protected IEnumerable<SGPA.Server.Models.CMU.Regional> regionalsForRegionales;


        protected int cuentaBancariaForCuentaBancariasCount;
        protected SGPA.Server.Models.CMU.CuentaBancarium cuentaBancariaForCuentaBancariasValue;
        protected async Task cuentaBancariaForCuentaBancariasLoadData(LoadDataArgs args)
        {
            try
            {
                var result = await CMUService.GetCuentaBancaria(top: args.Top, skip: args.Skip, count:args.Top != null && args.Skip != null, filter: $"{args.Filter}", orderby: $"{args.OrderBy}");
                cuentaBancariaForCuentaBancarias = result.Value.AsODataEnumerable();
                cuentaBancariaForCuentaBancariasCount = result.Count;

                if (!object.Equals(regionalregionalesCuentabancariacuentabancaria.CuentaBancarias, null))
                {
                    var valueResult = await CMUService.GetCuentaBancaria(filter: $"Id eq {regionalregionalesCuentabancariacuentabancaria.CuentaBancarias}");
                    var firstItem = valueResult.Value.FirstOrDefault();
                    if (firstItem != null)
                    {
                        cuentaBancariaForCuentaBancariasValue = firstItem;
                    }
                }

            }
            catch (System.Exception ex)
            {
                NotificationService.Notify(new NotificationMessage(){ Severity = NotificationSeverity.Error, Summary = $"Error", Detail = $"Unable to load CuentaBancarium" });
            }
        }

        protected int regionalsForRegionalesCount;
        protected SGPA.Server.Models.CMU.Regional regionalsForRegionalesValue;
        protected async Task regionalsForRegionalesLoadData(LoadDataArgs args)
        {
            try
            {
                var result = await CMUService.GetRegionals(top: args.Top, skip: args.Skip, count:args.Top != null && args.Skip != null, filter: $"{args.Filter}", orderby: $"{args.OrderBy}");
                regionalsForRegionales = result.Value.AsODataEnumerable();
                regionalsForRegionalesCount = result.Count;

                if (!object.Equals(regionalregionalesCuentabancariacuentabancaria.Regionales, null))
                {
                    var valueResult = await CMUService.GetRegionals(filter: $"Id eq {regionalregionalesCuentabancariacuentabancaria.Regionales}");
                    var firstItem = valueResult.Value.FirstOrDefault();
                    if (firstItem != null)
                    {
                        regionalsForRegionalesValue = firstItem;
                    }
                }

            }
            catch (System.Exception ex)
            {
                NotificationService.Notify(new NotificationMessage(){ Severity = NotificationSeverity.Error, Summary = $"Error", Detail = $"Unable to load Regional" });
            }
        }
        protected async Task FormSubmit()
        {
            try
            {
                var result = await CMUService.CreateRegionalregionalesCuentabancariacuentabancaria(regionalregionalesCuentabancariacuentabancaria);
                DialogService.Close(regionalregionalesCuentabancariacuentabancaria);
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