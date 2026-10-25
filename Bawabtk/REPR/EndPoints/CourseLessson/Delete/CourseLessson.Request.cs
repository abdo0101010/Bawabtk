using Microsoft.AspNetCore.Mvc;

namespace Bawabtk.REPR.EndPoints.CourseLessson.Delete
{
    public class CourseLesssonRequest
    {
        [FromRoute]
        public Guid CourseLessonId { get; set; }
    }
}
