using CurrieTechnologies.Razor.SweetAlert2;
using HandyMan_.Frontend.Pages.SubscriptionTypes;
using HandyMan_.Frontend.Repositories;
using HandyMan_.Shared.Entities;
using Microsoft.AspNetCore.Components;
using System.Net;

namespace HandyMan_.Frontend.Pages.SurveyDefinitions
{
    public partial class SurveyDefinitionEdit
{
        private SurveyDefinitionEntity? surveyDefinitionEntity;
        private SurveyDefinitionForm? surveyDefinitionForm;

        [Inject] private IRepository Repository { get; set; } = null!;
        [Inject] private SweetAlertService sweetAlertService { get; set; } = null!;
        [Inject] private NavigationManager navigationManager { get; set; } = null!;

        [EditorRequired, Parameter] public int Id { get; set; }

        protected override async Task OnParametersSetAsync()
        {
            var responseHttp = await Repository.GetAsync<SurveyDefinitionEntity>($"/api/surveyDefinition/{Id}");
            if (responseHttp.Error)
            {
                if (responseHttp.HttpResponseMessage.StatusCode == HttpStatusCode.NotFound)
                {
                    navigationManager.NavigateTo("/surveyDefinition");
                }
                else
                {
                    var messsage = await responseHttp.GetErrorMessageAsync();
                    await sweetAlertService.FireAsync("Error", messsage, SweetAlertIcon.Error);
                }
            }
            else
            {
                surveyDefinitionEntity = responseHttp.Response;
            }
        }

        private async Task EditAsync()
        {
            var responseHttp = await Repository.PutAsync("/api/surveyDefinition", surveyDefinitionEntity);
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

        private void Return()
        {
            surveyDefinitionForm!.FormPostedSuccessfully = true;
            navigationManager.NavigateTo("/surveyDefinitions");
        }
    }
}
