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
    public partial class EditColegiadoCambioCategorium
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
            colegiadoCambioCategorium = await CMUService.GetColegiadoCambioCategoriumById(id:Id);
        }
        protected bool errorVisible;
        protected SGPA.Server.Models.CMU.ColegiadoCambioCategorium colegiadoCambioCategorium;

        protected IEnumerable<SGPA.Server.Models.CMU.CategoriaColegiado> categoriaColegiadosForCategoria;

        protected IEnumerable<SGPA.Server.Models.CMU.Colegiado> colegiadosForColegiado;


        protected int categoriaColegiadosForCategoriaCount;
        protected SGPA.Server.Models.CMU.CategoriaColegiado categoriaColegiadosForCategoriaValue;
        protected async Task categoriaColegiadosForCategoriaLoadData(LoadDataArgs args)
        {
            try
            {
                var result = await CMUService.GetCategoriaColegiados(top: args.Top, skip: args.Skip, count:args.Top != null && args.Skip != null, filter: $"{args.Filter}", orderby: $"{args.OrderBy}");
                categoriaColegiadosForCategoria = result.Value.AsODataEnumerable();
                categoriaColegiadosForCategoriaCount = result.Count;

                if (!object.Equals(colegiadoCambioCategorium.Categoria, null))
                {
                    var valueResult = await CMUService.GetCategoriaColegiados(filter: $"Id eq {colegiadoCambioCategorium.Categoria}");
                    var firstItem = valueResult.Value.FirstOrDefault();
                    if (firstItem != null)
                    {
                        categoriaColegiadosForCategoriaValue = firstItem;
                    }
                }

            }
            catch (System.Exception ex)
            {
                NotificationService.Notify(new NotificationMessage(){ Severity = NotificationSeverity.Error, Summary = $"Error", Detail = $"Unable to load CategoriaColegiado" });
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

                if (!object.Equals(colegiadoCambioCategorium.Colegiado, null))
                {
                    var valueResult = await CMUService.GetColegiados(filter: $"Documento eq {colegiadoCambioCategorium.Colegiado}");
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
        protected async Task FormSubmit()
        {
            try
            {
                var result = await CMUService.UpdateColegiadoCambioCategorium(id:Id, colegiadoCambioCategorium);
                if (result.StatusCode == System.Net.HttpStatusCode.PreconditionFailed)
                {
                     hasChanges = true;
                     canEdit = false;
                     return;
                }
                DialogService.Close(colegiadoCambioCategorium);
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

            colegiadoCambioCategorium = await CMUService.GetColegiadoCambioCategoriumById(id:Id);
        }
    }
}