using HandyMan_.Backend.Repositories.Implementations;
using HandyMan_.Backend.Repositories.Interfaces;
using HandyMan_.Backend.UnitsOfWork.Interfaces;
using HandyMan_.Shered.DTOs;
using HandyMan_.Shered.Entities;
using HandyMan_.Shered.Responses;

namespace HandyMan_.Backend.UnitsOfWork.Implementations
{
    public class ServiceOrderUnitOfWork : GenericUnitOfWork<ServiceOrder>, IServiceOrderUnitOfWork
    {
        private readonly IServiceOrderRepository _serviceorderRepository;

        public ServiceOrderUnitOfWork(IGenericRepository<ServiceOrder> repository, IServiceOrderRepository serviceorderRepository) : base(repository)
        {
            _serviceorderRepository = serviceorderRepository;
        }

        public override async Task<ActionResponse<IEnumerable<ServiceOrder>>> GetAsync() => await _serviceorderRepository.GetAsync();
        public override async Task<ActionResponse<IEnumerable<ServiceOrder>>> GetAsync(PaginationDTO pagination) => await _serviceorderRepository.GetAsync(pagination);
        public override async Task<ActionResponse<int>> GetTotalPagesAsync(PaginationDTO pagination) => await _serviceorderRepository.GetTotalPagesAsync(pagination);
        public override async Task<ActionResponse<ServiceOrder>> GetAsync(int id) => await _serviceorderRepository.GetAsync(id);
    }
}
