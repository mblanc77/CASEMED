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
    public partial class AddRecetum
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

        protected override async Task OnInitializedAsync()
        {
            recetum = new SgpaNew.Server.Models.Sgpa.Recetum();
        }
        protected bool errorVisible;
        protected SgpaNew.Server.Models.Sgpa.Recetum recetum;

        protected IEnumerable<SgpaNew.Server.Models.Sgpa.RecetaDistancium> recetaDistanciaForCodRecetaDistancia;

        protected IEnumerable<SgpaNew.Server.Models.Sgpa.Prestacion> prestacionsForPrestacionId;


        protected int recetaDistanciaForCodRecetaDistanciaCount;
        protected SgpaNew.Server.Models.Sgpa.RecetaDistancium recetaDistanciaForCodRecetaDistanciaValue;
        protected async Task recetaDistanciaForCodRecetaDistanciaLoadData(LoadDataArgs args)
        {
            try
            {
                var result = await SgpaService.GetRecetaDistancia(top: args.Top, skip: args.Skip, count:args.Top != null && args.Skip != null, filter: $"contains(CodRecetaDistancia, '{(!string.IsNullOrEmpty(args.Filter) ? args.Filter : "")}')", orderby: $"{args.OrderBy}");
                recetaDistanciaForCodRecetaDistancia = result.Value.AsODataEnumerable();
                recetaDistanciaForCodRecetaDistanciaCount = result.Count;

                if (!object.Equals(recetum.CodRecetaDistancia, null))
                {
                    var valueResult = await SgpaService.GetRecetaDistancia(filter: $"CodRecetaDistancia eq '{recetum.CodRecetaDistancia}'");
                    var firstItem = valueResult.Value.FirstOrDefault();
                    if (firstItem != null)
                    {
                        recetaDistanciaForCodRecetaDistanciaValue = firstItem;
                    }
                }

            }
            catch (System.Exception ex)
            {
                NotificationService.Notify(new NotificationMessage(){ Severity = NotificationSeverity.Error, Summary = $"Error", Detail = $"Unable to load RecetaDistancium" });
            }
        }

        protected int prestacionsForPrestacionIdCount;
        protected SgpaNew.Server.Models.Sgpa.Prestacion prestacionsForPrestacionIdValue;
        protected async Task prestacionsForPrestacionIdLoadData(LoadDataArgs args)
        {
            try
            {
                var result = await SgpaService.GetPrestacions(top: args.Top, skip: args.Skip, count:args.Top != null && args.Skip != null, filter: $"contains(Moneda, '{(!string.IsNullOrEmpty(args.Filter) ? args.Filter : "")}')", orderby: $"{args.OrderBy}");
                prestacionsForPrestacionId = result.Value.AsODataEnumerable();
                prestacionsForPrestacionIdCount = result.Count;

                if (!object.Equals(recetum.PrestacionId, null))
                {
                    var valueResult = await SgpaService.GetPrestacions(filter: $"PrestacionId eq {recetum.PrestacionId}");
                    var firstItem = valueResult.Value.FirstOrDefault();
                    if (firstItem != null)
                    {
                        prestacionsForPrestacionIdValue = firstItem;
                    }
                }

            }
            catch (System.Exception ex)
            {
                NotificationService.Notify(new NotificationMessage(){ Severity = NotificationSeverity.Error, Summary = $"Error", Detail = $"Unable to load Prestacion" });
            }
        }
        protected async Task FormSubmit()
        {
            try
            {
                var result = await SgpaService.CreateRecetum(recetum);
                DialogService.Close(recetum);
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
    }
}