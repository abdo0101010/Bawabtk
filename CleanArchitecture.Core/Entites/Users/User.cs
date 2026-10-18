using CleanArchitecture.Core.Entites.ParentsProfile;
using CleanArchitecture.Core.Entites.Student;

namespace CleanArchitecture.Core.Entites.Users
{
    public class User:BaseEntity
    {
        public Guid _UserId { get; set; }
        public string FullName { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        public string? Email { get; set; }
        public string PasswordHash { get; set; } = string.Empty;
        public string? ProfilePictureUrl { get; set; }
        public Enums.UserRole Role { get; set; }
        public bool IsActive { get; set; } = true;

        // Relations
        public StudentProfile? StudentProfile { get; set; }
        public ParentProfile? ParentProfile { get; set; }
        public List<Course> CreatedCourses { get; set; } = new();

        public User() { } // ef core ctor

        public User(string fullName,
                    string phoneNumber,
                    string passwordHash,
                   Enums.UserRole role,
                    string? profilePictureUrl = null,
                    string? email = null,
                    Guid? createdBy = null)
        {
            _UserId = Guid.NewGuid();
            FullName = fullName;
            PhoneNumber = phoneNumber;
            PasswordHash = passwordHash;
            Role = role;
            ProfilePictureUrl = profilePictureUrl;
            Email = email;
            IsActive = true;
            CreatedBy = createdBy ?? _UserId;
            CreatedOn = DateTimeOffset.Now;
            ModifiedBy = Guid.Empty;
            ModifiedOn = DateTimeOffset.Now;
        }

        public void UpdateProfile(string fullName, string? email, string? profilePictureUrl, Guid modifiedBy)
        {
            ModifiedOn = DateTimeOffset.Now;
            ModifiedBy = modifiedBy;
            FullName = string.IsNullOrEmpty(fullName) ? this.FullName : fullName;
            Email = string.IsNullOrEmpty(email) ? this.Email : email;
            ProfilePictureUrl = string.IsNullOrEmpty(profilePictureUrl) ? this.ProfilePictureUrl : profilePictureUrl;
        }
    }
}
