using CleanArchitecture.Core.Entites.ParentsProfile;
using CleanArchitecture.Service.Factories;
using CleanArchitecture.Shared.Contract.ParentImplemention;
using huzcodes.Endpoints.Abstractions;
using huzcodes.Extensions.Exceptions;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Bawabtk.REPR.EndPoints.Parents.Update
{
    public class Parent(BawabtkFactory<ParentOperationContract, ParentContract> bawabtkFactory) : EndpointsAsync.WithRequest<ParentRequest>.WithActionResult<ParentResponse>
    {
        private readonly BawabtkFactory<ParentOperationContract, ParentContract> _bawabtkFactory = bawabtkFactory;
        [HttpPut(Route.ParentRoutes.Update)]
        public override async Task<ActionResult<ParentResponse>> HandleAsync(ParentRequest request, CancellationToken cancellationToken = default)
        {
            if (request.ParentId == Guid.Empty||request==null)
                throw new ResultException("cannot send nullable request", ((int)HttpStatusCode.BadRequest));
            var service = _bawabtkFactory.Build(nameof(ParentProfile));
            var oData = new ParentOperationContract()
            {
                Address = request.Address,
                FullName = request.FullName,
                WhatsAppNumber = request.WhatsAppNumber,
                Password = request.Password,
                ProfilePictureUrl = request.ProfilePictureUrl,
                Email = request.Email,
                PhoneNumber = request.PhoneNumber,
                JobTitle = request.JobTitle,
                ParentProfileId = request.ParentId

            };
            var oResult = await service.Update(oData, request.ParentId);
            var response = new ParentResponse()
            {
                Address = oResult.Address,
                FullName = oResult.FullName,
                JobTitle = oResult.JobTitle,
                WhatsAppNumber = oResult.WhatsAppNumber,
                ParentProfileGuid = oResult.ParentProfileGuid
            };
            return Ok(response as dynamic);
        }
    }
}
