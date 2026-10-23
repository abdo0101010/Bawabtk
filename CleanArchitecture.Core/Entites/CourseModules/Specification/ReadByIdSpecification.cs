using Ardalis.Specification;

namespace CleanArchitecture.Core.Entites.CourseModules.Specification
{
    public class ReadByIdSpecification:SingleResultSpecification<CourseModule>
    {
        public ReadByIdSpecification(Guid CourseModuleId)
        {
            Query.Where(c => c._ModuleId == CourseModuleId);
        }

    }
}
