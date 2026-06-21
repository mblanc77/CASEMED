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
    public partial class EditContactoInfoAdicional
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
            contactoInfoAdicional = await CMUService.GetContactoInfoAdicionalById(id:Id);
        }
        protected bool errorVisible;
        protected SGPA.Server.Models.CMU.ContactoInfoAdicional contactoInfoAdicional;

        protected IEnumerable<SGPA.Server.Models.CMU.Contacto> contactosForContacto;


        protected int contactosForContactoCount;
        protected SGPA.Server.Models.CMU.Contacto contactosForContactoValue;
        protected async Task contactosForContactoLoadData(LoadDataArgs args)
        {
            try
            {
                var result = await CMUService.GetContactos(top: args.Top, skip: args.Skip, count:args.Top != null && args.Skip != null, filter: $"{args.Filter}", orderby: $"{args.OrderBy}");
                contactosForContacto = result.Value.AsODataEnumerable();
                contactosForContactoCount = result.Count;

                if (!object.Equals(contactoInfoAdicional.Contacto, null))
                {
                    var valueResult = await CMUService.GetContactos(filter: $"Id eq {contactoInfoAdicional.Contacto}");
                    var firstItem = valueResult.Value.FirstOrDefault();
                    if (firstItem != null)
                    {
                        contactosForContactoValue = firstItem;
                    }
                }

            }
            catch (System.Exception ex)
            {
                NotificationService.Notify(new NotificationMessage(){ Severity = NotificationSeverity.Error, Summary = $"Error", Detail = $"Unable to load Contacto1" });
            }
        }
        protected async Task FormSubmit()
        {
            try
            {
                var result = await CMUService.UpdateContactoInfoAdicional(id:Id, contactoInfoAdicional);
                if (result.StatusCode == System.Net.HttpStatusCode.PreconditionFailed)
                {
                     hasChanges = true;
                     canEdit = false;
                     return;
                }
                DialogService.Close(contactoInfoAdicional);
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

            contactoInfoAdicional = await CMUService.GetContactoInfoAdicionalById(id:Id);
        }
    }
}