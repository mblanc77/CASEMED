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
    public partial class AddColegiadoBitacoraEMailRecepcion
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
            colegiadoBitacoraEMailRecepcion = new SGPA.Server.Models.CMU.ColegiadoBitacoraEMailRecepcion();
        }
        protected bool errorVisible;
        protected SGPA.Server.Models.CMU.ColegiadoBitacoraEMailRecepcion colegiadoBitacoraEMailRecepcion;

        protected IEnumerable<SGPA.Server.Models.CMU.ColegiadoBitacoraNotum> colegiadoBitacoraNotaForId;


        protected int colegiadoBitacoraNotaForIdCount;
        protected SGPA.Server.Models.CMU.ColegiadoBitacoraNotum colegiadoBitacoraNotaForIdValue;
        protected async Task colegiadoBitacoraNotaForIdLoadData(LoadDataArgs args)
        {
            try
            {
                var result = await CMUService.GetColegiadoBitacoraNota(top: args.Top, skip: args.Skip, count:args.Top != null && args.Skip != null, filter: $"{args.Filter}", orderby: $"{args.OrderBy}");
                colegiadoBitacoraNotaForId = result.Value.AsODataEnumerable();
                colegiadoBitacoraNotaForIdCount = result.Count;

                if (!object.Equals(colegiadoBitacoraEMailRecepcion.Id, null))
                {
                    var valueResult = await CMUService.GetColegiadoBitacoraNota(filter: $"Id eq {colegiadoBitacoraEMailRecepcion.Id}");
                    var firstItem = valueResult.Value.FirstOrDefault();
                    if (firstItem != null)
                    {
                        colegiadoBitacoraNotaForIdValue = firstItem;
                    }
                }

            }
            catch (System.Exception ex)
            {
                NotificationService.Notify(new NotificationMessage(){ Severity = NotificationSeverity.Error, Summary = $"Error", Detail = $"Unable to load ColegiadoBitacoraNotum" });
            }
        }
        protected async Task FormSubmit()
        {
            try
            {
                var result = await CMUService.CreateColegiadoBitacoraEMailRecepcion(colegiadoBitacoraEMailRecepcion);
                DialogService.Close(colegiadoBitacoraEMailRecepcion);
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