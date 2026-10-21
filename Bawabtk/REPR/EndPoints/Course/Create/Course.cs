using CleanArchitecture.Service.Factories;
using CleanArchitecture.Shared.Contract.CourseImplemention;
using CleanArchitecture.Shared.Contract.ParentImplemention;
using huzcodes.Endpoints.Abstractions;
using huzcodes.Extensions.Exceptions;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Bawabtk.REPR.EndPoints.Course.Create
{
    public class Course(BawabtkFactory<CourseOperationContract, CourseContract> bawabtkFactory) :EndpointsAsync.WithRequest<CourseRequest>.WithActionResult<CourseResponse>
    {
        private readonly BawabtkFactory<CourseOperationContract, CourseContract> _bawabtkFactory = bawabtkFactory;
        [HttpPost(Route.CourseRoutes.Create)]
        public override async Task<ActionResult<CourseResponse>> HandleAsync(CourseRequest request, CancellationToken cancellationToken = default)
        {
            if (request == null)
                throw new ResultException("cannot send nullable request", ((int)HttpStatusCode.BadRequest));
            var service = _bawabtkFactory.Build(nameof(CleanArchitecture.Core.Entites.Courses.Course));
            var oData = new CourseOperationContract()
            {
                CourseType=request.CourseType,
                Description=request.Description,
                GradeLevel=request.GradeLevel,
                IsPublished=request.IsPublished,
                Price=request.Price,
                ThumbnailUrl=request.ThumbnailUrl,
                Title=request.Title,
                UserId=request.UserId
            };
            
            var Course = await service.Create(oData);
            var oResult = new CourseResponse()
            {
                _CourseId = Course._CourseId,
                CourseType = Course.CourseType,
                Description = Course.Description,
                GradeLevel = Course.GradeLevel,
                IsPublished = Course.IsPublished,
                Price = Course.Price,
                ThumbnailUrl = Course.ThumbnailUrl,
                Title = Course.Title
            };
            return Ok(oResult);

        }
    }
}
