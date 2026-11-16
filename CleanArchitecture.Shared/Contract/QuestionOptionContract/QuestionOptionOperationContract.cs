using System;
using System.Collections.Generic;
using System.Text;

namespace CleanArchitecture.Shared.Contract.QuestionOptionContract
{
    public class QuestionOptionOperationContract
    {
        public Guid QuestionId { get; set; }
        public string Text { get; set; } = string.Empty;
        public bool IsCorrect { get; set; }
    }
}
