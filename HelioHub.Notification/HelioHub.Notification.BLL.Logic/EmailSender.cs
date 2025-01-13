using System.Net.Mail;
using System.Net;
using HelioHub.Notification.BLL.Logic.Contracts;
using HelioHub.Notification.Entities.Enums;
using HelioHub.Notification.Entities.Models;

namespace HelioHub.Notification.BLL.Logic
{
    public class EmailSender : IEmailSender
    {
        private readonly SmtpClient _smtpClient;

        public EmailSender(string smtpHost, int smtpPort, string smtpUser, string smtpPass)
        {
            _smtpClient = new SmtpClient(smtpHost)
            {
                Port = smtpPort,
                Credentials = new NetworkCredential(smtpUser, smtpPass),
                EnableSsl = true
            };
        }

        public async Task<EmailStatus> SendEmail(EmailMessage message)
        {
            try
            {
                var mailMessage = new MailMessage
                {
                    Subject = message.Subject,
                    Body = message.Body,
                };

                foreach (var recipient in message.Recipients)
                {
                    mailMessage.To.Add(recipient.Address);
                }

                await _smtpClient.SendMailAsync(mailMessage);
                message.Status = EmailStatus.Ok;
                return EmailStatus.Ok;
            }
            catch (Exception)
            {
                message.Status = EmailStatus.Failed;
                return EmailStatus.Failed;
            }
        }
    }
}
