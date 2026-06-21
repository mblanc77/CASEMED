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
    public partial class EditCuentaBancarium
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
            cuentaBancarium = await CMUService.GetCuentaBancariumById(id:Id);
        }
        protected bool errorVisible;
        protected SGPA.Server.Models.CMU.CuentaBancarium cuentaBancarium;

        protected IEnumerable<SGPA.Server.Models.CMU.Banco> bancosForBanco;


        protected int bancosForBancoCount;
        protected SGPA.Server.Models.CMU.Banco bancosForBancoValue;
        protected async Task bancosForBancoLoadData(LoadDataArgs args)
        {
            try
            {
                var result = await CMUService.GetBancos(top: args.Top, skip: args.Skip, count:args.Top != null && args.Skip != null, filter: $"{args.Filter}", orderby: $"{args.OrderBy}");
                bancosForBanco = result.Value.AsODataEnumerable();
                bancosForBancoCount = result.Count;

                if (!object.Equals(cuentaBancarium.Banco, null))
                {
                    var valueResult = await CMUService.GetBancos(filter: $"Id eq {cuentaBancarium.Banco}");
                    var firstItem = valueResult.Value.FirstOrDefault();
                    if (firstItem != null)
                    {
                        bancosForBancoValue = firstItem;
                    }
                }

            }
            catch (System.Exception ex)
            {
                NotificationService.Notify(new NotificationMessage(){ Severity = NotificationSeverity.Error, Summary = $"Error", Detail = $"Unable to load Banco1" });
            }
        }
        protected async Task FormSubmit()
        {
            try
            {
                var result = await CMUService.UpdateCuentaBancarium(id:Id, cuentaBancarium);
                if (result.StatusCode == System.Net.HttpStatusCode.PreconditionFailed)
                {
                     hasChanges = true;
                     canEdit = false;
                     return;
                }
                DialogService.Close(cuentaBancarium);
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

            cuentaBancarium = await CMUService.GetCuentaBancariumById(id:Id);
        }
    }
}