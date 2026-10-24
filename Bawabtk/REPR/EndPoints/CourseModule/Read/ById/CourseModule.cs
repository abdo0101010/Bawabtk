using CleanArchitecture.Service.Factories;
using CleanArchitecture.Shared.Contract.CourseModuleImplemention;
using huzcodes.Endpoints.Abstractions;
using huzcodes.Extensions.Exceptions;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Bawabtk.REPR.EndPoints.CourseModule.Read.ById
{
    public class CourseModule(BawabtkFactory<CourseModuleOPerationContract, CourseModuleContract> bawabtkFactory) : EndpointsAsync.WithRequest<CourseModuleRequest>.WithActionResult<CourseModuleResponse>
    {
        private readonly BawabtkFactory<CourseModuleOPerationContract, CourseModuleContract> _bawabtkFactory = bawabtkFactory;
        [HttpGet(Route.CourseModuleRoutes.ReadById)]
        public override async Task<ActionResult<CourseModuleResponse>> HandleAsync(CourseModuleRequest request, CancellationToken cancellationToken = default)
        {
            if (request.CourseModuleId == Guid.Empty)
                throw new ResultException("cannot send nullable request", ((int)HttpStatusCode.BadRequest));
            var service = _bawabtkFactory.Build(nameof(CleanArchitecture.Core.Entites.CourseModules.CourseModule));
            var oData =await service.ReadById(request.CourseModuleId);
            var oResult = new CourseModuleResponse()
            {
                CourseId=oData.CourseId,
                ModuleId=oData._ModuleId,
                SortOrder=oData.SortOrder,
                Title=oData.Title
            };
            return Ok(oResult);
        }
    }
}
