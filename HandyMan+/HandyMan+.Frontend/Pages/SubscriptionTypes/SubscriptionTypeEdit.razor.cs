using CurrieTechnologies.Razor.SweetAlert2;
using HandyMan_.Frontend.Repositories;
using HandyMan_.Frontend.Shared;
using HandyMan_.Shared.Entities;
using HandyMan_.Shered.Entities;
using Microsoft.AspNetCore.Components;
using System.Net;

namespace HandyMan_.Frontend.Pages.SubscriptionTypes
{
    public partial class SubscriptionTypeEdit
{
        private SubscriptionType? subscriptionType;
        private SubscriptionTypeForm? subscriptionTypeForm;

        [Inject] private IRepository Repository { get; set; } = null!;
        [Inject] private SweetAlertService sweetAlertService { get; set; } = null!;
        [Inject] private NavigationManager navigationManager { get; set; } = null!;

        [EditorRequired, Parameter] public int Id { get; set; }

        protected override async Task OnParametersSetAsync()
        {
            var responseHttp = await Repository.GetAsync<SubscriptionType>($"/api/subscriptions/{Id}");
            if (responseHttp.Error)
            {
                if (responseHttp.HttpResponseMessage.StatusCode == HttpStatusCode.NotFound)
                {
                    navigationManager.NavigateTo("/subscriptions");
                }
                else
                {
                    var messsage = await responseHttp.GetErrorMessageAsync();
                    await sweetAlertService.FireAsync("Error", messsage, SweetAlertIcon.Error);
                }
            }
            else
            {
                subscriptionType = responseHttp.Response;
            }
        }

        private async Task EditAsync()
        {
            var responseHttp = await Repository.PutAsync("/api/subscriptions", subscriptionType);
            if (responseHttp.Error)
            {
                var message = await responseHttp.GetErrorMessageAsync();
                await sweetAlertService.FireAsync("Error", message);
                return;
            }

            Return();
            var toast = sweetAlertService.Mixin(new SweetAlertOptions
            {
                Toast = true,
                Position = SweetAlertPosition.BottomEnd,
                ShowConfirmButton = true,
                Timer = 3000
            });
            await toast.FireAsync(icon: SweetAlertIcon.Success, message: "Cambios guardados con éxito.");
        }

        private void Return()
        {
            subscriptionTypeForm!.FormPostedSuccessfully = true;
            navigationManager.NavigateTo("/subscriptions");
        }
    }
}
