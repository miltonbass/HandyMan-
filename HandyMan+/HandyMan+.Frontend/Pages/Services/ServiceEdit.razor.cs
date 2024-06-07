using Blazored.Modal.Services;
using Blazored.Modal;
using CurrieTechnologies.Razor.SweetAlert2;
using HandyMan_.Frontend.Repositories;
using HandyMan_.Shered.Entities;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;

namespace HandyMan_.Frontend.Pages.Services
{
    public partial class ServiceEdit
    {
        private Service Service = new Service();
        private EditContext editContext = null!;
        [Inject] private IRepository Repository { get; set; } = null!;
        [Inject] private SweetAlertService SweetAlertService { get; set; } = null!;
        [Inject] private NavigationManager NavigationManager { get; set; } = null!;

        [EditorRequired, Parameter] public int Id { get; set; }
        [CascadingParameter] BlazoredModalInstance BlazoredModal { get; set; } = default!;
        private List<Category>? categories;
        private bool loading;
        private string? imageUrl;


        private async Task LoadCategoriesAsync()
        {
            var responseHttp = await Repository.GetAsync<List<Category>>("/api/categories/combo");
            if (responseHttp.Error)
            {
                var message = await responseHttp.GetErrorMessageAsync();
                await SweetAlertService.FireAsync("Error", message, SweetAlertIcon.Error);
                return;
            }

            categories = responseHttp.Response;
        }

        protected async override Task OnParametersSetAsync()
        {

            editContext = new(Service!);

            await LoadCategoriesAsync();
            

            var responseHttp = await Repository.GetAsync<Service>($"api/services/{Id}");

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
                Service = responseHttp.Response;
            }
        }

        private void ImageSelected(string imagenBase64)
        {
            Service!.Photo = imagenBase64;
            imageUrl = null;
        }

        private async Task CloseModalAsync()
        {

            await BlazoredModal.CloseAsync(ModalResult.Ok());
        }

        private async Task EditAsync()
        {
            
           // var responseHttp = await Repository.PutAsync("api/services", Service);
            var responseHttp = await Repository.PutAsync($"api/services/{Service.Id}", Service);


            loading = false;

            if (responseHttp.Error)
            {
                var mensajeError = await responseHttp.GetErrorMessageAsync();
                await SweetAlertService.FireAsync("Error", mensajeError, SweetAlertIcon.Error);
                return;
            }
            await CloseModalAsync();
            
            var toast = SweetAlertService.Mixin(new SweetAlertOptions
            {
                Toast = true,
                Position = SweetAlertPosition.BottomEnd,
                ShowConfirmButton = true,
                Timer = 3000
            });
            await toast.FireAsync(icon: SweetAlertIcon.Success, message: "Cambios guardados con éxito.");
            Return();
        }

        private void Return()
        {
            NavigationManager.NavigateTo("/services");
        }
    }
}