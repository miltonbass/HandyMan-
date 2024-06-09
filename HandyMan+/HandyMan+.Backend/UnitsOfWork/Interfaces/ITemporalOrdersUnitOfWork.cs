using HandyMan_.Shered.DTOs;
using HandyMan_.Shered.Entities;
using HandyMan_.Shered.Responses;

namespace HandyMan_.Backend.UnitsOfWork.Interfaces
{
    public interface ITemporalOrdersUnitOfWork
    {
        Task<ActionResponse<TemporalOrder>> DeleteAsync(int id);

        Task<ActionResponse<TemporalOrder>> GetAsync(int id);

        //Task<ActionResponse<TemporalOrder>> PutFullAsync(TemporalOrder temporalOrder);

        Task<ActionResponse<TemporalOrder>> UpdateAsync(TemporalOrder temporalOrder);

        Task<ActionResponse<TemporalOrder>> AddFullAsync(string email, TemporalOrder temporalOrder);

        Task<ActionResponse<IEnumerable<TemporalOrder>>> GetAsync(string email);

        Task<IEnumerable<TemporalOrder>> GetAllRequest();

        Task<ActionResponse<int>> GetCountAsync(string email);
    }
}
