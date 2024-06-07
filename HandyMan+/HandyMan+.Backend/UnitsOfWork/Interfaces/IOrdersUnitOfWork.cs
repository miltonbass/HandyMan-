using HandyMan_.Shered.DTOs;
using HandyMan_.Shered.Entities;
using HandyMan_.Shered.Responses;

namespace HandyMan_.Backend.UnitsOfWork.Interfaces
{
    public interface IOrdersUnitOfWork
    {
        Task<ActionResponse<Order>> AddAsync(Order order);

        Task<ActionResponse<IEnumerable<Order>>> GetAsync(string email, PaginationDTO pagination);

        Task<ActionResponse<int>> GetTotalPagesAsync(string email, PaginationDTO pagination);

        Task<ActionResponse<Order>> GetAsync(int id);

        Task<ActionResponse<Order>> UpdateFullAsync(string email, OrderDTO orderDTO);
        Task AddOrderAsync(Order order);
    }
}
