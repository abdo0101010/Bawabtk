using Microsoft.AspNetCore.Http.HttpResults;

namespace CleanArchitecture.Core.Entites
{
    public class QuestionOption:BaseEntity
    {
        public Guid _OptionId { get; set; }
        public int QuestionId { get; set; } // FK -> Question.Id
        public string Text { get; set; } = string.Empty; // نص الاختيار (أ، ب، ج...)
        public bool IsCorrect { get; set; } // هل دي الإجابة الصح؟

        // Relations
        public Question Question { get; set; } = null!;

        public QuestionOption() { } // ef core ctor

        public QuestionOption(int questionId, string text, bool isCorrect, Guid createdBy)
        {
            _OptionId = Guid.NewGuid();
            QuestionId = questionId;
            Text = text;
            IsCorrect = isCorrect;
            CreatedBy = createdBy;
            CreatedOn = DateTimeOffset.Now;
            ModifiedBy = Guid.Empty;
            ModifiedOn = DateTimeOffset.Now;
        }
    }
}
