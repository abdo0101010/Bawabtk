using CleanArchitecture.Core.Entites.ParentsProfile;
using CleanArchitecture.Service.Factories;
using CleanArchitecture.Shared.Contract.ParentImplemention;
using huzcodes.Endpoints.Abstractions;
using huzcodes.Extensions.Exceptions;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Bawabtk.REPR.EndPoints.Parents.Delete
{
    public class Parent(BawabtkFactory<ParentOperationContract, dynamic> bawabtkFactory) : EndpointsAsync.WithRequest<ParentRequest>.WithActionResult
    {
        private readonly BawabtkFactory<ParentOperationContract, dynamic> _bawabtkFactory = bawabtkFactory;
        [HttpDelete(Route.ParentRoutes.Delete)]
        public override async Task<ActionResult> HandleAsync(ParentRequest request, CancellationToken cancellationToken = default)
        {
            if(request.ParentId==Guid.Empty)
                throw new ResultException("cannot send nullable request", ((int)HttpStatusCode.BadRequest));
            var service = _bawabtkFactory.Build(nameof(ParentProfile));
            await service.Delete(request.ParentId);
            return Ok();


        }
    }
}
