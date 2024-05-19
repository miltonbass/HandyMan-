using Blazored.Modal;
using Blazored.Modal.Services;
using CurrieTechnologies.Razor.SweetAlert2;
using HandyMan_.Frontend.Pages.SubscriptionTypes;
using HandyMan_.Frontend.Repositories;
using HandyMan_.Frontend.Shared;
using HandyMan_.Shered.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components;


namespace HandyMan_.Frontend.Pages.ServiceOrders
{
    [Authorize(Roles = "Admin")]
    public partial class ServiceOrdersCreate
    {
        private ServiceOrder serviceorder = new();
        private ServiceOrdersForm? ServiceOrdersForm;
        [Inject] private IRepository Repository { get; set; } = null!;
        [Inject] private SweetAlertService SweetAlertService { get; set; } = null!;
        [Inject] private NavigationManager NavigationManager { get; set; } = null!;
        [CascadingParameter] BlazoredModalInstance BlazoredModal { get; set; } = default!;

        private async Task CreateAsync()
        {
            var responseHttp = await Repository.PostAsync("/api/serviceorder", serviceorder);
            if (responseHttp.Error)
            {
                var message = await responseHttp.GetErrorMessageAsync();
                await SweetAlertService.FireAsync("Error", message, SweetAlertIcon.Error);
                return;
            }


            await BlazoredModal.CloseAsync(ModalResult.Ok());
            Return();

            var toast = SweetAlertService.Mixin(new SweetAlertOptions
            {
                Toast = true,
                Position = SweetAlertPosition.BottomEnd,
                ShowConfirmButton = true,
                Timer = 3000
            });
            await toast.FireAsync(icon: SweetAlertIcon.Success, message: "Registro creado con éxito.");
        }

        private void Return()
        {
            ServiceOrdersForm!.FormPostedSuccessfully = true;
            NavigationManager.NavigateTo("/serviceorder");
        }
    }
}