using CurrieTechnologies.Razor.SweetAlert2;
using HandyMan_.Frontend.Repositories;
using HandyMan_.Shered.Entities;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;

namespace HandyMan_.Frontend.Pages.ServiceOrders
{
    public partial class ServiceOrdersCreate
    {
        private ServiceOrder ServiceOrder { get; set; } = new ServiceOrder();
        private EditContext editContext;

        [Inject] private IRepository Repository { get; set; } = null!;
        [Inject] public SweetAlertService SweetAlertService { get; set; } = null!;
        [Inject] private NavigationManager NavigationManager { get; set; } = null!;

        protected override void OnInitialized()
        {
            editContext = new EditContext(ServiceOrder);
        }

        private async Task CreateAsync()
        {
            var responseHttp = await Repository.PostAsync("/api/serviceorder", ServiceOrder);
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
            await toast.FireAsync(icon: SweetAlertIcon.Success, title: "Registro creado con éxito.");

            Return();
        }

        private void Return()
        {
            NavigationManager.NavigateTo("/serviceorder");
        }
    }
}