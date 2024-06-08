using Blazored.Modal.Services;
using Blazored.Modal;
using CurrieTechnologies.Razor.SweetAlert2;
using HandyMan_.Frontend.Pages.Auth;
using HandyMan_.Frontend.Repositories;
using HandyMan_.Frontend.Services;
using HandyMan_.Shared.DTOs;
using HandyMan_.Shered.DTOs;
using Microsoft.AspNetCore.Components;
using MatBlazor;
using MudBlazor;
using HandyMan_.Shared.Entities;
using HandyMan_.Shared.Enums;
using HandyMan_.Frontend.Pages.SurveyDefinitions;

public class DynamicSurveyParams
{
    public required string userType { get; set; }
    public required string userId { get; set; }
}


namespace HandyMan_.Frontend.Pages.DynamicSurvey
{
    public partial class DynamicSurveyPage
    {
        [Parameter] public DynamicSurveyParams dynamicSurveyParams { get; set; }

        [Inject] private NavigationManager navigationManager { get; set; } = null!;
        [Inject] private SweetAlertService sweetAlertService { get; set; } = null!;
        [Inject] private IRepository Repository { get; set; } = null!;
        [CascadingParameter] private BlazoredModalInstance BlazoredModal { get; set; } = default!;
        [CascadingParameter] private IModalService Modal { get; set; } = default!;

        public ICollection<SurveyDefinitionEntity>? SurveyDefinitionEntities { get; set; }

        private SurveyResponseDTO surveyResponseDTO = new();

        public List<AnswersDTO> ConvertSurveyDefinitionsToAnswersDTO(ICollection<SurveyDefinitionEntity> surveyDefinitions)
        {
            return surveyDefinitions.Select(survey => new AnswersDTO
            {
                Id = survey.Id,
                QuestionType = survey.QuestionType,
                SelectedOptions = survey.SelectedOptions,
                StarRating = survey.StarRating,
                Comment = survey.Answer,
                BooleanAnswer = survey.Recommend,
                UserType = survey.UserType,
                Title = survey.Title,
                Description = survey.Description
            }).ToList();
        }

        protected override async Task OnInitializedAsync()
        {
            await LoadListAsync();
        }

        private async Task<bool> LoadListAsync()
        {

            var url = $"api/surveyDefinition/combo";
            var responseHttp = await Repository.GetAsync<List<SurveyDefinitionEntity>>(url);
            if (responseHttp.Error)
            {
                var message = await responseHttp.GetErrorMessageAsync();
                await sweetAlertService.FireAsync("Error", message, SweetAlertIcon.Error);
                return false;
            }

            SurveyDefinitionEntities = responseHttp.Response?.Where(e => e.UserType == dynamicSurveyParams.userType).ToList();

            return true;
        }

        private async Task SubmitRatingAsync()
        {

            surveyResponseDTO.UserId = dynamicSurveyParams.userId;
            surveyResponseDTO.Responses = [];

            var responseHttp = await Repository.PostAsync("/api/surveyResponses", surveyResponseDTO);
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
            await toast.FireAsync(icon: SweetAlertIcon.Success, message: "Calificación enviada con éxito.");
        }

        private void Return()
        {
            navigationManager.NavigateTo("/surveyDefinitions");
        }
   
    


    private RenderFragment RenderQuestion(SurveyDefinitionEntity question) => builder =>
  {
      var seq = 0;
      switch (question.QuestionType)
      {
          case "StarRange":
              builder.OpenComponent<MudRating>(seq++);
              builder.AddAttribute(seq++, "SelectedValue", question.StarRating);
              builder.AddAttribute(seq++, "Size", Size.Large);
              builder.AddAttribute(seq++, "SelectedValueChanged", EventCallback.Factory.Create<int>(this, value => question.StarRating = value));
              builder.CloseComponent();
              break;

          case "Comment":
              builder.OpenComponent<MudTextField<string>>(seq++);

              builder.AddAttribute(seq++, "Lines", 5);
              builder.AddAttribute(seq++, "Placeholder", "Escribe aquí...");
              builder.AddAttribute(seq++, "Class", "my-custom-textfield");
              builder.AddAttribute(seq++, "Value", question.Answer);
              builder.AddAttribute(seq++, "ValueChanged", EventCallback.Factory.Create<string>(this, value => question.Answer = value));
              builder.CloseComponent();
              break;

          case "TrueFalse":
              builder.OpenComponent<MudRadioGroup<bool>>(seq++);
              builder.AddAttribute(seq++, "Value", question.Recommend);
              builder.AddAttribute(seq++, "ValueChanged", EventCallback.Factory.Create<bool>(this, value => question.Recommend = value));
              builder.AddAttribute(seq++, "ChildContent", (RenderFragment)(builder2 =>
              {
                  builder2.OpenComponent<MudRadio<bool>>(seq++);
                  builder2.AddAttribute(seq++, "Value", true);
                  builder2.AddAttribute(seq++, "Label", "Sí");
                  builder2.AddAttribute(seq++, "ChildContent", (RenderFragment)(builder3 =>
                  {
                      builder3.AddContent(seq++, "Sí");
                  }));
                  builder2.CloseComponent();

                  builder2.OpenComponent<MudRadio<bool>>(seq++);
                  builder2.AddAttribute(seq++, "Value", false);
                  builder2.AddAttribute(seq++, "Label", "No");
                  builder2.AddAttribute(seq++, "ChildContent", (RenderFragment)(builder3 =>
                  {
                      builder3.AddContent(seq++, "No");
                  }));
                  builder2.CloseComponent();
              }));
              builder.CloseComponent();
              break;


          case "MultipleChoice":
              builder.OpenElement(seq++, "div");
              foreach (var option in question.Options)
              {
                  builder.OpenComponent<MudCheckBox<bool>>(seq++);
                  builder.AddAttribute(seq++, "Label", option);
                  builder.AddAttribute(seq++, "Checked", question.SelectedOptions.Contains(option));
                  builder.AddAttribute(seq++, "CheckedChanged", EventCallback.Factory.Create<bool>(this, isChecked =>
                  {
                      if (isChecked)
                      {
                          if (!question.SelectedOptions.Contains(option))
                          {
                              question.SelectedOptions.Add(option);
                          }
                      }
                      else
                      {
                          if (question.SelectedOptions.Contains(option))
                          {
                              question.SelectedOptions.Remove(option);
                          }
                      }
                  }));
                  builder.CloseComponent();
              }
              builder.CloseElement();
              break;

          case "SingleResponse":
              builder.OpenComponent<MudRadioGroup<string>>(seq++);
              builder.AddAttribute(seq++, "Value", question.Answer);
              builder.AddAttribute(seq++, "ValueChanged", EventCallback.Factory.Create<string>(this, value => question.Answer = value));
              builder.AddAttribute(seq++, "ChildContent", (RenderFragment)(builder2 =>
              {
                  foreach (var option in question.Options)
                  {
                      builder2.OpenComponent<MudRadio<string>>(seq++);
                      builder2.AddAttribute(seq++, "Value", option);
                      builder2.AddAttribute(seq++, "ChildContent", (RenderFragment)(builder3 =>
                      {
                          builder3.AddContent(seq++, option);
                      }));
                      builder2.CloseComponent();
                  }
              }));
              builder.CloseComponent();
              break;
      }
  };


        private async Task CloseModalAsync()
        {
            await BlazoredModal.CloseAsync(ModalResult.Ok());
        }

    }
}

    public class SurveyModel
    {
        public List<SurveyDefinitionEntity> Questions { get; set; }
    }
