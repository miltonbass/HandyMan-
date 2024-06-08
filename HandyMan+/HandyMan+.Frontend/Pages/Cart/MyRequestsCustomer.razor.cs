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
    [Authorize(Roles = "Costumer, User")]
    public partial class MyRequestsCustomer
    {
        private int counter = 0;
        private bool isAuthenticated;
        private bool loading;
        public OrderDTO OrderDTO { get; set; } = new();
        public List<TemporalOrder>? ListTemporalOrder { get; set; }

        

        [Inject] private NavigationManager NavigationManager { get; set; } = null!;
        [Inject] private IRepository Repository { get; set; } = null!;
        [Inject] private SweetAlertService SweetAlertService { get; set; } = null!;

        private int activeTabIndex = 0;
        [CascadingParameter] BlazoredModalInstance BlazoredModal { get; set; } = default!;

        protected override async Task OnInitializedAsync()
        {
            await LoadAllTemporalDataAsync();

        }


        private async Task LoadAllTemporalDataAsync()
        {
            var url = $"api/temporalOrders/GetAllRequest";
            var responseHttp = await Repository.GetAsync<List<TemporalOrder>>(url);
            if (responseHttp.Error)
            {
                var message = await responseHttp.GetErrorMessageAsync();
                await SweetAlertService.FireAsync("Error", message, SweetAlertIcon.Error);
            }
            ListTemporalOrder = responseHttp.Response;
        }

        private string GetButtonText(int tabIndex)
        {
            return tabIndex switch
            {
                0 => "Realizar Pago",
                1 => "Confirmar Agenda",
                2 => "Ver Detalle",
            };
        }

        private List<TemporalOrder> GetFilteredOrders(string status)
        {
            return ListTemporalOrder.Where(order => order.Status == status).ToList();
        }

        private List<TemporalOrder> GetAllTemporalOrder()
        {
            return ListTemporalOrder.ToList();
        }

        public class Order
        {
            public Service Service { get; set; }
            public string Status { get; set; }
        }

        public class Service
        {
            public string Name { get; set; }
            public decimal Price { get; set; }
            public string Photo { get; set; }
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

        private async Task CloseCartModal()
        {
            await BlazoredModal.CloseAsync(ModalResult.Ok());
        }
    }
}