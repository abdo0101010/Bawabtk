using System.ComponentModel.DataAnnotations;

namespace Bawabtk.REPR.EndPoints.User.Login
{
    public class UserRequest
    {

        [EmailAddress]
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;

    }
}
