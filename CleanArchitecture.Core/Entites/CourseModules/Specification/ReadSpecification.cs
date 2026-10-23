using Ardalis.Specification;

namespace CleanArchitecture.Core.Entites.CourseModules.Specification
{
    public class ReadSpecification:Specification<CourseModule>
    {
        public ReadSpecification()
        {
            Query.Where(c => c._ModuleId != Guid.Empty).Include(c=>c.Course);
        }
    }
}
