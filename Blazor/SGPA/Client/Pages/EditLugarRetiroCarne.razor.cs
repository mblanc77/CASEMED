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
    public partial class EditLugarRetiroCarne
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
            lugarRetiroCarne = await CMUService.GetLugarRetiroCarneById(id:Id);
        }
        protected bool errorVisible;
        protected SGPA.Server.Models.CMU.LugarRetiroCarne lugarRetiroCarne;

        protected IEnumerable<SGPA.Server.Models.CMU.Departamento> departamentosForDepartamento;

        protected IEnumerable<SGPA.Server.Models.CMU.GrupoLugarRetiroCarne> grupoLugarRetiroCarnesForGrupo;


        protected int departamentosForDepartamentoCount;
        protected SGPA.Server.Models.CMU.Departamento departamentosForDepartamentoValue;
        protected async Task departamentosForDepartamentoLoadData(LoadDataArgs args)
        {
            try
            {
                var result = await CMUService.GetDepartamentos(top: args.Top, skip: args.Skip, count:args.Top != null && args.Skip != null, filter: $"{args.Filter}", orderby: $"{args.OrderBy}");
                departamentosForDepartamento = result.Value.AsODataEnumerable();
                departamentosForDepartamentoCount = result.Count;

                if (!object.Equals(lugarRetiroCarne.Departamento, null))
                {
                    var valueResult = await CMUService.GetDepartamentos(filter: $"Id eq {lugarRetiroCarne.Departamento}");
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

        protected int grupoLugarRetiroCarnesForGrupoCount;
        protected SGPA.Server.Models.CMU.GrupoLugarRetiroCarne grupoLugarRetiroCarnesForGrupoValue;
        protected async Task grupoLugarRetiroCarnesForGrupoLoadData(LoadDataArgs args)
        {
            try
            {
                var result = await CMUService.GetGrupoLugarRetiroCarnes(top: args.Top, skip: args.Skip, count:args.Top != null && args.Skip != null, filter: $"{args.Filter}", orderby: $"{args.OrderBy}");
                grupoLugarRetiroCarnesForGrupo = result.Value.AsODataEnumerable();
                grupoLugarRetiroCarnesForGrupoCount = result.Count;

                if (!object.Equals(lugarRetiroCarne.Grupo, null))
                {
                    var valueResult = await CMUService.GetGrupoLugarRetiroCarnes(filter: $"Id eq {lugarRetiroCarne.Grupo}");
                    var firstItem = valueResult.Value.FirstOrDefault();
                    if (firstItem != null)
                    {
                        grupoLugarRetiroCarnesForGrupoValue = firstItem;
                    }
                }

            }
            catch (System.Exception ex)
            {
                NotificationService.Notify(new NotificationMessage(){ Severity = NotificationSeverity.Error, Summary = $"Error", Detail = $"Unable to load GrupoLugarRetiroCarne" });
            }
        }
        protected async Task FormSubmit()
        {
            try
            {
                var result = await CMUService.UpdateLugarRetiroCarne(id:Id, lugarRetiroCarne);
                if (result.StatusCode == System.Net.HttpStatusCode.PreconditionFailed)
                {
                     hasChanges = true;
                     canEdit = false;
                     return;
                }
                DialogService.Close(lugarRetiroCarne);
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

            lugarRetiroCarne = await CMUService.GetLugarRetiroCarneById(id:Id);
        }
    }
}