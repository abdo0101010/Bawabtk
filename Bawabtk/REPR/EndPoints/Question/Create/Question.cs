using CleanArchitecture.Service.Factories;
using CleanArchitecture.Shared.Contract.QuestionImplemention;
using huzcodes.Endpoints.Abstractions;
using huzcodes.Extensions.Exceptions;
using Microsoft.AspNetCore.Mvc;

namespace Bawabtk.REPR.EndPoints.Question.Create
{
    public class Question(BawabtkFactory<QuestionOperationContract,QuestionContract> bawabtkFactory):EndpointsAsync.WithRequest<QuestionRequest>.WithActionResult<QuestionResponse>
    {
        private readonly BawabtkFactory<QuestionOperationContract, QuestionContract> _bawabtkFactory= bawabtkFactory;
        [HttpPost(Route.QuestionRoutes.Create)]
        public override async Task<ActionResult<QuestionResponse>> HandleAsync(QuestionRequest request, CancellationToken cancellationToken = default)
        {
            if (request == null || request.ExamId == Guid.Empty)
                throw new ResultException("cannot send null ExamId", ((int)System.Net.HttpStatusCode.BadRequest));
            var service = _bawabtkFactory.Build(nameof(CleanArchitecture.Core.Entites.Question.Question));
            var oContract = new QuestionOperationContract(request.Text, request.Points, request.ExamId, request.SortOrder);
            var oResult =await service.Create(oContract);
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
