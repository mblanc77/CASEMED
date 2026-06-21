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
    public partial class EditAfeccionGrupo
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
        public int CodAfeccionGrupo { get; set; }

        protected override async Task OnInitializedAsync()
        {
            afeccionGrupo = await SgpaService.GetAfeccionGrupoByCodAfeccionGrupo(codAfeccionGrupo:CodAfeccionGrupo);
        }
        protected bool errorVisible;
        protected SgpaNew.Server.Models.Sgpa.AfeccionGrupo afeccionGrupo;

        protected IEnumerable<SgpaNew.Server.Models.Sgpa.Patologium> patologiaForCodPatologia;


        protected int patologiaForCodPatologiaCount;
        protected SgpaNew.Server.Models.Sgpa.Patologium patologiaForCodPatologiaValue;
        protected async Task patologiaForCodPatologiaLoadData(LoadDataArgs args)
        {
            try
            {
                var result = await SgpaService.GetPatologia(top: args.Top, skip: args.Skip, count:args.Top != null && args.Skip != null, filter: $"contains(Descrip, '{(!string.IsNullOrEmpty(args.Filter) ? args.Filter : "")}')", orderby: $"{args.OrderBy}");
                patologiaForCodPatologia = result.Value.AsODataEnumerable();
                patologiaForCodPatologiaCount = result.Count;

                if (!object.Equals(afeccionGrupo.CodPatologia, null))
                {
                    var valueResult = await SgpaService.GetPatologia(filter: $"CodPatologia eq {afeccionGrupo.CodPatologia}");
                    var firstItem = valueResult.Value.FirstOrDefault();
                    if (firstItem != null)
                    {
                        patologiaForCodPatologiaValue = firstItem;
                    }
                }

            }
            catch (System.Exception ex)
            {
                NotificationService.Notify(new NotificationMessage(){ Severity = NotificationSeverity.Error, Summary = $"Error", Detail = $"Unable to load Patologium" });
            }
        }
        protected async Task FormSubmit()
        {
            try
            {
                var result = await SgpaService.UpdateAfeccionGrupo(codAfeccionGrupo:CodAfeccionGrupo, afeccionGrupo);
                if (result.StatusCode == System.Net.HttpStatusCode.PreconditionFailed)
                {
                     hasChanges = true;
                     canEdit = false;
                     return;
                }
                DialogService.Close(afeccionGrupo);
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

            afeccionGrupo = await SgpaService.GetAfeccionGrupoByCodAfeccionGrupo(codAfeccionGrupo:CodAfeccionGrupo);
        }
    }
}