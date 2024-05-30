using CurrieTechnologies.Razor.SweetAlert2;
using HandyMan_.Frontend.Repositories;
using HandyMan_.Shered.Entities;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Components;

namespace HandyMan_.Frontend.Pages.Provider
{
    public partial class ProviderEdit
    {
        private EditContext editContext = null!;
        [Inject] private IRepository Repository { get; set; } = null!;

        
        [Inject] public SweetAlertService SweetAlertService { get; set; } = null!;
        [Inject] private NavigationManager NavigationManager { get; set; } = null!;

        [EditorRequired, Parameter] public int Id { get; set; }

        

        private List<Country>? countries;
        private List<State>? states;
        private List<City>? cities;

        protected async override Task OnParametersSetAsync()
        {
           

        }

      
     
    

       

        
        private void Return()
        {
            NavigationManager.NavigateTo("/provider");
        }
    }
}