using HandyMan_.Shered.Entities;
using HandyMan_.Shered.Responses;

namespace HandyMan_.Backend.Repositories.Interfaces
{
    public interface ITemporalOrdersRepository
    {
        Task<ActionResponse<TemporalOrder>> GetAsync(int id);

        Task<ActionResponse<TemporalOrder>> PutFullAsync(TemporalOrder temporalOrder);

        Task<ActionResponse<TemporalOrder>> AddFullAsync(string email, TemporalOrder temporalOrder);

        Task<ActionResponse<IEnumerable<TemporalOrder>>> GetAsync(string email);

        Task<ActionResponse<int>> GetCountAsync(string email);
    }
}
