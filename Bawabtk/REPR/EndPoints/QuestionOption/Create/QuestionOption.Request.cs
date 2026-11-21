namespace Bawabtk.REPR.EndPoints.QuestionOption.Create
{
    public class QuestionOptionRequest
    {
        public string Text { get; set; } = string.Empty;
        public bool IsCorrect { get; set; }
        public Guid QuestionId { get; set; }
    }
   
}
