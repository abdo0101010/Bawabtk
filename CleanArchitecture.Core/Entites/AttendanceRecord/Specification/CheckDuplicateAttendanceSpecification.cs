using Ardalis.Specification;

namespace CleanArchitecture.Core.Entites.AttendanceRecord.Specification
{
    public class CheckDuplicateAttendanceSpecification:SingleResultSpecification<AttendanceRecord>
    {
        public CheckDuplicateAttendanceSpecification(int sessionId, int studentId)
        {
            Query.Where(a => a.SessionId == sessionId && a.StudentId == studentId);
        }
    }
}
