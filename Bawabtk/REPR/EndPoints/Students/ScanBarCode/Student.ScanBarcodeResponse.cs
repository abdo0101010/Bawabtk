using CleanArchitecture.Shared.Contract.AttendanceImplemention;
using static CleanArchitecture.Core.Entites.Enums;

namespace Bawabtk.REPR.EndPoints.Students.ScanBarCode
{
    public class StudentResponse
    {
        public Guid AttendanceId { get; set; }
        public Guid StudentProfileId { get; set; }
        public string StudentName { get; set; } = string.Empty;
        public string UnifiedBarcode { get; set; } = string.Empty;
        public string CourseTitle { get; set; } = string.Empty;
        public string SessionTitle { get; set; } = string.Empty;
        public AttendanceStatus Status { get; set; }
        public DateTimeOffset ?ScannedAt { get; set; }
        public string Message { get; set; } = string.Empty;

        public StudentResponse(AttendanceContract contract)
        {
            AttendanceId = contract.AttendanceId;
            StudentProfileId = contract.StudentProfileId;
            StudentName = contract.StudentName;
            UnifiedBarcode = contract.UnifiedBarcode;
            CourseTitle = contract.CourseTitle;
            SessionTitle = contract.SessionTitle;
            Status = contract.Status;
            ScannedAt = contract.ScannedAt;
            Message = contract.Message;
        }
    }
}
