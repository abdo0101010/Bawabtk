using CleanArchitecture.Service.Factories;
using CleanArchitecture.Shared.Contract.AttendanceImplemention;
using huzcodes.Endpoints.Abstractions;
using huzcodes.Extensions.Exceptions;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Bawabtk.REPR.EndPoints.Students.ScanBarCode
{
    public class Student(AttendanceFactory<AttendanceOperationContract,AttendanceContract> factory)
        : EndpointsAsync.WithRequest<StudentRequest>.WithActionResult<StudentResponse>
    {
        private readonly AttendanceFactory<AttendanceOperationContract, AttendanceContract> _factory = factory;
        [HttpPost(Route.AttendanceRoutes.ScanBarcode)]
        public override async Task<ActionResult<StudentResponse>> HandleAsync(
            [FromBody] StudentRequest request,
            CancellationToken cancellationToken = default)
        {
            if (request == null)
                throw new ResultException("Invalid request data", (int)HttpStatusCode.BadRequest);

            var attendanceService = _factory.Build();

            var oData = new AttendanceOperationContract
            {
                SessionId = request.SessionId,
                UnifiedBarcode = request.UnifiedBarcode,
                AssistantUserId = request.AssistantUserId,
                Notes = request.Notes
            };

            var oResult = await attendanceService.ScanBarcodeAsync(oData);
            var response = new StudentResponse(oResult);

            return Ok(response);
        }
    }
}
