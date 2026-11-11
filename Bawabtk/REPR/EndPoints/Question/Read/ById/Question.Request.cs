using Microsoft.AspNetCore.Mvc;

namespace Bawabtk.REPR.EndPoints.Question.Read.ById
{
    public class QuestionRequest
    {
        [FromRoute]
        public Guid QuestionId { get; set; }
    
    }
}
