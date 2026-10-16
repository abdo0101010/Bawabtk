
using CleanArchitecture.Core.Entites.Users;

namespace CleanArchitecture.Core.Entites
{
    public class ParentProfile:BaseEntity
    {
        public Guid _ParentProfileId { get; set; }
        public int UserId { get; set; } // FK -> User.Id
        public string WhatsAppNumber { get; set; } = string.Empty;
        public string RelationType { get; set; } = "الأب";
        public string? JobTitle { get; set; }
        public string? Address { get; set; }

        // Relations
        public User User { get; set; } = null!;
        public List<StudentProfile> Children { get; set; } = new();

        public ParentProfile() { } // ef core ctor

        public ParentProfile(int userId,
                             Guid createdByUserId,
                             string whatsAppNumber,
                             string relationType = "الأب",
                             string? jobTitle = null,
                             string? address = null)
        {
            _ParentProfileId = Guid.NewGuid();
            UserId = userId;
            WhatsAppNumber = whatsAppNumber;
            RelationType = relationType;
            JobTitle = jobTitle;
            Address = address;
            CreatedBy = createdByUserId;
            CreatedOn = DateTimeOffset.Now;
            ModifiedBy = Guid.Empty;
            ModifiedOn = DateTimeOffset.Now;
        }
       
    }
}
