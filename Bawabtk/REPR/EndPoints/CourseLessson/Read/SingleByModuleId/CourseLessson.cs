using CleanArchitecture.Service.Factories;
using CleanArchitecture.Shared.Contract.CourseLessonImplemention;
using huzcodes.Endpoints.Abstractions;
using huzcodes.Extensions.Exceptions;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Bawabtk.REPR.EndPoints.CourseLessson.Read.SingleByModuleId
{
    public class CourseLessson(BawabtkFactory<CourseLessonOperationContract, CourseLessonContract> bawabtkFactory) : EndpointsAsync.WithRequest<CourseLesssonRequest>.WithActionResult<CourseLesssonResponse>
    {
        private readonly BawabtkFactory<CourseLessonOperationContract, CourseLessonContract> _bawabtkFactory = bawabtkFactory;
        [HttpGet(Route.CourseLessoneRoutes.ReadSingleCourseLessonByCoursemoduleId)]
        public override async Task<ActionResult<CourseLesssonResponse>> HandleAsync(CourseLesssonRequest request, CancellationToken cancellationToken = default)
        {
            if (request.CourseModuleId == Guid.Empty || request.CourseLessonId == Guid.Empty)
                throw new ResultException("cannot send nullable request", ((int)HttpStatusCode.BadRequest));
            var service = _bawabtkFactory.Build(nameof(CleanArchitecture.Core.Entites.CourseLessons.CourseLesson));
            var oData = await service.ReadSingleByParentId(request.CourseLessonId, request.CourseModuleId);
            var oResult = new CourseLesssonResponse()
            {
                _LessonId=oData._LessonId,
                DurationInMinutes = oData.DurationInMinutes,
                IsFreePreview = oData.IsFreePreview,
                ModuleId = oData.ModuleId,
                PdfUrl = oData.PdfUrl,
                SortOrder = oData.SortOrder,
                Title = oData.Title,
                VideoUrl = oData.VideoUrl
            };
            return oResult as dynamic;

        }
    }
}
