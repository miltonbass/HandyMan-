using Blazored.Modal.Services;
using Blazored.Modal;
using CurrieTechnologies.Razor.SweetAlert2;
using HandyMan_.Frontend.Repositories;
using HandyMan_.Shered.Entities;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using System.Diagnostics.Metrics;
using System.Reflection;
using HandyMan_.Shared.DTOs;

namespace HandyMan_.Frontend.Pages.Services
{
    public partial class ServiceCreate
    {
        private List<User> users;

        private bool loading;
        [Inject] private IRepository Repository { get; set; } = null!;

        private Service? Service = new();
        private string? imageUrl;
        [Inject] public SweetAlertService SweetAlertService { get; set; } = null!;
        [Inject] private NavigationManager NavigationManager { get; set; } = null!;

        [CascadingParameter] BlazoredModalInstance BlazoredModal { get; set; } = default!;

        private List<Category>? categories;

        protected override async Task OnInitializedAsync()
        {
            await LoadCategoriesAsync();
            
            await LoadAllUsersAsync();
        }

        private async Task LoadAllUsersAsync()
        {
            var url = $"api/accounts/GetAllUsers";
            var responseHttp = await Repository.GetAsync<List<User>>(url);
            if (responseHttp.Error)
            {
                var message = await responseHttp.GetErrorMessageAsync();
                await SweetAlertService.FireAsync("Error", message, SweetAlertIcon.Error);
            }
            users = responseHttp.Response!;
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

        private async Task CloseModalAsync()
        {

            await BlazoredModal.CloseAsync(ModalResult.Ok());
        }

        private void ImageSelected(string imagenBase64)
        {
            Service!.Photo = imagenBase64;
            imageUrl = null;
        }

        private async Task CreateAsync()
        {
            var responseHttp = await Repository.PostAsync("/api/services/AddServicePhoto", Service);
            if (responseHttp.Error)
            {
                var message = await responseHttp.GetErrorMessageAsync();
                await SweetAlertService.FireAsync("Error", message);
                return;
            }

            Service = new();
            await CloseModalAsync();
            var toast = SweetAlertService.Mixin(new SweetAlertOptions
            {
                Toast = true,
                Position = SweetAlertPosition.BottomEnd,
                ShowConfirmButton = true,
                Timer = 3000
            });
            await toast.FireAsync(icon: SweetAlertIcon.Success, message: "Registro creado con éxito.");
        }

       


    }
}