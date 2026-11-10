using Ardalis.Specification;

namespace CleanArchitecture.Core.Entites.Question.Specification
{
    public class ReadSingleByExamIdSpecification : SingleResultSpecification<Question>
    {
        public ReadSingleByExamIdSpecification(Guid QuestionId,Guid ExamID)
        {
            Query.Where(Q=> Q.Exam._ExamId == ExamID && Q._QuestionId == QuestionId).Include(c => c.Exam);
        }
    
    }
}
