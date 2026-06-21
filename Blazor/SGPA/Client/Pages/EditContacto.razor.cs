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
    public partial class EditContacto
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
            contacto = await CMUService.GetContactoById(id:Id);
        }
        protected bool errorVisible;
        protected SGPA.Server.Models.CMU.Contacto contacto;

        protected IEnumerable<SGPA.Server.Models.CMU.AreaContacto> areaContactosForArea;

        protected IEnumerable<SGPA.Server.Models.CMU.CargoContacto> cargoContactosForCargo;

        protected IEnumerable<SGPA.Server.Models.CMU.GrupoContacto> grupoContactosForGrupo;


        protected int areaContactosForAreaCount;
        protected SGPA.Server.Models.CMU.AreaContacto areaContactosForAreaValue;
        protected async Task areaContactosForAreaLoadData(LoadDataArgs args)
        {
            try
            {
                var result = await CMUService.GetAreaContactos(top: args.Top, skip: args.Skip, count:args.Top != null && args.Skip != null, filter: $"{args.Filter}", orderby: $"{args.OrderBy}");
                areaContactosForArea = result.Value.AsODataEnumerable();
                areaContactosForAreaCount = result.Count;

                if (!object.Equals(contacto.Area, null))
                {
                    var valueResult = await CMUService.GetAreaContactos(filter: $"Id eq {contacto.Area}");
                    var firstItem = valueResult.Value.FirstOrDefault();
                    if (firstItem != null)
                    {
                        areaContactosForAreaValue = firstItem;
                    }
                }

            }
            catch (System.Exception ex)
            {
                NotificationService.Notify(new NotificationMessage(){ Severity = NotificationSeverity.Error, Summary = $"Error", Detail = $"Unable to load AreaContacto" });
            }
        }

        protected int cargoContactosForCargoCount;
        protected SGPA.Server.Models.CMU.CargoContacto cargoContactosForCargoValue;
        protected async Task cargoContactosForCargoLoadData(LoadDataArgs args)
        {
            try
            {
                var result = await CMUService.GetCargoContactos(top: args.Top, skip: args.Skip, count:args.Top != null && args.Skip != null, filter: $"{args.Filter}", orderby: $"{args.OrderBy}");
                cargoContactosForCargo = result.Value.AsODataEnumerable();
                cargoContactosForCargoCount = result.Count;

                if (!object.Equals(contacto.Cargo, null))
                {
                    var valueResult = await CMUService.GetCargoContactos(filter: $"Id eq {contacto.Cargo}");
                    var firstItem = valueResult.Value.FirstOrDefault();
                    if (firstItem != null)
                    {
                        cargoContactosForCargoValue = firstItem;
                    }
                }

            }
            catch (System.Exception ex)
            {
                NotificationService.Notify(new NotificationMessage(){ Severity = NotificationSeverity.Error, Summary = $"Error", Detail = $"Unable to load CargoContacto" });
            }
        }

        protected int grupoContactosForGrupoCount;
        protected SGPA.Server.Models.CMU.GrupoContacto grupoContactosForGrupoValue;
        protected async Task grupoContactosForGrupoLoadData(LoadDataArgs args)
        {
            try
            {
                var result = await CMUService.GetGrupoContactos(top: args.Top, skip: args.Skip, count:args.Top != null && args.Skip != null, filter: $"{args.Filter}", orderby: $"{args.OrderBy}");
                grupoContactosForGrupo = result.Value.AsODataEnumerable();
                grupoContactosForGrupoCount = result.Count;

                if (!object.Equals(contacto.Grupo, null))
                {
                    var valueResult = await CMUService.GetGrupoContactos(filter: $"Id eq {contacto.Grupo}");
                    var firstItem = valueResult.Value.FirstOrDefault();
                    if (firstItem != null)
                    {
                        grupoContactosForGrupoValue = firstItem;
                    }
                }

            }
            catch (System.Exception ex)
            {
                NotificationService.Notify(new NotificationMessage(){ Severity = NotificationSeverity.Error, Summary = $"Error", Detail = $"Unable to load GrupoContacto" });
            }
        }
        protected async Task FormSubmit()
        {
            try
            {
                var result = await CMUService.UpdateContacto(id:Id, contacto);
                if (result.StatusCode == System.Net.HttpStatusCode.PreconditionFailed)
                {
                     hasChanges = true;
                     canEdit = false;
                     return;
                }
                DialogService.Close(contacto);
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

            contacto = await CMUService.GetContactoById(id:Id);
        }
    }
}