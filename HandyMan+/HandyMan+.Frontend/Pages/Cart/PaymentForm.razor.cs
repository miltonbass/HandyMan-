using Blazored.Modal.Services;
using Blazored.Modal;
using CurrieTechnologies.Razor.SweetAlert2;
using HandyMan_.Frontend.Repositories;
using HandyMan_.Shered.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components;
using Microsoft.EntityFrameworkCore;
using HandyMan_.Shered.DTOs;
using HandyMan_.Shered.Enums;
namespace HandyMan_.Frontend.Pages.Cart

{
    [Authorize(Roles = "Admin, User")]
    public partial class PaymentForm
    {
        public List<TemporalOrder>? temporalOrders { get; set; }
        private TemporalOrder? temporalOrder;
        [Parameter] public int OrderId { get; set; }

        [Inject] private NavigationManager NavigationManager { get; set; } = null!;
        [Inject] private IRepository Repository { get; set; } = null!;

        [CascadingParameter] IModalService Modal { get; set; } = default!;

        [CascadingParameter] BlazoredModalInstance BlazoredModal { get; set; } = default!;

        [EditorRequired, Parameter] public int Id { get; set; }

        [Inject] private SweetAlertService SweetAlertService { get; set; } = null!;


        protected async override Task OnParametersSetAsync()
        {


            var responseHttp = await Repository.GetAsync<TemporalOrder>($"api/temporalOrders/{Id}");


            if (responseHttp.Error)
            {
                if (responseHttp.HttpResponseMessage.StatusCode == System.Net.HttpStatusCode.NotFound)
                {
                    NavigationManager.NavigateTo("/");
                }
                else
                {
                    var messageError = await responseHttp.GetErrorMessageAsync();
                    await SweetAlertService.FireAsync("Error", messageError, SweetAlertIcon.Error);
                }
            }
            else
            {
                temporalOrder = responseHttp.Response;
            }



        }

        private async Task CloseModalAsync()
        {
            await BlazoredModal.CloseAsync(ModalResult.Ok());

        }



        private async Task PaymentService(int id)
        {
            var status = "Pago";
            await ModifyTemporalOrder(id, status);
        }

        private async Task ModifyTemporalOrder(int id, string status)
        {
            var result = await SweetAlertService.FireAsync(new SweetAlertOptions
            {
                Title = "Confirmación",
                Text = $"¿Está seguro que quieres confirmar el pago?",
                Icon = SweetAlertIcon.Question,
                ShowCancelButton = true
            });

            var confirm = result.IsConfirmed;
            if (!confirm)
            {
                return;
            }

            var temporalOrderDTO = new TemporalOrderDTO
            {
                Id = id,
                Status = status
            };


            var responseHttp = await Repository.PutAsync("api/temporalorder", temporalOrderDTO);
            if (responseHttp.Error)
            {
                var mensajeError = await responseHttp.GetErrorMessageAsync();
                await SweetAlertService.FireAsync("Error", mensajeError, SweetAlertIcon.Error);
                return;
            }

            NavigationManager.NavigateTo("/temporalorder");
        }

        public void SubmitPayment()
        {
            SweetAlertService.FireAsync("Confirmación", "Su pago se esta procesando sera dirigido a sus servicios.", SweetAlertIcon.Info);
            NavigationManager.NavigateTo("/MyRequestCustomer");
        }




    }
}




