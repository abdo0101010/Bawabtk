
namespace CleanArchitecture.Core.Entites
{
    public class Enums
    {
        public enum UserRole { Teacher = 1, Assistant = 2, Student = 3, Parent = 4 }
        public enum CourseType { OnlineOnly = 1, OfflineCenterOnly = 2, Hybrid = 3 }
        public enum SubscriptionType { Online = 1, Offline = 2, Hybrid = 3 }
        public enum EnrollmentStatus { Active = 1, Expired = 2, Suspended = 3 }
        public enum SessionStatus { Scheduled = 1, ActiveScanning = 2, Closed = 3 }
        public enum AttendanceStatus { Present = 1, Late = 2, Absent = 3 }
        public enum NotificationType { UnifiedBarcodeWelcome = 1, AbsenceAlert = 2, LowGradeAlert = 3, MonthlyReport = 4 }
        public enum DeliveryStatus { Queued = 1, SentToN8n = 2, DeliveredToWhatsApp = 3, Failed = 4 }
    }
}
