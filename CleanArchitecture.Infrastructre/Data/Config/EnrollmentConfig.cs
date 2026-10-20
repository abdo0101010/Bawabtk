using CleanArchitecture.Core.Entites.Enrollment;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
namespace CleanArchitecture.Infrastructre.Data.Config
{
    public class EnrollmentConfig : IEntityTypeConfiguration<Enrollment>
    {
        public void Configure(EntityTypeBuilder<Enrollment> builder)
        {
            builder.HasKey(e => e.Id);
            builder.HasIndex(e => e._EnrollmentId).IsUnique();
            builder.HasIndex(e => new { e.StudentId, e.CourseId }).IsUnique();
            builder.Property(e => e.AmountPaid).HasColumnType("decimal(18,2)");
            builder.HasOne(e => e.Student)
                   .WithMany(sp => sp.Enrollments)
                   .HasForeignKey(e => e.StudentId)
                   .OnDelete(DeleteBehavior.Restrict);
        }
    
    }
}
