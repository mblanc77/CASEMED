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
    public partial class AddMovimientoCuentum
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
            movimientoCuentum = new SGPA.Server.Models.CMU.MovimientoCuentum();
        }
        protected bool errorVisible;
        protected SGPA.Server.Models.CMU.MovimientoCuentum movimientoCuentum;

        protected IEnumerable<SGPA.Server.Models.CMU.AjusteRetroactivo> ajusteRetroactivosForAjuste;

        protected IEnumerable<SGPA.Server.Models.CMU.Colegiado> colegiadosForColegiado;

        protected IEnumerable<SGPA.Server.Models.CMU.MovimientoCuentum> movimientoCuentaForMovimientoReferencia;

        protected IEnumerable<SGPA.Server.Models.CMU.MovimientoTipo> movimientoTiposForMovimientoTipo;

        protected IEnumerable<SGPA.Server.Models.CMU.XpObjectType> xpObjectTypesForObjectType;

        protected IEnumerable<SGPA.Server.Models.CMU.OrigenMovimiento> origenMovimientosForOrigen;


        protected int ajusteRetroactivosForAjusteCount;
        protected SGPA.Server.Models.CMU.AjusteRetroactivo ajusteRetroactivosForAjusteValue;
        protected async Task ajusteRetroactivosForAjusteLoadData(LoadDataArgs args)
        {
            try
            {
                var result = await CMUService.GetAjusteRetroactivos(top: args.Top, skip: args.Skip, count:args.Top != null && args.Skip != null, filter: $"{args.Filter}", orderby: $"{args.OrderBy}");
                ajusteRetroactivosForAjuste = result.Value.AsODataEnumerable();
                ajusteRetroactivosForAjusteCount = result.Count;

                if (!object.Equals(movimientoCuentum.Ajuste, null))
                {
                    var valueResult = await CMUService.GetAjusteRetroactivos(filter: $"Id eq {movimientoCuentum.Ajuste}");
                    var firstItem = valueResult.Value.FirstOrDefault();
                    if (firstItem != null)
                    {
                        ajusteRetroactivosForAjusteValue = firstItem;
                    }
                }

            }
            catch (System.Exception ex)
            {
                NotificationService.Notify(new NotificationMessage(){ Severity = NotificationSeverity.Error, Summary = $"Error", Detail = $"Unable to load AjusteRetroactivo" });
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

                if (!object.Equals(movimientoCuentum.Colegiado, null))
                {
                    var valueResult = await CMUService.GetColegiados(filter: $"Documento eq {movimientoCuentum.Colegiado}");
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

        protected int movimientoCuentaForMovimientoReferenciaCount;
        protected SGPA.Server.Models.CMU.MovimientoCuentum movimientoCuentaForMovimientoReferenciaValue;
        protected async Task movimientoCuentaForMovimientoReferenciaLoadData(LoadDataArgs args)
        {
            try
            {
                var result = await CMUService.GetMovimientoCuenta(top: args.Top, skip: args.Skip, count:args.Top != null && args.Skip != null, filter: $"{args.Filter}", orderby: $"{args.OrderBy}");
                movimientoCuentaForMovimientoReferencia = result.Value.AsODataEnumerable();
                movimientoCuentaForMovimientoReferenciaCount = result.Count;

                if (!object.Equals(movimientoCuentum.MovimientoReferencia, null))
                {
                    var valueResult = await CMUService.GetMovimientoCuenta(filter: $"Id eq {movimientoCuentum.MovimientoReferencia}");
                    var firstItem = valueResult.Value.FirstOrDefault();
                    if (firstItem != null)
                    {
                        movimientoCuentaForMovimientoReferenciaValue = firstItem;
                    }
                }

            }
            catch (System.Exception ex)
            {
                NotificationService.Notify(new NotificationMessage(){ Severity = NotificationSeverity.Error, Summary = $"Error", Detail = $"Unable to load MovimientoCuentum1" });
            }
        }

        protected int movimientoTiposForMovimientoTipoCount;
        protected SGPA.Server.Models.CMU.MovimientoTipo movimientoTiposForMovimientoTipoValue;
        protected async Task movimientoTiposForMovimientoTipoLoadData(LoadDataArgs args)
        {
            try
            {
                var result = await CMUService.GetMovimientoTipos(top: args.Top, skip: args.Skip, count:args.Top != null && args.Skip != null, filter: $"{args.Filter}", orderby: $"{args.OrderBy}");
                movimientoTiposForMovimientoTipo = result.Value.AsODataEnumerable();
                movimientoTiposForMovimientoTipoCount = result.Count;

                if (!object.Equals(movimientoCuentum.MovimientoTipo, null))
                {
                    var valueResult = await CMUService.GetMovimientoTipos(filter: $"Id eq {movimientoCuentum.MovimientoTipo}");
                    var firstItem = valueResult.Value.FirstOrDefault();
                    if (firstItem != null)
                    {
                        movimientoTiposForMovimientoTipoValue = firstItem;
                    }
                }

            }
            catch (System.Exception ex)
            {
                NotificationService.Notify(new NotificationMessage(){ Severity = NotificationSeverity.Error, Summary = $"Error", Detail = $"Unable to load MovimientoTipo1" });
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

                if (!object.Equals(movimientoCuentum.ObjectType, null))
                {
                    var valueResult = await CMUService.GetXpObjectTypes(filter: $"OID eq {movimientoCuentum.ObjectType}");
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

        protected int origenMovimientosForOrigenCount;
        protected SGPA.Server.Models.CMU.OrigenMovimiento origenMovimientosForOrigenValue;
        protected async Task origenMovimientosForOrigenLoadData(LoadDataArgs args)
        {
            try
            {
                var result = await CMUService.GetOrigenMovimientos(top: args.Top, skip: args.Skip, count:args.Top != null && args.Skip != null, filter: $"{args.Filter}", orderby: $"{args.OrderBy}");
                origenMovimientosForOrigen = result.Value.AsODataEnumerable();
                origenMovimientosForOrigenCount = result.Count;

                if (!object.Equals(movimientoCuentum.Origen, null))
                {
                    var valueResult = await CMUService.GetOrigenMovimientos(filter: $"Id eq {movimientoCuentum.Origen}");
                    var firstItem = valueResult.Value.FirstOrDefault();
                    if (firstItem != null)
                    {
                        origenMovimientosForOrigenValue = firstItem;
                    }
                }

            }
            catch (System.Exception ex)
            {
                NotificationService.Notify(new NotificationMessage(){ Severity = NotificationSeverity.Error, Summary = $"Error", Detail = $"Unable to load OrigenMovimiento" });
            }
        }
        protected async Task FormSubmit()
        {
            try
            {
                var result = await CMUService.CreateMovimientoCuentum(movimientoCuentum);
                DialogService.Close(movimientoCuentum);
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