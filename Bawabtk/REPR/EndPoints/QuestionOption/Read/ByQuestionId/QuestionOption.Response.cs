namespace Bawabtk.REPR.EndPoints.QuestionOption.Read.ByQuestionId
{
    public class QuestionOptionResponse
    {
        public Guid OptionId { get; set; }
        public Guid QuestionId { get; set; }
        public string Text { get; set; } = string.Empty;
        public bool IsCorrect { get; set; }
    
    }
}
