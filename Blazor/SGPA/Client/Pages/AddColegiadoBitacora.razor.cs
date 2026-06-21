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
    public partial class AddColegiadoBitacora
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
            colegiadoBitacora = new SGPA.Server.Models.CMU.ColegiadoBitacora();
        }
        protected bool errorVisible;
        protected SGPA.Server.Models.CMU.ColegiadoBitacora colegiadoBitacora;

        protected IEnumerable<SGPA.Server.Models.CMU.Colegiado> colegiadosForColegiado;

        protected IEnumerable<SGPA.Server.Models.CMU.XpObjectType> xpObjectTypesForObjectType;


        protected int colegiadosForColegiadoCount;
        protected SGPA.Server.Models.CMU.Colegiado colegiadosForColegiadoValue;
        protected async Task colegiadosForColegiadoLoadData(LoadDataArgs args)
        {
            try
            {
                var result = await CMUService.GetColegiados(top: args.Top, skip: args.Skip, count:args.Top != null && args.Skip != null, filter: $"{args.Filter}", orderby: $"{args.OrderBy}");
                colegiadosForColegiado = result.Value.AsODataEnumerable();
                colegiadosForColegiadoCount = result.Count;

                if (!object.Equals(colegiadoBitacora.Colegiado, null))
                {
                    var valueResult = await CMUService.GetColegiados(filter: $"Documento eq {colegiadoBitacora.Colegiado}");
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

        protected int xpObjectTypesForObjectTypeCount;
        protected SGPA.Server.Models.CMU.XpObjectType xpObjectTypesForObjectTypeValue;
        protected async Task xpObjectTypesForObjectTypeLoadData(LoadDataArgs args)
        {
            try
            {
                var result = await CMUService.GetXpObjectTypes(top: args.Top, skip: args.Skip, count:args.Top != null && args.Skip != null, filter: $"{args.Filter}", orderby: $"{args.OrderBy}");
                xpObjectTypesForObjectType = result.Value.AsODataEnumerable();
                xpObjectTypesForObjectTypeCount = result.Count;

                if (!object.Equals(colegiadoBitacora.ObjectType, null))
                {
                    var valueResult = await CMUService.GetXpObjectTypes(filter: $"OID eq {colegiadoBitacora.ObjectType}");
                    var firstItem = valueResult.Value.FirstOrDefault();
                    if (firstItem != null)
                    {
                        xpObjectTypesForObjectTypeValue = firstItem;
                    }
                }

            }
            catch (System.Exception ex)
            {
                NotificationService.Notify(new NotificationMessage(){ Severity = NotificationSeverity.Error, Summary = $"Error", Detail = $"Unable to load XpobjectType" });
            }
        }
        protected async Task FormSubmit()
        {
            try
            {
                var result = await CMUService.CreateColegiadoBitacora(colegiadoBitacora);
                DialogService.Close(colegiadoBitacora);
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