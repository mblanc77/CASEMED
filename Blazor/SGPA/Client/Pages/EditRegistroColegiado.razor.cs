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
    public partial class EditRegistroColegiado
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
        public int OID { get; set; }

        protected override async Task OnInitializedAsync()
        {
            registroColegiado = await CMUService.GetRegistroColegiadoByOid(oid:OID);
        }
        protected bool errorVisible;
        protected SGPA.Server.Models.CMU.RegistroColegiado registroColegiado;

        protected IEnumerable<SGPA.Server.Models.CMU.Departamento> departamentosForDepartamento;

        protected IEnumerable<SGPA.Server.Models.CMU.Pai> paisForPaisTitulo;

        protected IEnumerable<SGPA.Server.Models.CMU.Universidad> universidadsForUniversidadTitulo;

        protected IEnumerable<SGPA.Server.Models.CMU.UniversidadTituloGrado> universidadTituloGradosForUniversidadTituloGrado;


        protected int departamentosForDepartamentoCount;
        protected SGPA.Server.Models.CMU.Departamento departamentosForDepartamentoValue;
        protected async Task departamentosForDepartamentoLoadData(LoadDataArgs args)
        {
            try
            {
                var result = await CMUService.GetDepartamentos(top: args.Top, skip: args.Skip, count:args.Top != null && args.Skip != null, filter: $"{args.Filter}", orderby: $"{args.OrderBy}");
                departamentosForDepartamento = result.Value.AsODataEnumerable();
                departamentosForDepartamentoCount = result.Count;

                if (!object.Equals(registroColegiado.Departamento, null))
                {
                    var valueResult = await CMUService.GetDepartamentos(filter: $"Id eq {registroColegiado.Departamento}");
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

                if (!object.Equals(registroColegiado.PaisTitulo, null))
                {
                    var valueResult = await CMUService.GetPais(filter: $"Codigo eq {registroColegiado.PaisTitulo}");
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

        protected int universidadsForUniversidadTituloCount;
        protected SGPA.Server.Models.CMU.Universidad universidadsForUniversidadTituloValue;
        protected async Task universidadsForUniversidadTituloLoadData(LoadDataArgs args)
        {
            try
            {
                var result = await CMUService.GetUniversidads(top: args.Top, skip: args.Skip, count:args.Top != null && args.Skip != null, filter: $"{args.Filter}", orderby: $"{args.OrderBy}");
                universidadsForUniversidadTitulo = result.Value.AsODataEnumerable();
                universidadsForUniversidadTituloCount = result.Count;

                if (!object.Equals(registroColegiado.UniversidadTitulo, null))
                {
                    var valueResult = await CMUService.GetUniversidads(filter: $"Id eq {registroColegiado.UniversidadTitulo}");
                    var firstItem = valueResult.Value.FirstOrDefault();
                    if (firstItem != null)
                    {
                        universidadsForUniversidadTituloValue = firstItem;
                    }
                }

            }
            catch (System.Exception ex)
            {
                NotificationService.Notify(new NotificationMessage(){ Severity = NotificationSeverity.Error, Summary = $"Error", Detail = $"Unable to load Universidad" });
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

                if (!object.Equals(registroColegiado.UniversidadTituloGrado, null))
                {
                    var valueResult = await CMUService.GetUniversidadTituloGrados(filter: $"Id eq {registroColegiado.UniversidadTituloGrado}");
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
                var result = await CMUService.UpdateRegistroColegiado(oid:OID, registroColegiado);
                if (result.StatusCode == System.Net.HttpStatusCode.PreconditionFailed)
                {
                     hasChanges = true;
                     canEdit = false;
                     return;
                }
                DialogService.Close(registroColegiado);
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

            registroColegiado = await CMUService.GetRegistroColegiadoByOid(oid:OID);
        }
    }
}