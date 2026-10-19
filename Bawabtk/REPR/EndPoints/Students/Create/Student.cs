using CleanArchitecture.Core.Entites.Student;
using CleanArchitecture.Service.Factories;
using CleanArchitecture.Shared.Contract.Student;
using huzcodes.Endpoints.Abstractions;
using huzcodes.Extensions.Exceptions;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Bawabtk.REPR.EndPoints.Students.Create
{
    public class Student(BawabtkFactory<StudentOperationContract,StudentContract> bawabtkFactory) : EndpointsAsync.WithRequest<StudentRequest>.WithActionResult<StudentResponse>
    {
        private readonly BawabtkFactory<StudentOperationContract, StudentContract> _bawabtkFactory = bawabtkFactory;
        [HttpPost(Route.StudentRoutes.Create)]
        public override async Task<ActionResult<StudentResponse>> HandleAsync(StudentRequest request, CancellationToken cancellationToken = default)
        {
            if (request == null)
                throw new ResultException("cannot send nullable request", ((int)HttpStatusCode.BadRequest));
            var service = _bawabtkFactory.Build(nameof(StudentProfile));
            var oData = new StudentOperationContract()
            {
                Address = request.Address,
                GradeLevel=request.GradeLevel,
                ParentFullName=request.ParentFullName,
                ParentJobTitle=request.ParentJobTitle,
                ParentWhatsAppNumber=request.ParentWhatsAppNumber,
                Password=request.Password,
                ProfilePictureUrl=request.ProfilePictureUrl,
                RelationType=request.RelationType,
                SchoolName=request.SchoolName,
                StudentEmail=request.StudentEmail,
                StudentFullName=request.StudentFullName,
                StudentPhoneNumber=request.StudentPhoneNumber
            };
            var student = await service.Create(oData);
            var oResult = new StudentResponse()
            {
                GradeLevel = student.GradeLevel,
                Message = student.Message,
                ParentProfileGuid = student.ParentProfileGuid,
                StudentName = student.StudentName,
                StudentProfileGuid = student.StudentProfileGuid,
                StudentUserGuid = student.StudentUserGuid,
                UnifiedBarcode = student.UnifiedBarcode
            };
            return oResult as dynamic;

        }
    }
}
