
using static CleanArchitecture.Core.Entites.Enums;

namespace CleanArchitecture.Core.Entites
{
    public class AttendanceRecord:BaseEntity
    {
        public Guid _AttendanceId { get; set; }
        public int SessionId { get; set; } // FK -> OfflineSession.Id
        public int StudentId { get; set; } // FK -> StudentProfile.Id
        public int? ScannedByAssistantId { get; set; } // FK -> User.Id
        public DateTimeOffset? ScannedAt { get; set; }
        public AttendanceStatus Status { get; set; }
        public bool IsAbsenceAlertSent { get; set; }
        public string Notes { get; set; } = string.Empty;

        // Relations
        public OfflineSession Session { get; set; } = null!;
        public StudentProfile Student { get; set; } = null!;
        public User? ScannedByAssistant { get; set; }

        public AttendanceRecord() { } // ef core ctor

        public AttendanceRecord(int sessionId,
                                int studentId,
                                int? scannedByAssistantId,
                                AttendanceStatus status,
                                Guid createdBy,
                                string notes = "")
        {
            _AttendanceId = Guid.NewGuid();
            SessionId = sessionId;
            StudentId = studentId;
            ScannedByAssistantId = scannedByAssistantId;
            ScannedAt = status == AttendanceStatus.Absent ? null : DateTimeOffset.Now;
            Status = status;
            IsAbsenceAlertSent = false;
            Notes = notes;
            CreatedBy = createdBy;
            CreatedOn = DateTimeOffset.Now;
            ModifiedBy = Guid.Empty;
            ModifiedOn = DateTimeOffset.Now;
        }
    }
}
