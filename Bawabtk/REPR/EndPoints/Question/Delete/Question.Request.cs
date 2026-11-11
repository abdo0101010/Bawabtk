using Microsoft.AspNetCore.Mvc;

namespace Bawabtk.REPR.EndPoints.Question.Delete
{
    public class QuestionRequest
    {
        [FromRoute]
        public Guid QuestionId { get; set; }
    
    }
}
