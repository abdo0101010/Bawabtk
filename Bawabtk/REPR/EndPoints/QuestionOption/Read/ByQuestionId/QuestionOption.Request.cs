using Microsoft.AspNetCore.Mvc;

namespace Bawabtk.REPR.EndPoints.QuestionOption.Read.ByQuestionId
{
    public class QuestionOptionRequest
    {
        [FromRoute]
        public Guid QuestionId { get; set; }
        [FromRoute]
        public Guid OptionId { get; set; }

    }
}
