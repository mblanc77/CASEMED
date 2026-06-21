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
    public partial class EditMovimientoCuentaCuotum
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
            movimientoCuentaCuotum = await CMUService.GetMovimientoCuentaCuotumById(id:Id);
        }
        protected bool errorVisible;
        protected SGPA.Server.Models.CMU.MovimientoCuentaCuotum movimientoCuentaCuotum;

        protected IEnumerable<SGPA.Server.Models.CMU.CategoriaColegiado> categoriaColegiadosForCategoria;

        protected IEnumerable<SGPA.Server.Models.CMU.MovimientoCuentum> movimientoCuentaForId;


        protected int categoriaColegiadosForCategoriaCount;
        protected SGPA.Server.Models.CMU.CategoriaColegiado categoriaColegiadosForCategoriaValue;
        protected async Task categoriaColegiadosForCategoriaLoadData(LoadDataArgs args)
        {
            try
            {
                var result = await CMUService.GetCategoriaColegiados(top: args.Top, skip: args.Skip, count:args.Top != null && args.Skip != null, filter: $"{args.Filter}", orderby: $"{args.OrderBy}");
                categoriaColegiadosForCategoria = result.Value.AsODataEnumerable();
                categoriaColegiadosForCategoriaCount = result.Count;

                if (!object.Equals(movimientoCuentaCuotum.Categoria, null))
                {
                    var valueResult = await CMUService.GetCategoriaColegiados(filter: $"Id eq {movimientoCuentaCuotum.Categoria}");
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

        protected int movimientoCuentaForIdCount;
        protected SGPA.Server.Models.CMU.MovimientoCuentum movimientoCuentaForIdValue;
        protected async Task movimientoCuentaForIdLoadData(LoadDataArgs args)
        {
            try
            {
                var result = await CMUService.GetMovimientoCuenta(top: args.Top, skip: args.Skip, count:args.Top != null && args.Skip != null, filter: $"{args.Filter}", orderby: $"{args.OrderBy}");
                movimientoCuentaForId = result.Value.AsODataEnumerable();
                movimientoCuentaForIdCount = result.Count;

                if (!object.Equals(movimientoCuentaCuotum.Id, null))
                {
                    var valueResult = await CMUService.GetMovimientoCuenta(filter: $"Id eq {movimientoCuentaCuotum.Id}");
                    var firstItem = valueResult.Value.FirstOrDefault();
                    if (firstItem != null)
                    {
                        movimientoCuentaForIdValue = firstItem;
                    }
                }

            }
            catch (System.Exception ex)
            {
                NotificationService.Notify(new NotificationMessage(){ Severity = NotificationSeverity.Error, Summary = $"Error", Detail = $"Unable to load MovimientoCuentum" });
            }
        }
        protected async Task FormSubmit()
        {
            try
            {
                var result = await CMUService.UpdateMovimientoCuentaCuotum(id:Id, movimientoCuentaCuotum);
                if (result.StatusCode == System.Net.HttpStatusCode.PreconditionFailed)
                {
                     hasChanges = true;
                     canEdit = false;
                     return;
                }
                DialogService.Close(movimientoCuentaCuotum);
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

            movimientoCuentaCuotum = await CMUService.GetMovimientoCuentaCuotumById(id:Id);
        }
    }
}