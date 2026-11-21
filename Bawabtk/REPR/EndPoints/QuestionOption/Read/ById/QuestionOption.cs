using CleanArchitecture.Service.Factories;
using CleanArchitecture.Shared.Contract.QuestionOptionImplemention;
using huzcodes.Endpoints.Abstractions;
using huzcodes.Extensions.Exceptions;
using Microsoft.AspNetCore.Mvc;

namespace Bawabtk.REPR.EndPoints.QuestionOption.Read.ById
{
    public class QuestionOption(BawabtkFactory<QuestionOptionOperationContract, QuestionOptionContract> bawabtkFactory)
        : EndpointsAsync.WithRequest<QuestionOptionRequest>.WithActionResult<QuestionOptionResponse>
    {
        private readonly BawabtkFactory<QuestionOptionOperationContract, QuestionOptionContract> _bawabtkFactory = bawabtkFactory;
        [HttpGet(Route.QuestionOptionRoutes.ReadById)]
        public override async Task<ActionResult<QuestionOptionResponse>> HandleAsync(QuestionOptionRequest request, CancellationToken cancellationToken = default)
        {
            if(request.OptionId == Guid.Empty)
                throw new ResultException("cannot send null OptionId", ((int)System.Net.HttpStatusCode.BadRequest));
            var Service = _bawabtkFactory.Build(nameof(CleanArchitecture.Core.Entites.QuestionOption.QuestionOption));
            var oResult =await Service.ReadById(request.OptionId);
            if(oResult == null)
                throw new ResultException("Option not found", ((int)System.Net.HttpStatusCode.NotFound));
            var oResponse = new QuestionOptionResponse()
            {
                OptionId = oResult._OptionId,
                QuestionId = oResult.QuestionId,
                Text = oResult.Text,
                IsCorrect = oResult.IsCorrect
            };
            return Ok(oResponse);

        }
    }
}
