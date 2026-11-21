using Microsoft.AspNetCore.Mvc;

namespace Bawabtk.REPR.EndPoints.QuestionOption.Update
{
    public class QuestionOptionRequest
    {
        [FromRoute]
        public Guid OptionId { get; set; }
        public Guid QuestionId { get; set; }
        public string Text { get; set; } = string.Empty;
        public bool IsCorrect { get; set; }
    }
}
