using Blazored.Modal.Services;
using Blazored.Modal;
using CurrieTechnologies.Razor.SweetAlert2;
using HandyMan_.Frontend.Repositories;
using HandyMan_.Shered.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components;
using HandyMan_.Shered.DTOs;

namespace HandyMan_.Frontend.Pages.Cart
{
    [Authorize(Roles = "Admin, User")]
    public partial class ShowCart
    {
        private int counter = 0;
        private bool isAuthenticated;
        private bool loading;
        public OrderDTO OrderDTO { get; set; } = new();
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
            loading = true;

            var responseHttp = await Repository.GetAsync<int>("/api/temporalOrders/count");
            loading = false;
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

        private async Task ConfirmOrderAsync()
        {
            var result = await SweetAlertService.FireAsync(new SweetAlertOptions
            {
                Title = "Confirmación",
                Text = "¿Esta seguro que quieres confirmar el pedido?",
                Icon = SweetAlertIcon.Question,
                ShowCancelButton = true
            });

            var confirm = string.IsNullOrEmpty(result.Value);
            if (confirm)
            {
                return;
            }

            var httpActionResponse = await Repository.PostAsync("/api/orders", OrderDTO);
            if (httpActionResponse.Error)
            {
                var message = await httpActionResponse.GetErrorMessageAsync();
                await SweetAlertService.FireAsync("Error", message, SweetAlertIcon.Error);
                return;
            }

            CloseCartModal();
            await SweetAlertService.FireAsync("Confirmación", "Su peidido ha sido confirmado. En pronto se contactarán para materializar los servicios que contrató, muchas gracias.", SweetAlertIcon.Info);
            //NavigationManager.NavigateTo("/Cart/OrderConfirmed");
        }

        private async Task Delete(int temporalOrderId)
        {
            var result = await SweetAlertService.FireAsync(new SweetAlertOptions
            {
                Title = "Confirmación",
                Text = "¿Esta seguro que quieres borrar el registro?",
                Icon = SweetAlertIcon.Question,
                ShowCancelButton = true
            });

            var confirm = string.IsNullOrEmpty(result.Value);

            if (confirm)
            {
                return;
            }

            var responseHttp = await Repository.DeleteAsync<TemporalOrder>($"api/temporalOrders/{temporalOrderId}");

            if (responseHttp.Error)
            {
                if (responseHttp.HttpResponseMessage.StatusCode == System.Net.HttpStatusCode.NotFound)
                {
                    NavigationManager.NavigateTo("/");
                    return;
                }

                var mensajeError = await responseHttp.GetErrorMessageAsync();
                await SweetAlertService.FireAsync("Error", mensajeError, SweetAlertIcon.Error);
                return;
            }

            await LoadAsync();
            var toast = SweetAlertService.Mixin(new SweetAlertOptions
            {
                Toast = true,
                Position = SweetAlertPosition.BottomEnd,
                ShowConfirmButton = false,
                Timer = 3000
            });
            await toast.FireAsync(icon: SweetAlertIcon.Success, message: "Producto eliminado del carro de compras.");
        }
        private async Task CloseCartModal()
        {
            await BlazoredModal.CloseAsync(ModalResult.Ok());
        }
    }
}