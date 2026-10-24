using Bawabtk.REPR.EndPoints.CourseModule.Create;
using CleanArchitecture.Service.Factories;
using CleanArchitecture.Shared.Contract.CourseModuleImplemention;
using huzcodes.Endpoints.Abstractions;
using huzcodes.Extensions.Exceptions;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Bawabtk.REPR.EndPoints.CourseModule.Update
{
    public class CourseModule(BawabtkFactory<CourseModuleOPerationContract, CourseModuleContract> bawabtkFactory) : EndpointsAsync.WithRequest<CourseModuleRequest>.WithActionResult<CourseModuleReaponse>
    {
        private readonly BawabtkFactory<CourseModuleOPerationContract, CourseModuleContract> _bawabtkFactory = bawabtkFactory;
        [HttpPut(Route.CourseModuleRoutes.Update)]
        public override async Task<ActionResult<CourseModuleReaponse>> HandleAsync(CourseModuleRequest request, CancellationToken cancellationToken = default)
        {
            if (request.CourseModuleId == Guid.Empty)
                throw new ResultException("cannot send nullable request", ((int)HttpStatusCode.BadRequest));
            var service = _bawabtkFactory.Build(nameof(CleanArchitecture.Core.Entites.CourseModules.CourseModule));
            var Course = new CourseModuleOPerationContract()
            {
                CourseId=request.CourseId,
                SortOrder=request.SortOrder,
                Title=request.Title
            };
            var oData = await service.Update(Course, request.CourseModuleId);
            var oResult = new CourseModuleResponse()
            {
                _ModuleId=oData._ModuleId,
                CourseId=oData.CourseId,
                Title=oData.Title,
                SortOrder=oData.SortOrder
            };
            return Ok(oResult);
        }
    }
}
