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
    public partial class EditAgenteCobranza
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
            agenteCobranza = await CMUService.GetAgenteCobranzaById(id:Id);
        }
        protected bool errorVisible;
        protected SGPA.Server.Models.CMU.AgenteCobranza agenteCobranza;

        protected IEnumerable<SGPA.Server.Models.CMU.AgenteCobranzaTipo> agenteCobranzaTiposForAgenteTipo;

        protected IEnumerable<SGPA.Server.Models.CMU.CuentaBancarium> cuentaBancariaForCuentaBancaria;

        protected IEnumerable<SGPA.Server.Models.CMU.Departamento> departamentosForDepartamento;

        protected IEnumerable<SGPA.Server.Models.CMU.AgenteGrupo> agenteGruposForGrupo;

        protected IEnumerable<SGPA.Server.Models.CMU.OrigenMovimiento> origenMovimientosForId;

        protected IEnumerable<SGPA.Server.Models.CMU.Region> regionsForRegion;


        protected int agenteCobranzaTiposForAgenteTipoCount;
        protected SGPA.Server.Models.CMU.AgenteCobranzaTipo agenteCobranzaTiposForAgenteTipoValue;
        protected async Task agenteCobranzaTiposForAgenteTipoLoadData(LoadDataArgs args)
        {
            try
            {
                var result = await CMUService.GetAgenteCobranzaTipos(top: args.Top, skip: args.Skip, count:args.Top != null && args.Skip != null, filter: $"{args.Filter}", orderby: $"{args.OrderBy}");
                agenteCobranzaTiposForAgenteTipo = result.Value.AsODataEnumerable();
                agenteCobranzaTiposForAgenteTipoCount = result.Count;

                if (!object.Equals(agenteCobranza.AgenteTipo, null))
                {
                    var valueResult = await CMUService.GetAgenteCobranzaTipos(filter: $"Id eq {agenteCobranza.AgenteTipo}");
                    var firstItem = valueResult.Value.FirstOrDefault();
                    if (firstItem != null)
                    {
                        agenteCobranzaTiposForAgenteTipoValue = firstItem;
                    }
                }

            }
            catch (System.Exception ex)
            {
                NotificationService.Notify(new NotificationMessage(){ Severity = NotificationSeverity.Error, Summary = $"Error", Detail = $"Unable to load AgenteCobranzaTipo" });
            }
        }

        protected int cuentaBancariaForCuentaBancariaCount;
        protected SGPA.Server.Models.CMU.CuentaBancarium cuentaBancariaForCuentaBancariaValue;
        protected async Task cuentaBancariaForCuentaBancariaLoadData(LoadDataArgs args)
        {
            try
            {
                var result = await CMUService.GetCuentaBancaria(top: args.Top, skip: args.Skip, count:args.Top != null && args.Skip != null, filter: $"{args.Filter}", orderby: $"{args.OrderBy}");
                cuentaBancariaForCuentaBancaria = result.Value.AsODataEnumerable();
                cuentaBancariaForCuentaBancariaCount = result.Count;

                if (!object.Equals(agenteCobranza.CuentaBancaria, null))
                {
                    var valueResult = await CMUService.GetCuentaBancaria(filter: $"Id eq {agenteCobranza.CuentaBancaria}");
                    var firstItem = valueResult.Value.FirstOrDefault();
                    if (firstItem != null)
                    {
                        cuentaBancariaForCuentaBancariaValue = firstItem;
                    }
                }

            }
            catch (System.Exception ex)
            {
                NotificationService.Notify(new NotificationMessage(){ Severity = NotificationSeverity.Error, Summary = $"Error", Detail = $"Unable to load CuentaBancarium" });
            }
        }

        protected int departamentosForDepartamentoCount;
        protected SGPA.Server.Models.CMU.Departamento departamentosForDepartamentoValue;
        protected async Task departamentosForDepartamentoLoadData(LoadDataArgs args)
        {
            try
            {
                var result = await CMUService.GetDepartamentos(top: args.Top, skip: args.Skip, count:args.Top != null && args.Skip != null, filter: $"{args.Filter}", orderby: $"{args.OrderBy}");
                departamentosForDepartamento = result.Value.AsODataEnumerable();
                departamentosForDepartamentoCount = result.Count;

                if (!object.Equals(agenteCobranza.Departamento, null))
                {
                    var valueResult = await CMUService.GetDepartamentos(filter: $"Id eq {agenteCobranza.Departamento}");
                    var firstItem = valueResult.Value.FirstOrDefault();
                    if (firstItem != null)
                    {
                        departamentosForDepartamentoValue = firstItem;
                    }
                }

            }
            catch (System.Exception ex)
            {
                NotificationService.Notify(new NotificationMessage(){ Severity = NotificationSeverity.Error, Summary = $"Error", Detail = $"Unable to load Departamento1" });
            }
        }

        protected int agenteGruposForGrupoCount;
        protected SGPA.Server.Models.CMU.AgenteGrupo agenteGruposForGrupoValue;
        protected async Task agenteGruposForGrupoLoadData(LoadDataArgs args)
        {
            try
            {
                var result = await CMUService.GetAgenteGrupos(top: args.Top, skip: args.Skip, count:args.Top != null && args.Skip != null, filter: $"{args.Filter}", orderby: $"{args.OrderBy}");
                agenteGruposForGrupo = result.Value.AsODataEnumerable();
                agenteGruposForGrupoCount = result.Count;

                if (!object.Equals(agenteCobranza.Grupo, null))
                {
                    var valueResult = await CMUService.GetAgenteGrupos(filter: $"Id eq {agenteCobranza.Grupo}");
                    var firstItem = valueResult.Value.FirstOrDefault();
                    if (firstItem != null)
                    {
                        agenteGruposForGrupoValue = firstItem;
                    }
                }

            }
            catch (System.Exception ex)
            {
                NotificationService.Notify(new NotificationMessage(){ Severity = NotificationSeverity.Error, Summary = $"Error", Detail = $"Unable to load AgenteGrupo" });
            }
        }

        protected int origenMovimientosForIdCount;
        protected SGPA.Server.Models.CMU.OrigenMovimiento origenMovimientosForIdValue;
        protected async Task origenMovimientosForIdLoadData(LoadDataArgs args)
        {
            try
            {
                var result = await CMUService.GetOrigenMovimientos(top: args.Top, skip: args.Skip, count:args.Top != null && args.Skip != null, filter: $"{args.Filter}", orderby: $"{args.OrderBy}");
                origenMovimientosForId = result.Value.AsODataEnumerable();
                origenMovimientosForIdCount = result.Count;

                if (!object.Equals(agenteCobranza.Id, null))
                {
                    var valueResult = await CMUService.GetOrigenMovimientos(filter: $"Id eq {agenteCobranza.Id}");
                    var firstItem = valueResult.Value.FirstOrDefault();
                    if (firstItem != null)
                    {
                        origenMovimientosForIdValue = firstItem;
                    }
                }

            }
            catch (System.Exception ex)
            {
                NotificationService.Notify(new NotificationMessage(){ Severity = NotificationSeverity.Error, Summary = $"Error", Detail = $"Unable to load OrigenMovimiento" });
            }
        }

        protected int regionsForRegionCount;
        protected SGPA.Server.Models.CMU.Region regionsForRegionValue;
        protected async Task regionsForRegionLoadData(LoadDataArgs args)
        {
            try
            {
                var result = await CMUService.GetRegions(top: args.Top, skip: args.Skip, count:args.Top != null && args.Skip != null, filter: $"{args.Filter}", orderby: $"{args.OrderBy}");
                regionsForRegion = result.Value.AsODataEnumerable();
                regionsForRegionCount = result.Count;

                if (!object.Equals(agenteCobranza.Region, null))
                {
                    var valueResult = await CMUService.GetRegions(filter: $"Id eq {agenteCobranza.Region}");
                    var firstItem = valueResult.Value.FirstOrDefault();
                    if (firstItem != null)
                    {
                        regionsForRegionValue = firstItem;
                    }
                }

            }
            catch (System.Exception ex)
            {
                NotificationService.Notify(new NotificationMessage(){ Severity = NotificationSeverity.Error, Summary = $"Error", Detail = $"Unable to load Region1" });
            }
        }
        protected async Task FormSubmit()
        {
            try
            {
                var result = await CMUService.UpdateAgenteCobranza(id:Id, agenteCobranza);
                if (result.StatusCode == System.Net.HttpStatusCode.PreconditionFailed)
                {
                     hasChanges = true;
                     canEdit = false;
                     return;
                }
                DialogService.Close(agenteCobranza);
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

            agenteCobranza = await CMUService.GetAgenteCobranzaById(id:Id);
        }
    }
}