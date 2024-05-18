

using HandyMan_.Shered.Responses;

namespace HandyMan_.Backend.Helpers
{
    public interface IMailHelper
    {
        ActionResponse<string> SendMail(string toName, string toEmail, string subject, string body);
    }
}
