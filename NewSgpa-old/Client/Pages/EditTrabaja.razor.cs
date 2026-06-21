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
    public partial class EditTrabaja
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
        public int IdTrabaja { get; set; }

        protected override async Task OnInitializedAsync()
        {
            trabaja = await SgpaService.GetTrabajaByIdTrabaja(idTrabaja:IdTrabaja);
        }
        protected bool errorVisible;
        protected SgpaNew.Server.Models.Sgpa.Trabaja trabaja;

        protected IEnumerable<SgpaNew.Server.Models.Sgpa.Afiliado> afiliadosForCI;

        protected IEnumerable<SgpaNew.Server.Models.Sgpa.BajaMotivo> bajaMotivosForCodBajaMotivo;

        protected IEnumerable<SgpaNew.Server.Models.Sgpa.Empresa> empresasForCodEmpresa;


        protected int afiliadosForCICount;
        protected SgpaNew.Server.Models.Sgpa.Afiliado afiliadosForCIValue;
        protected async Task afiliadosForCILoadData(LoadDataArgs args)
        {
            try
            {
                var result = await SgpaService.GetAfiliados(top: args.Top, skip: args.Skip, count:args.Top != null && args.Skip != null, filter: $"contains(Nombres, '{(!string.IsNullOrEmpty(args.Filter) ? args.Filter : "")}')", orderby: $"{args.OrderBy}");
                afiliadosForCI = result.Value.AsODataEnumerable();
                afiliadosForCICount = result.Count;

                if (!object.Equals(trabaja.CI, null))
                {
                    var valueResult = await SgpaService.GetAfiliados(filter: $"CI eq {trabaja.CI}");
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

        protected int bajaMotivosForCodBajaMotivoCount;
        protected SgpaNew.Server.Models.Sgpa.BajaMotivo bajaMotivosForCodBajaMotivoValue;
        protected async Task bajaMotivosForCodBajaMotivoLoadData(LoadDataArgs args)
        {
            try
            {
                var result = await SgpaService.GetBajaMotivos(top: args.Top, skip: args.Skip, count:args.Top != null && args.Skip != null, filter: $"contains(Descrip, '{(!string.IsNullOrEmpty(args.Filter) ? args.Filter : "")}')", orderby: $"{args.OrderBy}");
                bajaMotivosForCodBajaMotivo = result.Value.AsODataEnumerable();
                bajaMotivosForCodBajaMotivoCount = result.Count;

                if (!object.Equals(trabaja.CodBajaMotivo, null))
                {
                    var valueResult = await SgpaService.GetBajaMotivos(filter: $"CodBajaMotivo eq {trabaja.CodBajaMotivo}");
                    var firstItem = valueResult.Value.FirstOrDefault();
                    if (firstItem != null)
                    {
                        bajaMotivosForCodBajaMotivoValue = firstItem;
                    }
                }

            }
            catch (System.Exception ex)
            {
                NotificationService.Notify(new NotificationMessage(){ Severity = NotificationSeverity.Error, Summary = $"Error", Detail = $"Unable to load BajaMotivo" });
            }
        }

        protected int empresasForCodEmpresaCount;
        protected SgpaNew.Server.Models.Sgpa.Empresa empresasForCodEmpresaValue;
        protected async Task empresasForCodEmpresaLoadData(LoadDataArgs args)
        {
            try
            {
                var result = await SgpaService.GetEmpresas(top: args.Top, skip: args.Skip, count:args.Top != null && args.Skip != null, filter: $"contains(Nombre, '{(!string.IsNullOrEmpty(args.Filter) ? args.Filter : "")}')", orderby: $"{args.OrderBy}");
                empresasForCodEmpresa = result.Value.AsODataEnumerable();
                empresasForCodEmpresaCount = result.Count;

                if (!object.Equals(trabaja.CodEmpresa, null))
                {
                    var valueResult = await SgpaService.GetEmpresas(filter: $"CodEmpresa eq {trabaja.CodEmpresa}");
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
                var result = await SgpaService.UpdateTrabaja(idTrabaja:IdTrabaja, trabaja);
                if (result.StatusCode == System.Net.HttpStatusCode.PreconditionFailed)
                {
                     hasChanges = true;
                     canEdit = false;
                     return;
                }
                DialogService.Close(trabaja);
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

            trabaja = await SgpaService.GetTrabajaByIdTrabaja(idTrabaja:IdTrabaja);
        }
    }
}