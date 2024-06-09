using HandyMan_.Shered.Entities;
using HandyMan_.Shered.Responses;
using System.Threading.Tasks;

namespace HandyMan_.Backend.Repositories.Interfaces
{
    public interface ITemporalOrdersRepository
    {
        Task<ActionResponse<TemporalOrder>> GetAsync(int id);

        //Task<ActionResponse<TemporalOrder>> PutFullAsync(TemporalOrder temporalOrder);
        Task<ActionResponse<TemporalOrder>> UpdateAsync(TemporalOrder temporalOrder);

        Task<ActionResponse<TemporalOrder>> AddFullAsync(string email, TemporalOrder temporalOrder);

        Task<ActionResponse<IEnumerable<TemporalOrder>>> GetAsync(string email);

        Task<ActionResponse<int>> GetCountAsync(string email);

        Task<IEnumerable<TemporalOrder>> GetAllRequest();
        
    }
}
