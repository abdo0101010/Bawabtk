using CleanArchitecture.Service.Factories;
using CleanArchitecture.Shared.Contract.QuestionOptionImplemention;
using huzcodes.Endpoints.Abstractions;
using huzcodes.Extensions.Exceptions;
using Microsoft.AspNetCore.Mvc;

namespace Bawabtk.REPR.EndPoints.QuestionOption.Read.ByQuestionId
{
    public class QuestionOption(BawabtkFactory<QuestionOptionOperationContract, QuestionOptionContract> bawabtkFactory)
        : EndpointsAsync.WithRequest<QuestionOptionRequest>.WithActionResult<QuestionOptionResponse>
    {
        private readonly BawabtkFactory<QuestionOptionOperationContract, QuestionOptionContract> _bawabtkFactory = bawabtkFactory;
        [HttpGet(Route.QuestionOptionRoutes.ReadSingleQuestionOptionByQuestionId)]
        public override async Task<ActionResult<QuestionOptionResponse>> HandleAsync(QuestionOptionRequest request, CancellationToken cancellationToken = default)
        {
            if (request.QuestionId == Guid.Empty|| request.OptionId == Guid.Empty)
                throw new ResultException("cannot send null QuestionId or OptionId", ((int)System.Net.HttpStatusCode.BadRequest));
            var Service = _bawabtkFactory.Build(nameof(CleanArchitecture.Core.Entites.QuestionOption.QuestionOption));
            var oResult = await Service.ReadSingleByParentId(request.OptionId, request.QuestionId);
            if (oResult == null)
                throw new ResultException("Options not found", ((int)System.Net.HttpStatusCode.NotFound));
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
