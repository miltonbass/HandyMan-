using HandyMan_.Backend.Repositories.Interfaces;
using HandyMan_.Backend.UnitsOfWork.Interfaces;
using HandyMan_.Shered.DTOs;
using HandyMan_.Shered.Entities;
using HandyMan_.Shered.Responses;

namespace HandyMan_.Backend.UnitsOfWork.Implementations
{
    public class OrdersUnitOfWork : GenericUnitOfWork<Order>, IOrdersUnitOfWork
    {
        private readonly IOrdersRepository _ordersRepository;

        public OrdersUnitOfWork(IGenericRepository<Order> repository, IOrdersRepository ordersRepository) : base(repository)
        {
            _ordersRepository = ordersRepository;
        }
        public async Task<ActionResponse<IEnumerable<Order>>> GetAsync(string email, PaginationDTO pagination) => await _ordersRepository.GetAsync(email, pagination);

        public async Task<ActionResponse<int>> GetTotalPagesAsync(string email, PaginationDTO pagination) => await _ordersRepository.GetTotalPagesAsync(email, pagination);

        public async Task<ActionResponse<Order>> UpdateFullAsync(string email, OrderDTO orderDTO) => await _ordersRepository.UpdateFullAsync(email, orderDTO);


        public override async Task<ActionResponse<Order>> GetAsync(int id) => await _ordersRepository.GetAsync(id);

        public Task AddOrderAsync(Order order) => _ordersRepository.AddOrderAsync(order);
    }
}
