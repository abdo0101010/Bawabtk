using Bawabtk.REPR.EndPoints.CourseModule.Create;
using CleanArchitecture.Service.Factories;
using CleanArchitecture.Shared.Contract.CourseModuleImplemention;
using huzcodes.Endpoints.Abstractions;
using huzcodes.Extensions.Exceptions;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Bawabtk.REPR.EndPoints.CourseModule.Read
{
    public class CourseModule(BawabtkFactory<CourseModuleOPerationContract, CourseModuleContract> bawabtkFactory) : EndpointsAsync.WithoutRequest.WithActionResult<List<CourseModuleReaponse>>
    {
        private readonly BawabtkFactory<CourseModuleOPerationContract, CourseModuleContract> _bawabtkFactory = bawabtkFactory;
        [HttpGet(Route.CourseModuleRoutes.BaseUrl)]
        public override async Task<ActionResult<List<CourseModuleReaponse>>> HandleAsync(CancellationToken cancellationToken = default)
        {
          
            var service = _bawabtkFactory.Build(nameof(CleanArchitecture.Core.Entites.CourseModules.CourseModule));

            var oData = await service.Read();
            var oResult = oData.Select(c => new CourseModuleResponse()
            {
                CourseId=c.CourseId,
                ModuleId=c._ModuleId,
                SortOrder=c.SortOrder,
                Title=c.Title
            });
            return Ok(oResult);
        }
    }
}
