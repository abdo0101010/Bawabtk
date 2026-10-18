using CleanArchitecture.Core.Entites.Student;
using CleanArchitecture.Service.Factories;
using CleanArchitecture.Shared.Contract.Student;
using huzcodes.Endpoints.Abstractions;
using huzcodes.Extensions.Exceptions;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Bawabtk.REPR.EndPoints.Students.Update
{
    public class Student(BawabtkFactory<StudentOperationContract, StudentContract> bawabtkFactory) : EndpointsAsync.WithRequest<StudentRequest>.WithActionResult<StudentResponse>
    {
      private readonly  BawabtkFactory<StudentOperationContract, StudentContract> _bawabtkFactory = bawabtkFactory;
        [HttpPut(Route.StudentRoutes.Update)]
        public override async Task<ActionResult<StudentResponse>> HandleAsync(StudentRequest request, CancellationToken cancellationToken = default)
        {
            if (request == null)
                throw new ResultException("cannot send null request ", ((int)HttpStatusCode.BadRequest));
            var service = _bawabtkFactory.Build(nameof(StudentProfile));
            var oData = new StudentOperationContract()
            {
                GradeLevel = request.GradeLevel,
                ParentWhatsAppNumber = request.ParentWhatsAppNumber,
                Password = request.Password,
                SchoolName = request.SchoolName,
                StudentFullName = request.StudentFullName,
                StudentPhoneNumber = request.StudentPhoneNumber,
                StudentEmail = request.StudentEmail
            };
            var oResult = await service.Update(oData, request.StudentId);
            var response = new StudentResponse()
            {

                GradeLevel = oResult.GradeLevel,
                StudentName = oResult.StudentName,
                ParentProfileGuid = oResult.ParentProfileGuid,
                StudentProfileGuid = oResult.StudentProfileGuid,
                StudentUserGuid = oResult.StudentUserGuid,
                UnifiedBarcode = oResult.UnifiedBarcode
            };
            return response as dynamic;
        }
    }
}
