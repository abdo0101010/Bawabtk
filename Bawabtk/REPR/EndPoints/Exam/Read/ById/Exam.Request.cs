using Microsoft.AspNetCore.Mvc;

namespace Bawabtk.REPR.EndPoints.Exam.Read.ById
{
    public class ExamRequest
    {
        [FromRoute]
        public Guid ExamId { get; set; }
    
    }
}
