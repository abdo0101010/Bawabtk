using Microsoft.AspNetCore.Mvc;

namespace Bawabtk.REPR.EndPoints.Students.Read.ById
{
    public class StudentRequest
    {
        [FromRoute]
        public Guid StudentId { get; set; }

    }
}
