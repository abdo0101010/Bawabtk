using CleanArchitecture.Service.Factories;
using CleanArchitecture.Shared.Contract.AttendanceImplemention;
using huzcodes.Endpoints.Abstractions;
using huzcodes.Endpoints.Attributes;
using Microsoft.AspNetCore.Mvc;

namespace Bawabtk.REPR.EndPoints.User.CloseSession
{
    public class User(AttendanceFactory<AttendanceOperationContract,AttendanceContract> factory)
        : EndpointsAsync.WithRequest<UserRequest>.WithActionResult<UserResponse>
    {
        private readonly AttendanceFactory<AttendanceOperationContract, AttendanceContract> _factory = factory;
        [HttpPut(Route.AttendanceRoutes.CloseSession)]
        public override async Task<ActionResult<UserResponse>> HandleAsync(
                [FromMultiSource] UserRequest request,
                CancellationToken cancellationToken = default)
        {
            var attendanceService = _factory.Build();

            var absentCount = await attendanceService.CloseSessionAndMarkAbsentAsync(
            request.SessionId,
            request.ClosedByUserId);

            return Ok(new UserResponse(absentCount));
        }
    }
}
