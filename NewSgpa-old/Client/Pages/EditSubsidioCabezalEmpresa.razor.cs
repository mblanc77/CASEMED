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
    public partial class EditSubsidioCabezalEmpresa
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
        public int SubsidioCabezalempresaId { get; set; }

        protected override async Task OnInitializedAsync()
        {
            subsidioCabezalEmpresa = await SgpaService.GetSubsidioCabezalEmpresaBySubsidioCabezalempresaId(subsidioCabezalempresaId:SubsidioCabezalempresaId);
        }
        protected bool errorVisible;
        protected SgpaNew.Server.Models.Sgpa.SubsidioCabezalEmpresa subsidioCabezalEmpresa;

        protected IEnumerable<SgpaNew.Server.Models.Sgpa.Empresa> empresasForCodEmpresa;

        protected IEnumerable<SgpaNew.Server.Models.Sgpa.SubsidioCabezal> subsidioCabezalsForIdSubsidio;


        protected int empresasForCodEmpresaCount;
        protected SgpaNew.Server.Models.Sgpa.Empresa empresasForCodEmpresaValue;
        protected async Task empresasForCodEmpresaLoadData(LoadDataArgs args)
        {
            try
            {
                var result = await SgpaService.GetEmpresas(top: args.Top, skip: args.Skip, count:args.Top != null && args.Skip != null, filter: $"contains(Nombre, '{(!string.IsNullOrEmpty(args.Filter) ? args.Filter : "")}')", orderby: $"{args.OrderBy}");
                empresasForCodEmpresa = result.Value.AsODataEnumerable();
                empresasForCodEmpresaCount = result.Count;

                if (!object.Equals(subsidioCabezalEmpresa.CodEmpresa, null))
                {
                    var valueResult = await SgpaService.GetEmpresas(filter: $"CodEmpresa eq {subsidioCabezalEmpresa.CodEmpresa}");
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

        protected int subsidioCabezalsForIdSubsidioCount;
        protected SgpaNew.Server.Models.Sgpa.SubsidioCabezal subsidioCabezalsForIdSubsidioValue;
        protected async Task subsidioCabezalsForIdSubsidioLoadData(LoadDataArgs args)
        {
            try
            {
                var result = await SgpaService.GetSubsidioCabezals(top: args.Top, skip: args.Skip, count:args.Top != null && args.Skip != null, filter: $"contains(NroCuenta, '{(!string.IsNullOrEmpty(args.Filter) ? args.Filter : "")}')", orderby: $"{args.OrderBy}");
                subsidioCabezalsForIdSubsidio = result.Value.AsODataEnumerable();
                subsidioCabezalsForIdSubsidioCount = result.Count;

                if (!object.Equals(subsidioCabezalEmpresa.IdSubsidio, null))
                {
                    var valueResult = await SgpaService.GetSubsidioCabezals(filter: $"IdSubsidio eq {subsidioCabezalEmpresa.IdSubsidio}");
                    var firstItem = valueResult.Value.FirstOrDefault();
                    if (firstItem != null)
                    {
                        subsidioCabezalsForIdSubsidioValue = firstItem;
                    }
                }

            }
            catch (System.Exception ex)
            {
                NotificationService.Notify(new NotificationMessage(){ Severity = NotificationSeverity.Error, Summary = $"Error", Detail = $"Unable to load SubsidioCabezal" });
            }
        }
        protected async Task FormSubmit()
        {
            try
            {
                var result = await SgpaService.UpdateSubsidioCabezalEmpresa(subsidioCabezalempresaId:SubsidioCabezalempresaId, subsidioCabezalEmpresa);
                if (result.StatusCode == System.Net.HttpStatusCode.PreconditionFailed)
                {
                     hasChanges = true;
                     canEdit = false;
                     return;
                }
                DialogService.Close(subsidioCabezalEmpresa);
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

            subsidioCabezalEmpresa = await SgpaService.GetSubsidioCabezalEmpresaBySubsidioCabezalempresaId(subsidioCabezalempresaId:SubsidioCabezalempresaId);
        }
    }
}