using CleanArchitecture.Core.Entites.Users;
using static CleanArchitecture.Core.Entites.Enums;

namespace CleanArchitecture.Core.Entites.Courses
{
    public class Course:BaseEntity
    {
        public Guid _CourseId { get; set; }
        public int TeacherId { get; set; } // FK -> User.Id
        public string Title { get; set; } = string.Empty;
        public string? Description { get; set; }
        public string? ThumbnailUrl { get; set; }
        public CourseType CourseType { get; set; }
        public decimal Price { get; set; }
        public string GradeLevel { get; set; } = string.Empty;
        public bool IsPublished { get; set; }

        // Relations
        public User Teacher { get; set; } = null!;
        public List<CourseModule> Modules { get; set; } = new();
        public List<Enrollment.Enrollment> Enrollments { get; set; } = new();
        public List<OfflineSession.OfflineSession> OfflineSessions { get; set; } = new();
        public List<Exam> Exams { get; set; } = new();

        public Course() { } // ef core ctor

        public Course(
                      Guid teacherUserGuid,
                      string title,
                      CourseType courseType,
                      decimal price,
                      string gradeLevel,
                      string? description = null,
                      string? thumbnailUrl = null)
        {
            _CourseId = Guid.NewGuid();
            Title = title;
            CourseType = courseType;
            Price = price;
            GradeLevel = gradeLevel;
            Description = description;
            ThumbnailUrl = thumbnailUrl;
            IsPublished = false;
            CreatedBy = teacherUserGuid;
            CreatedOn = DateTimeOffset.Now;
            ModifiedBy = Guid.Empty;
            ModifiedOn = DateTimeOffset.Now;
        }

        public void UpdateCourse(string title, string? description, decimal price, bool isPublished, Guid modifiedBy)
        {
            ModifiedOn = DateTimeOffset.Now;
            ModifiedBy = modifiedBy;
            Title = string.IsNullOrEmpty(title) ? this.Title : title;
            Description = string.IsNullOrEmpty(description) ? this.Description : description;
            Price = price == 0 ? this.Price : price;
            IsPublished = isPublished;
        }
        public void SetTeacher(User teacher)
        {
            Teacher = teacher;
        }
    }
}
