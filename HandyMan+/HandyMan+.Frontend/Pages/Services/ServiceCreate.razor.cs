using Blazored.Modal.Services;
using Blazored.Modal;
using CurrieTechnologies.Razor.SweetAlert2;
using HandyMan_.Frontend.Repositories;
using HandyMan_.Shered.Entities;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using System.Diagnostics.Metrics;
using System.Reflection;
using HandyMan_.Frontend.Shared;
using System.Runtime.CompilerServices;
using HandyMan_.Frontend.Pages.Auth;

namespace HandyMan_.Frontend.Pages.Services
{
    public partial class ServiceCreate
    {

        private EditContext editContext = null!;
        private bool wasClose;
        private bool loading;
        [Inject] private IRepository Repository { get; set; } = null!;

        private Service? Service { get; set; } = new Service();
        [Inject] public SweetAlertService SweetAlertService { get; set; } = null!;
        [Inject] private NavigationManager NavigationManager { get; set; } = null!;

        [CascadingParameter] BlazoredModalInstance BlazoredModal { get; set; } = default!;
        [CascadingParameter] IModalService Modal { get; set; } = default!;

        private List<Category>? categories;

        private List<People>? peoples;

        protected override void OnInitialized()
        {
            editContext = new(Service!);
        }
        protected override async Task OnInitializedAsync()
        {
            await LoadCategoriesAsync();
            await LoadProvidersAsync();
        }
        private async Task LoadCategoriesAsync()
        {
            var responseHttp = await Repository.GetAsync<List<Category>>("/api/categories/combo");
            if (responseHttp.Error)
            {
                var message = await responseHttp.GetErrorMessageAsync();
                await SweetAlertService.FireAsync("Error", message, SweetAlertIcon.Error);
                return;
            }

            categories = responseHttp.Response;
        }

        private async Task LoadProvidersAsync()
        {
            var responseHttp = await Repository.GetAsync<List<People>>("/api/peoples/combo");
            if (responseHttp.Error)
            {
                var message = await responseHttp.GetErrorMessageAsync();
                await SweetAlertService.FireAsync("Error", message, SweetAlertIcon.Error);
                return;
            }

            peoples = responseHttp.Response;
        }

        private async Task CreateAsync()
        {
            loading = true;
            var responseHttp = await Repository.PostAsync("/api/services", Service);
            loading = false;

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
        private async Task CloseModalAsync()
        {
            wasClose = true;
            await BlazoredModal.CloseAsync(ModalResult.Ok());
        }

       


    }
}