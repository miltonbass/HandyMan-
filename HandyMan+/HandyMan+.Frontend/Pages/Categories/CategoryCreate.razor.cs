using Blazored.Modal.Services;
using Blazored.Modal;
using CurrieTechnologies.Razor.SweetAlert2;
using HandyMan_.Frontend.Repositories;
using HandyMan_.Frontend.Shared;
using HandyMan_.Shered.Entities;
using Microsoft.AspNetCore.Components;
using System.Runtime.CompilerServices;

namespace HandyMan_.Frontend.Pages.Categories
{
    public partial class CategoryCreate
    {
        private Category Category = new();

        [Inject] private IRepository Repository { get; set; } = null!;
        [Inject] private SweetAlertService sweetAlertService { get; set; } = null!;
        [Inject] private NavigationManager navigationManager { get; set; } = null!;

        private bool loading;

        [CascadingParameter] BlazoredModalInstance BlazoredModal { get; set; } = default!;

        private async Task CreateAsync()
        {
            var responseHttp = await Repository.PostAsync("/api/categories/", Category);
            loading = false;

       
            Category = new();
            await CloseModalAsync();
            var toast = sweetAlertService.Mixin(new SweetAlertOptions
            {
                Toast = true,
                Position = SweetAlertPosition.BottomEnd,
                ShowConfirmButton = true,
                Timer = 3000
            });
            await toast.FireAsync(icon: SweetAlertIcon.Success, message: "Registro creado con éxito.");
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
