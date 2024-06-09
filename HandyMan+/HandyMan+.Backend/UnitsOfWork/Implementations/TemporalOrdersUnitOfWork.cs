using HandyMan_.Backend.Repositories.Implementations;
using HandyMan_.Backend.Repositories.Interfaces;
using HandyMan_.Backend.UnitsOfWork.Interfaces;
using HandyMan_.Shered.Entities;
using HandyMan_.Shered.Responses;

namespace HandyMan_.Backend.UnitsOfWork.Implementations
{
    public class TemporalOrdersUnitOfWork : GenericUnitOfWork<TemporalOrder>, ITemporalOrdersUnitOfWork
    {
        private readonly ITemporalOrdersRepository _temporalOrdersRepository;

        public TemporalOrdersUnitOfWork(IGenericRepository<TemporalOrder> repository, ITemporalOrdersRepository temporalOrdersRepository) : base(repository)
        {
            _temporalOrdersRepository = temporalOrdersRepository;
        }

        public async Task<ActionResponse<TemporalOrder>> PutFullAsync(TemporalOrder temporalOrder) => await _temporalOrdersRepository.PutFullAsync(temporalOrder);

        public override async Task<ActionResponse<TemporalOrder>> GetAsync(int id) => await _temporalOrdersRepository.GetAsync(id);

        public async Task<ActionResponse<TemporalOrder>> AddFullAsync(string email, TemporalOrder temporalOrder) => await _temporalOrdersRepository.AddFullAsync(email, temporalOrder);

        public async Task<ActionResponse<IEnumerable<TemporalOrder>>> GetAsync(string email) => await _temporalOrdersRepository.GetAsync(email);

        public async Task<ActionResponse<int>> GetCountAsync(string email) => await _temporalOrdersRepository.GetCountAsync(email);

        public Task<IEnumerable<TemporalOrder>> GetAllRequest() => _temporalOrdersRepository.GetAllRequest();
    }
}
