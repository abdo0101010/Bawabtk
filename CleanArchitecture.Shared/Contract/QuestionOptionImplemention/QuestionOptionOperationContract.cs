
namespace CleanArchitecture.Shared.Contract.QuestionOptionImplemention 
{ 
    public class QuestionOptionOperationContract
    {
        public Guid QuestionId { get; set; }
        public string Text { get; set; } = string.Empty;
        public bool IsCorrect { get; set; }
    }
}
