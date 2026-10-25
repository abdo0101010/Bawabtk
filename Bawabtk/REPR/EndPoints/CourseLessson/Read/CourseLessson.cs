using CleanArchitecture.Service.Factories;
using CleanArchitecture.Shared.Contract.CourseLessonImplemention;
using huzcodes.Endpoints.Abstractions;
using Microsoft.AspNetCore.Mvc;

namespace Bawabtk.REPR.EndPoints.CourseLessson.Read
{
    public class CourseLessson(BawabtkFactory<CourseLessonOperationContract, CourseLessonContract> bawabtkFactory) : EndpointsAsync.WithoutRequest.WithActionResult<List<CourseLesssonResponse>>
    {
        private readonly BawabtkFactory<CourseLessonOperationContract, CourseLessonContract> _bawabtkFactory = bawabtkFactory;
        [HttpGet(Route.CourseLessoneRoutes.BaseUrl)]
        public override async Task<ActionResult<List<CourseLesssonResponse>>> HandleAsync(CancellationToken cancellationToken = default)
        {
            var service = _bawabtkFactory.Build(nameof(CleanArchitecture.Core.Entites.CourseLessons.CourseLesson));

            var oData = await service.Read();
            var oResult = oData.Select(c => new CourseLesssonResponse()
            {
                _LessonId=c._LessonId,
                DurationInMinutes = c.DurationInMinutes,
                IsFreePreview = c.IsFreePreview,
                ModuleId = c.ModuleId,
                PdfUrl = c.PdfUrl,
                SortOrder = c.SortOrder,
                Title = c.Title,
                VideoUrl = c.VideoUrl
            }).ToList();
            return Ok(oResult);
        }
    }
}
