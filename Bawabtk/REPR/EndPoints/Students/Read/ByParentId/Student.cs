using CleanArchitecture.Core.Entites.Student;
using CleanArchitecture.Service.Factories;
using CleanArchitecture.Shared.Contract.Student;
using huzcodes.Endpoints.Abstractions;
using huzcodes.Extensions.Exceptions;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Bawabtk.REPR.EndPoints.Students.Read.ByParentId
{
    public class Student(BawabtkFactory<StudentOperationContract, StudentContract> bawabtkFactory):EndpointsAsync.WithRequest<StudentRequest>.WithActionResult<StudentResponse>
    {
       private readonly BawabtkFactory<StudentOperationContract, StudentContract> _bawabtkFactory = bawabtkFactory;
        [HttpGet(Route.StudentRoutes.ReadStudentByParentId)]
        public override async Task<ActionResult<StudentResponse>> HandleAsync(StudentRequest request, CancellationToken cancellationToken = default)
        {
            if (request.ParentId == Guid.Empty || request.StudentId == Guid.Empty)
                throw new ResultException("must send id for parent and student", ((int)HttpStatusCode.BadRequest));
            var service = _bawabtkFactory.Build(nameof(StudentProfile));
            var oData = await service.ReadSingleByParentId(request.StudentId, request.ParentId);
            var oResult = new StudentResponse()
            {
                GradeLevel = oData.GradeLevel,
                ParentProfileGuid = oData.ParentProfileGuid,
                StudentName = oData.StudentName,
                StudentProfileGuid = oData.StudentProfileGuid,
                StudentUserGuid = oData.StudentUserGuid,
                UnifiedBarcode = oData.UnifiedBarcode
            };
            return Ok(oResult as dynamic);
        }
    }
}
