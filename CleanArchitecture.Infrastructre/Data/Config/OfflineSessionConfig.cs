using CleanArchitecture.Core.Entites;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CleanArchitecture.Infrastructre.Data.Config
{
    public class OfflineSessionConfig : IEntityTypeConfiguration<OfflineSession>
    {
        public void Configure(EntityTypeBuilder<OfflineSession> builder)
        {
            builder.HasKey(s => s.Id);
            builder.HasIndex(s => s._SessionId).IsUnique();

            builder.Property(s => s.Title).IsRequired().HasMaxLength(200);
            builder.Property(s => s.CenterName).IsRequired().HasMaxLength(150);
            builder.Property(s => s.RoomNumber).HasMaxLength(50);
        }
    
    }
}
