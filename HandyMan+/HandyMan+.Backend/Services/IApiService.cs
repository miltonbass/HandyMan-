using HandyMan_.Shered.Responses;

namespace HandyMan_.Backend.Services
{
    public interface IApiService
    {
        Task<ActionResponse<T>> GetAsync<T>(string servicePrefix, string controller);
    }
}
