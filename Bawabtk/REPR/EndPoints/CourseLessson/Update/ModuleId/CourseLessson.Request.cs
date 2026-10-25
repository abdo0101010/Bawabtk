using Microsoft.AspNetCore.Mvc;

namespace Bawabtk.REPR.EndPoints.CourseLessson.Update.ModuleId
{
    public class CourseLesssonRequest
    {
        [FromRoute]
        public Guid CourseLessonId { get; set; }
        [FromRoute]
        public Guid CourseModuleId { get; set; }
    }
}
