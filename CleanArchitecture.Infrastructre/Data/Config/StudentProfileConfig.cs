
using CleanArchitecture.Core.Entites;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CleanArchitecture.Infrastructre.Data.Config
{
    public class StudentProfileConfig : IEntityTypeConfiguration<StudentProfile>
    {
        public void Configure(EntityTypeBuilder<StudentProfile> builder)
        {
            builder.HasKey(sp => sp.Id);
            builder.HasIndex(sp => sp._StudentProfileId).IsUnique();
            builder.HasIndex(sp => sp.UnifiedBarcode).IsUnique();
            builder.HasIndex(sp => sp.UserId).IsUnique();

            builder.Property(sp => sp.UnifiedBarcode).IsRequired().HasMaxLength(50);
            builder.Property(sp => sp.GradeLevel).IsRequired().HasMaxLength(50);
            builder.Property(sp => sp.SchoolName).HasMaxLength(150);

            builder.HasOne(sp => sp.User)
                   .WithOne(u => u.StudentProfile)
                   .HasForeignKey<StudentProfile>(sp => sp.UserId)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(sp => sp.Parent)
                   .WithMany(pp => pp.Children)
                   .HasForeignKey(sp => sp.ParentId)
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
