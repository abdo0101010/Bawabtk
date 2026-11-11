using Microsoft.AspNetCore.Mvc;

namespace Bawabtk.REPR.EndPoints.Question.Read.ByExamId
{
    public class QuestionRequest
    {
        [FromRoute]
        public Guid ExamId { get; set; }
        [FromRoute]
        public Guid QuestionId { get; set; }

    }
}
