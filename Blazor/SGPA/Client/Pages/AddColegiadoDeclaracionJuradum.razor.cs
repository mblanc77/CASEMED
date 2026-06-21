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
    public partial class AddColegiadoDeclaracionJuradum
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
            colegiadoDeclaracionJuradum = new SGPA.Server.Models.CMU.ColegiadoDeclaracionJuradum();
        }
        protected bool errorVisible;
        protected SGPA.Server.Models.CMU.ColegiadoDeclaracionJuradum colegiadoDeclaracionJuradum;

        protected IEnumerable<SGPA.Server.Models.CMU.Colegiado> colegiadosForColegiado;

        protected IEnumerable<SGPA.Server.Models.CMU.DjInactividadMotivo> djInactividadMotivosForMotivoInactividad;

        protected IEnumerable<SGPA.Server.Models.CMU.XpObjectType> xpObjectTypesForObjectType;

        protected IEnumerable<SGPA.Server.Models.CMU.DeclaracionJuradaTipo> declaracionJuradaTiposForTipo;


        protected int colegiadosForColegiadoCount;
        protected SGPA.Server.Models.CMU.Colegiado colegiadosForColegiadoValue;
        protected async Task colegiadosForColegiadoLoadData(LoadDataArgs args)
        {
            try
            {
                var result = await CMUService.GetColegiados(top: args.Top, skip: args.Skip, count:args.Top != null && args.Skip != null, filter: $"{args.Filter}", orderby: $"{args.OrderBy}");
                colegiadosForColegiado = result.Value.AsODataEnumerable();
                colegiadosForColegiadoCount = result.Count;

                if (!object.Equals(colegiadoDeclaracionJuradum.Colegiado, null))
                {
                    var valueResult = await CMUService.GetColegiados(filter: $"Documento eq {colegiadoDeclaracionJuradum.Colegiado}");
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

        protected int djInactividadMotivosForMotivoInactividadCount;
        protected SGPA.Server.Models.CMU.DjInactividadMotivo djInactividadMotivosForMotivoInactividadValue;
        protected async Task djInactividadMotivosForMotivoInactividadLoadData(LoadDataArgs args)
        {
            try
            {
                var result = await CMUService.GetDjInactividadMotivos(top: args.Top, skip: args.Skip, count:args.Top != null && args.Skip != null, filter: $"{args.Filter}", orderby: $"{args.OrderBy}");
                djInactividadMotivosForMotivoInactividad = result.Value.AsODataEnumerable();
                djInactividadMotivosForMotivoInactividadCount = result.Count;

                if (!object.Equals(colegiadoDeclaracionJuradum.MotivoInactividad, null))
                {
                    var valueResult = await CMUService.GetDjInactividadMotivos(filter: $"Id eq {colegiadoDeclaracionJuradum.MotivoInactividad}");
                    var firstItem = valueResult.Value.FirstOrDefault();
                    if (firstItem != null)
                    {
                        djInactividadMotivosForMotivoInactividadValue = firstItem;
                    }
                }

            }
            catch (System.Exception ex)
            {
                NotificationService.Notify(new NotificationMessage(){ Severity = NotificationSeverity.Error, Summary = $"Error", Detail = $"Unable to load DjinactividadMotivo" });
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

                if (!object.Equals(colegiadoDeclaracionJuradum.ObjectType, null))
                {
                    var valueResult = await CMUService.GetXpObjectTypes(filter: $"OID eq {colegiadoDeclaracionJuradum.ObjectType}");
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

        protected int declaracionJuradaTiposForTipoCount;
        protected SGPA.Server.Models.CMU.DeclaracionJuradaTipo declaracionJuradaTiposForTipoValue;
        protected async Task declaracionJuradaTiposForTipoLoadData(LoadDataArgs args)
        {
            try
            {
                var result = await CMUService.GetDeclaracionJuradaTipos(top: args.Top, skip: args.Skip, count:args.Top != null && args.Skip != null, filter: $"{args.Filter}", orderby: $"{args.OrderBy}");
                declaracionJuradaTiposForTipo = result.Value.AsODataEnumerable();
                declaracionJuradaTiposForTipoCount = result.Count;

                if (!object.Equals(colegiadoDeclaracionJuradum.Tipo, null))
                {
                    var valueResult = await CMUService.GetDeclaracionJuradaTipos(filter: $"Id eq {colegiadoDeclaracionJuradum.Tipo}");
                    var firstItem = valueResult.Value.FirstOrDefault();
                    if (firstItem != null)
                    {
                        declaracionJuradaTiposForTipoValue = firstItem;
                    }
                }

            }
            catch (System.Exception ex)
            {
                NotificationService.Notify(new NotificationMessage(){ Severity = NotificationSeverity.Error, Summary = $"Error", Detail = $"Unable to load DeclaracionJuradaTipo" });
            }
        }
        protected async Task FormSubmit()
        {
            try
            {
                var result = await CMUService.CreateColegiadoDeclaracionJuradum(colegiadoDeclaracionJuradum);
                DialogService.Close(colegiadoDeclaracionJuradum);
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