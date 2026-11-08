using Microsoft.AspNetCore.Http.HttpResults;

namespace CleanArchitecture.Core.Entites
{
    public class StudentAnswer:BaseEntity   
    {
        public Guid _StudentAnswerId { get; set; }

        public int ExamResultId { get; set; }
        public int QuestionId { get; set; } 

        public int? SelectedOptionId { get; set; } 

        public bool IsCorrect { get; set; }
        public decimal PointsAwarded { get; set; } 

        public ExamResult ExamResult { get; set; } = null!;
        public Question Question { get; set; } = null!;
        public QuestionOption? SelectedOption { get; set; }

        public StudentAnswer() { }

        public StudentAnswer(int examResultId, int questionId, int? selectedOptionId, bool isCorrect, decimal pointsAwarded, Guid createdBy)
        {
            _StudentAnswerId = Guid.NewGuid();
            ExamResultId = examResultId;
            QuestionId = questionId;
            SelectedOptionId = selectedOptionId;
            IsCorrect = isCorrect;
            PointsAwarded = pointsAwarded;

            CreatedBy = createdBy;
            CreatedOn = DateTimeOffset.Now;
            ModifiedBy = Guid.Empty;
            ModifiedOn = DateTimeOffset.Now;
        }
    }
}
