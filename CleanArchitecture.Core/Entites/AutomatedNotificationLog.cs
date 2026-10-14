
using static CleanArchitecture.Core.Entites.Enums;

namespace CleanArchitecture.Core.Entites
{
    public class AutomatedNotificationLog:BaseEntity
    {
        public Guid _NotificationLogId { get; set; }
        public int StudentId { get; set; } // FK -> StudentProfile.Id
        public string RecipientPhone { get; set; } = string.Empty;
        public NotificationType NotificationType { get; set; }
        public Guid? ReferenceEntityGuid { get; set; } // _SessionId or _ExamId
        public string? PayloadJson { get; set; }
        public DeliveryStatus DeliveryStatus { get; set; }
        public string? ErrorMessage { get; set; }
        public DateTimeOffset? SentAt { get; set; }

        // Relations
        public StudentProfile Student { get; set; } = null!;

        public AutomatedNotificationLog() { } // ef core ctor

        public AutomatedNotificationLog(int studentId,
                                        string recipientPhone,
                                        NotificationType notificationType,
                                        Guid createdBy,
                                        Guid? referenceEntityGuid = null,
                                        string? payloadJson = null)
        {
            _NotificationLogId = Guid.NewGuid();
            StudentId = studentId;
            RecipientPhone = recipientPhone;
            NotificationType = notificationType;
            ReferenceEntityGuid = referenceEntityGuid;
            PayloadJson = payloadJson;
            DeliveryStatus = DeliveryStatus.Queued;
            CreatedBy = createdBy;
            CreatedOn = DateTimeOffset.Now;
            ModifiedBy = Guid.Empty;
            ModifiedOn = DateTimeOffset.Now;
        }
    }
}
