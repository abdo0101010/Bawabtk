using CleanArchitecture.Service.Factories;
using CleanArchitecture.Shared.Contract.QuestionOptionImplemention;
using huzcodes.Endpoints.Abstractions;
using Microsoft.AspNetCore.Mvc;

namespace Bawabtk.REPR.EndPoints.QuestionOption.Read
{
    public class QuestionOption(BawabtkFactory<QuestionOptionOperationContract, QuestionOptionContract> bawabtkFactory)
        : EndpointsAsync.WithoutRequest.WithActionResult<List<QuestionOptionResponse>>
    {
        private readonly BawabtkFactory<QuestionOptionOperationContract, QuestionOptionContract> _bawabtkFactory = bawabtkFactory;
        [HttpGet(Route.QuestionOptionRoutes.BaseUrl)]
        public override async Task<ActionResult<List<QuestionOptionResponse>>> HandleAsync(CancellationToken cancellationToken = default)
        {
            var Service = _bawabtkFactory.Build(nameof(CleanArchitecture.Core.Entites.QuestionOption.QuestionOption));
            var oResult =await Service.Read();
            var oResponse = oResult.Select(e => new QuestionOptionResponse()
            {
                OptionId = e._OptionId,
                QuestionId = e.QuestionId,
                Text = e.Text,
                IsCorrect = e.IsCorrect
            }).ToList();
            return Ok(oResponse);

        }
    }
}
