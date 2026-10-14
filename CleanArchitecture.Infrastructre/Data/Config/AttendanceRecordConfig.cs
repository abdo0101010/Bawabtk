using CleanArchitecture.Core.Entites;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
namespace CleanArchitecture.Infrastructre.Data.Config
{
    public class AttendanceRecordConfig : IEntityTypeConfiguration<AttendanceRecord>
    {
        public void Configure(EntityTypeBuilder<AttendanceRecord> builder)
        {
            builder.HasKey(ar => ar.Id);
            builder.HasIndex(ar => ar._AttendanceId).IsUnique();
            builder.HasIndex(ar => new { ar.SessionId, ar.StudentId }).IsUnique();

            builder.Property(ar => ar.Notes).HasMaxLength(300);

            builder.HasOne(ar => ar.Student)
                   .WithMany(sp => sp.AttendanceRecords)
                   .HasForeignKey(ar => ar.StudentId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(ar => ar.ScannedByAssistant)
                   .WithMany()
                   .HasForeignKey(ar => ar.ScannedByAssistantId)
                   .OnDelete(DeleteBehavior.Restrict);
        }
    
    }
}
