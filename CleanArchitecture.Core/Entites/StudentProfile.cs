
using CleanArchitecture.Core.Entites.ParentsProfile;
using CleanArchitecture.Core.Entites.Users;

namespace CleanArchitecture.Core.Entites
{
    public class StudentProfile : BaseEntity
    {
        public Guid _StudentProfileId { get; set; }
        public int UserId { get; set; }   // FK -> User.Id
        public int ParentId { get; set; } // FK -> ParentProfile.Id
        public string UnifiedBarcode { get; set; } = string.Empty;
        public string GradeLevel { get; set; } = string.Empty;
        public string? SchoolName { get; set; }
        public bool IsBarcodeSentViaWhatsApp { get; set; }

        // Relations
        public User User { get; set; } = null!;
        public ParentProfile Parent { get; set; } = null!;
        public List<Enrollment> Enrollments { get; set; } = new();
        public List<AttendanceRecord> AttendanceRecords { get; set; } = new();
        public List<ExamResult> ExamResults { get; set; } = new();
        public List<AutomatedNotificationLog> NotificationLogs { get; set; } = new();

        public StudentProfile() { } // ef core ctor

        public StudentProfile(int userId,
                              int parentId,
                              Guid createdByUserId,
                              string gradeLevel,
                              string? schoolName = null)
        {
            _StudentProfileId = Guid.NewGuid();
            UserId = userId;
            ParentId = parentId;
            UnifiedBarcode = $"BW-{DateTimeOffset.Now.Year}-{Random.Shared.Next(100000, 999999)}";
            GradeLevel = gradeLevel;
            SchoolName = schoolName;
            IsBarcodeSentViaWhatsApp = false;
            CreatedBy = createdByUserId;
            CreatedOn = DateTimeOffset.Now;
            ModifiedBy = Guid.Empty;
            ModifiedOn = DateTimeOffset.Now;
        }
        public void MarkBarcodeSent(Guid modifiedBy)
        {
            IsBarcodeSentViaWhatsApp = true;
            ModifiedBy = modifiedBy;
            ModifiedOn = DateTimeOffset.Now;
        }
    }
}  
