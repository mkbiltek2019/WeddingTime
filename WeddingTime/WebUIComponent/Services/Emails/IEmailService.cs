using AIT.WebUIComponent.Models.Emails;
using AIT.WebUIComponent.Services.Emails.Enum;
using System.Threading.Tasks;

namespace AIT.WebUIComponent.Services.Emails
{
    public interface IEmailService
    {
        Task SendAsync(EmailType type, MessageModel model, string destination);
        Task SendAsync(EmailType type, MessageModel model);
    }
}