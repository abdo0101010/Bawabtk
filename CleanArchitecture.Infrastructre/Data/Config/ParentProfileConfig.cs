
using CleanArchitecture.Core.Entites;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CleanArchitecture.Infrastructre.Data.Config
{
    public class ParentProfileConfig : IEntityTypeConfiguration<ParentProfile>
    {
        public void Configure(EntityTypeBuilder<ParentProfile> builder)
        {
            builder.HasKey(pp => pp.Id);
            builder.HasIndex(pp => pp._ParentProfileId).IsUnique();
            builder.HasIndex(pp => pp.WhatsAppNumber);
            builder.HasIndex(pp => pp.UserId).IsUnique();
            builder.Property(pp => pp.WhatsAppNumber).IsRequired().HasMaxLength(20);
            builder.Property(pp => pp.RelationType).IsRequired().HasMaxLength(50);
            builder.Property(pp => pp.JobTitle).HasMaxLength(100);
            builder.Property(pp => pp.Address).HasMaxLength(250);
            builder.HasOne(pp => pp.User)
                   .WithOne(u => u.ParentProfile)
                   .HasForeignKey<ParentProfile>(pp => pp.UserId)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
