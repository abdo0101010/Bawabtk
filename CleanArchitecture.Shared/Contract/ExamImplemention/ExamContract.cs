namespace CleanArchitecture.Shared.Contract.ExamImplemention
{
    public class ExamContract: ExamOperationContract
    {
        public Guid ExamId { get; set; }
        public ExamContract(Guid examId, Guid courseId, string title, decimal maxScore, decimal passingScore, decimal lowGradeAlertThreshold, DateTimeOffset examDate, bool isOnline)
            : base(courseId, title, maxScore, passingScore, lowGradeAlertThreshold, examDate, isOnline)
        {
            ExamId = examId;
        }

    }
}
