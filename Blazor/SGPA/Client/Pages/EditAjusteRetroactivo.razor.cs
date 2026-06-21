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
    public partial class EditAjusteRetroactivo
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
            ajusteRetroactivo = await CMUService.GetAjusteRetroactivoById(id:Id);
        }
        protected bool errorVisible;
        protected SGPA.Server.Models.CMU.AjusteRetroactivo ajusteRetroactivo;

        protected IEnumerable<SGPA.Server.Models.CMU.AjusteRetroactivo> ajusteRetroactivosForAnulaA;

        protected IEnumerable<SGPA.Server.Models.CMU.ColegiadoCambioCategorium> colegiadoCambioCategoriaForCambioCategoria;

        protected IEnumerable<SGPA.Server.Models.CMU.Colegiado> colegiadosForColegiado;

        protected IEnumerable<SGPA.Server.Models.CMU.ColegiadoDeclaracionJuradum> colegiadoDeclaracionJuradaForDeclaracionJurada;


        protected int ajusteRetroactivosForAnulaACount;
        protected SGPA.Server.Models.CMU.AjusteRetroactivo ajusteRetroactivosForAnulaAValue;
        protected async Task ajusteRetroactivosForAnulaALoadData(LoadDataArgs args)
        {
            try
            {
                var result = await CMUService.GetAjusteRetroactivos(top: args.Top, skip: args.Skip, count:args.Top != null && args.Skip != null, filter: $"{args.Filter}", orderby: $"{args.OrderBy}");
                ajusteRetroactivosForAnulaA = result.Value.AsODataEnumerable();
                ajusteRetroactivosForAnulaACount = result.Count;

                if (!object.Equals(ajusteRetroactivo.AnulaA, null))
                {
                    var valueResult = await CMUService.GetAjusteRetroactivos(filter: $"Id eq {ajusteRetroactivo.AnulaA}");
                    var firstItem = valueResult.Value.FirstOrDefault();
                    if (firstItem != null)
                    {
                        ajusteRetroactivosForAnulaAValue = firstItem;
                    }
                }

            }
            catch (System.Exception ex)
            {
                NotificationService.Notify(new NotificationMessage(){ Severity = NotificationSeverity.Error, Summary = $"Error", Detail = $"Unable to load AjusteRetroactivo1" });
            }
        }

        protected int colegiadoCambioCategoriaForCambioCategoriaCount;
        protected SGPA.Server.Models.CMU.ColegiadoCambioCategorium colegiadoCambioCategoriaForCambioCategoriaValue;
        protected async Task colegiadoCambioCategoriaForCambioCategoriaLoadData(LoadDataArgs args)
        {
            try
            {
                var result = await CMUService.GetColegiadoCambioCategoria(top: args.Top, skip: args.Skip, count:args.Top != null && args.Skip != null, filter: $"{args.Filter}", orderby: $"{args.OrderBy}");
                colegiadoCambioCategoriaForCambioCategoria = result.Value.AsODataEnumerable();
                colegiadoCambioCategoriaForCambioCategoriaCount = result.Count;

                if (!object.Equals(ajusteRetroactivo.CambioCategoria, null))
                {
                    var valueResult = await CMUService.GetColegiadoCambioCategoria(filter: $"Id eq {ajusteRetroactivo.CambioCategoria}");
                    var firstItem = valueResult.Value.FirstOrDefault();
                    if (firstItem != null)
                    {
                        colegiadoCambioCategoriaForCambioCategoriaValue = firstItem;
                    }
                }

            }
            catch (System.Exception ex)
            {
                NotificationService.Notify(new NotificationMessage(){ Severity = NotificationSeverity.Error, Summary = $"Error", Detail = $"Unable to load ColegiadoCambioCategorium" });
            }
        }

        protected int colegiadosForColegiadoCount;
        protected SGPA.Server.Models.CMU.Colegiado colegiadosForColegiadoValue;
        protected async Task colegiadosForColegiadoLoadData(LoadDataArgs args)
        {
            try
            {
                var result = await CMUService.GetColegiados(top: args.Top, skip: args.Skip, count:args.Top != null && args.Skip != null, filter: $"{args.Filter}", orderby: $"{args.OrderBy}");
                colegiadosForColegiado = result.Value.AsODataEnumerable();
                colegiadosForColegiadoCount = result.Count;

                if (!object.Equals(ajusteRetroactivo.Colegiado, null))
                {
                    var valueResult = await CMUService.GetColegiados(filter: $"Documento eq {ajusteRetroactivo.Colegiado}");
                    var firstItem = valueResult.Value.FirstOrDefault();
                    if (firstItem != null)
                    {
                        colegiadosForColegiadoValue = firstItem;
                    }
                }

            }
            catch (System.Exception ex)
            {
                NotificationService.Notify(new NotificationMessage(){ Severity = NotificationSeverity.Error, Summary = $"Error", Detail = $"Unable to load Colegiado1" });
            }
        }

        protected int colegiadoDeclaracionJuradaForDeclaracionJuradaCount;
        protected SGPA.Server.Models.CMU.ColegiadoDeclaracionJuradum colegiadoDeclaracionJuradaForDeclaracionJuradaValue;
        protected async Task colegiadoDeclaracionJuradaForDeclaracionJuradaLoadData(LoadDataArgs args)
        {
            try
            {
                var result = await CMUService.GetColegiadoDeclaracionJurada(top: args.Top, skip: args.Skip, count:args.Top != null && args.Skip != null, filter: $"{args.Filter}", orderby: $"{args.OrderBy}");
                colegiadoDeclaracionJuradaForDeclaracionJurada = result.Value.AsODataEnumerable();
                colegiadoDeclaracionJuradaForDeclaracionJuradaCount = result.Count;

                if (!object.Equals(ajusteRetroactivo.DeclaracionJurada, null))
                {
                    var valueResult = await CMUService.GetColegiadoDeclaracionJurada(filter: $"Id eq {ajusteRetroactivo.DeclaracionJurada}");
                    var firstItem = valueResult.Value.FirstOrDefault();
                    if (firstItem != null)
                    {
                        colegiadoDeclaracionJuradaForDeclaracionJuradaValue = firstItem;
                    }
                }

            }
            catch (System.Exception ex)
            {
                NotificationService.Notify(new NotificationMessage(){ Severity = NotificationSeverity.Error, Summary = $"Error", Detail = $"Unable to load ColegiadoDeclaracionJuradum" });
            }
        }
        protected async Task FormSubmit()
        {
            try
            {
                var result = await CMUService.UpdateAjusteRetroactivo(id:Id, ajusteRetroactivo);
                if (result.StatusCode == System.Net.HttpStatusCode.PreconditionFailed)
                {
                     hasChanges = true;
                     canEdit = false;
                     return;
                }
                DialogService.Close(ajusteRetroactivo);
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

            ajusteRetroactivo = await CMUService.GetAjusteRetroactivoById(id:Id);
        }
    }
}