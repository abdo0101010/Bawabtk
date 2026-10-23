using CleanArchitecture.Core.Entites.CourseLessons;
using CleanArchitecture.Core.Entites.Courses;
using MongoDB.Bson;

namespace CleanArchitecture.Core.Entites.CourseModules
{
    public class CourseModule:BaseEntity
    {
        public Guid _ModuleId { get; set; }
        public int CourseId { get; set; } // FK -> Course.Id
        public string Title { get; set; } = string.Empty;
        public int SortOrder { get; set; }

        // Relations
        public Course Course { get; set; } = null!;
        public List<CourseLesson> Lessons { get; set; } = new();

        public CourseModule() { } // ef core ctor

        public CourseModule( string title, int sortOrder, Guid createdBy)
        {
            _ModuleId = Guid.NewGuid();
            Title = title;
            SortOrder = sortOrder;
            CreatedBy = createdBy;
            CreatedOn = DateTimeOffset.Now;
            ModifiedBy = Guid.Empty;
            ModifiedOn = DateTimeOffset.Now;
        }
        public void SetCourse(Course course)=> Course = course;
        
        public void Update( string title)=> Title = title;
        
    }                      
}
