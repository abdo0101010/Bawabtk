using Ardalis.Specification;
using static CleanArchitecture.Core.Entites.Enums;
namespace CleanArchitecture.Core.Entites.Student.Specefication
{
    public class ReadByBarcodeSpecification:SingleResultSpecification<StudentProfile>
    {
        public ReadByBarcodeSpecification(string Barcode, int courseId)
        {
            Query.Where(p => p.UnifiedBarcode == Barcode)
                .Include(u=>u.User)
                .Include(s => s.Enrollments.Where(e =>
                     e.CourseId == courseId &&
                     e.Status == EnrollmentStatus.Active &&
                     (e.ExpiresAt == null || e.ExpiresAt > DateTimeOffset.Now)));

        }
    }
}
