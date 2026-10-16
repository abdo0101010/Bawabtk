using CleanArchitecture.Core.Entites.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CleanArchitecture.Infrastructre.Data.Config
{
    public class UserConfig : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(u => u.Id);
            builder.HasIndex(u => u._UserId).IsUnique();
            builder.HasIndex(u => u.PhoneNumber).IsUnique();
            builder.Property(u => u.FullName).IsRequired().HasMaxLength(150);
            builder.Property(u => u.PhoneNumber).IsRequired().HasMaxLength(20);
            builder.Property(u => u.Email).HasMaxLength(150);
            builder.Property(u => u.PasswordHash).IsRequired().HasMaxLength(500);
            builder.Property(u => u.ProfilePictureUrl).HasMaxLength(500);
        }
    }
}
