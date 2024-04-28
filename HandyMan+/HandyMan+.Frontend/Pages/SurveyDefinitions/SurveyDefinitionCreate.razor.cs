﻿using CurrieTechnologies.Razor.SweetAlert2;
using HandyMan_.Frontend.Repositories;
using HandyMan_.Shared.Entities;
using Microsoft.AspNetCore.Components;

namespace HandyMan_.Frontend.Pages.SurveyDefinitions
{
    public partial class SurveyDefinitionCreate
{
        private SurveyDefinitionEntity surveyDefinitionEntity = new();
        private SurveyDefinitionForm? surveyDefinitionForm;
        [Inject] private IRepository Repository { get; set; } = null!;
        [Inject] private SweetAlertService sweetAlertService { get; set; } = null!;
        [Inject] private NavigationManager navigationManager { get; set; } = null!;

        private async Task CreateAsync()
        {

            var responseHttp = await Repository.PostAsync("/api/surveyDefinition", surveyDefinitionEntity);
            if (responseHttp.Error)
            {
                var message = await responseHttp.GetErrorMessageAsync();
                await sweetAlertService.FireAsync("Error", message, SweetAlertIcon.Error);
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
            await toast.FireAsync(icon: SweetAlertIcon.Success, message: "Registro creado con éxito.");
        }

        private void Return()
        {
            surveyDefinitionForm!.FormPostedSuccessfully = true;
            navigationManager.NavigateTo("/surveyDefinitions");
        }
    }
}
