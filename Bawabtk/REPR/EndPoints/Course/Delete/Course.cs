using CleanArchitecture.Service.Factories;
using CleanArchitecture.Shared.Contract.CourseImplemention;
using huzcodes.Endpoints.Abstractions;
using huzcodes.Extensions.Exceptions;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Bawabtk.REPR.EndPoints.Course.Delete
{
    public class Course(BawabtkFactory<CourseOperationContract, CourseContract> bawabtkFactory) : EndpointsAsync.WithRequest<CourseRequest>.WithActionResult
    {
        private readonly BawabtkFactory<CourseOperationContract, CourseContract> _bawabtkFactory = bawabtkFactory;
        [HttpDelete(Route.CourseRoutes.Delete)]
        public override async Task<ActionResult> HandleAsync(CourseRequest request, CancellationToken cancellationToken = default)
        {
            if (request.CourseId == Guid.Empty)
                throw new ResultException("cannot send nullable id", ((int)HttpStatusCode.BadRequest));
            var service = _bawabtkFactory.Build(nameof(CleanArchitecture.Core.Entites.Courses.Course));
            await service.Delete(request.CourseId);
            return Ok();

        }
    }
}

