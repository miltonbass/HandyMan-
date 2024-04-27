using HandyMan_.Backend.Repositories.Implementations;
using HandyMan_.Backend.Repositories.Interfaces;
using HandyMan_.Backend.UnitsOfWork.Interfaces;
using HandyMan_.Shered.DTOs;
using HandyMan_.Shered.Entities;
using HandyMan_.Shered.Responses;

namespace HandyMan_.Backend.UnitsOfWork.Implementations
{
    public class PeopleUnitOfWork : GenericUnitOfWork<People>, IPeopleUnitOfWork
    {
        private readonly IPeopleRepository _peopleRepository;

        public PeopleUnitOfWork(IGenericRepository<People> repository, IPeopleRepository peopleRepository) : base(repository)
        {
            _peopleRepository = peopleRepository;
        }

        public override async Task<ActionResponse<IEnumerable<People>>> GetAsync() => await _peopleRepository.GetAsync();
        public override async Task<ActionResponse<IEnumerable<People>>> GetAsync(PaginationDTO pagination) => await _peopleRepository.GetAsync(pagination);
        public override async Task<ActionResponse<int>> GetTotalPagesAsync(PaginationDTO pagination) => await _peopleRepository.GetTotalPagesAsync(pagination);
        public override async Task<ActionResponse<People>> GetAsync(int id) => await _peopleRepository.GetAsync(id);
    }
}
