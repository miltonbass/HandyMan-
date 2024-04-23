using Microsoft.AspNetCore.Components;
using HandyMan_.Frontend.Repositories;
using HandyMan_.Shered.Entities;

namespace HandyMan_.Frontend.Pages.Countries
{
    public partial class CountriesIndex
    {
        [Inject] private IRepository Repository { get; set; } = null!;

        public List<Country>? Countries { get; set; }

        protected override async Task OnInitializedAsync()
        {
            var responseHppt = await Repository.GetAsync<List<Country>>("api/countries");
            Countries = responseHppt.Response!;
        }
    }
}
