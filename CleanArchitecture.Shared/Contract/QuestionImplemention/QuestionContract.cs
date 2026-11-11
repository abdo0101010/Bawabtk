
namespace CleanArchitecture.Shared.Contract.QuestionImplemention
{
    public class QuestionContract : QuestionOperationContract
    {
        public Guid QuestionId { get; set; }
        public QuestionContract(Guid questionId, Guid examId, string text, decimal points, int sortOrder) : base(text, points, examId, sortOrder)
        {
            QuestionId = questionId;
            
        }
    }
}
