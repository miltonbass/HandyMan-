using CurrieTechnologies.Razor.SweetAlert2;
using HandyMan_.Shered.Entities;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Components.Routing;
using Microsoft.AspNetCore.Components;
using HandyMan_.Shared.Entities;
using System.ComponentModel;

namespace HandyMan_.Frontend.Pages.SubscriptionTypes
{
    public partial class SubscriptionTypeForm
{

        private EditContext editContext = null!;

        [EditorRequired, Parameter] public SubscriptionType SubscriptionType { get; set; } = null!;
        [EditorRequired, Parameter] public EventCallback OnValidSubmit { get; set; }
        [EditorRequired, Parameter] public EventCallback ReturnAction { get; set; }
        [Inject] public SweetAlertService SweetAlertService { get; set; } = null!;
        public bool FormPostedSuccessfully { get; set; }

        List<string> SubscriptionTypeEnumToList = UserTypeEnumToList.GetList();

        protected override void OnInitialized()
        {
            editContext = new(SubscriptionType);
        }

        private async Task OnBeforeInternalNavigation(LocationChangingContext context)
        {
            var formWasEdited = editContext.IsModified();
            if (!formWasEdited || FormPostedSuccessfully)
            {
                return;
            }

            var result = await SweetAlertService.FireAsync(new SweetAlertOptions
            {
                Title = "Confirmación",
                Text = "¿Desea abandonar la página y perder los cambios?",
                Icon = SweetAlertIcon.Question,
                ShowCancelButton = true,
            });
            var confirm = !string.IsNullOrEmpty(result.Value);
            if (confirm)
            {
                return;
            }

            context.PreventNavigation();
        }

       
    }
}
