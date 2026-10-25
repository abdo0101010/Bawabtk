
using Ardalis.Specification;

namespace CleanArchitecture.Core.Entites.CourseLessons.Specification
{
    public class ReadSpecification:Specification<CourseLesson>
    {
        public ReadSpecification()
        {
            Query.Where(c => c._LessonId != Guid.Empty).Include(c=>c.Module);
        }
    }
}
