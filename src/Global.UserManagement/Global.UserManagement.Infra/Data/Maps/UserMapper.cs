using Microsoft.EntityFrameworkCore;
using Global.UserManagement.Application.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Global.UserManagement.Infra.Data.Maps
{
    public class UserMapper : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("User");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Name).HasColumnType("varchar(200)");
        }
    }
}
