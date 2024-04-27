using HandyMan_.Shered.DTOs;
using HandyMan_.Shered.Entities;
using HandyMan_.Shered.Responses;

namespace HandyMan_.Backend.UnitsOfWork.Interfaces
{
    public interface ICategoriesUnitOfWork
    {
        Task<ActionResponse<IEnumerable<Category>>> GetAsync(PaginationDTO pagination);
        Task<ActionResponse<int>> GetTotalPagesAsync(PaginationDTO pagination);
    }
}

