using CleanArchitecture.Service.Factories;
using CleanArchitecture.Shared.Contract.CourseLessonImplemention;
using huzcodes.Endpoints.Abstractions;
using huzcodes.Extensions.Exceptions;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Bawabtk.REPR.EndPoints.CourseLessson.Update.ModuleId
{
    public class CourseLessson(BawabtkFactory<CourseLessonOperationContract, CourseLessonContract> bawabtkFactory) : EndpointsAsync.WithRequest<CourseLesssonRequest>.WithActionResult<CourseLesssonResponse>
    {
        private readonly BawabtkFactory<CourseLessonOperationContract, CourseLessonContract> _bawabtkFactory = bawabtkFactory;
        [HttpPut(Route.CourseLessoneRoutes.UpdateCourseModuleId)]
        public override async Task<ActionResult<CourseLesssonResponse>> HandleAsync(CourseLesssonRequest request, CancellationToken cancellationToken = default)
        {
            if (request.CourseModuleId == Guid.Empty || request.CourseLessonId == Guid.Empty)
                throw new ResultException("cannot send nullable request", ((int)HttpStatusCode.BadRequest));
            var service = _bawabtkFactory.Build(nameof(CleanArchitecture.Core.Entites.CourseLessons.CourseLesson));
            await service.UpdateParentById(request.CourseModuleId, request.CourseLessonId);
            return Ok();
        }
    }
}
