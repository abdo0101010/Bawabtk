using CleanArchitecture.Service.Factories;
using CleanArchitecture.Shared.Contract.UserImplemention;
using huzcodes.Endpoints.Abstractions;
using Microsoft.AspNetCore.Mvc;

namespace Bawabtk.REPR.EndPoints.User.Login
{
    public class User(BawabtkAuthFactory<UserOperationContract, UserContract> bawabtkAuthFactory) : EndpointsAsync.WithRequest<UserRequest>.WithActionResult<UserResponse>
    {
        private readonly BawabtkAuthFactory<UserOperationContract, UserContract> _bawabtkAuthFactory = bawabtkAuthFactory;
        [HttpPost(Route.userRoutes.Login)]
        public override async Task<ActionResult<UserResponse>> HandleAsync(UserRequest request, CancellationToken cancellationToken = default)
        {
            var service = _bawabtkAuthFactory.BuildAuthService();
            var oContract = new UserOperationContract()
            {
                Email = request.Email,
                Password = request.Password
            };
            var oData = await service.Auth(oContract);
            var oResult = new UserResponse()
            {
                Name = oData.Name,
                Role = oData.Role,
                UserId = oData.UserId,
                Token = oData.Token
            };
            return Ok(oResult); 
        }
    }
}
