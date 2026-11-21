using CleanArchitecture.Service.Factories;
using CleanArchitecture.Shared.Contract.QuestionOptionImplemention;
using huzcodes.Endpoints.Abstractions;
using huzcodes.Extensions.Exceptions;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Bawabtk.REPR.EndPoints.QuestionOption.Create
{
    public class QuestionOption(BawabtkFactory<QuestionOptionOperationContract, QuestionOptionContract> bawabtkFactory)
        : EndpointsAsync.WithRequest<QuestionOptionRequest>.WithActionResult<QuestionOptionResponse>
    {
        private readonly BawabtkFactory<QuestionOptionOperationContract, QuestionOptionContract> _bawabtkFactory = bawabtkFactory;
        [HttpPost(Route.QuestionOptionRoutes.Create)]
        public override async Task<ActionResult<QuestionOptionResponse>> HandleAsync(QuestionOptionRequest request, CancellationToken cancellationToken = default)
        {
            if (request == null)
                throw new ResultException("Cannot Send null Request", ((int)HttpStatusCode.BadRequest));

            var Service = _bawabtkFactory.Build(nameof(CleanArchitecture.Core.Entites.QuestionOption.QuestionOption));

            var oData = new QuestionOptionOperationContract()
            {
                Text = request.Text,
                IsCorrect = request.IsCorrect,
                QuestionId = request.QuestionId,
            };

            var oResult = await Service.Create(oData);

            var Response = new QuestionOptionResponse()
            {
                OptionId = oResult._OptionId,
                QuestionId = oResult.QuestionId,
                Text = oResult.Text,
                IsCorrect = oResult.IsCorrect
            };

            return Ok(Response);
        }
    }
}