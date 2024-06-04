using HandyMan_.Shered.Responses;

namespace HandyMan_.Backend.Helpers
{
    public interface IOrdersHelper
    {
        Task<ActionResponse<bool>> ProcessOrderAsync(string email);
    }
}
