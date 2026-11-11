namespace CleanArchitecture.Shared.Contract.QuestionImplemention
{
    public class QuestionOperationContract
    {
        public Guid ExamId { get; set; } 
        public string Text { get; set; } = string.Empty;
        public decimal Points { get; set; }
        public int SortOrder { get; set; }
        public QuestionOperationContract(string text, decimal points, Guid examId, int sortOrder)
        {
            Text = text;
            Points = points;
            ExamId = examId;
            SortOrder = sortOrder;
        }
    }
}
