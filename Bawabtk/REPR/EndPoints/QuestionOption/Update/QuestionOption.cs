using CleanArchitecture.Service.Factories;
using CleanArchitecture.Shared.Contract.QuestionOptionImplemention;
using huzcodes.Endpoints.Abstractions;
using huzcodes.Extensions.Exceptions;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Bawabtk.REPR.EndPoints.QuestionOption.Update
{
    public class QuestionOption(BawabtkFactory<QuestionOptionOperationContract, QuestionOptionContract> bawabtkFactory)
        : EndpointsAsync.WithRequest<QuestionOptionRequest>.WithActionResult<QuestionOptionResponse>
    {
        private readonly BawabtkFactory<QuestionOptionOperationContract, QuestionOptionContract> _bawabtkFactory = bawabtkFactory;
        [HttpPut(Route.QuestionOptionRoutes.Update)]
        public override async Task<ActionResult<QuestionOptionResponse>> HandleAsync(QuestionOptionRequest request, CancellationToken cancellationToken = default)
        {
            if (request == null)
                throw new ResultException("cannont send null Request", ((int)HttpStatusCode.BadRequest));
            var Service = _bawabtkFactory.Build(nameof(CleanArchitecture.Core.Entites.QuestionOption.QuestionOption));
            var oContract = new QuestionOptionOperationContract()
            {
                IsCorrect=request.IsCorrect,
                QuestionId=request.QuestionId,
                Text=request.Text
            };

            var oData = await Service.Update(oContract, request.OptionId);
            var oResult = new QuestionOptionRequest()
            {
                IsCorrect=oData.IsCorrect,
                Text=oData.Text,
                OptionId=oData._OptionId,
                QuestionId=oData.QuestionId
            };
            return Ok(oResult);
           
        }
    }
}
