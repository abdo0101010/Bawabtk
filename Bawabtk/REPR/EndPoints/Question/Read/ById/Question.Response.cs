namespace Bawabtk.REPR.EndPoints.Question.Read.ById
{
    public class QuestionResponse
    {
        public Guid QuestionId { get; set; }
        public Guid ExamId { get; set; }
        public string Text { get; set; } = string.Empty;
        public decimal Points { get; set; }
        public int SortOrder { get; set; }
    
    }
}
