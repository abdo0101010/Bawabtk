using CleanArchitecture.Core.Entites;
using CleanArchitecture.Core.Entites.ParentsProfile;
using CleanArchitecture.Core.Entites.Student;
using CleanArchitecture.Core.Entites.Users.Specefication;
using CleanArchitecture.Core.Interfaces;
using CleanArchitecture.Shared.Contract.UserImplemention;
using huzcodes.Extensions.Exceptions;
using huzcodes.Persistence.Interfaces.Repositories;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;
using System.Text;

namespace CleanArchitecture.Service.Services.User
{
    public class Userservice<TRequest, TResponse>(IRepository<Core.Entites.Users.User> userrepo) : IUserService<TRequest, TResponse>
    {
        private readonly IRepository<Core.Entites.Users.User> _userrepo = userrepo;
        public async ValueTask<TResponse> Auth(TRequest Request)
        {
            var oContract = Request as UserOperationContract;
            if (oContract == null)
                throw new ResultException("cannot send empty user", (int)HttpStatusCode.BadRequest);

            var spec = new ReadByEmailSpecifition(oContract.Email);
            var user = await _userrepo.FirstOrDefaultAsync(spec);

            if (user == null)
                throw new ResultException("password or email is invaild", (int)HttpStatusCode.Unauthorized);

            bool isPasswordValid = BCrypt.Net.BCrypt.Verify(oContract.Password, user.PasswordHash);
            if (!isPasswordValid)
                throw new ResultException("the password is invaild", (int)HttpStatusCode.Unauthorized);

            Guid targetGuid = user._UserId;

            if (user.Role == Enums.UserRole.Parent && user.ParentProfile != null)
            {
                targetGuid = user.ParentProfile._ParentProfileId;
            }
            else if (user.Role == Enums.UserRole.Student && user.StudentProfile != null)
            {
                targetGuid = user.StudentProfile._StudentProfileId;
            }

            string securityKey = "this is my custom Secret key for authentication";
            var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(securityKey));
            var sgnr = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);

            List<Claim> userInfo = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, targetGuid.ToString()),
                new Claim(ClaimTypes.Name, user.FullName),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Role, user.Role.ToString())
            };

            var jwttoken = new JwtSecurityToken(
                claims: userInfo,
                expires: DateTime.UtcNow.AddDays(7),
                signingCredentials: sgnr
            );

            var tokenString = new JwtSecurityTokenHandler().WriteToken(jwttoken);

            var oResult = new UserContract()
            {
                UserId = targetGuid,
                Name = user.FullName,
                Role = user.Role,
                Token = tokenString
            };

            return oResult as dynamic;
        }
    }
}


