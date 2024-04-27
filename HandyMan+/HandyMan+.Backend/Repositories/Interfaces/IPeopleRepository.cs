using HandyMan_.Shered.DTOs;
using HandyMan_.Shered.Entities;
using HandyMan_.Shered.Responses;

namespace HandyMan_.Backend.Repositories.Interfaces
{
    public interface IPeopleRepository
    {
        Task<ActionResponse<People>> GetAsync(int id);
        Task<ActionResponse<IEnumerable<People>>> GetAsync(PaginationDTO pagination);
        Task<ActionResponse<IEnumerable<People>>> GetAsync();
        Task<ActionResponse<int>> GetTotalPagesAsync(PaginationDTO pagination);
    }
}
