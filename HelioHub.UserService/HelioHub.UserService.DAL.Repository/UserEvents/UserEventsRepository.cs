using HelioHub.UserService.Common.Entities;

namespace HelioHub.UserService.DAL.Repository.UserEvents
{
    public class UserEventsRepository
    {
        private readonly UserDbContext _context;

        public UserEventsRepository(UserDbContext context) => _context = context;

        public Task CreateUserAsync(User user) => LogEventAsync(ChangeDbEventType.Created, user);

        public Task UpdateUserAsync(User user) => LogEventAsync(ChangeDbEventType.Updated, user);

        public Task DeleteUserAsync(User user) => LogEventAsync(ChangeDbEventType.Deleted, user);

        private async Task LogEventAsync(ChangeDbEventType eventType, User user)
        {
            var eventLog = new UserEventDto(eventType, user);
            await _context.EventLogs.AddAsync(eventLog);
            await _context.SaveChangesAsync();
        }
    }
}