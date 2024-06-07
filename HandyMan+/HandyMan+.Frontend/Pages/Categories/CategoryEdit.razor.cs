using Blazored.Modal.Services;
using Blazored.Modal;
using CurrieTechnologies.Razor.SweetAlert2;
using HandyMan_.Frontend.Repositories;
using HandyMan_.Frontend.Shared;
using HandyMan_.Shered.Entities;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using System.Net;

namespace HandyMan_.Frontend.Pages.Categories
{
    public partial class CategoryEdit
    {
        //private Category? category;
        private Category Category = new Category();
        private EditContext editContext = null!;

        [Inject] private IRepository Repository { get; set; } = null!;
        [Inject] private NavigationManager navigationManager { get; set; } = null!;

        [Inject] private SweetAlertService sweetAlertService { get; set; } = null!;

        private bool loading;

        [EditorRequired, Parameter] public int Id { get; set; }
        [CascadingParameter] BlazoredModalInstance BlazoredModal { get; set; } = default!;

        protected override async Task OnParametersSetAsync()
        {
            editContext = new(Category!);


            var responseHttp = await Repository.GetAsync<Category>($"api/categories/{Id}");

            if (responseHttp.Error)
            {
                if (responseHttp.HttpResponseMessage.StatusCode == System.Net.HttpStatusCode.NotFound)
                {
                    navigationManager.NavigateTo("categories");
                }
                else
                {
                    var messageError = await responseHttp.GetErrorMessageAsync();
                    await sweetAlertService.FireAsync("Error", messageError, SweetAlertIcon.Error);
                }
            }
            else
            {
                Category = responseHttp.Response;
            }
        }

        private async Task EditAsync()
        {
            
            var responseHttp = await Repository.PutAsync("/api/categories", Category);
            loading = false;
            if (responseHttp.Error)
            {
                var message = await responseHttp.GetErrorMessageAsync();
                await sweetAlertService.FireAsync("Error", message);
                return;
            }

            Return();
            var toast = sweetAlertService.Mixin(new SweetAlertOptions
            {
                Toast = true,
                Position = SweetAlertPosition.BottomEnd,
                ShowConfirmButton = true,
                Timer = 3000
            });
            await toast.FireAsync(icon: SweetAlertIcon.Success, message: "Cambios guardados con éxito.");
        }

        private async Task CloseModalAsync()
        {

            await BlazoredModal.CloseAsync(ModalResult.Ok());
        }

        private void Return()
        {
            navigationManager.NavigateTo("/categories");
        }
    }
}
