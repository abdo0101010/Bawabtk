using CleanArchitecture.Core.Entites;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
namespace CleanArchitecture.Infrastructre.Data.Config
{
    public class ExamConfig : IEntityTypeConfiguration<Exam>
    {
        public void Configure(EntityTypeBuilder<Exam> builder)
        {
            builder.HasKey(e => e.Id);
            builder.HasIndex(e => e._ExamId).IsUnique();
            builder.Property(e => e.Title).IsRequired().HasMaxLength(200);
            builder.Property(e => e.MaxScore).HasColumnType("decimal(8,2)");
            builder.Property(e => e.PassingScore).HasColumnType("decimal(8,2)");
            builder.Property(e => e.LowGradeAlertThreshold).HasColumnType("decimal(8,2)");
        }
    
    }
}
