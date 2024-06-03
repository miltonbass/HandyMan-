using HandyMan_.Shered.DTOs;
using HandyMan_.Shered.Entities;
using HandyMan_.Shered.Responses;

namespace HandyMan_.Backend.Repositories.Interfaces
{
    public interface IServicesRepository
    {
        Task<ActionResponse<Service>> GetAsync(int id);
        Task<ActionResponse<IEnumerable<Service>>> GetAsync(PaginationDTO pagination);
        Task<ActionResponse<IEnumerable<Service>>> GetAsync();
        Task<ActionResponse<int>> GetTotalPagesAsync(PaginationDTO pagination);

        Task<IEnumerable<Service>> GetAllServices();

        Task<ActionResponse<Service>> AddServicePhotoAsync(Service Service);
    }
}
