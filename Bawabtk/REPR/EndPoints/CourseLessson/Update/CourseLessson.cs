using CleanArchitecture.Service.Factories;
using CleanArchitecture.Shared.Contract.CourseLessonImplemention;
using huzcodes.Endpoints.Abstractions;
using huzcodes.Extensions.Exceptions;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Bawabtk.REPR.EndPoints.CourseLessson.Update
{
    public class CourseLessson(BawabtkFactory<CourseLessonOperationContract, CourseLessonContract> bawabtkFactory) : EndpointsAsync.WithRequest<CourseLesssonRequest>.WithActionResult<CourseLesssonResponse>
    {
        private readonly BawabtkFactory<CourseLessonOperationContract, CourseLessonContract> _bawabtkFactory = bawabtkFactory;
        [HttpPut(Route.CourseLessoneRoutes.Update)]
        public override async Task<ActionResult<CourseLesssonResponse>> HandleAsync(CourseLesssonRequest request, CancellationToken cancellationToken = default)
        {
            if (request.CourseLessonId == Guid.Empty)
                throw new ResultException("cannot send nullable request", ((int)HttpStatusCode.BadRequest));
            var service = _bawabtkFactory.Build(nameof(CleanArchitecture.Core.Entites.CourseLessons.CourseLesson));
            var oData = new CourseLessonOperationContract()
            {
                 
                DurationInMinutes=request.DurationInMinutes,
                IsFreePreview= request.IsFreePreview,
                ModuleId= request.ModuleId,
                PdfUrl= request.PdfUrl,
                SortOrder= request.SortOrder,
                Title=request.Title,
                VideoUrl= request.VideoUrl

            };
            var UpdatedLesson = await service.Update(oData, request.CourseLessonId);
            var oResult = new CourseLesssonResponse()
            {_LessonId = UpdatedLesson._LessonId,
                DurationInMinutes = UpdatedLesson.DurationInMinutes,
                IsFreePreview = UpdatedLesson.IsFreePreview,
                ModuleId = UpdatedLesson.ModuleId,
                PdfUrl = UpdatedLesson.PdfUrl,
                SortOrder = UpdatedLesson.SortOrder,
                Title = UpdatedLesson.Title,
                VideoUrl = UpdatedLesson.VideoUrl
            };
            return oResult as dynamic;
        }
    }
}
