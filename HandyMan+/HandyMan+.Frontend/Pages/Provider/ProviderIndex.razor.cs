using Blazored.Modal;
using Blazored.Modal.Services;
using CurrieTechnologies.Razor.SweetAlert2;
using HandyMan_.Frontend.Pages.Auth;
using HandyMan_.Frontend.Repositories;
using HandyMan_.Shered.Entities;
using Microsoft.AspNetCore.Components;
using Org.BouncyCastle.Math;

namespace HandyMan_.Frontend.Pages.Provider
{
    public partial class ProviderIndex
    {

        private List<Country>? countries;
        private List<Country>? paises;
        private List<User> users;
        public string data { get; set; } = null!;
        public List<User>? Users { get; set; }
        private User User { get; set; } = new();

        [Inject] private IRepository Repository { get; set; } = null!;
        [CascadingParameter] BlazoredModalInstance BlazoredModal { get; set; } = default!;
        [Inject] private NavigationManager NavigationManager { get; set; } = null!;
        [Inject] public SweetAlertService SweetAlertService { get; set; } = null!;

        [CascadingParameter] IModalService Modal { get; set; } = default!;


        bool dialogIsOpen = false;


        void OpenDialog()
        {

            dialogIsOpen = true;

        }

        void OkClick()
        {
            dialogIsOpen = false;
        }

        protected override async Task OnInitializedAsync()
        {
            await LoadCountriesAsync();
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
            users = responseHttp.Response;
        }

        private async Task LoadCountriesAsync()
        {
            var responseHttp = await Repository.GetAsync<List<Country>>("/api/countries/combo");
            if (responseHttp.Error)
            {
                var message = await responseHttp.GetErrorMessageAsync();
                await SweetAlertService.FireAsync("Error", message, SweetAlertIcon.Error);
                return;
            }

            countries = responseHttp.Response;

        }

        private void ShowModal()
        {
            
            var parameters = new ModalParameters()
            .Add(nameof(countries), countries);
            Modal.Show<ProviderCreate>();
            
        }
       
        private async Task DeleteAsync(User people)
        {
            var result = await SweetAlertService.FireAsync(new SweetAlertOptions
            {
                Title = "Confirmación",
                Text = $"¿Esta seguro que quieres borrar el proveedor: ?",
                Icon = SweetAlertIcon.Question,
                ShowCancelButton = true
            });

            var confirm = string.IsNullOrEmpty(result.Value);

            if (confirm)
            {
                return;
            }

            var responseHttp = await Repository.DeleteAsync<Country>($"api/peoples/{people.Id}");
            if (responseHttp.Error)
            {
                if (responseHttp.HttpResponseMessage.StatusCode == System.Net.HttpStatusCode.NotFound)
                {
                    NavigationManager.NavigateTo("/");
                }
                else
                {
                    var mensajeError = await responseHttp.GetErrorMessageAsync();
                    await SweetAlertService.FireAsync("Error", mensajeError, SweetAlertIcon.Error);
                }
                return;
            }

           
            var toast = SweetAlertService.Mixin(new SweetAlertOptions
            {
                Toast = true,
                Position = SweetAlertPosition.BottomEnd,
                ShowConfirmButton = true,
                Timer = 3000
            });
            await toast.FireAsync(icon: SweetAlertIcon.Success, message: "Registro borrado con éxito.");
        }
    }
}