using CleanArchitecture.Service.Factories;
using CleanArchitecture.Shared.Contract.QuestionOptionImplemention;
using huzcodes.Endpoints.Abstractions;
using huzcodes.Extensions.Exceptions;
using Microsoft.AspNetCore.Mvc;

namespace Bawabtk.REPR.EndPoints.QuestionOption.Delete
{
    public class QuestionOption(BawabtkFactory<QuestionOptionOperationContract, QuestionOptionContract> bawabtkFactory)
        : EndpointsAsync.WithRequest<QuestionOptionRequest>.WithActionResult
    {
        private readonly BawabtkFactory<QuestionOptionOperationContract, QuestionOptionContract> _bawabtkFactory = bawabtkFactory;
        [HttpDelete(Route.QuestionOptionRoutes.Delete)]
        public override async Task<ActionResult> HandleAsync(QuestionOptionRequest request, CancellationToken cancellationToken = default)
        {
           if(request == null || request.OptionId == Guid.Empty)
                throw new ResultException("cannot send null OptionId", ((int)System.Net.HttpStatusCode.BadRequest));
            var Service = _bawabtkFactory.Build(nameof(CleanArchitecture.Core.Entites.QuestionOption.QuestionOption));
            await Service.Delete(request.OptionId);
         
            return Ok();
        }
    }
}
