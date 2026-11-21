using CleanArchitecture.Service.Factories;
using CleanArchitecture.Shared.Contract.QuestionOptionImplemention;
using huzcodes.Endpoints.Abstractions;
using huzcodes.Extensions.Exceptions;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Bawabtk.REPR.EndPoints.QuestionOption.Update.QuestionId
{
    public class QuestionOption(BawabtkFactory<QuestionOptionOperationContract, QuestionOptionContract> bawabtkFactory)
        : EndpointsAsync.WithRequest<QuestionOptionRequest>.WithActionResult
    {
        private readonly BawabtkFactory<QuestionOptionOperationContract, QuestionOptionContract> _bawabtkFactory = bawabtkFactory;
        [HttpPut(Route.QuestionOptionRoutes.UpdateQuestionId)]
        public override async Task<ActionResult> HandleAsync(QuestionOptionRequest request, CancellationToken cancellationToken = default)
        {
            if (request.OptionId == Guid.Empty || request.QuestionId == Guid.Empty)
                throw new ResultException("Cannot send null option id or null question id ", ((int)HttpStatusCode.BadRequest));
            var Service = _bawabtkFactory.Build(nameof(CleanArchitecture.Core.Entites.QuestionOption.QuestionOption));
             await Service.UpdateParentById(request.QuestionId, request.OptionId);
            return Ok();

        }
    }
}
