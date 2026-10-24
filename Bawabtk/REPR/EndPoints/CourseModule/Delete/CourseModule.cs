using Bawabtk.REPR.EndPoints.CourseModule.Create;
using CleanArchitecture.Service.Factories;
using CleanArchitecture.Shared.Contract.CourseModuleImplemention;
using huzcodes.Endpoints.Abstractions;
using huzcodes.Extensions.Exceptions;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Bawabtk.REPR.EndPoints.CourseModule.Delete
{
    public class CourseModule(BawabtkFactory<CourseModuleOPerationContract, CourseModuleContract> bawabtkFactory) : EndpointsAsync.WithRequest<CourseModuleRequest>.WithActionResult
    {
        
        private readonly BawabtkFactory<CourseModuleOPerationContract, CourseModuleContract> _bawabtkFactory = bawabtkFactory;
        [HttpDelete(Route.CourseModuleRoutes.Delete)]
        public override async Task<ActionResult>HandleAsync(CourseModuleRequest request, CancellationToken cancellationToken = default)
        {
            if (request.CourseModuleId == Guid.Empty)
                throw new ResultException("cannot send nullable request", ((int)HttpStatusCode.BadRequest));
            var service = _bawabtkFactory.Build(nameof(CleanArchitecture.Core.Entites.CourseModules.CourseModule));
            await service.Delete(request.CourseModuleId);
            return Ok();
        }
    }
}
