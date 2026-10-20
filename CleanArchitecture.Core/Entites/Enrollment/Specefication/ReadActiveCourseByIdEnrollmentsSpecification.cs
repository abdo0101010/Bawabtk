using Ardalis.Specification;
using static CleanArchitecture.Core.Entites.Enums;

namespace CleanArchitecture.Core.Entites.Enrollment.Specefication
{
    public class ReadActiveCourseByIdEnrollmentsSpecification: Specification<Enrollment>
    {
        public ReadActiveCourseByIdEnrollmentsSpecification(int courseId)
        {
            Query.Where(e => e.CourseId == courseId && e.Status == EnrollmentStatus.Active);
        }
    }
}
