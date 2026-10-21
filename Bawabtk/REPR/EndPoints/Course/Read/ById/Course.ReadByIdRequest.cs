using Microsoft.AspNetCore.Mvc;

namespace Bawabtk.REPR.EndPoints.Course.Read.ById
{
    public class CourseRequest
    {
        [FromRoute]
        public Guid CourseId { get; set; }

    }
}
