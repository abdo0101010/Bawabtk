using Ardalis.Specification;

namespace CleanArchitecture.Core.Entites.ExamResults.Specification
{
    public class ReadSpecification : Specification<ExamResult>
    {
        public ReadSpecification()
        {
            Query.Include(er => er.Exam)
                 .Include(er => er.Student)
                 .Include(er => er.GradedByUser);

        }
    }
}
