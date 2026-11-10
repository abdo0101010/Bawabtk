
using Ardalis.Specification;

namespace CleanArchitecture.Core.Entites.Question.Specification
{
    public class ReadByIdSpecification: SingleResultSpecification<Question>
    {
        public ReadByIdSpecification(Guid QuestionId)
        {
            Query.Where(e => e._QuestionId == QuestionId).Include(c => c.Exam);
        }
    }   
    

    
}
