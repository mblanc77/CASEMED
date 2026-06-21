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
    public partial class EditAjusteDetalle
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
            ajusteDetalle = await CMUService.GetAjusteDetalleById(id:Id);
        }
        protected bool errorVisible;
        protected SGPA.Server.Models.CMU.AjusteDetalle ajusteDetalle;

        protected IEnumerable<SGPA.Server.Models.CMU.AjusteRetroactivo> ajusteRetroactivosForAjuste;


        protected int ajusteRetroactivosForAjusteCount;
        protected SGPA.Server.Models.CMU.AjusteRetroactivo ajusteRetroactivosForAjusteValue;
        protected async Task ajusteRetroactivosForAjusteLoadData(LoadDataArgs args)
        {
            try
            {
                var result = await CMUService.GetAjusteRetroactivos(top: args.Top, skip: args.Skip, count:args.Top != null && args.Skip != null, filter: $"{args.Filter}", orderby: $"{args.OrderBy}");
                ajusteRetroactivosForAjuste = result.Value.AsODataEnumerable();
                ajusteRetroactivosForAjusteCount = result.Count;

                if (!object.Equals(ajusteDetalle.Ajuste, null))
                {
                    var valueResult = await CMUService.GetAjusteRetroactivos(filter: $"Id eq {ajusteDetalle.Ajuste}");
                    var firstItem = valueResult.Value.FirstOrDefault();
                    if (firstItem != null)
                    {
                        ajusteRetroactivosForAjusteValue = firstItem;
                    }
                }

            }
            catch (System.Exception ex)
            {
                NotificationService.Notify(new NotificationMessage(){ Severity = NotificationSeverity.Error, Summary = $"Error", Detail = $"Unable to load AjusteRetroactivo" });
            }
        }
        protected async Task FormSubmit()
        {
            try
            {
                var result = await CMUService.UpdateAjusteDetalle(id:Id, ajusteDetalle);
                if (result.StatusCode == System.Net.HttpStatusCode.PreconditionFailed)
                {
                     hasChanges = true;
                     canEdit = false;
                     return;
                }
                DialogService.Close(ajusteDetalle);
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

            ajusteDetalle = await CMUService.GetAjusteDetalleById(id:Id);
        }
    }
}