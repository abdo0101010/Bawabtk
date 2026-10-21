using Ardalis.Specification;
namespace CleanArchitecture.Core.Entites.Courses.Specification
{
    public class ReadByIdSpecification:SingleResultSpecification<Course>
    {
        public ReadByIdSpecification(Guid CourseId)
        {
            Query.Where(c => c._CourseId == CourseId);
            
        }
    }
}
