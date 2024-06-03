using CurrieTechnologies.Razor.SweetAlert2;
using HandyMan_.Frontend.Repositories;
using HandyMan_.Shered.Entities;
using Microsoft.AspNetCore.Components;

namespace HandyMan_.Frontend.Pages
{
    public partial class Home
    {
        public List<Service>? ListServices { get; set; }
        [Inject] private IRepository Repository { get; set; } = null!;
        [Inject] private NavigationManager NavigationManager { get; set; } = null!;

        [Inject] public SweetAlertService SweetAlertService { get; set; } = null!;

        protected override async Task OnInitializedAsync()
        {
            await LoadAllServiceAsync();

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
    }
}