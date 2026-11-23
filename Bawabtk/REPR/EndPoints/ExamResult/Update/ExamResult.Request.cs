using Microsoft.AspNetCore.Mvc;

namespace Bawabtk.REPR.EndPoints.ExamResult.Update
{
    public class ExamResultRequest
    {
        [FromRoute]
        public Guid ExamResultId { get; set; }
        public Guid ExamId { get; set; }
        public Guid StudentId { get; set; }
        public decimal Score { get; set; }
        public DateTimeOffset SubmittedAt { get; set; }
        public Guid GradedByUserId { get; set; }
        public string? TeacherFeedback { get; set; }
        public bool IsLowGradeAlertSent { get; set; }
    }
}
