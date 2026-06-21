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
    public partial class AddAfiliado
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
            afiliado = new SgpaNew.Server.Models.Sgpa.Afiliado();
        }
        protected bool errorVisible;
        protected SgpaNew.Server.Models.Sgpa.Afiliado afiliado;

        protected IEnumerable<SgpaNew.Server.Models.Sgpa.Banco> bancosForCodBanco;

        protected IEnumerable<SgpaNew.Server.Models.Sgpa.Mutualistum> mutualistaForCodMutualista;

        protected IEnumerable<SgpaNew.Server.Models.Sgpa.RegimenJubilatorio> regimenJubilatoriosForCodRegimenJubilatorio;


        protected int bancosForCodBancoCount;
        protected SgpaNew.Server.Models.Sgpa.Banco bancosForCodBancoValue;
        protected async Task bancosForCodBancoLoadData(LoadDataArgs args)
        {
            try
            {
                var result = await SgpaService.GetBancos(top: args.Top, skip: args.Skip, count:args.Top != null && args.Skip != null, filter: $"contains(Descripcion, '{(!string.IsNullOrEmpty(args.Filter) ? args.Filter : "")}')", orderby: $"{args.OrderBy}");
                bancosForCodBanco = result.Value.AsODataEnumerable();
                bancosForCodBancoCount = result.Count;

                if (!object.Equals(afiliado.CodBanco, null))
                {
                    var valueResult = await SgpaService.GetBancos(filter: $"CodBanco eq {afiliado.CodBanco}");
                    var firstItem = valueResult.Value.FirstOrDefault();
                    if (firstItem != null)
                    {
                        bancosForCodBancoValue = firstItem;
                    }
                }

            }
            catch (System.Exception ex)
            {
                NotificationService.Notify(new NotificationMessage(){ Severity = NotificationSeverity.Error, Summary = $"Error", Detail = $"Unable to load Banco" });
            }
        }

        protected int mutualistaForCodMutualistaCount;
        protected SgpaNew.Server.Models.Sgpa.Mutualistum mutualistaForCodMutualistaValue;
        protected async Task mutualistaForCodMutualistaLoadData(LoadDataArgs args)
        {
            try
            {
                var result = await SgpaService.GetMutualista(top: args.Top, skip: args.Skip, count:args.Top != null && args.Skip != null, filter: $"contains(Nombre, '{(!string.IsNullOrEmpty(args.Filter) ? args.Filter : "")}')", orderby: $"{args.OrderBy}");
                mutualistaForCodMutualista = result.Value.AsODataEnumerable();
                mutualistaForCodMutualistaCount = result.Count;

                if (!object.Equals(afiliado.CodMutualista, null))
                {
                    var valueResult = await SgpaService.GetMutualista(filter: $"CodMutualista eq {afiliado.CodMutualista}");
                    var firstItem = valueResult.Value.FirstOrDefault();
                    if (firstItem != null)
                    {
                        mutualistaForCodMutualistaValue = firstItem;
                    }
                }

            }
            catch (System.Exception ex)
            {
                NotificationService.Notify(new NotificationMessage(){ Severity = NotificationSeverity.Error, Summary = $"Error", Detail = $"Unable to load Mutualistum" });
            }
        }

        protected int regimenJubilatoriosForCodRegimenJubilatorioCount;
        protected SgpaNew.Server.Models.Sgpa.RegimenJubilatorio regimenJubilatoriosForCodRegimenJubilatorioValue;
        protected async Task regimenJubilatoriosForCodRegimenJubilatorioLoadData(LoadDataArgs args)
        {
            try
            {
                var result = await SgpaService.GetRegimenJubilatorios(top: args.Top, skip: args.Skip, count:args.Top != null && args.Skip != null, filter: $"contains(Descrip, '{(!string.IsNullOrEmpty(args.Filter) ? args.Filter : "")}')", orderby: $"{args.OrderBy}");
                regimenJubilatoriosForCodRegimenJubilatorio = result.Value.AsODataEnumerable();
                regimenJubilatoriosForCodRegimenJubilatorioCount = result.Count;

                if (!object.Equals(afiliado.CodRegimenJubilatorio, null))
                {
                    var valueResult = await SgpaService.GetRegimenJubilatorios(filter: $"CodRegimenJubilatorio eq {afiliado.CodRegimenJubilatorio}");
                    var firstItem = valueResult.Value.FirstOrDefault();
                    if (firstItem != null)
                    {
                        regimenJubilatoriosForCodRegimenJubilatorioValue = firstItem;
                    }
                }

            }
            catch (System.Exception ex)
            {
                NotificationService.Notify(new NotificationMessage(){ Severity = NotificationSeverity.Error, Summary = $"Error", Detail = $"Unable to load RegimenJubilatorio" });
            }
        }
        protected async Task FormSubmit()
        {
            try
            {
                var result = await SgpaService.CreateAfiliado(afiliado);
                DialogService.Close(afiliado);
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