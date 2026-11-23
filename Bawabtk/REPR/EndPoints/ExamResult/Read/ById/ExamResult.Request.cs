using Microsoft.AspNetCore.Mvc;

namespace Bawabtk.REPR.EndPoints.ExamResult.Read.ById
{
    public class ExamResultRequest
    {
        [FromRoute]
        public Guid ExamResultId { get; set; }
    }
}
