using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using HelioHub.UserService.DAL.Repository.UserEvents;

namespace HelioHub.UserService.DAL.Repository
{
    public class UserDbContext : DbContext
    {
        internal DbSet<UserEventDto> EventLogs { get; set; }

        public UserDbContext(DbContextOptions<UserDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserEventDto>()
                .Property(ue => ue.UserState)
                .HasMaxLength(500);

            modelBuilder.Entity<UserEventDto>()
                .HasKey(ue => ue.Id);
        }
    }
}
