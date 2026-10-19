using Microsoft.AspNetCore.Mvc;

namespace Bawabtk.REPR.EndPoints.Students.Update.ParentById
{
    public class StudentRequest
    {
        [FromRoute]
        public Guid StudentId { get; set; }
        public Guid ParentId { get; set; }
    }
}
