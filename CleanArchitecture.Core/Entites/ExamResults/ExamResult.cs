using CleanArchitecture.Core.Entites.Exams;
using CleanArchitecture.Core.Entites.Student;
using CleanArchitecture.Core.Entites.Users;

namespace CleanArchitecture.Core.Entites.ExamResults
{
    public class ExamResult:BaseEntity
    {
        public Guid _ExamResultId { get; set; }
        public int ExamId { get; set; }    
        public int StudentId { get; set; } 
        public decimal Score { get; set; }
        public DateTimeOffset SubmittedAt { get; set; }
        public int? GradedByUserId { get; set; } 
        public string? TeacherFeedback { get; set; }
        public bool IsLowGradeAlertSent { get; set; }

        public Exam Exam { get; set; } = null!;
        public StudentProfile Student { get; set; } = null!;
        public User? GradedByUser { get; set; }

        public ExamResult() { } 

        public ExamResult(
                         
                          decimal score,
                          Guid createdBy,
                         
                          string? teacherFeedback = null)
        {
            _ExamResultId = Guid.NewGuid();
            Score = score;
            SubmittedAt = DateTimeOffset.Now;
            TeacherFeedback = teacherFeedback;
            IsLowGradeAlertSent = false;
            CreatedBy = createdBy;
            CreatedOn = DateTimeOffset.Now;
            ModifiedBy = Guid.Empty;
            ModifiedOn = DateTimeOffset.Now;
        }
        public void SetExam(Exam exam) => Exam = exam;
        
        public void Update(decimal score, bool isLowGradeAlertSent)
        {
            Score = score;

            IsLowGradeAlertSent = isLowGradeAlertSent;

        }
        public void SetStudent(StudentProfile student) => Student = student;
        public void SetUser(User user) => GradedByUser = user;
    }
}
