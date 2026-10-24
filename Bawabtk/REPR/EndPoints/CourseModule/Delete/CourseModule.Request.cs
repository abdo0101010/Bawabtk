using Microsoft.AspNetCore.Mvc;

namespace Bawabtk.REPR.EndPoints.CourseModule.Delete
{
    public class CourseModuleRequest
    {
        [FromRoute]
        public Guid CourseModuleId { get; set; }
    }
}
