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
    public partial class AddAfiliadoEspecialidad
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

        protected override async Task OnInitializedAsync()
        {
            afiliadoEspecialidad = new SgpaNew.Server.Models.Sgpa.AfiliadoEspecialidad();
        }
        protected bool errorVisible;
        protected SgpaNew.Server.Models.Sgpa.AfiliadoEspecialidad afiliadoEspecialidad;

        protected IEnumerable<SgpaNew.Server.Models.Sgpa.Afiliado> afiliadosForCI;

        protected IEnumerable<SgpaNew.Server.Models.Sgpa.Especialidad> especialidadsForCodEspecialidad;


        protected int afiliadosForCICount;
        protected SgpaNew.Server.Models.Sgpa.Afiliado afiliadosForCIValue;
        protected async Task afiliadosForCILoadData(LoadDataArgs args)
        {
            try
            {
                var result = await SgpaService.GetAfiliados(top: args.Top, skip: args.Skip, count:args.Top != null && args.Skip != null, filter: $"contains(Nombres, '{(!string.IsNullOrEmpty(args.Filter) ? args.Filter : "")}')", orderby: $"{args.OrderBy}");
                afiliadosForCI = result.Value.AsODataEnumerable();
                afiliadosForCICount = result.Count;

                if (!object.Equals(afiliadoEspecialidad.CI, null))
                {
                    var valueResult = await SgpaService.GetAfiliados(filter: $"CI eq {afiliadoEspecialidad.CI}");
                    var firstItem = valueResult.Value.FirstOrDefault();
                    if (firstItem != null)
                    {
                        afiliadosForCIValue = firstItem;
                    }
                }

            }
            catch (System.Exception ex)
            {
                NotificationService.Notify(new NotificationMessage(){ Severity = NotificationSeverity.Error, Summary = $"Error", Detail = $"Unable to load Afiliado" });
            }
        }

        protected int especialidadsForCodEspecialidadCount;
        protected SgpaNew.Server.Models.Sgpa.Especialidad especialidadsForCodEspecialidadValue;
        protected async Task especialidadsForCodEspecialidadLoadData(LoadDataArgs args)
        {
            try
            {
                var result = await SgpaService.GetEspecialidads(top: args.Top, skip: args.Skip, count:args.Top != null && args.Skip != null, filter: $"contains(Descrip, '{(!string.IsNullOrEmpty(args.Filter) ? args.Filter : "")}')", orderby: $"{args.OrderBy}");
                especialidadsForCodEspecialidad = result.Value.AsODataEnumerable();
                especialidadsForCodEspecialidadCount = result.Count;

                if (!object.Equals(afiliadoEspecialidad.CodEspecialidad, null))
                {
                    var valueResult = await SgpaService.GetEspecialidads(filter: $"CodEspecialidad eq {afiliadoEspecialidad.CodEspecialidad}");
                    var firstItem = valueResult.Value.FirstOrDefault();
                    if (firstItem != null)
                    {
                        especialidadsForCodEspecialidadValue = firstItem;
                    }
                }

            }
            catch (System.Exception ex)
            {
                NotificationService.Notify(new NotificationMessage(){ Severity = NotificationSeverity.Error, Summary = $"Error", Detail = $"Unable to load Especialidad" });
            }
        }
        protected async Task FormSubmit()
        {
            try
            {
                var result = await SgpaService.CreateAfiliadoEspecialidad(afiliadoEspecialidad);
                DialogService.Close(afiliadoEspecialidad);
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
    }
}