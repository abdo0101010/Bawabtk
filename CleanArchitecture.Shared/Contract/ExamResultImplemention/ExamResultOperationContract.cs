
namespace CleanArchitecture.Shared.Contract.ExamResultImplemention
{
    public class ExamResultOperationContract
    {
        public Guid ExamId { get; set; }   
        public Guid StudentId { get; set; }
        public decimal Score { get; set; }
        public DateTimeOffset SubmittedAt { get; set; }
        public Guid GradedByUserId { get; set; } 
        public string? TeacherFeedback { get; set; }
        public bool IsLowGradeAlertSent { get; set; }
    }
}
