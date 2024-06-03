using Blazored.Modal.Services;
using CurrieTechnologies.Razor.SweetAlert2;
using HandyMan_.Frontend.Repositories;
using HandyMan_.Shered.Entities;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using System.Diagnostics.Metrics;

namespace HandyMan_.Frontend.Pages
{
    public partial class Home
    {
        private int counter = 0;
        private bool isAuthenticated;

        public List<Service>? ListServices { get; set; }
        [Inject] private IRepository Repository { get; set; } = null!;
        [Inject] private NavigationManager NavigationManager { get; set; } = null!;

        [Inject] public SweetAlertService SweetAlertService { get; set; } = null!;

        [CascadingParameter] private Task<AuthenticationState> authenticationStateTask { get; set; } = null!;
        [CascadingParameter] private IModalService Modal { get; set; } = default!;

        protected override async Task OnInitializedAsync()
        {
            await CheckIsAuthenticatedAsync();
            await LoadCounterAsync();
            await LoadAllServiceAsync();
        }

        private async Task CheckIsAuthenticatedAsync()
        {
            var authenticationState = await authenticationStateTask;
            isAuthenticated = authenticationState.User.Identity!.IsAuthenticated;
        }
        private async Task LoadCounterAsync()
        {
            if (!isAuthenticated)
            {
                return;
            }

            var responseHttp = await Repository.GetAsync<int>("/api/temporalOrders/count");
            if (responseHttp.Error)
            {
                return;
            }
            counter = responseHttp.Response;
        }
        private async Task LoadAllServiceAsync()
        {
            var url = $"api/services/GetAllServices";
            var responseHttp = await Repository.GetAsync<List<Service>>(url);
            if (responseHttp.Error)
            {
                var message = await responseHttp.GetErrorMessageAsync();
                await SweetAlertService.FireAsync("Error", message, SweetAlertIcon.Error);
            }
            ListServices = responseHttp.Response;
        }

        private async Task AddToCartAsync(int productId)
        {
        }
    }
}