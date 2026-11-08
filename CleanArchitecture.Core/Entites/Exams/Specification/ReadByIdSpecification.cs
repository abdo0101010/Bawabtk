using Ardalis.Specification;

namespace CleanArchitecture.Core.Entites.Exams.Specification
{
    public class ReadByIdSpecification:SingleResultSpecification<Exam>
    {
        public ReadByIdSpecification(Guid ExamId)
        {
            Query.Where(e => e._ExamId == ExamId);
        }
    }
    
    
}
