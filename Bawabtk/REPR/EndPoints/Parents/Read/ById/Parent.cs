using Azure;
using CleanArchitecture.Core.Entites.ParentsProfile;
using CleanArchitecture.Service.Factories;
using CleanArchitecture.Shared.Contract.ParentImplemention;
using huzcodes.Endpoints.Abstractions;
using huzcodes.Extensions.Exceptions;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Bawabtk.REPR.EndPoints.Parents.Read.ById
{
    public class Parent(BawabtkFactory<ParentOperationContract, ParentContract> bawabtkFactory) : EndpointsAsync.WithRequest<ParentRequest>.WithActionResult<ParentResponse>
    {
        private readonly BawabtkFactory<ParentOperationContract, ParentContract> _bawabtkFactory = bawabtkFactory;
        [HttpGet(Route.ParentRoutes.ReadById)]
        public override async Task<ActionResult<ParentResponse>> HandleAsync(ParentRequest request, CancellationToken cancellationToken = default)
        {
            if (request.ParentId == Guid.Empty)
                throw new ResultException("cannot send nullable request", ((int)HttpStatusCode.BadRequest));
            var service = _bawabtkFactory.Build(nameof(ParentProfile));
            var oData = await service.ReadById(request.ParentId);
            var oResult = new ParentResponse()
            {
                Address = oData.Address,
                FullName = oData.FullName,
                JobTitle = oData.JobTitle,
                WhatsAppNumber = oData.WhatsAppNumber,
                ParentProfileGuid = oData.ParentProfileGuid
            };
            return Ok(oResult);
        }
    }
}
