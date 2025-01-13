namespace HelioHub.Notification.Contracts
{
    public class EmailServiceMessage
    {
        public EmailServiceMessage(string subject, string content, string recipient) : this(subject, content, new List<string>() { recipient })
        { }

        public EmailServiceMessage(string subject, string content, IEnumerable<string> recipients)
        {
            Recipients = new List<string>();
            Recipients.AddRange(recipients.Select(x => x));
            Subject = subject;
            Content = content;
        }

        public List<string> Recipients { get; set; }

        public string Subject { get; set; }

        public string Content { get; set; }
    }
}
