using Blazored.Modal.Services;
using HandyMan_.Frontend.Pages.Auth;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components;
using HandyMan_.Shered.DTOs;
using CurrieTechnologies.Razor.SweetAlert2;
using HandyMan_.Frontend.Repositories;
using Orders.Frontend.Services;
using Orders.Shared.DTOs;
using Microsoft.AspNetCore.Components.Forms;

namespace HandyMan_.Frontend.Shared
{
    public partial class AuthLinks
    {
        private EditContext editContext = null!;

        private LoginDTO loginDTO { get; set; } = new LoginDTO();
        [Inject] private IRepository Repository { get; set; } = null!;
        [Inject] private NavigationManager NavigationManager { get; set; } = null!;

        [Inject] private ILoginService LoginService { get; set; } = null!;

        private string? photoUser;
        private bool dialogIsOpen = false;
        

        [CascadingParameter] private Task<AuthenticationState> AuthenticationStateTask { get; set; } = null!;
        [CascadingParameter] IModalService Modal { get; set; } = default!;

        [Inject] private SweetAlertService SweetAlertService { get; set; } = null!;

        protected override void OnInitialized()
        {
            editContext = new(loginDTO);
        }

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

        private async Task LoginAsync()
        {
            
            var responseHttp = await Repository.PostAsync<LoginDTO, TokenDTO>("/api/accounts/Login", loginDTO);
            if (responseHttp.Error)
            {
                var message = await responseHttp.GetErrorMessageAsync();
                await SweetAlertService.FireAsync("Error", message, SweetAlertIcon.Error);
                return;
            }
            dialogIsOpen = false;
            loginDTO = new();
            await LoginService.LoginAsync(responseHttp.Response!.Token);
            NavigationManager.NavigateTo("/");

        }

        private void ShowModal()
        {
            Modal.Show<Login>();
        }

        void OpenDialog()
        {
            dialogIsOpen = true;
        }
    }
}