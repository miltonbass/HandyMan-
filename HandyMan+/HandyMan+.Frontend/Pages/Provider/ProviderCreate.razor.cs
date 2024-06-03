using Blazored.Modal.Services;
using Blazored.Modal;
using CurrieTechnologies.Razor.SweetAlert2;
using HandyMan_.Frontend.Repositories;
using HandyMan_.Shared.DTOs;
using HandyMan_.Shared.Enums;
using HandyMan_.Shered.Entities;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using System;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace HandyMan_.Frontend.Pages.Provider
{
    public partial class ProviderCreate
    {

        
        private UserDTO userDTO = new();
        public List<Country>? countries;
        private List<State>? states;
        private List<City>? cities;
        private bool loading;
        private string? imageUrl;
        private EditContext editContext = null!;
        [Inject] private IRepository Repository { get; set; } = null!;
        [Inject] public SweetAlertService SweetAlertService { get; set; } = null!;
        [Inject] private NavigationManager NavigationManager { get; set; } = null!;
        private IEnumerable<UserType> UserType => Enum.GetValues(typeof(UserType)).Cast<UserType>();
        [CascadingParameter] BlazoredModalInstance BlazoredModal { get; set; } = default!;

        protected override async Task OnInitializedAsync()
        {
            await LoadCountriesAsync();
        }

        private async Task CloseModalAsync()
        {
           
            await BlazoredModal.CloseAsync(ModalResult.Ok());
        }

        private void ImageSelected(string imagenBase64)
        {
            userDTO.Photo = imagenBase64;
            imageUrl = null;
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

        private async Task CountryChangedAsync(ChangeEventArgs e)
        {
            var selectedCountry = Convert.ToInt32(e.Value!);
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

        private async Task StateChangedAsync(ChangeEventArgs e)
        {
            var selectedState = Convert.ToInt32(e.Value!);
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

        private async Task CreteUserAsync()
        {
            userDTO.UserName = userDTO.Email;
            loading = true;
            var responseHttp = await Repository.PostAsync<UserDTO>("/api/accounts/CreateUser", userDTO);
            loading = false;

            if (responseHttp.Error)
            {
                var message = await responseHttp.GetErrorMessageAsync();
                await SweetAlertService.FireAsync("Error", message, SweetAlertIcon.Error);
                return;
            }

            await BlazoredModal.CloseAsync(ModalResult.Ok());
            userDTO = new();
            await SweetAlertService.FireAsync("Confirmación", "Cuenta ha sido creada con éxito. Se te ha enviado un correo electrónico con las instrucciones para activar el usuario.", SweetAlertIcon.Info);
            NavigationManager.NavigateTo("/");
        }

    }
}