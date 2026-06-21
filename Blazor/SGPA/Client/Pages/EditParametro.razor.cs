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
    public partial class EditParametro
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
            parametro = await CMUService.GetParametroById(id:Id);
        }
        protected bool errorVisible;
        protected SGPA.Server.Models.CMU.Parametro parametro;

        protected IEnumerable<SGPA.Server.Models.CMU.AgenteCobranza> agenteCobranzasForAgenteIMM;

        protected IEnumerable<SGPA.Server.Models.CMU.AgenteCobranzaDebito> agenteCobranzaDebitosForAgenteVisaNET;

        protected IEnumerable<SGPA.Server.Models.CMU.CategoriaColegiado> categoriaColegiadosForCategoriaColegiadoDefecto;

        protected IEnumerable<SGPA.Server.Models.CMU.CategoriaColegiado> categoriaColegiadosForCategoriaColegiadoMora;

        protected IEnumerable<SGPA.Server.Models.CMU.DeclaracionJuradaTipo> declaracionJuradaTiposForDeclaracionJuradaTipoPaternidad;

        protected IEnumerable<SGPA.Server.Models.CMU.DeclaracionJuradaTipo> declaracionJuradaTiposForDJ05;

        protected IEnumerable<SGPA.Server.Models.CMU.MovimientoTipo> movimientoTiposForMovimientoTipoCobroManual;

        protected IEnumerable<SGPA.Server.Models.CMU.MovimientoTipo> movimientoTiposForMovimientoTipoCuota;

        protected IEnumerable<SGPA.Server.Models.CMU.MovimientoTipo> movimientoTiposForMovimientoTipoDebito;

        protected IEnumerable<SGPA.Server.Models.CMU.MovimientoTipo> movimientoTiposForMovimientoTipoDeposito;


        protected int agenteCobranzasForAgenteIMMCount;
        protected SGPA.Server.Models.CMU.AgenteCobranza agenteCobranzasForAgenteIMMValue;
        protected async Task agenteCobranzasForAgenteIMMLoadData(LoadDataArgs args)
        {
            try
            {
                var result = await CMUService.GetAgenteCobranzas(top: args.Top, skip: args.Skip, count:args.Top != null && args.Skip != null, filter: $"{args.Filter}", orderby: $"{args.OrderBy}");
                agenteCobranzasForAgenteIMM = result.Value.AsODataEnumerable();
                agenteCobranzasForAgenteIMMCount = result.Count;

                if (!object.Equals(parametro.AgenteIMM, null))
                {
                    var valueResult = await CMUService.GetAgenteCobranzas(filter: $"Id eq {parametro.AgenteIMM}");
                    var firstItem = valueResult.Value.FirstOrDefault();
                    if (firstItem != null)
                    {
                        agenteCobranzasForAgenteIMMValue = firstItem;
                    }
                }

            }
            catch (System.Exception ex)
            {
                NotificationService.Notify(new NotificationMessage(){ Severity = NotificationSeverity.Error, Summary = $"Error", Detail = $"Unable to load AgenteCobranza" });
            }
        }

        protected int agenteCobranzaDebitosForAgenteVisaNETCount;
        protected SGPA.Server.Models.CMU.AgenteCobranzaDebito agenteCobranzaDebitosForAgenteVisaNETValue;
        protected async Task agenteCobranzaDebitosForAgenteVisaNETLoadData(LoadDataArgs args)
        {
            try
            {
                var result = await CMUService.GetAgenteCobranzaDebitos(top: args.Top, skip: args.Skip, count:args.Top != null && args.Skip != null, filter: $"{args.Filter}", orderby: $"{args.OrderBy}");
                agenteCobranzaDebitosForAgenteVisaNET = result.Value.AsODataEnumerable();
                agenteCobranzaDebitosForAgenteVisaNETCount = result.Count;

                if (!object.Equals(parametro.AgenteVisaNET, null))
                {
                    var valueResult = await CMUService.GetAgenteCobranzaDebitos(filter: $"Id eq {parametro.AgenteVisaNET}");
                    var firstItem = valueResult.Value.FirstOrDefault();
                    if (firstItem != null)
                    {
                        agenteCobranzaDebitosForAgenteVisaNETValue = firstItem;
                    }
                }

            }
            catch (System.Exception ex)
            {
                NotificationService.Notify(new NotificationMessage(){ Severity = NotificationSeverity.Error, Summary = $"Error", Detail = $"Unable to load AgenteCobranzaDebito" });
            }
        }

        protected int categoriaColegiadosForCategoriaColegiadoDefectoCount;
        protected SGPA.Server.Models.CMU.CategoriaColegiado categoriaColegiadosForCategoriaColegiadoDefectoValue;
        protected async Task categoriaColegiadosForCategoriaColegiadoDefectoLoadData(LoadDataArgs args)
        {
            try
            {
                var result = await CMUService.GetCategoriaColegiados(top: args.Top, skip: args.Skip, count:args.Top != null && args.Skip != null, filter: $"{args.Filter}", orderby: $"{args.OrderBy}");
                categoriaColegiadosForCategoriaColegiadoDefecto = result.Value.AsODataEnumerable();
                categoriaColegiadosForCategoriaColegiadoDefectoCount = result.Count;

                if (!object.Equals(parametro.CategoriaColegiadoDefecto, null))
                {
                    var valueResult = await CMUService.GetCategoriaColegiados(filter: $"Id eq {parametro.CategoriaColegiadoDefecto}");
                    var firstItem = valueResult.Value.FirstOrDefault();
                    if (firstItem != null)
                    {
                        categoriaColegiadosForCategoriaColegiadoDefectoValue = firstItem;
                    }
                }

            }
            catch (System.Exception ex)
            {
                NotificationService.Notify(new NotificationMessage(){ Severity = NotificationSeverity.Error, Summary = $"Error", Detail = $"Unable to load CategoriaColegiado" });
            }
        }

        protected int categoriaColegiadosForCategoriaColegiadoMoraCount;
        protected SGPA.Server.Models.CMU.CategoriaColegiado categoriaColegiadosForCategoriaColegiadoMoraValue;
        protected async Task categoriaColegiadosForCategoriaColegiadoMoraLoadData(LoadDataArgs args)
        {
            try
            {
                var result = await CMUService.GetCategoriaColegiados(top: args.Top, skip: args.Skip, count:args.Top != null && args.Skip != null, filter: $"{args.Filter}", orderby: $"{args.OrderBy}");
                categoriaColegiadosForCategoriaColegiadoMora = result.Value.AsODataEnumerable();
                categoriaColegiadosForCategoriaColegiadoMoraCount = result.Count;

                if (!object.Equals(parametro.CategoriaColegiadoMora, null))
                {
                    var valueResult = await CMUService.GetCategoriaColegiados(filter: $"Id eq {parametro.CategoriaColegiadoMora}");
                    var firstItem = valueResult.Value.FirstOrDefault();
                    if (firstItem != null)
                    {
                        categoriaColegiadosForCategoriaColegiadoMoraValue = firstItem;
                    }
                }

            }
            catch (System.Exception ex)
            {
                NotificationService.Notify(new NotificationMessage(){ Severity = NotificationSeverity.Error, Summary = $"Error", Detail = $"Unable to load CategoriaColegiado1" });
            }
        }

        protected int declaracionJuradaTiposForDeclaracionJuradaTipoPaternidadCount;
        protected SGPA.Server.Models.CMU.DeclaracionJuradaTipo declaracionJuradaTiposForDeclaracionJuradaTipoPaternidadValue;
        protected async Task declaracionJuradaTiposForDeclaracionJuradaTipoPaternidadLoadData(LoadDataArgs args)
        {
            try
            {
                var result = await CMUService.GetDeclaracionJuradaTipos(top: args.Top, skip: args.Skip, count:args.Top != null && args.Skip != null, filter: $"{args.Filter}", orderby: $"{args.OrderBy}");
                declaracionJuradaTiposForDeclaracionJuradaTipoPaternidad = result.Value.AsODataEnumerable();
                declaracionJuradaTiposForDeclaracionJuradaTipoPaternidadCount = result.Count;

                if (!object.Equals(parametro.DeclaracionJuradaTipoPaternidad, null))
                {
                    var valueResult = await CMUService.GetDeclaracionJuradaTipos(filter: $"Id eq {parametro.DeclaracionJuradaTipoPaternidad}");
                    var firstItem = valueResult.Value.FirstOrDefault();
                    if (firstItem != null)
                    {
                        declaracionJuradaTiposForDeclaracionJuradaTipoPaternidadValue = firstItem;
                    }
                }

            }
            catch (System.Exception ex)
            {
                NotificationService.Notify(new NotificationMessage(){ Severity = NotificationSeverity.Error, Summary = $"Error", Detail = $"Unable to load DeclaracionJuradaTipo" });
            }
        }

        protected int declaracionJuradaTiposForDJ05Count;
        protected SGPA.Server.Models.CMU.DeclaracionJuradaTipo declaracionJuradaTiposForDJ05Value;
        protected async Task declaracionJuradaTiposForDJ05LoadData(LoadDataArgs args)
        {
            try
            {
                var result = await CMUService.GetDeclaracionJuradaTipos(top: args.Top, skip: args.Skip, count:args.Top != null && args.Skip != null, filter: $"{args.Filter}", orderby: $"{args.OrderBy}");
                declaracionJuradaTiposForDJ05 = result.Value.AsODataEnumerable();
                declaracionJuradaTiposForDJ05Count = result.Count;

                if (!object.Equals(parametro.DJ05, null))
                {
                    var valueResult = await CMUService.GetDeclaracionJuradaTipos(filter: $"Id eq {parametro.DJ05}");
                    var firstItem = valueResult.Value.FirstOrDefault();
                    if (firstItem != null)
                    {
                        declaracionJuradaTiposForDJ05Value = firstItem;
                    }
                }

            }
            catch (System.Exception ex)
            {
                NotificationService.Notify(new NotificationMessage(){ Severity = NotificationSeverity.Error, Summary = $"Error", Detail = $"Unable to load DeclaracionJuradaTipo1" });
            }
        }

        protected int movimientoTiposForMovimientoTipoCobroManualCount;
        protected SGPA.Server.Models.CMU.MovimientoTipo movimientoTiposForMovimientoTipoCobroManualValue;
        protected async Task movimientoTiposForMovimientoTipoCobroManualLoadData(LoadDataArgs args)
        {
            try
            {
                var result = await CMUService.GetMovimientoTipos(top: args.Top, skip: args.Skip, count:args.Top != null && args.Skip != null, filter: $"{args.Filter}", orderby: $"{args.OrderBy}");
                movimientoTiposForMovimientoTipoCobroManual = result.Value.AsODataEnumerable();
                movimientoTiposForMovimientoTipoCobroManualCount = result.Count;

                if (!object.Equals(parametro.MovimientoTipoCobroManual, null))
                {
                    var valueResult = await CMUService.GetMovimientoTipos(filter: $"Id eq {parametro.MovimientoTipoCobroManual}");
                    var firstItem = valueResult.Value.FirstOrDefault();
                    if (firstItem != null)
                    {
                        movimientoTiposForMovimientoTipoCobroManualValue = firstItem;
                    }
                }

            }
            catch (System.Exception ex)
            {
                NotificationService.Notify(new NotificationMessage(){ Severity = NotificationSeverity.Error, Summary = $"Error", Detail = $"Unable to load MovimientoTipo" });
            }
        }

        protected int movimientoTiposForMovimientoTipoCuotaCount;
        protected SGPA.Server.Models.CMU.MovimientoTipo movimientoTiposForMovimientoTipoCuotaValue;
        protected async Task movimientoTiposForMovimientoTipoCuotaLoadData(LoadDataArgs args)
        {
            try
            {
                var result = await CMUService.GetMovimientoTipos(top: args.Top, skip: args.Skip, count:args.Top != null && args.Skip != null, filter: $"{args.Filter}", orderby: $"{args.OrderBy}");
                movimientoTiposForMovimientoTipoCuota = result.Value.AsODataEnumerable();
                movimientoTiposForMovimientoTipoCuotaCount = result.Count;

                if (!object.Equals(parametro.MovimientoTipoCuota, null))
                {
                    var valueResult = await CMUService.GetMovimientoTipos(filter: $"Id eq {parametro.MovimientoTipoCuota}");
                    var firstItem = valueResult.Value.FirstOrDefault();
                    if (firstItem != null)
                    {
                        movimientoTiposForMovimientoTipoCuotaValue = firstItem;
                    }
                }

            }
            catch (System.Exception ex)
            {
                NotificationService.Notify(new NotificationMessage(){ Severity = NotificationSeverity.Error, Summary = $"Error", Detail = $"Unable to load MovimientoTipo1" });
            }
        }

        protected int movimientoTiposForMovimientoTipoDebitoCount;
        protected SGPA.Server.Models.CMU.MovimientoTipo movimientoTiposForMovimientoTipoDebitoValue;
        protected async Task movimientoTiposForMovimientoTipoDebitoLoadData(LoadDataArgs args)
        {
            try
            {
                var result = await CMUService.GetMovimientoTipos(top: args.Top, skip: args.Skip, count:args.Top != null && args.Skip != null, filter: $"{args.Filter}", orderby: $"{args.OrderBy}");
                movimientoTiposForMovimientoTipoDebito = result.Value.AsODataEnumerable();
                movimientoTiposForMovimientoTipoDebitoCount = result.Count;

                if (!object.Equals(parametro.MovimientoTipoDebito, null))
                {
                    var valueResult = await CMUService.GetMovimientoTipos(filter: $"Id eq {parametro.MovimientoTipoDebito}");
                    var firstItem = valueResult.Value.FirstOrDefault();
                    if (firstItem != null)
                    {
                        movimientoTiposForMovimientoTipoDebitoValue = firstItem;
                    }
                }

            }
            catch (System.Exception ex)
            {
                NotificationService.Notify(new NotificationMessage(){ Severity = NotificationSeverity.Error, Summary = $"Error", Detail = $"Unable to load MovimientoTipo2" });
            }
        }

        protected int movimientoTiposForMovimientoTipoDepositoCount;
        protected SGPA.Server.Models.CMU.MovimientoTipo movimientoTiposForMovimientoTipoDepositoValue;
        protected async Task movimientoTiposForMovimientoTipoDepositoLoadData(LoadDataArgs args)
        {
            try
            {
                var result = await CMUService.GetMovimientoTipos(top: args.Top, skip: args.Skip, count:args.Top != null && args.Skip != null, filter: $"{args.Filter}", orderby: $"{args.OrderBy}");
                movimientoTiposForMovimientoTipoDeposito = result.Value.AsODataEnumerable();
                movimientoTiposForMovimientoTipoDepositoCount = result.Count;

                if (!object.Equals(parametro.MovimientoTipoDeposito, null))
                {
                    var valueResult = await CMUService.GetMovimientoTipos(filter: $"Id eq {parametro.MovimientoTipoDeposito}");
                    var firstItem = valueResult.Value.FirstOrDefault();
                    if (firstItem != null)
                    {
                        movimientoTiposForMovimientoTipoDepositoValue = firstItem;
                    }
                }

            }
            catch (System.Exception ex)
            {
                NotificationService.Notify(new NotificationMessage(){ Severity = NotificationSeverity.Error, Summary = $"Error", Detail = $"Unable to load MovimientoTipo3" });
            }
        }
        protected async Task FormSubmit()
        {
            try
            {
                var result = await CMUService.UpdateParametro(id:Id, parametro);
                if (result.StatusCode == System.Net.HttpStatusCode.PreconditionFailed)
                {
                     hasChanges = true;
                     canEdit = false;
                     return;
                }
                DialogService.Close(parametro);
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

            parametro = await CMUService.GetParametroById(id:Id);
        }
    }
}