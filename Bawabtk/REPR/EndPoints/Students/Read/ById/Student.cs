using CleanArchitecture.Core.Entites.Student;
using CleanArchitecture.Service.Factories;
using CleanArchitecture.Shared.Contract.Student;
using huzcodes.Endpoints.Abstractions;
using huzcodes.Extensions.Exceptions;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Bawabtk.REPR.EndPoints.Students.Read.ById
{
    public class Student(BawabtkFactory<StudentOperationContract, StudentContract> bawabtkFactory) : EndpointsAsync.WithRequest<StudentRequest>.WithActionResult<StudentResponse>
    {
        private readonly BawabtkFactory<StudentOperationContract, StudentContract> _bawabtkFactory = bawabtkFactory;
        [HttpGet(Route.StudentRoutes.ReadById)]
        public override async Task<ActionResult<StudentResponse>> HandleAsync(StudentRequest request, CancellationToken cancellationToken = default)
        {
            if (request.StudentId == Guid.Empty)
                throw new ResultException("cannot send null id", ((int)HttpStatusCode.BadRequest));
            var service = _bawabtkFactory.Build(nameof(StudentProfile));
            var oData = await service.ReadById(request.StudentId);
            var oResult = new StudentResponse()
            {
                GradeLevel = oData.GradeLevel,
                ParentProfileGuid = oData.ParentProfileGuid,
                StudentName = oData.StudentName,
                StudentProfileGuid = oData.StudentProfileGuid,
                StudentUserGuid = oData.StudentUserGuid,
                UnifiedBarcode = oData.UnifiedBarcode
            };
            return oResult as dynamic;
        }
    }
}
