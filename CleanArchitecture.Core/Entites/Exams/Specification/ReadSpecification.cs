using Ardalis.Specification;

namespace CleanArchitecture.Core.Entites.Exams.Specification
{
    public class ReadSpecification:Specification<Exam>
    {
        public ReadSpecification()
        {
            Query.Include(e => e.Course)
                 .Include(e => e.Questions)
                 .Include(e => e.ExamResults);
        }
    
    }
}
