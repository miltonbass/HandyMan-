using Blazored.Modal.Services;
using Blazored.Modal;
using CurrieTechnologies.Razor.SweetAlert2;
using HandyMan_.Frontend.Repositories;
using HandyMan_.Shered.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components;

namespace HandyMan_.Frontend.Pages.Cart
{
    [Authorize(Roles = "Admin, User")]
    public partial class ShowCart
    {
        private int counter = 0;
        private bool isAuthenticated;
        public List<TemporalOrder>? temporalOrders { get; set; }

        [Inject] private NavigationManager NavigationManager { get; set; } = null!;
        [Inject] private IRepository Repository { get; set; } = null!;
        [Inject] private SweetAlertService SweetAlertService { get; set; } = null!;

        [CascadingParameter] BlazoredModalInstance BlazoredModal { get; set; } = default!;

        protected override async Task OnInitializedAsync()
        {
            await LoadAsync();
            await LoadCounterAsync();
        }

        private async Task LoadCounterAsync()
        {
           
            var responseHttp = await Repository.GetAsync<int>("/api/temporalOrders/count");
            if (responseHttp.Error)
            {
                return;
            }
            counter = responseHttp.Response;
        }

        private async Task LoadAsync()
        {
            try
            {
                var responseHppt = await Repository.GetAsync<List<TemporalOrder>>("api/temporalOrders/my");
                temporalOrders = responseHppt.Response!;
                //sumQuantity = temporalOrders.Sum(x => x.Quantity);
                //sumValue = temporalOrders.Sum(x => x.Value);
            }
            catch (Exception ex)
            {
                await SweetAlertService.FireAsync("Error", ex.Message, SweetAlertIcon.Error);
            }
        }

        private async Task CloseCartModal()
        {
            await BlazoredModal.CloseAsync(ModalResult.Ok());
        }

    }
}