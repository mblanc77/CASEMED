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
    public partial class AddEmpresaPago
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
            empresaPago = new SgpaNew.Server.Models.Sgpa.EmpresaPago();
        }
        protected bool errorVisible;
        protected SgpaNew.Server.Models.Sgpa.EmpresaPago empresaPago;

        protected IEnumerable<SgpaNew.Server.Models.Sgpa.Empresa> empresasForCodEmpresa;


        protected int empresasForCodEmpresaCount;
        protected SgpaNew.Server.Models.Sgpa.Empresa empresasForCodEmpresaValue;
        protected async Task empresasForCodEmpresaLoadData(LoadDataArgs args)
        {
            try
            {
                var result = await SgpaService.GetEmpresas(top: args.Top, skip: args.Skip, count:args.Top != null && args.Skip != null, filter: $"contains(Nombre, '{(!string.IsNullOrEmpty(args.Filter) ? args.Filter : "")}')", orderby: $"{args.OrderBy}");
                empresasForCodEmpresa = result.Value.AsODataEnumerable();
                empresasForCodEmpresaCount = result.Count;

                if (!object.Equals(empresaPago.CodEmpresa, null))
                {
                    var valueResult = await SgpaService.GetEmpresas(filter: $"CodEmpresa eq {empresaPago.CodEmpresa}");
                    var firstItem = valueResult.Value.FirstOrDefault();
                    if (firstItem != null)
                    {
                        empresasForCodEmpresaValue = firstItem;
                    }
                }

            }
            catch (System.Exception ex)
            {
                NotificationService.Notify(new NotificationMessage(){ Severity = NotificationSeverity.Error, Summary = $"Error", Detail = $"Unable to load Empresa" });
            }
        }
        protected async Task FormSubmit()
        {
            try
            {
                var result = await SgpaService.CreateEmpresaPago(empresaPago);
                DialogService.Close(empresaPago);
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