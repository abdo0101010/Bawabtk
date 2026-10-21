using Ardalis.Specification;
namespace CleanArchitecture.Core.Entites.Courses.Specification
{
    public class ReadSpecification:Specification<Course>
    {
        public ReadSpecification()
        {
            Query.Where(c => c._CourseId != Guid.Empty);
        }
    }
}
