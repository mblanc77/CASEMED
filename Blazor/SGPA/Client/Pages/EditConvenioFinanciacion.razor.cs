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
    public partial class EditConvenioFinanciacion
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
            convenioFinanciacion = await CMUService.GetConvenioFinanciacionById(id:Id);
        }
        protected bool errorVisible;
        protected SGPA.Server.Models.CMU.ConvenioFinanciacion convenioFinanciacion;

        protected IEnumerable<SGPA.Server.Models.CMU.Convenio> conveniosForId;


        protected int conveniosForIdCount;
        protected SGPA.Server.Models.CMU.Convenio conveniosForIdValue;
        protected async Task conveniosForIdLoadData(LoadDataArgs args)
        {
            try
            {
                var result = await CMUService.GetConvenios(top: args.Top, skip: args.Skip, count:args.Top != null && args.Skip != null, filter: $"{args.Filter}", orderby: $"{args.OrderBy}");
                conveniosForId = result.Value.AsODataEnumerable();
                conveniosForIdCount = result.Count;

                if (!object.Equals(convenioFinanciacion.Id, null))
                {
                    var valueResult = await CMUService.GetConvenios(filter: $"Id eq {convenioFinanciacion.Id}");
                    var firstItem = valueResult.Value.FirstOrDefault();
                    if (firstItem != null)
                    {
                        conveniosForIdValue = firstItem;
                    }
                }

            }
            catch (System.Exception ex)
            {
                NotificationService.Notify(new NotificationMessage(){ Severity = NotificationSeverity.Error, Summary = $"Error", Detail = $"Unable to load Convenio" });
            }
        }
        protected async Task FormSubmit()
        {
            try
            {
                var result = await CMUService.UpdateConvenioFinanciacion(id:Id, convenioFinanciacion);
                if (result.StatusCode == System.Net.HttpStatusCode.PreconditionFailed)
                {
                     hasChanges = true;
                     canEdit = false;
                     return;
                }
                DialogService.Close(convenioFinanciacion);
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

            convenioFinanciacion = await CMUService.GetConvenioFinanciacionById(id:Id);
        }
    }
}