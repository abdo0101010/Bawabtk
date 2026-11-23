using Microsoft.AspNetCore.Mvc;

namespace Bawabtk.REPR.EndPoints.ExamResult.Delete
{
    public class ExamResultRequest
    {
        [FromRoute]
        public Guid ExamResultId { get; set; }
    }
}
