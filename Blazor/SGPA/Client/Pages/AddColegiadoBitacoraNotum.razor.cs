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
    public partial class AddColegiadoBitacoraNotum
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
            colegiadoBitacoraNotum = new SGPA.Server.Models.CMU.ColegiadoBitacoraNotum();
        }
        protected bool errorVisible;
        protected SGPA.Server.Models.CMU.ColegiadoBitacoraNotum colegiadoBitacoraNotum;

        protected IEnumerable<SGPA.Server.Models.CMU.ColegiadoBitacora> colegiadoBitacorasForId;


        protected int colegiadoBitacorasForIdCount;
        protected SGPA.Server.Models.CMU.ColegiadoBitacora colegiadoBitacorasForIdValue;
        protected async Task colegiadoBitacorasForIdLoadData(LoadDataArgs args)
        {
            try
            {
                var result = await CMUService.GetColegiadoBitacoras(top: args.Top, skip: args.Skip, count:args.Top != null && args.Skip != null, filter: $"{args.Filter}", orderby: $"{args.OrderBy}");
                colegiadoBitacorasForId = result.Value.AsODataEnumerable();
                colegiadoBitacorasForIdCount = result.Count;

                if (!object.Equals(colegiadoBitacoraNotum.Id, null))
                {
                    var valueResult = await CMUService.GetColegiadoBitacoras(filter: $"Id eq {colegiadoBitacoraNotum.Id}");
                    var firstItem = valueResult.Value.FirstOrDefault();
                    if (firstItem != null)
                    {
                        colegiadoBitacorasForIdValue = firstItem;
                    }
                }

            }
            catch (System.Exception ex)
            {
                NotificationService.Notify(new NotificationMessage(){ Severity = NotificationSeverity.Error, Summary = $"Error", Detail = $"Unable to load ColegiadoBitacora" });
            }
        }
        protected async Task FormSubmit()
        {
            try
            {
                var result = await CMUService.CreateColegiadoBitacoraNotum(colegiadoBitacoraNotum);
                DialogService.Close(colegiadoBitacoraNotum);
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