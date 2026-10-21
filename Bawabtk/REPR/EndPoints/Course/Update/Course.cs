using Azure.Core;
using CleanArchitecture.Service.Factories;
using CleanArchitecture.Shared.Contract.CourseImplemention;
using huzcodes.Endpoints.Abstractions;
using huzcodes.Extensions.Exceptions;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Bawabtk.REPR.EndPoints.Course.Update
{
    public class Course(BawabtkFactory<CourseOperationContract, CourseContract> bawabtkFactory) : EndpointsAsync.WithRequest<CourseRequest>.WithActionResult<List<CourseResponse>>
    {
        private readonly BawabtkFactory<CourseOperationContract, CourseContract> _bawabtkFactory = bawabtkFactory;
        [HttpPut(Route.CourseRoutes.Update)]
        public override async Task<ActionResult<List<CourseResponse>>> HandleAsync(CourseRequest request, CancellationToken cancellationToken = default)
        {
            if (request.CourseId == Guid.Empty)
                throw new ResultException("cannot send nullable id", ((int)HttpStatusCode.BadRequest));
            var service = _bawabtkFactory.Build(nameof(CleanArchitecture.Core.Entites.Courses.Course));
            var oData = new CourseOperationContract()
            {
                CourseType = request.CourseType,
                Description = request.Description,
                GradeLevel = request.GradeLevel,
                IsPublished = request.IsPublished,
                Price = request.Price,
                ThumbnailUrl = request.ThumbnailUrl,
                Title = request.Title,
            };
            var oResult = await service.Update(oData, request.CourseId);
            var Response = new CourseResponse()
            {
                CourseId = oResult._CourseId,
                CourseType = oResult.CourseType,
                Description = oResult.Description,
                GradeLevel = oResult.GradeLevel,
                IsPublished = oResult.IsPublished,
                Price = oResult.Price,
                ThumbnailUrl = oResult.ThumbnailUrl,
                Title = oResult.Title
            };
            return Ok(Response);
        }
    }
}
