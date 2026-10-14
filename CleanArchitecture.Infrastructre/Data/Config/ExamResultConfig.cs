using CleanArchitecture.Core.Entites;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
namespace CleanArchitecture.Infrastructre.Data.Config
{
    public class ExamResultConfig : IEntityTypeConfiguration<ExamResult>
    {
        public void Configure(EntityTypeBuilder<ExamResult> builder)
        {
            builder.HasKey(er => er.Id);
            builder.HasIndex(er => er._ExamResultId).IsUnique();
            builder.HasIndex(er => new { er.ExamId, er.StudentId }).IsUnique();
            builder.Property(er => er.Score).HasColumnType("decimal(8,2)");
            builder.Property(er => er.TeacherFeedback).HasMaxLength(500);
            builder.HasOne(er => er.Student)
                   .WithMany(sp => sp.ExamResults)
                   .HasForeignKey(er => er.StudentId)
                   .OnDelete(DeleteBehavior.Restrict);
        }
    
    }
}
