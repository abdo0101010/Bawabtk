using CleanArchitecture.Service.Factories;
using CleanArchitecture.Shared.Contract.CourseLessonImplemention;
using huzcodes.Endpoints.Abstractions;
using huzcodes.Extensions.Exceptions;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Bawabtk.REPR.EndPoints.CourseLessson.Delete
{
    
    public class CourseLessson(BawabtkFactory<CourseLessonOperationContract, CourseLessonContract> bawabtkFactory) : EndpointsAsync.WithRequest<CourseLesssonRequest>.WithActionResult
    {
        private readonly BawabtkFactory<CourseLessonOperationContract, CourseLessonContract> _bawabtkFactory = bawabtkFactory;
        [HttpDelete(Route.CourseLessoneRoutes.Delete)]
        public override async Task<ActionResult> HandleAsync(CourseLesssonRequest request, CancellationToken cancellationToken = default)
        {
            if (request.CourseLessonId == Guid.Empty)
                throw new ResultException("cannot send nullable request", ((int)HttpStatusCode.BadRequest));
            var service = _bawabtkFactory.Build(nameof(CleanArchitecture.Core.Entites.CourseLessons.CourseLesson));
            await service.Delete(request.CourseLessonId);
            return Ok();
        }
    }
}
