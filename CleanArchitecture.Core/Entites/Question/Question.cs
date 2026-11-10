using CleanArchitecture.Core.Entites.Exams;
using System;
using System.Collections.Generic;
using System.Text;

namespace CleanArchitecture.Core.Entites.Question
{
    public class Question:BaseEntity
    {
        public Guid _QuestionId { get; set; }
        public int ExamId { get; set; }
        public string Text { get; set; } = string.Empty;
        public decimal Points { get; set; }
        public int SortOrder { get; set; }

        // Relations
        public Exam Exam { get; set; } = null!;
        public List<QuestionOption> Options { get; set; } = new();

        public Question() { } // ef core ctor

        public Question( string text, decimal points, Guid createdBy, int sortOrder)
        {
            _QuestionId = Guid.NewGuid();
          
            Text = text;
            Points = points;
            SortOrder = sortOrder;
            CreatedBy = createdBy;
            CreatedOn = DateTimeOffset.Now;
            ModifiedBy = Guid.Empty;
            ModifiedOn = DateTimeOffset.Now;
        }
        public void UpdateQuestion(string text, decimal points, int sortOrder, Guid modifiedBy)
        {
          
            Text = text;
            Points = points;
            SortOrder = sortOrder;
            ModifiedBy = modifiedBy;
            ModifiedOn = DateTimeOffset.Now;
        }
        public void SetExam(Exam exam) => Exam = exam;
    }
}
