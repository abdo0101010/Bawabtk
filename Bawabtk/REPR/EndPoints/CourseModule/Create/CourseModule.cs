using CleanArchitecture.Service.Factories;
using CleanArchitecture.Shared.Contract.CourseModuleImplemention;
using huzcodes.Endpoints.Abstractions;
using huzcodes.Extensions.Exceptions;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Bawabtk.REPR.EndPoints.CourseModule.Create
{
    public class CourseModule(BawabtkFactory<CourseModuleOPerationContract, CourseModuleContract> bawabtkFactory) : EndpointsAsync.WithRequest<CourseModuleRequest>.WithActionResult<CourseModuleReaponse>
    {
        private readonly BawabtkFactory<CourseModuleOPerationContract, CourseModuleContract> _bawabtkFactory = bawabtkFactory;
        [HttpPost(Route.CourseModuleRoutes.Create)]
        public override async Task<ActionResult<CourseModuleReaponse>> HandleAsync(CourseModuleRequest request, CancellationToken cancellationToken = default)
        {
            if (request == null)
                throw new ResultException("cannot send nullable request", ((int)HttpStatusCode.BadRequest));
            var service = _bawabtkFactory.Build(nameof(CleanArchitecture.Core.Entites.CourseModules.CourseModule));
            var CourseModule = new CourseModuleOPerationContract()
            {
                CourseId=request.CourseId,
                SortOrder=request.SortOrder,
                Title=request.Title
            };
            var oData = await service.Create(CourseModule);
            var oResult = new CourseModuleReaponse()
            {
                CourseId = oData.CourseId,
                ModuleId = oData._ModuleId,
                Title = oData.Title,
                SortOrder = oData.SortOrder
            };
            return oResult as dynamic;
        }
    }
}
