namespace Bawabtk.REPR.EndPoints.Exam.Update
{
    public class ExamResponse
    {
        public Guid ExamId { get; set; }
        public Guid CourseId { get; set; }
        public string Title { get; set; } = string.Empty;
        public decimal MaxScore { get; set; }
        public decimal PassingScore { get; set; }
        public decimal LowGradeAlertThreshold { get; set; }
        public DateTimeOffset ExamDate { get; set; }
        public bool IsOnline { get; set; }
    }
}
