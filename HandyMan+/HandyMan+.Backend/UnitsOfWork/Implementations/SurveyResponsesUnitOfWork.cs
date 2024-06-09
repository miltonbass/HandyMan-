using HandyMan_.Backend.Repositories.Interfaces;
using HandyMan_.Backend.UnitsOfWork.Interfaces;
using HandyMan_.Shered.DTOs;
using HandyMan_.Shered.Responses;

namespace HandyMan_.Backend.UnitsOfWork.Implementations
{
    public class SurveyResponsesUnitOfWork : GenericUnitOfWork<SurveyResponseDTO>, ISurveyResponsesUnitOfWork
    {
        private readonly ISurveyResponsesRepository _surveyResponseRepository;

        public SurveyResponsesUnitOfWork(IGenericRepository<SurveyResponseDTO> repository, ISurveyResponsesRepository surveyResponseRepository) : base(repository)
        {
            _surveyResponseRepository = surveyResponseRepository;
        }

        public override async Task<ActionResponse<IEnumerable<SurveyResponseDTO>>> GetAsync(PaginationDTO pagination) => await _surveyResponseRepository.GetAsync(pagination);

        public override async Task<ActionResponse<int>> GetTotalPagesAsync(PaginationDTO pagination) => await _surveyResponseRepository.GetTotalPagesAsync(pagination);

        public async Task<IEnumerable<SurveyResponseDTO>> GetComboAsync() => await _surveyResponseRepository.GetComboAsync();
    }
}
