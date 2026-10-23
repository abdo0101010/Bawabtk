using Ardalis.Specification;

namespace CleanArchitecture.Core.Entites.CourseLessons.Specification
{
    public class ReadByIdSpecification:SingleResultSpecification<CourseLesson>
    {
        public ReadByIdSpecification(Guid CourseLessonId)
        {
            Query.Where(c => c._LessonId == CourseLessonId);
            
        }
    }
}
