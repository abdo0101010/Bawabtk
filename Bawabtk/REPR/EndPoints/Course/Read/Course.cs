using Bawabtk.REPR.EndPoints.Course.Create;
using CleanArchitecture.Service.Factories;
using CleanArchitecture.Shared.Contract.CourseImplemention;
using huzcodes.Endpoints.Abstractions;
using Microsoft.AspNetCore.Mvc;

namespace Bawabtk.REPR.EndPoints.Course.Read
{
    public class Course(BawabtkFactory<CourseOperationContract, CourseContract> bawabtkFactory) : EndpointsAsync.WithoutRequest.WithActionResult<List<CourseResponse>>
    {
    
        private readonly BawabtkFactory<CourseOperationContract, CourseContract> _bawabtkFactory = bawabtkFactory;
        [HttpGet(Route.CourseRoutes.BaseUrl)]
        public override async Task<ActionResult<List<CourseResponse>>> HandleAsync(CancellationToken cancellationToken = default)
        {
            var service = _bawabtkFactory.Build(nameof(CleanArchitecture.Core.Entites.Courses.Course));
            var oData = await service.Read();
            var oResult = oData.Select(c => new CourseResponse()
            {
                _CourseId = c._CourseId,
                CourseType = c.CourseType,
                Description = c.Description,
                GradeLevel = c.GradeLevel,
                IsPublished = c.IsPublished,
                Price = c.Price,
                ThumbnailUrl = c.ThumbnailUrl,
                Title = c.Title,
            }).ToList();
            return Ok(oResult);
        }
    }
}
