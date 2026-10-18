using Microsoft.AspNetCore.Mvc;

namespace Bawabtk.REPR.EndPoints.Students.Delete
{
    public class StudentRequest
    {
        [FromRoute]
        public Guid StudentId { get; set; }
    }
}
