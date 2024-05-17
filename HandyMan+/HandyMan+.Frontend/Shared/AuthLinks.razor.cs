using Blazored.Modal.Services;
using HandyMan_.Frontend.Pages.Auth;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components;

namespace HandyMan_.Frontend.Shared
{
    public partial class AuthLinks
    {
        private string? photoUser;
        private bool dialogIsOpen = false;
        private string name = null;
        private string animal = null;
        private string dialogAnimal = null;

        [CascadingParameter] private Task<AuthenticationState> AuthenticationStateTask { get; set; } = null!;
        [CascadingParameter] IModalService Modal { get; set; } = default!;


        protected override async Task OnParametersSetAsync()
        {
            var authenticationState = await AuthenticationStateTask;
            var claims = authenticationState.User.Claims.ToList();
            var photoClaim = claims.FirstOrDefault(x => x.Type == "Photo");
            if (photoClaim is not null)
            {
                photoUser = photoClaim.Value;
            }
        }

        private void ShowModal()
        {
            Modal.Show<Login>();
        }

        void OpenDialog()
        {
            dialogAnimal = null;
            dialogIsOpen = true;
        }
    }
}