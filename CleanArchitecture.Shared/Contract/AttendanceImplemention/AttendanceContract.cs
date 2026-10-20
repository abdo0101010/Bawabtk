using static CleanArchitecture.Core.Entites.Enums;

namespace CleanArchitecture.Shared.Contract.AttendanceImplemention
{
    public class AttendanceContract
    {
        public Guid AttendanceId { get; set; }
        public Guid StudentProfileId { get; set; }
        public string StudentName { get; set; } = string.Empty;
        public string UnifiedBarcode { get; set; } = string.Empty;
        public string CourseTitle { get; set; } = string.Empty;
        public string SessionTitle { get; set; } = string.Empty;
        public AttendanceStatus Status { get; set; }
        public DateTimeOffset? ScannedAt { get; set; }
        public string Message { get; set; } = string.Empty;
    }
}
