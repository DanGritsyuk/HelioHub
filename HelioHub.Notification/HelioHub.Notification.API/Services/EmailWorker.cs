using HelioHub.Notification.BLL.Logic.Contracts;
using HelioHub.Notification.Entities.Models;

namespace HelioHub.Notification.API.Services
{
    public class EmailWorker : BaseKafkaWorker<EmailMessage>
    {
        private readonly IEmailSender _emailService;

        public EmailWorker(IEmailSender emailService) => _emailService = emailService;

        protected override string GetTopicName() => "email.service.topic";

        protected override Task ProcessMessage(EmailMessage msg)
        {
            throw new NotImplementedException();
        }
    }
}
