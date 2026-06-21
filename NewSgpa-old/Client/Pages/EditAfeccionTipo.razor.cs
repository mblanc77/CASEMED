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
    public partial class EditAfeccionTipo
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
        public short CodAfeccionTipo { get; set; }

        protected override async Task OnInitializedAsync()
        {
            afeccionTipo = await SgpaService.GetAfeccionTipoByCodAfeccionTipo(codAfeccionTipo:CodAfeccionTipo);
        }
        protected bool errorVisible;
        protected SgpaNew.Server.Models.Sgpa.AfeccionTipo afeccionTipo;

        protected IEnumerable<SgpaNew.Server.Models.Sgpa.AfeccionGrupo> afeccionGruposForCodAfeccionGrupo;


        protected int afeccionGruposForCodAfeccionGrupoCount;
        protected SgpaNew.Server.Models.Sgpa.AfeccionGrupo afeccionGruposForCodAfeccionGrupoValue;
        protected async Task afeccionGruposForCodAfeccionGrupoLoadData(LoadDataArgs args)
        {
            try
            {
                var result = await SgpaService.GetAfeccionGrupos(top: args.Top, skip: args.Skip, count:args.Top != null && args.Skip != null, filter: $"contains(Descrip, '{(!string.IsNullOrEmpty(args.Filter) ? args.Filter : "")}')", orderby: $"{args.OrderBy}");
                afeccionGruposForCodAfeccionGrupo = result.Value.AsODataEnumerable();
                afeccionGruposForCodAfeccionGrupoCount = result.Count;

                if (!object.Equals(afeccionTipo.CodAfeccionGrupo, null))
                {
                    var valueResult = await SgpaService.GetAfeccionGrupos(filter: $"CodAfeccionGrupo eq {afeccionTipo.CodAfeccionGrupo}");
                    var firstItem = valueResult.Value.FirstOrDefault();
                    if (firstItem != null)
                    {
                        afeccionGruposForCodAfeccionGrupoValue = firstItem;
                    }
                }

            }
            catch (System.Exception ex)
            {
                NotificationService.Notify(new NotificationMessage(){ Severity = NotificationSeverity.Error, Summary = $"Error", Detail = $"Unable to load AfeccionGrupo" });
            }
        }
        protected async Task FormSubmit()
        {
            try
            {
                var result = await SgpaService.UpdateAfeccionTipo(codAfeccionTipo:CodAfeccionTipo, afeccionTipo);
                if (result.StatusCode == System.Net.HttpStatusCode.PreconditionFailed)
                {
                     hasChanges = true;
                     canEdit = false;
                     return;
                }
                DialogService.Close(afeccionTipo);
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

            afeccionTipo = await SgpaService.GetAfeccionTipoByCodAfeccionTipo(codAfeccionTipo:CodAfeccionTipo);
        }
    }
}