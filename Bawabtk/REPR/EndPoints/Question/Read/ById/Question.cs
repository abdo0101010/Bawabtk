using CleanArchitecture.Service.Factories;
using CleanArchitecture.Shared.Contract.QuestionImplemention;
using huzcodes.Endpoints.Abstractions;
using Microsoft.AspNetCore.Mvc;

namespace Bawabtk.REPR.EndPoints.Question.Read.ById
{
    public class Question(BawabtkFactory<QuestionOperationContract, QuestionContract> factory) : EndpointsAsync.WithRequest<QuestionRequest>.WithActionResult<QuestionResponse>
    {
        private readonly BawabtkFactory<QuestionOperationContract, QuestionContract> _factory = factory;
        [HttpGet(Route.QuestionRoutes.ReadById)]
        public override async Task<ActionResult<QuestionResponse>> HandleAsync(QuestionRequest request, CancellationToken cancellationToken = default)
        {
            var service = _factory.Build(nameof(CleanArchitecture.Core.Entites.Question.Question));
            var oResult = await service.ReadById(request.QuestionId);
            var oResponse = new QuestionResponse()
            {
                QuestionId = oResult.QuestionId,
                ExamId = oResult.ExamId,
                Text = oResult.Text,
                Points = oResult.Points,
                SortOrder = oResult.SortOrder
            };
            return Ok(oResponse);
        }
    }
}
