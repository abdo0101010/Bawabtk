
using CleanArchitecture.Service.Factories;
using CleanArchitecture.Shared.Contract.QuestionImplemention;
using huzcodes.Endpoints.Abstractions;
using huzcodes.Extensions.Exceptions;
using Microsoft.AspNetCore.Mvc;

namespace Bawabtk.REPR.EndPoints.Question.Read.ByExamId
{
    public class Question(BawabtkFactory<QuestionOperationContract, QuestionContract> factory) : EndpointsAsync.WithRequest<QuestionRequest>.WithActionResult<QuestionResponse>
    {
        private readonly BawabtkFactory<QuestionOperationContract, QuestionContract> _factory = factory;
        [HttpGet(Route.QuestionRoutes.ReadSingleQuestionByExamId)]
        public override async Task<ActionResult<QuestionResponse>> HandleAsync(QuestionRequest request, CancellationToken cancellationToken = default)
        {
            if (request == null || request.ExamId == Guid.Empty)
                throw new ResultException("cannot send null ExamId", ((int)System.Net.HttpStatusCode.BadRequest));
            var service = _factory.Build(nameof(CleanArchitecture.Core.Entites.Question.Question));
            var oResult = await service.ReadSingleByParentId(request.QuestionId, request.ExamId);
            var oResponse = new QuestionResponse()
            {
                QuestionId = oResult.QuestionId,
                ExamId = oResult.ExamId,
                Text = oResult.Text,
                Points = oResult.Points,
                SortOrder = oResult.SortOrder
            };
            return Ok(oResponse); }
    }
}
