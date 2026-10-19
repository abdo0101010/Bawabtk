using CleanArchitecture.Core.Entites.ParentsProfile;
using CleanArchitecture.Service.Factories;
using CleanArchitecture.Shared.Contract.ParentImplemention;
using huzcodes.Endpoints.Abstractions;
using huzcodes.Extensions.Exceptions;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Bawabtk.REPR.EndPoints.Parents.Create
{
    public class Parent(BawabtkFactory<ParentOperationContract, ParentContract> bawabtkFactory) : EndpointsAsync.WithRequest<ParentRequest>.WithActionResult<ParentResponse>
    {
        private readonly BawabtkFactory<ParentOperationContract, ParentContract> _bawabtkFactory = bawabtkFactory;
        [HttpPost(Route.ParentRoutes.Create)]

        public override async Task<ActionResult<ParentResponse>> HandleAsync(ParentRequest request, CancellationToken cancellationToken = default)
        {
            if (request == null)
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
                ParentProfileId=request.ParentProfileId

            };
            var oResult = await service.Create(oData);
            var response = new ParentResponse()
            {
                Address=oResult.Address,
                FullName=oResult.FullName,
                JobTitle=oResult.JobTitle,
                WhatsAppNumber=oResult.WhatsAppNumber,
                ParentProfileGuid=oResult.ParentProfileGuid
            };
            return Ok(response as dynamic);

        }
    }
    
}
