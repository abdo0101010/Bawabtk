using CleanArchitecture.Core.Entites;
using CleanArchitecture.Core.Entites.AttendanceRecord;
using CleanArchitecture.Core.Entites.Enrollment;
using CleanArchitecture.Core.Entites.OfflineSession;
using CleanArchitecture.Core.Entites.ParentsProfile;
using CleanArchitecture.Core.Entites.Student;
using CleanArchitecture.Core.Entites.Users;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
namespace CleanArchitecture.Infrastructre.Data
{
    public class BawabtkDbContext:DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<ParentProfile> ParentProfiles { get; set; }
        public DbSet<StudentProfile> StudentProfiles { get; set; }

        public DbSet<Course> Courses { get; set; }
        public DbSet<CourseModule> CourseModules { get; set; }
        public DbSet<CourseLesson> CourseLessons { get; set; }
        public DbSet<Enrollment> Enrollments { get; set; }

        public DbSet<OfflineSession> OfflineSessions { get; set; }
        public DbSet<AttendanceRecord> AttendanceRecords { get; set; }

        public DbSet<Exam> Exams { get; set; }
        public DbSet<ExamResult> ExamResults { get; set; }
        public DbSet<AutomatedNotificationLog> AutomatedNotificationLogs { get; set; }
        public BawabtkDbContext(DbContextOptions<BawabtkDbContext> options) : base(options)
        {
        }

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
