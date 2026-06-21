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
    public partial class AddColegiado
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
            colegiado = new SGPA.Server.Models.CMU.Colegiado();
        }
        protected bool errorVisible;
        protected SGPA.Server.Models.CMU.Colegiado colegiado;

        protected IEnumerable<SGPA.Server.Models.CMU.AgenteCobranza> agenteCobranzasForAgenteCobro;

        protected IEnumerable<SGPA.Server.Models.CMU.BajaMotivo> bajaMotivosForBajaMotivo;

        protected IEnumerable<SGPA.Server.Models.CMU.CategoriaColegiado> categoriaColegiadosForCategoriaColegiado;

        protected IEnumerable<SGPA.Server.Models.CMU.Departamento> departamentosForDepartamento;

        protected IEnumerable<SGPA.Server.Models.CMU.Pai> paisForPaisTitulo;

        protected IEnumerable<SGPA.Server.Models.CMU.Regional> regionalsForRegionalTrabaja;

        protected IEnumerable<SGPA.Server.Models.CMU.AgenteCobranza> agenteCobranzasForUltimoAgenteCobro;

        protected IEnumerable<SGPA.Server.Models.CMU.UniversidadTituloGrado> universidadTituloGradosForUniversidadTituloGrado;


        protected int agenteCobranzasForAgenteCobroCount;
        protected SGPA.Server.Models.CMU.AgenteCobranza agenteCobranzasForAgenteCobroValue;
        protected async Task agenteCobranzasForAgenteCobroLoadData(LoadDataArgs args)
        {
            try
            {
                var result = await CMUService.GetAgenteCobranzas(top: args.Top, skip: args.Skip, count:args.Top != null && args.Skip != null, filter: $"{args.Filter}", orderby: $"{args.OrderBy}");
                agenteCobranzasForAgenteCobro = result.Value.AsODataEnumerable();
                agenteCobranzasForAgenteCobroCount = result.Count;

                if (!object.Equals(colegiado.AgenteCobro, null))
                {
                    var valueResult = await CMUService.GetAgenteCobranzas(filter: $"Id eq {colegiado.AgenteCobro}");
                    var firstItem = valueResult.Value.FirstOrDefault();
                    if (firstItem != null)
                    {
                        agenteCobranzasForAgenteCobroValue = firstItem;
                    }
                }

            }
            catch (System.Exception ex)
            {
                NotificationService.Notify(new NotificationMessage(){ Severity = NotificationSeverity.Error, Summary = $"Error", Detail = $"Unable to load AgenteCobranza" });
            }
        }

        protected int bajaMotivosForBajaMotivoCount;
        protected SGPA.Server.Models.CMU.BajaMotivo bajaMotivosForBajaMotivoValue;
        protected async Task bajaMotivosForBajaMotivoLoadData(LoadDataArgs args)
        {
            try
            {
                var result = await CMUService.GetBajaMotivos(top: args.Top, skip: args.Skip, count:args.Top != null && args.Skip != null, filter: $"{args.Filter}", orderby: $"{args.OrderBy}");
                bajaMotivosForBajaMotivo = result.Value.AsODataEnumerable();
                bajaMotivosForBajaMotivoCount = result.Count;

                if (!object.Equals(colegiado.BajaMotivo, null))
                {
                    var valueResult = await CMUService.GetBajaMotivos(filter: $"Id eq {colegiado.BajaMotivo}");
                    var firstItem = valueResult.Value.FirstOrDefault();
                    if (firstItem != null)
                    {
                        bajaMotivosForBajaMotivoValue = firstItem;
                    }
                }

            }
            catch (System.Exception ex)
            {
                NotificationService.Notify(new NotificationMessage(){ Severity = NotificationSeverity.Error, Summary = $"Error", Detail = $"Unable to load BajaMotivo1" });
            }
        }

        protected int categoriaColegiadosForCategoriaColegiadoCount;
        protected SGPA.Server.Models.CMU.CategoriaColegiado categoriaColegiadosForCategoriaColegiadoValue;
        protected async Task categoriaColegiadosForCategoriaColegiadoLoadData(LoadDataArgs args)
        {
            try
            {
                var result = await CMUService.GetCategoriaColegiados(top: args.Top, skip: args.Skip, count:args.Top != null && args.Skip != null, filter: $"{args.Filter}", orderby: $"{args.OrderBy}");
                categoriaColegiadosForCategoriaColegiado = result.Value.AsODataEnumerable();
                categoriaColegiadosForCategoriaColegiadoCount = result.Count;

                if (!object.Equals(colegiado.CategoriaColegiado, null))
                {
                    var valueResult = await CMUService.GetCategoriaColegiados(filter: $"Id eq {colegiado.CategoriaColegiado}");
                    var firstItem = valueResult.Value.FirstOrDefault();
                    if (firstItem != null)
                    {
                        categoriaColegiadosForCategoriaColegiadoValue = firstItem;
                    }
                }

            }
            catch (System.Exception ex)
            {
                NotificationService.Notify(new NotificationMessage(){ Severity = NotificationSeverity.Error, Summary = $"Error", Detail = $"Unable to load CategoriaColegiado1" });
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

                if (!object.Equals(colegiado.Departamento, null))
                {
                    var valueResult = await CMUService.GetDepartamentos(filter: $"Id eq {colegiado.Departamento}");
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

        protected int paisForPaisTituloCount;
        protected SGPA.Server.Models.CMU.Pai paisForPaisTituloValue;
        protected async Task paisForPaisTituloLoadData(LoadDataArgs args)
        {
            try
            {
                var result = await CMUService.GetPais(top: args.Top, skip: args.Skip, count:args.Top != null && args.Skip != null, filter: $"{args.Filter}", orderby: $"{args.OrderBy}");
                paisForPaisTitulo = result.Value.AsODataEnumerable();
                paisForPaisTituloCount = result.Count;

                if (!object.Equals(colegiado.PaisTitulo, null))
                {
                    var valueResult = await CMUService.GetPais(filter: $"Codigo eq {colegiado.PaisTitulo}");
                    var firstItem = valueResult.Value.FirstOrDefault();
                    if (firstItem != null)
                    {
                        paisForPaisTituloValue = firstItem;
                    }
                }

            }
            catch (System.Exception ex)
            {
                NotificationService.Notify(new NotificationMessage(){ Severity = NotificationSeverity.Error, Summary = $"Error", Detail = $"Unable to load Pai" });
            }
        }

        protected int regionalsForRegionalTrabajaCount;
        protected SGPA.Server.Models.CMU.Regional regionalsForRegionalTrabajaValue;
        protected async Task regionalsForRegionalTrabajaLoadData(LoadDataArgs args)
        {
            try
            {
                var result = await CMUService.GetRegionals(top: args.Top, skip: args.Skip, count:args.Top != null && args.Skip != null, filter: $"{args.Filter}", orderby: $"{args.OrderBy}");
                regionalsForRegionalTrabaja = result.Value.AsODataEnumerable();
                regionalsForRegionalTrabajaCount = result.Count;

                if (!object.Equals(colegiado.RegionalTrabaja, null))
                {
                    var valueResult = await CMUService.GetRegionals(filter: $"Id eq {colegiado.RegionalTrabaja}");
                    var firstItem = valueResult.Value.FirstOrDefault();
                    if (firstItem != null)
                    {
                        regionalsForRegionalTrabajaValue = firstItem;
                    }
                }

            }
            catch (System.Exception ex)
            {
                NotificationService.Notify(new NotificationMessage(){ Severity = NotificationSeverity.Error, Summary = $"Error", Detail = $"Unable to load Regional" });
            }
        }

        protected int agenteCobranzasForUltimoAgenteCobroCount;
        protected SGPA.Server.Models.CMU.AgenteCobranza agenteCobranzasForUltimoAgenteCobroValue;
        protected async Task agenteCobranzasForUltimoAgenteCobroLoadData(LoadDataArgs args)
        {
            try
            {
                var result = await CMUService.GetAgenteCobranzas(top: args.Top, skip: args.Skip, count:args.Top != null && args.Skip != null, filter: $"{args.Filter}", orderby: $"{args.OrderBy}");
                agenteCobranzasForUltimoAgenteCobro = result.Value.AsODataEnumerable();
                agenteCobranzasForUltimoAgenteCobroCount = result.Count;

                if (!object.Equals(colegiado.UltimoAgenteCobro, null))
                {
                    var valueResult = await CMUService.GetAgenteCobranzas(filter: $"Id eq {colegiado.UltimoAgenteCobro}");
                    var firstItem = valueResult.Value.FirstOrDefault();
                    if (firstItem != null)
                    {
                        agenteCobranzasForUltimoAgenteCobroValue = firstItem;
                    }
                }

            }
            catch (System.Exception ex)
            {
                NotificationService.Notify(new NotificationMessage(){ Severity = NotificationSeverity.Error, Summary = $"Error", Detail = $"Unable to load AgenteCobranza1" });
            }
        }

        protected int universidadTituloGradosForUniversidadTituloGradoCount;
        protected SGPA.Server.Models.CMU.UniversidadTituloGrado universidadTituloGradosForUniversidadTituloGradoValue;
        protected async Task universidadTituloGradosForUniversidadTituloGradoLoadData(LoadDataArgs args)
        {
            try
            {
                var result = await CMUService.GetUniversidadTituloGrados(top: args.Top, skip: args.Skip, count:args.Top != null && args.Skip != null, filter: $"{args.Filter}", orderby: $"{args.OrderBy}");
                universidadTituloGradosForUniversidadTituloGrado = result.Value.AsODataEnumerable();
                universidadTituloGradosForUniversidadTituloGradoCount = result.Count;

                if (!object.Equals(colegiado.UniversidadTituloGrado, null))
                {
                    var valueResult = await CMUService.GetUniversidadTituloGrados(filter: $"Id eq {colegiado.UniversidadTituloGrado}");
                    var firstItem = valueResult.Value.FirstOrDefault();
                    if (firstItem != null)
                    {
                        universidadTituloGradosForUniversidadTituloGradoValue = firstItem;
                    }
                }

            }
            catch (System.Exception ex)
            {
                NotificationService.Notify(new NotificationMessage(){ Severity = NotificationSeverity.Error, Summary = $"Error", Detail = $"Unable to load UniversidadTituloGrado1" });
            }
        }
        protected async Task FormSubmit()
        {
            try
            {
                var result = await CMUService.CreateColegiado(colegiado);
                DialogService.Close(colegiado);
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