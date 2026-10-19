using CleanArchitecture.Core.Entites.Student;
using CleanArchitecture.Service.Factories;
using CleanArchitecture.Shared.Contract.Student;
using huzcodes.Endpoints.Abstractions;
using huzcodes.Extensions.Exceptions;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Bawabtk.REPR.EndPoints.Students.Update.ParentById
{
    public class Student(BawabtkFactory<StudentOperationContract, StudentContract> bawabtkFactory) :EndpointsAsync.WithRequest<StudentRequest>.WithActionResult
    {
      private readonly  BawabtkFactory<StudentOperationContract, StudentContract> _bawabtkFactory = bawabtkFactory;
        [HttpPut(Route.StudentRoutes.UpdateStudentParentId)]
        public override async Task<ActionResult> HandleAsync(StudentRequest request, CancellationToken cancellationToken = default)
        {
            if (request.ParentId == Guid.Empty || request.StudentId == Guid.Empty)
                throw new ResultException("must send id for parent and student", ((int)HttpStatusCode.BadRequest));
            var service = _bawabtkFactory.Build(nameof(StudentProfile));
            await service.UpdateParentById(request.ParentId, request.StudentId);
            return Ok();
        }
    }
}
