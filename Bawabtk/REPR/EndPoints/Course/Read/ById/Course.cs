using CleanArchitecture.Service.Factories;
using CleanArchitecture.Shared.Contract.CourseImplemention;
using huzcodes.Endpoints.Abstractions;
using huzcodes.Extensions.Exceptions;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Bawabtk.REPR.EndPoints.Course.Read.ById
{
    public class Course(BawabtkFactory<CourseOperationContract, CourseContract> bawabtkFactory) : EndpointsAsync.WithRequest<CourseRequest>.WithActionResult<CourseResponse>
    {
        private readonly BawabtkFactory<CourseOperationContract, CourseContract> _bawabtkFactory = bawabtkFactory;
        [HttpGet(Route.CourseRoutes.ReadById)]
        public override async Task<ActionResult<CourseResponse>> HandleAsync(CourseRequest request, CancellationToken cancellationToken = default)
        {
            if (request.CourseId == Guid.Empty)
                throw new ResultException("cannot send nullable id", ((int)HttpStatusCode.BadRequest));
            var service = _bawabtkFactory.Build(nameof(CleanArchitecture.Core.Entites.Courses.Course));
            var oData = await service.ReadById(request.CourseId);
            var oResult = new CourseResponse()
            {
                _CourseId = oData._CourseId,
                CourseType = oData.CourseType,
                Description = oData.Description,
                GradeLevel = oData.GradeLevel,
                IsPublished = oData.IsPublished,
                Price = oData.Price,
                ThumbnailUrl = oData.ThumbnailUrl,
                Title = oData.Title,
            };
            return Ok(oResult);
        }
    }
}
