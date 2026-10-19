using Microsoft.AspNetCore.Mvc;

namespace Bawabtk.REPR.EndPoints.Students.Read.ByParentId
{
    public class StudentRequest
    {
        [FromRoute]
        public Guid StudentId { get; set; }
        public Guid ParentId { get; set; }
    }
}
