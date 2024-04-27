using HandyMan_.Shered.Entities;
using HandyMan_.Shered.Responses;

namespace HandyMan_.Backend.Repositories.Interfaces
{
    public interface IStatesRepository
    {
        Task<ActionResponse<State>> GetAsync(int id);

        Task<ActionResponse<IEnumerable<State>>> GetAsync();
    }
}