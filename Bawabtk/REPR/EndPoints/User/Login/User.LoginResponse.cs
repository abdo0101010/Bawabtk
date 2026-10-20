using CleanArchitecture.Core.Entites;

namespace Bawabtk.REPR.EndPoints.User.Login
{
    public class UserResponse
    {
        public string Name { get; set; } = string.Empty;
        public Guid UserId { get; set; }
        public Enums.UserRole Role { get; set; }
        public string Token { get; set; } = string.Empty;
    }
}
