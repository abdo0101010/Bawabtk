

using CleanArchitecture.Core.Entites.Student;
using CleanArchitecture.Core.Entites.Users;

namespace CleanArchitecture.Core.Entites
{
    public class ExamResult:BaseEntity
    {
        public Guid _ExamResultId { get; set; }
        public int ExamId { get; set; }    // FK -> Exam.Id
        public int StudentId { get; set; } // FK -> StudentProfile.Id
        public decimal Score { get; set; }
        public DateTimeOffset SubmittedAt { get; set; }
        public int? GradedByUserId { get; set; } // FK -> User.Id
        public string? TeacherFeedback { get; set; }
        public bool IsLowGradeAlertSent { get; set; }

        // Relations
        public Exam Exam { get; set; } = null!;
        public StudentProfile Student { get; set; } = null!;
        public User? GradedByUser { get; set; }

        public ExamResult() { } // ef core ctor

        public ExamResult(int examId,
                          int studentId,
                          decimal score,
                          Guid createdBy,
                          int? gradedByUserId = null,
                          string? teacherFeedback = null)
        {
            _ExamResultId = Guid.NewGuid();
            ExamId = examId;
            StudentId = studentId;
            Score = score;
            SubmittedAt = DateTimeOffset.Now;
            GradedByUserId = gradedByUserId;
            TeacherFeedback = teacherFeedback;
            IsLowGradeAlertSent = false;
            CreatedBy = createdBy;
            CreatedOn = DateTimeOffset.Now;
            ModifiedBy = Guid.Empty;
            ModifiedOn = DateTimeOffset.Now;
        }
    }
}
