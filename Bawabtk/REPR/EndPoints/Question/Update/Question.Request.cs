namespace Bawabtk.REPR.EndPoints.Question.Update
{
    public class QuestionRequest
    {
        public Guid QuestionId { get; set; }
        public string Text { get; set; } = string.Empty;
        public decimal Points { get; set; }
        public int SortOrder { get; set; }
    
    }
}
