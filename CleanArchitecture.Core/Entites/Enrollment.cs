

using static CleanArchitecture.Core.Entites.Enums;

namespace CleanArchitecture.Core.Entites
{
    public class Enrollment:BaseEntity
    {
        public Guid _EnrollmentId { get; set; }
        public int StudentId { get; set; } // FK -> StudentProfile.Id
        public int CourseId { get; set; }  // FK -> Course.Id
        public SubscriptionType SubscriptionType { get; set; }
        public EnrollmentStatus Status { get; set; }
        public DateTimeOffset EnrolledAt { get; set; }
        public DateTimeOffset? ExpiresAt { get; set; }
        public decimal AmountPaid { get; set; }

        // Relations
        public StudentProfile Student { get; set; } = null!;
        public Course Course { get; set; } = null!;

        public Enrollment() { } // ef core ctor

        public Enrollment(int studentId,
                          int courseId,
                          SubscriptionType subscriptionType,
                          decimal amountPaid,
                          Guid createdBy,
                          DateTimeOffset? expiresAt = null)
        {
            _EnrollmentId = Guid.NewGuid();
            StudentId = studentId;
            CourseId = courseId;
            SubscriptionType = subscriptionType;
            Status = EnrollmentStatus.Active;
            EnrolledAt = DateTimeOffset.Now;
            ExpiresAt = expiresAt;
            AmountPaid = amountPaid;
            CreatedBy = createdBy;
            CreatedOn = DateTimeOffset.Now;
            ModifiedBy = Guid.Empty;
            ModifiedOn = DateTimeOffset.Now;
        }
    }
}
