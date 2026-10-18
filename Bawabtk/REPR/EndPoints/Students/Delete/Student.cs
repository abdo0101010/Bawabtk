using CleanArchitecture.Core.Entites.Student;
using CleanArchitecture.Service.Factories;
using CleanArchitecture.Shared.Contract.Student;
using huzcodes.Endpoints.Abstractions;
using huzcodes.Extensions.Exceptions;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Bawabtk.REPR.EndPoints.Students.Delete
{
    public class Student(BawabtkFactory<StudentOperationContract, StudentContract> bawabtkFactory) : EndpointsAsync.WithRequest<StudentRequest>.WithActionResult
    {
        private readonly BawabtkFactory<StudentOperationContract, StudentContract> _bawabtkFactory = bawabtkFactory;
        [HttpDelete(Route.StudentRoutes.Delete)]

        public override async Task<ActionResult> HandleAsync(StudentRequest request, CancellationToken cancellationToken = default)
        {
            if (request.StudentId == Guid.Empty)
                throw new ResultException("cannot send null id", ((int)HttpStatusCode.BadRequest));
            var service = _bawabtkFactory.Build(nameof(StudentProfile));
          await service.Delete(request.StudentId);
            return Ok();
        }
    }
}
