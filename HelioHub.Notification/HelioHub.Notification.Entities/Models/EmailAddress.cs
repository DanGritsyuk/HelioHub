namespace HelioHub.Notification.Entities.Models
{
    public class EmailAddress
    {
        public EmailAddress(string address)
        {
            Address = address;
        }

        public string Address { get; set; }
    }
}
