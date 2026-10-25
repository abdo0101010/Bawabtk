using Microsoft.AspNetCore.Mvc;

namespace Bawabtk.REPR.EndPoints.CourseLessson.Read.SingleByModuleId
{
    public class CourseLesssonRequest
    {
        [FromRoute]
        public Guid CourseLessonId { get; set; }
        [FromRoute]
        public Guid CourseModuleId { get; set; }
    }
}
