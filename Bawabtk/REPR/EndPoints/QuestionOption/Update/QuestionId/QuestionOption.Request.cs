using Microsoft.AspNetCore.Mvc;

namespace Bawabtk.REPR.EndPoints.QuestionOption.Update.QuestionId
{
    public class QuestionOptionRequest
    {
        [FromRoute]
        public Guid OptionId { get; set; }
        [FromRoute]
        public Guid QuestionId { get; set; }
    }
}
