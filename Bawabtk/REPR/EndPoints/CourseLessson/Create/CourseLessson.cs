using CleanArchitecture.Service.Factories;
using CleanArchitecture.Shared.Contract.CourseLessonImplemention;
using huzcodes.Endpoints.Abstractions;
using huzcodes.Extensions.Exceptions;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Bawabtk.REPR.EndPoints.CourseLessson.Create
{
    public class CourseLessson(BawabtkFactory<CourseLessonOperationContract,CourseLessonContract> bawabtkFactory):EndpointsAsync.WithRequest<CourseLesssonRequest>.WithActionResult<CourseLesssonResponse>
    {
        private readonly BawabtkFactory<CourseLessonOperationContract, CourseLessonContract> _bawabtkFactory = bawabtkFactory;
        [HttpPost(Route.CourseLessoneRoutes.Create)]
        public override async Task<ActionResult<CourseLesssonResponse>> HandleAsync(CourseLesssonRequest request, CancellationToken cancellationToken = default)
        {
            if (request == null)
                throw new ResultException("cannot send nullable request", ((int)HttpStatusCode.BadRequest));
            var service = _bawabtkFactory.Build(nameof(CleanArchitecture.Core.Entites.CourseLessons.CourseLesson));
            var oData = new CourseLessonOperationContract()
            {
                DurationInMinutes=request.DurationInMinutes,
                IsFreePreview=request.IsFreePreview,
                ModuleId=request.ModuleId,
                PdfUrl=request.PdfUrl,
                SortOrder=request.SortOrder,
                Title=request.Title,
                VideoUrl=request.VideoUrl
            };
            var CourseLesson = await service.Create(oData);
            var oResullt = new CourseLesssonResponse()
            {
                VideoUrl=CourseLesson.VideoUrl,
                DurationInMinutes= CourseLesson.DurationInMinutes,
                IsFreePreview= CourseLesson.IsFreePreview,
                ModuleId= CourseLesson.ModuleId,
                PdfUrl= CourseLesson.PdfUrl,
                SortOrder= CourseLesson.SortOrder,
                Title= CourseLesson.Title,
                _LessonId= CourseLesson._LessonId
            };
            return Ok(oResullt);
        }
    }
}
