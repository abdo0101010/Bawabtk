using CleanArchitecture.Core.Entites.Courses;

namespace CleanArchitecture.Core.Entites.Exams
{
    public class Exam:BaseEntity
    {
        public Guid _ExamId { get; set; }
        public int CourseId { get; set; } // FK -> Course.Id
        public string Title { get; set; } = string.Empty;
        public decimal MaxScore { get; set; }
        public decimal PassingScore { get; set; }
        public decimal LowGradeAlertThreshold { get; set; }
        public DateTimeOffset ExamDate { get; set; }
        public bool IsOnline { get; set; }

        // Relations
        public Course Course { get; set; } = null!;
        public List<ExamResult> ExamResults { get; set; } = new();
        public List<Question.Question> Questions { get; set; } = new();

        public Exam() { } // ef core ctor

        public Exam(
                    string title,
                    decimal maxScore,
                    decimal passingScore,
                    decimal lowGradeAlertThreshold,
                    DateTimeOffset examDate,
                    bool isOnline,
                    Guid createdBy)
        {
            _ExamId = Guid.NewGuid();
            Title = title;
            MaxScore = maxScore;
            PassingScore = passingScore;
            LowGradeAlertThreshold = lowGradeAlertThreshold;
            ExamDate = examDate;
            IsOnline = isOnline;
            CreatedBy = createdBy;
            CreatedOn = DateTimeOffset.Now;
            ModifiedBy = Guid.Empty;
            ModifiedOn = DateTimeOffset.Now;
        }
        public void UpdateExam(
                               string title,
                               decimal maxScore,
                               decimal passingScore,
                               decimal lowGradeAlertThreshold,
                               DateTimeOffset examDate,
                               bool isOnline,
                               Guid modifiedBy)
        {
            Title = title;
            MaxScore = maxScore;
            PassingScore = passingScore;
            LowGradeAlertThreshold = lowGradeAlertThreshold;
            ExamDate = examDate;
            IsOnline = isOnline;
            ModifiedBy = modifiedBy;
            ModifiedOn = DateTimeOffset.Now;
        }
        public void SetCourse(Course course)
        {
            Course = course;
        }
    }
}
