using HelioHub.UserService.Common.Entities;

namespace HelioHub.UserService.DAL.Repository.UserEvents
{
    internal class UserEventDto
    {
        public UserEventDto(ChangeDbEventType eventType, User user)
        {
            SetUserData(user);
            CreatedAt = DateTime.UtcNow;
            EventType = eventType;
        }

        public int Id { get; set; }
        public string UserState { get; private set; } = string.Empty;
        public Guid UserId { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public ChangeDbEventType EventType { get; set; }

        private void SetUserData(User user)
        {
            UserId = user.Id;
            UserState = $"FirstName={user.FirstName};" +
                         $"LastName={user.LastName};" +
                         $"Phone={user.PhoneNumber};" +
                         $"Email={user.Email}";
        }

        public User GetUserData()
        {
            var userData = UserState.Split(';');
            var user = new User
            {
                Id = UserId,
                FirstName = ExtractValue(userData, "FirstName"),
                LastName = ExtractValue(userData, "LastName"),
                PhoneNumber = ExtractValue(userData, "Phone"),
                Email = ExtractValue(userData, "Email")
            };
            return user;
        }

        private string ExtractValue(string[] userData, string key)
        {
            var item = userData.FirstOrDefault(data => data.StartsWith($"{key}="));
            return item?.Substring($"{key}=".Length) ?? string.Empty;
        }
    }

}