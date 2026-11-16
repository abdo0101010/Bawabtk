

using Ardalis.Specification;

namespace CleanArchitecture.Core.Entites.QuestionOption.Specification
{
    public class ReadByIdSpecification:SingleResultSpecification<QuestionOption>
    {
        public ReadByIdSpecification(Guid id)
        {
            Query.Where(e => e._OptionId == id);
        }
    }
}
