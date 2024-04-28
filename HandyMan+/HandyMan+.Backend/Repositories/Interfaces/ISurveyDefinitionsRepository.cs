using HandyMan_.Shared.Entities;
using HandyMan_.Shered.DTOs;
using HandyMan_.Shered.Responses;


namespace HandyMan_.Backend.Repositories.Interfaces
{
    public interface ISurveyDefinitionsRepository
    {

        Task<ActionResponse<IEnumerable<SurveyDefinitionEntity>>> GetAsync(PaginationDTO pagination);

        Task<ActionResponse<int>> GetTotalPagesAsync(PaginationDTO pagination);

        Task<IEnumerable<SurveyDefinitionEntity>> GetComboAsync();
    }
}
