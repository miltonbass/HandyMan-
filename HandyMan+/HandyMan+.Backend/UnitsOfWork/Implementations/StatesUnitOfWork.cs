using HandyMan_.Backend.Repositories.Interfaces;
using HandyMan_.Backend.UnitsOfWork.Interfaces;
using HandyMan_.Shered.Entities;
using HandyMan_.Shered.Responses;

namespace HandyMan_.Backend.UnitsOfWork.Implementations
{
    public class StatesUnitOfWork : GenericUnitOfWork<State>, IStatesUnitOfWork
    {
        private readonly IStatesRepository _statesRepository;

        public StatesUnitOfWork(IGenericRepository<State> repository, IStatesRepository statesRepository) : base(repository)
        {
            _statesRepository = statesRepository;
        }

        public override async Task<ActionResponse<IEnumerable<State>>> GetAsync() => await _statesRepository.GetAsync();

        public override async Task<ActionResponse<State>> GetAsync(int id) => await _statesRepository.GetAsync(id);
    }
}