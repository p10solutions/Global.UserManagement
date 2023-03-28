
using Global.UserManagement.Application.Entities;
using Global.UserManagement.Infra.Data.Maps;
using Microsoft.EntityFrameworkCore;

namespace Global.UserManagement.Infra.Data
{
    public class UserManagementContext : DbContext
    {
        public UserManagementContext(DbContextOptions<UserManagementContext> options)
        : base(options)
        {

        }

        public DbSet<User> TaskItem { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new UserMapper());

            base.OnModelCreating(modelBuilder);
        }
    }
}
