using Bawabtk.REPR.EndPoints.Parents.Create;
using CleanArchitecture.Core.Entites.ParentsProfile;
using CleanArchitecture.Service.Factories;
using CleanArchitecture.Shared.Contract.ParentImplemention;
using huzcodes.Endpoints.Abstractions;
using Microsoft.AspNetCore.Mvc;

namespace Bawabtk.REPR.EndPoints.Parents.Read
{
    public class Parent(BawabtkFactory<ParentOperationContract, ParentContract> bawabtkFactory) : EndpointsAsync.WithoutRequest.WithActionResult<ParentResponse>
    {
        private readonly BawabtkFactory<ParentOperationContract, ParentContract> _bawabtkFactory = bawabtkFactory;
        [HttpGet(Route.ParentRoutes.BaseUrl)]

        public override async Task<ActionResult<ParentResponse>> HandleAsync(CancellationToken cancellationToken = default)
        {
            var service = _bawabtkFactory.Build(nameof(ParentProfile));
            var oData = await service.Read();
            var oResult = oData.Select(p => new ParentResponse()
            {
                Address = p.Address,
                FullName = p.FullName,
                JobTitle = p.JobTitle,
                WhatsAppNumber = p.WhatsAppNumber,
                ParentProfileGuid = p.ParentProfileGuid
            }).ToList();
            return Ok(oResult as dynamic);
        }
    }
}
