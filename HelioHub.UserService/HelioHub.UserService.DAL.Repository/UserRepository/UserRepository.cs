using HelioHub.UserService.Common.Entities;
using HelioHub.UserService.DAL.Repository.UserEvents;
using Microsoft.EntityFrameworkCore;
using MongoDB.Driver;

namespace HelioHub.UserService.DAL.Repository.UserRepository
{
    public class UserRepository
    {
        private readonly IMongoCollection<User> _users;
        private readonly UserDbContext _context;
        private readonly MongoClient _client;

        public UserRepository(IMongoDatabase database, UserDbContext context, MongoClient client)
        {
            _users = database.GetCollection<User>("Users");
            _context = context;
            _client = client;
        }

        public async Task<User?> GetByIdAsync(Guid userId) =>
            await _users.Find(u => u.Id == userId).FirstOrDefaultAsync();

        public async Task<IEnumerable<User>> GetAllAsync() =>
            await _users.Find(_ => true).ToListAsync();

        public async Task AddUserWithEventAsync(User user) =>
            await ExecuteWithEventAsync(user, ChangeDbEventType.Created, (session) =>
                _users.InsertOneAsync(session, user));

        public async Task UpdateUserWithEventAsync(User user) =>
            await ExecuteWithEventAsync(user, ChangeDbEventType.Updated, (session) =>
                _users.ReplaceOneAsync(session, u => u.Id == user.Id, user));

        public async Task DeleteUserWithEventAsync(User user) =>
            await ExecuteWithEventAsync(user, ChangeDbEventType.Deleted, (session) =>
                _users.DeleteOneAsync(session, u => u.Id == user.Id));

        private async Task ExecuteWithEventAsync(User user, ChangeDbEventType eventType, Func<IClientSessionHandle, Task> operation)
        {
            using var session = await _client.StartSessionAsync();
            session.StartTransaction();

            try
            {
                await operation(session);
                var eventLog = new UserEventDto(eventType, user);
                await _context.EventLogs.AddAsync(eventLog);
                await _context.SaveChangesAsync();
                await session.CommitTransactionAsync();
            }
            catch
            {
                await session.AbortTransactionAsync();
                throw;
            }
        }
    }
}
