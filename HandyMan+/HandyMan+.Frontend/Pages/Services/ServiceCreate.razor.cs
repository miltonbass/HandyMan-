using CurrieTechnologies.Razor.SweetAlert2;
using HandyMan_.Frontend.Repositories;
using HandyMan_.Shered.Entities;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using System.Diagnostics.Metrics;
using System.Reflection;

namespace HandyMan_.Frontend.Pages.Services
{
    public partial class ServiceCreate
    {

        private EditContext editContext = null!;
        [Inject] private IRepository Repository { get; set; } = null!;

        private Service? Service { get; set; } = new Service();
        [Inject] public SweetAlertService SweetAlertService { get; set; } = null!;
        [Inject] private NavigationManager NavigationManager { get; set; } = null!;

        protected override void OnInitialized()
        {
            editContext = new(Service!);
        }
        private async Task CreateAsync()
        {
            var responseHttp = await Repository.PostAsync("/api/services", Service);
            if (responseHttp.Error)
            {
                var message = await responseHttp.GetErrorMessageAsync();
                await SweetAlertService.FireAsync("Error", message);
                return;
            }

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
            NavigationManager.NavigateTo("/services");
        }


    }
}