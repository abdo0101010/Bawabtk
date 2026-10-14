using CleanArchitecture.Core.Entites;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
namespace CleanArchitecture.Infrastructre.Data
{
    public class BawabtkDbContext:DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<ParentProfile> ParentProfiles { get; set; }
        public DbSet<StudentProfile> StudentProfiles { get; set; }

        // جداول الكورسات والاشتراكات
        public DbSet<Course> Courses { get; set; }
        public DbSet<CourseModule> CourseModules { get; set; }
        public DbSet<CourseLesson> CourseLessons { get; set; }
        public DbSet<Enrollment> Enrollments { get; set; }

        // جداول حصص السنتر والباركود
        public DbSet<OfflineSession> OfflineSessions { get; set; }
        public DbSet<AttendanceRecord> AttendanceRecords { get; set; }

        // جداول الامتحانات وإشعارات الـ n8n
        public DbSet<Exam> Exams { get; set; }
        public DbSet<ExamResult> ExamResults { get; set; }
        public DbSet<AutomatedNotificationLog> AutomatedNotificationLogs { get; set; }
        public BawabtkDbContext(DbContextOptions<BawabtkDbContext> options) : base(options)
        {
        }

        // (اختياري) Constructor فارغ احتياطي لو احتجته في حالات نادرة
        public BawabtkDbContext()
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}
