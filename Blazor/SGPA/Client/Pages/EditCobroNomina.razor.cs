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
    public partial class EditCobroNomina
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
            cobroNomina = await CMUService.GetCobroNominaById(id:Id);
        }
        protected bool errorVisible;
        protected SGPA.Server.Models.CMU.CobroNomina cobroNomina;

        protected IEnumerable<SGPA.Server.Models.CMU.Cobro> cobrosForCobro;

        protected IEnumerable<SGPA.Server.Models.CMU.Colegiado> colegiadosForColegiado;

        protected IEnumerable<SGPA.Server.Models.CMU.Convenio> conveniosForConvenio;

        protected IEnumerable<SGPA.Server.Models.CMU.MovimientoCuentum> movimientoCuentaForMovimiento;

        protected IEnumerable<SGPA.Server.Models.CMU.XpObjectType> xpObjectTypesForObjectType;


        protected int cobrosForCobroCount;
        protected SGPA.Server.Models.CMU.Cobro cobrosForCobroValue;
        protected async Task cobrosForCobroLoadData(LoadDataArgs args)
        {
            try
            {
                var result = await CMUService.GetCobros(top: args.Top, skip: args.Skip, count:args.Top != null && args.Skip != null, filter: $"{args.Filter}", orderby: $"{args.OrderBy}");
                cobrosForCobro = result.Value.AsODataEnumerable();
                cobrosForCobroCount = result.Count;

                if (!object.Equals(cobroNomina.Cobro, null))
                {
                    var valueResult = await CMUService.GetCobros(filter: $"Id eq {cobroNomina.Cobro}");
                    var firstItem = valueResult.Value.FirstOrDefault();
                    if (firstItem != null)
                    {
                        cobrosForCobroValue = firstItem;
                    }
                }

            }
            catch (System.Exception ex)
            {
                NotificationService.Notify(new NotificationMessage(){ Severity = NotificationSeverity.Error, Summary = $"Error", Detail = $"Unable to load Cobro1" });
            }
        }

        protected int colegiadosForColegiadoCount;
        protected SGPA.Server.Models.CMU.Colegiado colegiadosForColegiadoValue;
        protected async Task colegiadosForColegiadoLoadData(LoadDataArgs args)
        {
            try
            {
                var result = await CMUService.GetColegiados(top: args.Top, skip: args.Skip, count:args.Top != null && args.Skip != null, filter: $"{args.Filter}", orderby: $"{args.OrderBy}");
                colegiadosForColegiado = result.Value.AsODataEnumerable();
                colegiadosForColegiadoCount = result.Count;

                if (!object.Equals(cobroNomina.Colegiado, null))
                {
                    var valueResult = await CMUService.GetColegiados(filter: $"Documento eq {cobroNomina.Colegiado}");
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

        protected int conveniosForConvenioCount;
        protected SGPA.Server.Models.CMU.Convenio conveniosForConvenioValue;
        protected async Task conveniosForConvenioLoadData(LoadDataArgs args)
        {
            try
            {
                var result = await CMUService.GetConvenios(top: args.Top, skip: args.Skip, count:args.Top != null && args.Skip != null, filter: $"{args.Filter}", orderby: $"{args.OrderBy}");
                conveniosForConvenio = result.Value.AsODataEnumerable();
                conveniosForConvenioCount = result.Count;

                if (!object.Equals(cobroNomina.Convenio, null))
                {
                    var valueResult = await CMUService.GetConvenios(filter: $"Id eq {cobroNomina.Convenio}");
                    var firstItem = valueResult.Value.FirstOrDefault();
                    if (firstItem != null)
                    {
                        conveniosForConvenioValue = firstItem;
                    }
                }

            }
            catch (System.Exception ex)
            {
                NotificationService.Notify(new NotificationMessage(){ Severity = NotificationSeverity.Error, Summary = $"Error", Detail = $"Unable to load Convenio1" });
            }
        }

        protected int movimientoCuentaForMovimientoCount;
        protected SGPA.Server.Models.CMU.MovimientoCuentum movimientoCuentaForMovimientoValue;
        protected async Task movimientoCuentaForMovimientoLoadData(LoadDataArgs args)
        {
            try
            {
                var result = await CMUService.GetMovimientoCuenta(top: args.Top, skip: args.Skip, count:args.Top != null && args.Skip != null, filter: $"{args.Filter}", orderby: $"{args.OrderBy}");
                movimientoCuentaForMovimiento = result.Value.AsODataEnumerable();
                movimientoCuentaForMovimientoCount = result.Count;

                if (!object.Equals(cobroNomina.Movimiento, null))
                {
                    var valueResult = await CMUService.GetMovimientoCuenta(filter: $"Id eq {cobroNomina.Movimiento}");
                    var firstItem = valueResult.Value.FirstOrDefault();
                    if (firstItem != null)
                    {
                        movimientoCuentaForMovimientoValue = firstItem;
                    }
                }

            }
            catch (System.Exception ex)
            {
                NotificationService.Notify(new NotificationMessage(){ Severity = NotificationSeverity.Error, Summary = $"Error", Detail = $"Unable to load MovimientoCuentum" });
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

                if (!object.Equals(cobroNomina.ObjectType, null))
                {
                    var valueResult = await CMUService.GetXpObjectTypes(filter: $"OID eq {cobroNomina.ObjectType}");
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
                var result = await CMUService.UpdateCobroNomina(id:Id, cobroNomina);
                if (result.StatusCode == System.Net.HttpStatusCode.PreconditionFailed)
                {
                     hasChanges = true;
                     canEdit = false;
                     return;
                }
                DialogService.Close(cobroNomina);
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

            cobroNomina = await CMUService.GetCobroNominaById(id:Id);
        }
    }
}