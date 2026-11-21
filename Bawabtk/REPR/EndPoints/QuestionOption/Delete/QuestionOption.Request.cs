using Microsoft.AspNetCore.Mvc;

namespace Bawabtk.REPR.EndPoints.QuestionOption.Delete
{
    public class QuestionOptionRequest
    {
        [FromRoute]
        public Guid OptionId { get; set; }
    }
}
