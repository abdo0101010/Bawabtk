using Ardalis.Specification;
using CleanArchitecture.Core.Entites.CourseModules;
using CleanArchitecture.Core.Entites.Courses;

namespace CleanArchitecture.Core.Entites.CourseLessons.Specification
{
    public class ReadSingleByCourseModuleIdSpceification:SingleResultSpecification<CourseLesson>
    {
        public ReadSingleByCourseModuleIdSpceification(Guid ModuleId,Guid LessonId)
        {
            Query.Where(c => c._LessonId == LessonId && c.Module._ModuleId == ModuleId);
        }

    }
}
