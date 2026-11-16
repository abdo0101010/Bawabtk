

using Ardalis.Specification;

namespace CleanArchitecture.Core.Entites.QuestionOption.Specification
{
    public class ReadSpecification: Specification<QuestionOption>
    {
        public ReadSpecification()
        {
            Query.Include(e => e.Question);
        }
    }
    
}
