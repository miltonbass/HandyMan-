using Blazored.Modal;
using Blazored.Modal.Services;
using CurrieTechnologies.Razor.SweetAlert2;
using HandyMan_.Frontend.Pages.Provider;
using HandyMan_.Frontend.Repositories;
using HandyMan_.Frontend.Shared;
using HandyMan_.Shered.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using System.Reflection;

namespace HandyMan_.Frontend.Pages.Services
{
    [Authorize(Roles = "Admin")]
    public partial class ServicesIndex
    {   

        [Inject] private IRepository Repository { get; set; } = null!;
   
        [Inject] private NavigationManager NavigationManager { get; set; } = null!;
        [Inject] public SweetAlertService SweetAlertService { get; set; } = null!;

        [CascadingParameter] IModalService Modal { get; set; } = default!;


        public List<Service>? Services { get; set; }
        public List<Service>? ListServices { get; set; }

        private List<Category>? categories;
        
        private Service Service { get; set; } = new();
        

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

        private async Task DeleteAsync(Service service)
        {
            var result = await SweetAlertService.FireAsync(new SweetAlertOptions
            {
                Title = "Confirmación",
                Text = $"¿Esta seguro que quieres borrar el país: {service.Name}?",
                Icon = SweetAlertIcon.Question,
                ShowCancelButton = true
            });

            var confirm = string.IsNullOrEmpty(result.Value);

            if (confirm)
            {
                return;
            }

            var responseHttp = await Repository.DeleteAsync<Country>($"api/services/{service.Id}");
            if (responseHttp.Error)
            {
                if (responseHttp.HttpResponseMessage.StatusCode == System.Net.HttpStatusCode.NotFound)
                {
                    NavigationManager.NavigateTo("/");
                }
                else
                {
                    var mensajeError = await responseHttp.GetErrorMessageAsync();
                    await SweetAlertService.FireAsync("Error", mensajeError, SweetAlertIcon.Error);
                }
                return;
            }

           
            var toast = SweetAlertService.Mixin(new SweetAlertOptions
            {
                Toast = true,
                Position = SweetAlertPosition.BottomEnd,
                ShowConfirmButton = true,
                Timer = 3000
            });
            await toast.FireAsync(icon: SweetAlertIcon.Success, message: "Registro borrado con éxito.");
        }


      

        private void ShowModal()
        {
            Modal.Show<ServiceCreate>();
        }

    }
}