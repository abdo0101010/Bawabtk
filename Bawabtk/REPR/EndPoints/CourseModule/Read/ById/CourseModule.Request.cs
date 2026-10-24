using Microsoft.AspNetCore.Mvc;

namespace Bawabtk.REPR.EndPoints.CourseModule.Read.ById
{
    public class CourseModuleRequest
    {
        [FromRoute]
        public Guid CourseModuleId { get; set; }

    }
}
