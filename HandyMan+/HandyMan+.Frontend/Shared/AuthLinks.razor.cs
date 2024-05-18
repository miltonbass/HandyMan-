using Blazored.Modal.Services;
using HandyMan_.Frontend.Pages.Auth;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components;
using HandyMan_.Shered.DTOs;
using CurrieTechnologies.Razor.SweetAlert2;
using HandyMan_.Frontend.Repositories;
using HandyMan_.Frontend.Services;
using HandyMan_.Shared.DTOs;
using HandyMan_.Shered.Entities;
using HandyMan_.Shared.Enums;
using MatBlazor;
using Microsoft.AspNetCore.Components.Web;

namespace HandyMan_.Frontend.Shared
{
    public partial class AuthLinks
    {
        MatChipSet chipset = null;
        private string CountryId = null!;
        private string StateId = null!;
        private UserDTO userDTO { get; set; } = new UserDTO();
        private List<Country>? countries;
        private List<State>? states;
        private List<City>? cities;
        private string? imageUrl;
        private bool loading;

        private LoginDTO loginDTO = new();
        [Inject] private IRepository Repository { get; set; } = null!;
        [Inject] private NavigationManager NavigationManager { get; set; } = null!;

        [Inject] private ILoginService LoginService { get; set; } = null!;

        private string? photoUser;
        private bool dialogIsOpen = false;
        private bool dialogIsOpenRegister = false;
        private bool dialogIsOpenResendEmail = false;
        private bool dialogIsOpenEditUser = false;

        [CascadingParameter] private Task<AuthenticationState> AuthenticationStateTask { get; set; } = null!;
        [CascadingParameter] IModalService Modal { get; set; } = default!;

        [Inject] private SweetAlertService SweetAlertService { get; set; } = null!;

        protected override async Task OnParametersSetAsync()
        {
            var authenticationState = await AuthenticationStateTask;
            var claims = authenticationState.User.Claims.ToList();
            var photoClaim = claims.FirstOrDefault(x => x.Type == "Photo");
            if (photoClaim is not null)
            {
                photoUser = photoClaim.Value;
            }
        }

        ForwardRef buttonForwardRef = new ForwardRef();
        BaseMatMenu Menu;

        public void OnClick(MouseEventArgs e)
        {
            this.Menu.OpenAsync();
        }

        protected override async Task OnInitializedAsync()
        {
            await LoadCountriesAsync();
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

        private async Task LoginAsync()
        {
            
            var responseHttp = await Repository.PostAsync<LoginDTO, TokenDTO>("/api/accounts/Login", loginDTO);
            if (responseHttp.Error)
            {
                var message = await responseHttp.GetErrorMessageAsync();
                await SweetAlertService.FireAsync("Error", message, SweetAlertIcon.Error);
                return;
            }
            dialogIsOpen = false;
            loginDTO = new();
            await LoginService.LoginAsync(responseHttp.Response!.Token);
            NavigationManager.NavigateTo("/");

        }

        void OpenDialog()
        {
            dialogIsOpen = true;
        }
        void OpenDialogRegister()
        {
            dialogIsOpenRegister = true;

        }

        private async Task CreteUserAsync()
        {
            userDTO.UserName = userDTO.Email;
            userDTO.UserType = UserType.Costumer;
            loading = true;
            var responseHttp = await Repository.PostAsync<UserDTO>("/api/accounts/CreateUser", userDTO);
            loading = false;

            if (responseHttp.Error)
            {
                var message = await responseHttp.GetErrorMessageAsync();
                await SweetAlertService.FireAsync("Error", message, SweetAlertIcon.Error);
                return;
            }

            await SweetAlertService.FireAsync("Confirmación", "Su cuenta ha sido creada con éxito. Se te ha enviado un correo electrónico con las instrucciones para activar tu usuario.", SweetAlertIcon.Info);
            NavigationManager.NavigateTo("/");
        }



    private async Task CountryChangedAsync(string e)
        {
            var selectedCountry = Convert.ToInt32(e);
            states = null;
            cities = null;
            userDTO.CityId = 0;
            await LoadStatesAsyn(selectedCountry);
        }
        private async Task LoadStatesAsyn(int countryId)
        {
            var responseHttp = await Repository.GetAsync<List<State>>($"/api/states/combo/{countryId}");
            if (responseHttp.Error)
            {
                var message = await responseHttp.GetErrorMessageAsync();
                await SweetAlertService.FireAsync("Error", message, SweetAlertIcon.Error);
                return;
            }

            states = responseHttp.Response;
        }
        private async Task StateChangedAsync(string e)
        {
            var selectedState = Convert.ToInt32(e);
            cities = null;
            userDTO.CityId = 0;
            await LoadCitiesAsyn(selectedState);
        }

        private async Task LoadCitiesAsyn(int stateId)
        {
            var responseHttp = await Repository.GetAsync<List<City>>($"/api/cities/combo/{stateId}");
            if (responseHttp.Error)
            {
                var message = await responseHttp.GetErrorMessageAsync();
                await SweetAlertService.FireAsync("Error", message, SweetAlertIcon.Error);
                return;
            }

            cities = responseHttp.Response;
        }


        void CloseDialog() {
            loginDTO = new();
            dialogIsOpen = false;
        }

        void CloseDialogRegister() {
            dialogIsOpenRegister = false;
            dialogIsOpenResendEmail = false;
            userDTO = new();
        }

        private void ImageSelected(string imagenBase64)
        {
            userDTO.Photo = imagenBase64;
            imageUrl = null;
        }

        void reSend() {
            NavigationManager.NavigateTo("/ResendToken");
            return;
        }

        void registerDialog() {
            dialogIsOpen = false;
            dialogIsOpenRegister = true;
            return;
        }


        void OpenDialogEditUser()
        {
            
            dialogIsOpenEditUser = true;
        }

        private void ShowModal()
        {
            Modal.Show<Login>();
        }

        private void ShowModalRegister()
        {
            Modal.Show<Register>();
        }

        private void ShowModalEditUser()
        {
            Modal.Show<EditUser>();
        }
        private void ShowChangePassword()
        {
            Modal.Show<ChangePassword>();
        }
    }
}