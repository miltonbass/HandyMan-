using CurrieTechnologies.Razor.SweetAlert2;
using HandyMan_.Shared.Entities;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Components.Routing;
using Microsoft.AspNetCore.Components;

namespace HandyMan_.Frontend.Pages.SurveyDefinitions
{
    public partial class SurveyDefinitionForm
{
        private EditContext editContext = null!;

        private string newOption = string.Empty;

        [EditorRequired, Parameter] public SurveyDefinitionEntity SurveyDefinitionEntity { get; set; } = null!;
        [EditorRequired, Parameter] public EventCallback OnValidSubmit { get; set; }
        [EditorRequired, Parameter] public EventCallback ReturnAction { get; set; }
        [Inject] public SweetAlertService SweetAlertService { get; set; } = null!;
        public bool FormPostedSuccessfully { get; set; }

        List<string> SurveyDefinitionTypeEnumToList = QuestionTypeEnumToList.GetList();

        List<string> UserTypesEnumToList = UserTypeEnumToList.GetList();

        protected override void OnInitialized()
        {
            editContext = new(SurveyDefinitionEntity);
        }

        private void RemoveOption(string option)
        {
            SurveyDefinitionEntity.Options.Remove(option);
        }

        private void AddOption()
        {
            if (!string.IsNullOrWhiteSpace(newOption) && !SurveyDefinitionEntity.Options.Contains(newOption))
            {
                SurveyDefinitionEntity.Options.Add(newOption);
                newOption = string.Empty;
            }
        }


        private async Task OnBeforeInternalNavigation(LocationChangingContext context)
        {
            var formWasEdited = editContext.IsModified();
            if (!formWasEdited || FormPostedSuccessfully)
            {
                return;
            }

            var result = await SweetAlertService.FireAsync(new SweetAlertOptions
            {
                Title = "Confirmación",
                Text = "¿Desea abandonar la página y perder los cambios?",
                Icon = SweetAlertIcon.Question,
                ShowCancelButton = true,
            });
            var confirm = !string.IsNullOrEmpty(result.Value);
            if (confirm)
            {
                return;
            }

            context.PreventNavigation();
        }



    }
}
