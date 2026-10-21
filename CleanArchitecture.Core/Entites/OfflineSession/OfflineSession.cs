using CleanArchitecture.Core.Entites.Courses;
using CleanArchitecture.Core.Entites.Users;
using static CleanArchitecture.Core.Entites.Enums;

namespace CleanArchitecture.Core.Entites.OfflineSession
{
    public class OfflineSession:BaseEntity
    {
        public Guid _SessionId { get; set; }
        public int CourseId { get; set; } // FK -> Course.Id
        public string Title { get; set; } = string.Empty;
        public string CenterName { get; set; } = string.Empty;
        public string? RoomNumber { get; set; }
        public DateTimeOffset StartTime { get; set; }
        public DateTimeOffset EndTime { get; set; }
        public SessionStatus Status { get; set; }
        public DateTimeOffset? ClosedAt { get; set; }
        public int? ClosedByUserId { get; set; } // FK -> User.Id
        public bool IsAbsenceWebhookTriggered { get; set; }

        // Relations
        public Course Course { get; set; } = null!;
        public User? ClosedByUser { get; set; }
        public List<Core.Entites.AttendanceRecord.AttendanceRecord> AttendanceRecords { get; set; } = new();

        public OfflineSession() { } // ef core ctor

        public OfflineSession(int courseId,
                              string title,
                              string centerName,
                              DateTimeOffset startTime,
                              DateTimeOffset endTime,
                              Guid createdBy,
                              string? roomNumber = null)
        {
            _SessionId = Guid.NewGuid();
            CourseId = courseId;
            Title = title;
            CenterName = centerName;
            RoomNumber = roomNumber;
            StartTime = startTime;
            EndTime = endTime;
            Status = SessionStatus.Scheduled;
            IsAbsenceWebhookTriggered = false;
            CreatedBy = createdBy;
            CreatedOn = DateTimeOffset.Now;
            ModifiedBy = Guid.Empty;
            ModifiedOn = DateTimeOffset.Now;
        }

        public void OpenScanning(Guid modifiedBy)
        {
            Status = SessionStatus.ActiveScanning;
            ModifiedBy = modifiedBy;
            ModifiedOn = DateTimeOffset.Now;
        }

        public void CloseSession(int closedByUserId, Guid modifiedByGuid)
        {
            Status = SessionStatus.Closed;
            ClosedAt = DateTimeOffset.Now;
            ClosedByUserId = closedByUserId;
            ModifiedBy = modifiedByGuid;
            ModifiedOn = DateTimeOffset.Now;
        }
    }
}
