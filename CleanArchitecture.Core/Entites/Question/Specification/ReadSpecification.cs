using Ardalis.Specification;

namespace CleanArchitecture.Core.Entites.Question.Specification
{
    public class ReadSpecification:Specification<Question>
    {
        public ReadSpecification()
        {
            Query.Where(Q=>Q._QuestionId != Guid.Empty).Include(c => c.Exam );
        }

    }
}
