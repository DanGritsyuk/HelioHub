using HelioHub.Notification.Entities.Enums;
using HelioHub.Notification.Entities.Models;

namespace HelioHub.Notification.BLL.Logic.Contracts
{
    public interface IEmailSender
    {
        Task<EmailStatus> SendEmail(EmailMessage message);
    }
}
