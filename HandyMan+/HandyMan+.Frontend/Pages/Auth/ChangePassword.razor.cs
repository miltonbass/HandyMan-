using CurrieTechnologies.Razor.SweetAlert2;
using HandyMan_.Frontend.Repositories;
using Microsoft.AspNetCore.Components;
using HandyMan_.Shared.DTOs;
using Blazored.Modal.Services;
using Blazored.Modal;

namespace HandyMan_.Frontend.Pages.Auth
{
    public partial class ChangePassword
    {
        private ChangePasswordDTO changePasswordDTO = new();
        private bool loading;
        private bool wasClose;

        [Inject] private NavigationManager NavigationManager { get; set; } = null!;
        [Inject] private SweetAlertService SweetAlertService { get; set; } = null!;
        [Inject] private IRepository Repository { get; set; } = null!;

        [CascadingParameter] BlazoredModalInstance BlazoredModal { get; set; } = default!;

        private async Task ChangePasswordAsync()
        {
            loading = true;
            var responseHttp = await Repository.PostAsync("/api/accounts/changePassword", changePasswordDTO);
            loading = false;
            if (responseHttp.Error)
            {
                var message = await responseHttp.GetErrorMessageAsync();
                await SweetAlertService.FireAsync("Error", message, SweetAlertIcon.Error);
                return;
            }

            NavigationManager.NavigateTo("/");
            var toast = SweetAlertService.Mixin(new SweetAlertOptions
            {
                Toast = true,
                Position = SweetAlertPosition.BottomEnd,
                ShowConfirmButton = true,
                Timer = 3000
            });
            await toast.FireAsync(icon: SweetAlertIcon.Success, message: "Contraseña cambiada con éxito.");
        }

        private async Task CloseModalAsync()
        {
            wasClose = true;
            await BlazoredModal.CloseAsync(ModalResult.Ok());
        }
    }
}