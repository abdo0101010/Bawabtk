using Microsoft.AspNetCore.Mvc;

namespace Bawabtk.REPR.EndPoints.CourseModule.Read.SingleByCourseId
{
    public class CourseModuleRequest
    {
        [FromRoute]
        public Guid CourseModuleId { get; set; }
        [FromRoute]
        public Guid CourseId { get; set; }
    }
}
