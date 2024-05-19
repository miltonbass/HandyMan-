using CurrieTechnologies.Razor.SweetAlert2;
using HandyMan_.Frontend.Repositories;
using HandyMan_.Shered.Entities;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;

namespace HandyMan_.Frontend.Pages.ServiceOrders
{
    public partial class ServiceOrdersCreate
    {
        private EditContext editContext = null!;
        [Inject] private IRepository Repository { get; set; } = null!;

        private ServiceOrder? ServiceOrder { get; set; } = new ServiceOrder();
        [Inject] public SweetAlertService SweetAlertService { get; set; } = null!;
        [Inject] private NavigationManager NavigationManager { get; set; } = null!;



        protected override async Task OnInitializedAsync()
        {
            editContext = new(ServiceOrder!);

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

            Return();
            var toast = SweetAlertService.Mixin(new SweetAlertOptions
            {
                Toast = true,
                Position = SweetAlertPosition.BottomEnd,
                ShowConfirmButton = true,
                Timer = 3000
            });
            await toast.FireAsync(icon: SweetAlertIcon.Success, message: "Registro creado con éxito.");
        }

        private void Return()
        {
            NavigationManager.NavigateTo("/serviceorder");
        }
    }
}