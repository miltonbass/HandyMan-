using HandyMan_.Shered.DTOs;
using HandyMan_.Shered.Responses;

namespace HandyMan_.Backend.UnitsOfWork.Interfaces
{
    public interface ISurveyResponsesUnitOfWork
    {
        Task<ActionResponse<IEnumerable<SurveyResponseDTO>>> GetAsync(PaginationDTO pagination);

        Task<ActionResponse<int>> GetTotalPagesAsync(PaginationDTO pagination);

        Task<IEnumerable<SurveyResponseDTO>> GetComboAsync();
    }
}
