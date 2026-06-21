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
    public partial class EditCertificacion
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
        public int NroLlamado { get; set; }

        protected override async Task OnInitializedAsync()
        {
            certificacion = await SgpaService.GetCertificacionByNroLlamado(nroLlamado:NroLlamado);
        }
        protected bool errorVisible;
        protected SgpaNew.Server.Models.Sgpa.Certificacion certificacion;

        protected IEnumerable<SgpaNew.Server.Models.Sgpa.Afiliado> afiliadosForCI;

        protected IEnumerable<SgpaNew.Server.Models.Sgpa.AfeccionTipo> afeccionTiposForCodAfeccionTipo;

        protected IEnumerable<SgpaNew.Server.Models.Sgpa.Certificador> certificadorsForCodCertificador;

        protected IEnumerable<SgpaNew.Server.Models.Sgpa.SalidaTipo> salidaTiposForCodSalidaTipo;


        protected int afiliadosForCICount;
        protected SgpaNew.Server.Models.Sgpa.Afiliado afiliadosForCIValue;
        protected async Task afiliadosForCILoadData(LoadDataArgs args)
        {
            try
            {
                var result = await SgpaService.GetAfiliados(top: args.Top, skip: args.Skip, count:args.Top != null && args.Skip != null, filter: $"contains(Nombres, '{(!string.IsNullOrEmpty(args.Filter) ? args.Filter : "")}')", orderby: $"{args.OrderBy}");
                afiliadosForCI = result.Value.AsODataEnumerable();
                afiliadosForCICount = result.Count;

                if (!object.Equals(certificacion.CI, null))
                {
                    var valueResult = await SgpaService.GetAfiliados(filter: $"CI eq {certificacion.CI}");
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

        protected int afeccionTiposForCodAfeccionTipoCount;
        protected SgpaNew.Server.Models.Sgpa.AfeccionTipo afeccionTiposForCodAfeccionTipoValue;
        protected async Task afeccionTiposForCodAfeccionTipoLoadData(LoadDataArgs args)
        {
            try
            {
                var result = await SgpaService.GetAfeccionTipos(top: args.Top, skip: args.Skip, count:args.Top != null && args.Skip != null, filter: $"contains(Descrip, '{(!string.IsNullOrEmpty(args.Filter) ? args.Filter : "")}')", orderby: $"{args.OrderBy}");
                afeccionTiposForCodAfeccionTipo = result.Value.AsODataEnumerable();
                afeccionTiposForCodAfeccionTipoCount = result.Count;

                if (!object.Equals(certificacion.CodAfeccionTipo, null))
                {
                    var valueResult = await SgpaService.GetAfeccionTipos(filter: $"CodAfeccionTipo eq {certificacion.CodAfeccionTipo}");
                    var firstItem = valueResult.Value.FirstOrDefault();
                    if (firstItem != null)
                    {
                        afeccionTiposForCodAfeccionTipoValue = firstItem;
                    }
                }

            }
            catch (System.Exception ex)
            {
                NotificationService.Notify(new NotificationMessage(){ Severity = NotificationSeverity.Error, Summary = $"Error", Detail = $"Unable to load AfeccionTipo" });
            }
        }

        protected int certificadorsForCodCertificadorCount;
        protected SgpaNew.Server.Models.Sgpa.Certificador certificadorsForCodCertificadorValue;
        protected async Task certificadorsForCodCertificadorLoadData(LoadDataArgs args)
        {
            try
            {
                var result = await SgpaService.GetCertificadors(top: args.Top, skip: args.Skip, count:args.Top != null && args.Skip != null, filter: $"contains(Descrip, '{(!string.IsNullOrEmpty(args.Filter) ? args.Filter : "")}')", orderby: $"{args.OrderBy}");
                certificadorsForCodCertificador = result.Value.AsODataEnumerable();
                certificadorsForCodCertificadorCount = result.Count;

                if (!object.Equals(certificacion.CodCertificador, null))
                {
                    var valueResult = await SgpaService.GetCertificadors(filter: $"CodCertificador eq {certificacion.CodCertificador}");
                    var firstItem = valueResult.Value.FirstOrDefault();
                    if (firstItem != null)
                    {
                        certificadorsForCodCertificadorValue = firstItem;
                    }
                }

            }
            catch (System.Exception ex)
            {
                NotificationService.Notify(new NotificationMessage(){ Severity = NotificationSeverity.Error, Summary = $"Error", Detail = $"Unable to load Certificador" });
            }
        }

        protected int salidaTiposForCodSalidaTipoCount;
        protected SgpaNew.Server.Models.Sgpa.SalidaTipo salidaTiposForCodSalidaTipoValue;
        protected async Task salidaTiposForCodSalidaTipoLoadData(LoadDataArgs args)
        {
            try
            {
                var result = await SgpaService.GetSalidaTipos(top: args.Top, skip: args.Skip, count:args.Top != null && args.Skip != null, filter: $"contains(Descrip, '{(!string.IsNullOrEmpty(args.Filter) ? args.Filter : "")}')", orderby: $"{args.OrderBy}");
                salidaTiposForCodSalidaTipo = result.Value.AsODataEnumerable();
                salidaTiposForCodSalidaTipoCount = result.Count;

                if (!object.Equals(certificacion.CodSalidaTipo, null))
                {
                    var valueResult = await SgpaService.GetSalidaTipos(filter: $"CodSalidaTipo eq {certificacion.CodSalidaTipo}");
                    var firstItem = valueResult.Value.FirstOrDefault();
                    if (firstItem != null)
                    {
                        salidaTiposForCodSalidaTipoValue = firstItem;
                    }
                }

            }
            catch (System.Exception ex)
            {
                NotificationService.Notify(new NotificationMessage(){ Severity = NotificationSeverity.Error, Summary = $"Error", Detail = $"Unable to load SalidaTipo" });
            }
        }
        protected async Task FormSubmit()
        {
            try
            {
                var result = await SgpaService.UpdateCertificacion(nroLlamado:NroLlamado, certificacion);
                if (result.StatusCode == System.Net.HttpStatusCode.PreconditionFailed)
                {
                     hasChanges = true;
                     canEdit = false;
                     return;
                }
                DialogService.Close(certificacion);
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

            certificacion = await SgpaService.GetCertificacionByNroLlamado(nroLlamado:NroLlamado);
        }
    }
}