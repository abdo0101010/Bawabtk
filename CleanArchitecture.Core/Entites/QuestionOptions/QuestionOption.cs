using Microsoft.AspNetCore.Http.HttpResults;

namespace CleanArchitecture.Core.Entites.QuestionOption
{
    public class QuestionOption:BaseEntity
    {
        public Guid _OptionId { get; set; }
        public int QuestionId { get; set; } 
        public string Text { get; set; } = string.Empty; 
        public bool IsCorrect { get; set; } 

        // Relations
        public Question.Question Question { get; set; } = null!;

        public QuestionOption() { } // ef core ctor

        public QuestionOption( string text, bool isCorrect, Guid createdBy)
        {
            _OptionId = Guid.NewGuid();
            Text = text;
            IsCorrect = isCorrect;
            CreatedBy = createdBy;
            CreatedOn = DateTimeOffset.Now;
            ModifiedBy = Guid.Empty;
            ModifiedOn = DateTimeOffset.Now;
        }
        public void SetQuestion(Question.Question question) => Question = question;
        public void Update(string text, bool isCorrect, Guid modifiedBy)
        {
            Text = text;
            IsCorrect = isCorrect;
            ModifiedBy = modifiedBy;
            ModifiedOn = DateTimeOffset.Now;
        }

    }
}
