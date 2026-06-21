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
    public partial class AddRolrolesMovimientotipomovimientostipo
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
            rolrolesMovimientotipomovimientostipo = new SGPA.Server.Models.CMU.RolrolesMovimientotipomovimientostipo();
        }
        protected bool errorVisible;
        protected SGPA.Server.Models.CMU.RolrolesMovimientotipomovimientostipo rolrolesMovimientotipomovimientostipo;

        protected IEnumerable<SGPA.Server.Models.CMU.MovimientoTipo> movimientoTiposForMovimientosTipo;

        protected IEnumerable<SGPA.Server.Models.CMU.Rol> rolsForRoles;


        protected int movimientoTiposForMovimientosTipoCount;
        protected SGPA.Server.Models.CMU.MovimientoTipo movimientoTiposForMovimientosTipoValue;
        protected async Task movimientoTiposForMovimientosTipoLoadData(LoadDataArgs args)
        {
            try
            {
                var result = await CMUService.GetMovimientoTipos(top: args.Top, skip: args.Skip, count:args.Top != null && args.Skip != null, filter: $"{args.Filter}", orderby: $"{args.OrderBy}");
                movimientoTiposForMovimientosTipo = result.Value.AsODataEnumerable();
                movimientoTiposForMovimientosTipoCount = result.Count;

                if (!object.Equals(rolrolesMovimientotipomovimientostipo.MovimientosTipo, null))
                {
                    var valueResult = await CMUService.GetMovimientoTipos(filter: $"Id eq {rolrolesMovimientotipomovimientostipo.MovimientosTipo}");
                    var firstItem = valueResult.Value.FirstOrDefault();
                    if (firstItem != null)
                    {
                        movimientoTiposForMovimientosTipoValue = firstItem;
                    }
                }

            }
            catch (System.Exception ex)
            {
                NotificationService.Notify(new NotificationMessage(){ Severity = NotificationSeverity.Error, Summary = $"Error", Detail = $"Unable to load MovimientoTipo" });
            }
        }

        protected int rolsForRolesCount;
        protected SGPA.Server.Models.CMU.Rol rolsForRolesValue;
        protected async Task rolsForRolesLoadData(LoadDataArgs args)
        {
            try
            {
                var result = await CMUService.GetRols(top: args.Top, skip: args.Skip, count:args.Top != null && args.Skip != null, filter: $"{args.Filter}", orderby: $"{args.OrderBy}");
                rolsForRoles = result.Value.AsODataEnumerable();
                rolsForRolesCount = result.Count;

                if (!object.Equals(rolrolesMovimientotipomovimientostipo.Roles, null))
                {
                    var valueResult = await CMUService.GetRols(filter: $"Oid eq {rolrolesMovimientotipomovimientostipo.Roles}");
                    var firstItem = valueResult.Value.FirstOrDefault();
                    if (firstItem != null)
                    {
                        rolsForRolesValue = firstItem;
                    }
                }

            }
            catch (System.Exception ex)
            {
                NotificationService.Notify(new NotificationMessage(){ Severity = NotificationSeverity.Error, Summary = $"Error", Detail = $"Unable to load Rol" });
            }
        }
        protected async Task FormSubmit()
        {
            try
            {
                var result = await CMUService.CreateRolrolesMovimientotipomovimientostipo(rolrolesMovimientotipomovimientostipo);
                DialogService.Close(rolrolesMovimientotipomovimientostipo);
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