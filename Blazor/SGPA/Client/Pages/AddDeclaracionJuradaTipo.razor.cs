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
    public partial class AddDeclaracionJuradaTipo
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
            declaracionJuradaTipo = new SGPA.Server.Models.CMU.DeclaracionJuradaTipo();
        }
        protected bool errorVisible;
        protected SGPA.Server.Models.CMU.DeclaracionJuradaTipo declaracionJuradaTipo;

        protected IEnumerable<SGPA.Server.Models.CMU.CategoriaColegiado> categoriaColegiadosForCategoria;


        protected int categoriaColegiadosForCategoriaCount;
        protected SGPA.Server.Models.CMU.CategoriaColegiado categoriaColegiadosForCategoriaValue;
        protected async Task categoriaColegiadosForCategoriaLoadData(LoadDataArgs args)
        {
            try
            {
                var result = await CMUService.GetCategoriaColegiados(top: args.Top, skip: args.Skip, count:args.Top != null && args.Skip != null, filter: $"{args.Filter}", orderby: $"{args.OrderBy}");
                categoriaColegiadosForCategoria = result.Value.AsODataEnumerable();
                categoriaColegiadosForCategoriaCount = result.Count;

                if (!object.Equals(declaracionJuradaTipo.Categoria, null))
                {
                    var valueResult = await CMUService.GetCategoriaColegiados(filter: $"Id eq {declaracionJuradaTipo.Categoria}");
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
        protected async Task FormSubmit()
        {
            try
            {
                var result = await CMUService.CreateDeclaracionJuradaTipo(declaracionJuradaTipo);
                DialogService.Close(declaracionJuradaTipo);
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