using HelioHub.Notification.Entities.Enums;

namespace HelioHub.Notification.Entities.Models
{
    public class EmailMessage
    {
        public EmailMessage(string subject, string message, string adress) : this(subject, message, new List<string>() { adress })
        { }

        public EmailMessage(string subject, string message, IEnumerable<string> adresses)
        {
            Subject = subject;
            Body = message;
            Recipients = new List<EmailAddress>();
            Recipients.AddRange(adresses.Select(x => new EmailAddress(x)));
        }

        public string Subject { get; set; }
        public string Body { get; set; }
        public List<EmailAddress> Recipients { get; set; }
        public EmailStatus Status { get; set; }
    }
}
