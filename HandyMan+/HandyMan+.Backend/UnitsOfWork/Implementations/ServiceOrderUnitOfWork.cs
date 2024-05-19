

using HandyMan_.Backend.Repositories.Interfaces;
using HandyMan_.Backend.UnitsOfWork.Interfaces;
using HandyMan_.Shered.Responses;
using HandyMan_.Shered.DTOs;
using HandyMan_.Shered.Entities;


namespace HandyMan_.Backend.UnitsOfWork.Implementations
{
    public class ServiceOrderUnitOfWork : GenericUnitOfWork<ServiceOrder>, IServiceOrderUnitOfWork
    {
        private readonly IServiceOrderRepository _serviceOrderRepository;

        public ServiceOrderUnitOfWork(IGenericRepository<ServiceOrder> repository, IServiceOrderRepository serviceOrderRepository) : base(repository)
        {
            _serviceOrderRepository = serviceOrderRepository;
        }

        public override async Task<ActionResponse<IEnumerable<ServiceOrder>>> GetAsync(PaginationDTO pagination) => await _serviceOrderRepository.GetAsync(pagination);

        public override async Task<ActionResponse<int>> GetTotalPagesAsync(PaginationDTO pagination) => await _serviceOrderRepository.GetTotalPagesAsync(pagination);

        public async Task<IEnumerable<ServiceOrder>> GetComboAsync() => await _serviceOrderRepository.GetComboAsync();
    }
}
