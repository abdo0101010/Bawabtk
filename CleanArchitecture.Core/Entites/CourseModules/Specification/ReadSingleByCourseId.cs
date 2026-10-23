using Ardalis.Specification;
namespace CleanArchitecture.Core.Entites.CourseModules.Specification
{
    public class ReadSingleByCourseId:SingleResultSpecification<CourseModule>
    {
        public ReadSingleByCourseId(Guid CourseId,Guid CourseModuleId)
        {
            Query.Where(c => c._ModuleId == CourseModuleId && c.Course._CourseId == CourseId);
            
        }
    }
}
