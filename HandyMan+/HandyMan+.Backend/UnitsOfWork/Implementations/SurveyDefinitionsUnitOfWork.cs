

using HandyMan_.Backend.Repositories.Interfaces;
using HandyMan_.Backend.UnitsOfWork.Interfaces;
using HandyMan_.Shared.Entities;
using HandyMan_.Shered.Responses;
using Orders.Shared.DTOs;

namespace HandyMan_.Backend.UnitsOfWork.Implementations
{
    public class SurveyDefinitionsUnitOfWork : GenericUnitOfWork<SurveyDefinitionEntity>, ISurveyDefinitionsUnitOfWork
    {
        private readonly ISurveyDefinitionsRepository _surveyDefinitionsRepository;

        public SurveyDefinitionsUnitOfWork(IGenericRepository<SurveyDefinitionEntity> repository, ISurveyDefinitionsRepository surveyDefinitionsRepository) : base(repository)
        {
            _surveyDefinitionsRepository = surveyDefinitionsRepository;
        }

        public override async Task<ActionResponse<IEnumerable<SurveyDefinitionEntity>>> GetAsync(PaginationDTO pagination) => await _surveyDefinitionsRepository.GetAsync(pagination);

        public override async Task<ActionResponse<int>> GetTotalPagesAsync(PaginationDTO pagination) => await _surveyDefinitionsRepository.GetTotalPagesAsync(pagination);

        public async Task<IEnumerable<SurveyDefinitionEntity>> GetComboAsync() => await _surveyDefinitionsRepository.GetComboAsync();
    }
}