namespace Bawabtk.REPR.EndPoints.Question.Create
{
    public class QuestionRequest
    {
        public Guid ExamId { get; set; }
        public string Text { get; set; } = string.Empty;
        public decimal Points { get; set; }
        public int SortOrder { get; set; }
    }
}
