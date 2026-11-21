using Microsoft.AspNetCore.Mvc;

namespace Bawabtk.REPR.EndPoints.QuestionOption.Read.ById
{
    public class QuestionOptionRequest
    {
        [FromRoute]
        public Guid OptionId { get; set; }
    }
}
