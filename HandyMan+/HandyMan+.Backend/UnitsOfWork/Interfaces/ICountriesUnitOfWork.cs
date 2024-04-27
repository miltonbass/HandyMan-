using HandyMan_.Shered.Entities;
using HandyMan_.Shered.Responses;

namespace HandyMan_.Backend.UnitsOfWork.Interfaces
{
    public interface ICountriesUnitOfWork
    {
        Task<ActionResponse<Country>> GetAsync(int id);

        Task<ActionResponse<IEnumerable<Country>>> GetAsync();
    }
}

