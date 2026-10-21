using Microsoft.AspNetCore.Mvc;

namespace Bawabtk.REPR.EndPoints.Course.Delete
{
    public class CourseRequest
    {
        [FromRoute]
        public Guid CourseId { get; set; }
    }
}
