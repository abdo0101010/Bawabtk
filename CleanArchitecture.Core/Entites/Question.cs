using CleanArchitecture.Core.Entites.Exams;
using System;
using System.Collections.Generic;
using System.Text;

namespace CleanArchitecture.Core.Entites
{
    public class Question:BaseEntity
    {
        public Guid _QuestionId { get; set; }
        public int ExamId { get; set; } // FK -> Exam.Id
        public string Text { get; set; } = string.Empty;
        public decimal Points { get; set; }
        public int SortOrder { get; set; }// درجة السؤال ده كام؟

        // Relations
        public Exam Exam { get; set; } = null!;
        public List<QuestionOption> Options { get; set; } = new();

        public Question() { } // ef core ctor

        public Question(int examId, string text, decimal points, Guid createdBy)
        {
            _QuestionId = Guid.NewGuid();
            ExamId = examId;
            Text = text;
            Points = points;
            CreatedBy = createdBy;
            CreatedOn = DateTimeOffset.Now;
            ModifiedBy = Guid.Empty;
            ModifiedOn = DateTimeOffset.Now;
        }
    }
}
