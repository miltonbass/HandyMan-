using Blazored.Modal.Services;
using Blazored.Modal;
using CurrieTechnologies.Razor.SweetAlert2;
using HandyMan_.Frontend.Repositories;
using HandyMan_.Shered.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components;
using HandyMan_.Shered.DTOs;
using Microsoft.AspNetCore.Components.Forms;
using HandyMan_.Shared.Entities;
using System.Net;


namespace HandyMan_.Frontend.Pages.Cart
{
    [Authorize(Roles = "Admin, User")]
    public  partial class Payment
    {
        public List<TemporalOrder>? temporalOrders { get; set; }
        private TemporalOrder? temporalOrder;


        [Inject] private NavigationManager NavigationManager { get; set; } = null!;
        [Inject] private IRepository Repository { get; set; } = null!;
        [Inject] private SweetAlertService SweetAlertService { get; set; } = null!;

        [CascadingParameter] IModalService Modal { get; set; } = default!;

        [CascadingParameter] BlazoredModalInstance BlazoredModal { get; set; } = default!;

        [EditorRequired, Parameter] public int Id { get; set; }

   
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

        private async Task ShowModal(int id = 0)

        {
            CloseModalAsync();
            await Task.Delay(400);
            Modal.Show<PaymentForm>(string.Empty, new ModalParameters().Add("Id", id));

        }


    }
}