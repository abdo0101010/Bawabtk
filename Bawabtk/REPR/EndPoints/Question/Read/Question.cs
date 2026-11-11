using CleanArchitecture.Service.Factories;
using CleanArchitecture.Shared.Contract.QuestionImplemention;
using huzcodes.Endpoints.Abstractions;
using huzcodes.Extensions.Exceptions;
using Microsoft.AspNetCore.Mvc;

namespace Bawabtk.REPR.EndPoints.Question.Read
{
    public class Question(BawabtkFactory<QuestionOperationContract, QuestionContract> factory) : EndpointsAsync.WithoutRequest.WithActionResult<QuestionResponse>
    {
        private readonly BawabtkFactory<QuestionOperationContract, QuestionContract> _factory = factory;
        [HttpGet(Route.QuestionRoutes.BaseUrl)]
        public override async Task<ActionResult<QuestionResponse>> HandleAsync( CancellationToken cancellationToken = default)
        {
            var service = _factory.Build(nameof(CleanArchitecture.Core.Entites.Question.Question));
            var oResult = await service.Read();
            var oResponse = oResult.Select(e => new QuestionResponse()
            {
                QuestionId = e.QuestionId,
                ExamId = e.ExamId,
                Text = e.Text,
                Points = e.Points,
                SortOrder = e.SortOrder
            }).ToList();
            return Ok(oResponse);


        }
    
    }
}
