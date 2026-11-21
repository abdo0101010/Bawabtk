namespace CleanArchitecture.Shared.Contract.QuestionOptionImplemention
{
    public class QuestionOptionContract
    {
        public Guid _OptionId { get; set; }
        public Guid QuestionId { get; set; } 
        public string Text { get; set; } = string.Empty; 
        public bool IsCorrect { get; set; } 
    }
}
