using HandyMan_.Shered.Entities;
using HandyMan_.Shered.Responses;

namespace HandyMan_.Backend.UnitsOfWork.Interfaces
{
    public interface IStatesUnitOfWork
    {
        Task<ActionResponse<State>> GetAsync(int id);

        Task<ActionResponse<IEnumerable<State>>> GetAsync();
    }
}