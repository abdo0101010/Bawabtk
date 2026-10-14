using CleanArchitecture.Core.Entites;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CleanArchitecture.Infrastructre.Data.Config
{
    public class CourseConfig : IEntityTypeConfiguration<Course>
    {
        public void Configure(EntityTypeBuilder<Course> builder)
        {
            builder.HasKey(c => c.Id);
            builder.HasIndex(c => c._CourseId).IsUnique();

            builder.Property(c => c.Title).IsRequired().HasMaxLength(200);
            builder.Property(c => c.Price).HasColumnType("decimal(18,2)");
            builder.Property(c => c.GradeLevel).IsRequired().HasMaxLength(50);

            builder.HasOne(c => c.Teacher)
                   .WithMany(u => u.CreatedCourses)
                   .HasForeignKey(c => c.TeacherId)
                   .OnDelete(DeleteBehavior.Restrict);

        }
    }
}
