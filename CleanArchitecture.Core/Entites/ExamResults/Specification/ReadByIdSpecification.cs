using Ardalis.Specification;

namespace CleanArchitecture.Core.Entites.ExamResults.Specification
{
    public class ReadByIdSpecification:SingleResultSpecification<ExamResult>
    {
        public ReadByIdSpecification(Guid id)
        {
            Query.Where(er => er._ExamResultId == id);
        }
    }
}
