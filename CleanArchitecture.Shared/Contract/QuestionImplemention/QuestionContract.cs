
namespace CleanArchitecture.Shared.Contract.QuestionImplemention
{
    public class QuestionContract : QuestionOperationContract
    {
        public Guid QuestionId { get; set; }
        public QuestionContract(Guid questionId, Guid examId, string text, decimal points, int sortOrder) 
        {
            QuestionId = questionId;
            ExamId = examId;
            Text = text;
            Points = points;
            SortOrder = sortOrder;
        }
    }
}
