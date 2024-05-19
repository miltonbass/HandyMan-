using Blazored.Modal;
using Blazored.Modal.Services;
using CurrieTechnologies.Razor.SweetAlert2;
using HandyMan_.Frontend.Pages.SubscriptionTypes;
using HandyMan_.Frontend.Repositories;
using HandyMan_.Frontend.Shared;
using HandyMan_.Shared.Entities;
using HandyMan_.Shered.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using System.Net;

namespace HandyMan_.Frontend.Pages.ServiceOrders
{
    [Authorize(Roles = "Admin")]
    public partial class ServiceOrdersEdit
    {
        [Parameter] public int id { get; set; }
        private ServiceOrder ServiceOrder { get; set; }
        private EditContext editContext;

        [Inject] private IRepository Repository { get; set; } = null!;
        [Inject] public SweetAlertService SweetAlertService { get; set; } = null!;
        [Inject] private NavigationManager NavigationManager { get; set; } = null!;

        protected override async Task OnInitializedAsync()
        {
           var responseHttp = await Repository.GetAsync<ServiceOrder>($"api/serviceorder/{id}");

            if (responseHttp.Error)
            {
                if (responseHttp.HttpResponseMessage.StatusCode == System.Net.HttpStatusCode.NotFound)
                {
                    NavigationManager.NavigateTo("services");
                }
                else
                {
                    var messageError = await responseHttp.GetErrorMessageAsync();
                    await SweetAlertService.FireAsync("Error", messageError, SweetAlertIcon.Error);
                }
            }
            else
            {
                ServiceOrder = responseHttp.Response;
            }
            if (ServiceOrder != null)
            {
                editContext = new EditContext(ServiceOrder);
            }
        }

        private async Task UpdateAsync()
        {
            var responseHttp = await Repository.PutAsync($"/api/serviceorder/{id}", ServiceOrder);
            if (responseHttp.Error)
            {
                var message = await responseHttp.GetErrorMessageAsync();
                await SweetAlertService.FireAsync("Error", message);
                return;
            }

            Console.WriteLine($"Estado: {ServiceOrder.State}");
            Console.WriteLine($"Fecha Ejecución: {ServiceOrder.ExecutionDate}");
            Console.WriteLine($"Detalle: {ServiceOrder.Detail}");

            var toast = SweetAlertService.Mixin(new SweetAlertOptions
            {
                Toast = true,
                Position = SweetAlertPosition.BottomEnd,
                ShowConfirmButton = true,
                Timer = 3000
            });
            await toast.FireAsync(icon: SweetAlertIcon.Success, title: "Registro actualizado con éxito.");

            Return();
        }

        private void Return()
        {
            NavigationManager.NavigateTo("/serviceorder");
        }
    }
}