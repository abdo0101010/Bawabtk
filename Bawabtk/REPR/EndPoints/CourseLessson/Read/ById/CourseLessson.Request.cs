using Microsoft.AspNetCore.Mvc;

namespace Bawabtk.REPR.EndPoints.CourseLessson.Read.ById
{
    public class CourseLesssonRequest
    {
        [FromRoute]
        public Guid CourseLessonId { get; set; }
    }
}
