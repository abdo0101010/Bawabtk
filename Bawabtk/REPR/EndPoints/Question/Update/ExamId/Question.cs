using CleanArchitecture.Service.Factories;
using CleanArchitecture.Shared.Contract.QuestionImplemention;
using huzcodes.Endpoints.Abstractions;
using huzcodes.Extensions.Exceptions;
using Microsoft.AspNetCore.Mvc;

namespace Bawabtk.REPR.EndPoints.Question.Update.ExamId
{
    public class Question(BawabtkFactory<QuestionOperationContract,QuestionContract> bawabtkFactory) : EndpointsAsync.WithRequest<QuestionRequest>.WithActionResult
    {
        private readonly BawabtkFactory<QuestionOperationContract, QuestionContract> _bawabtkFactory = bawabtkFactory;
        [HttpPut(Route.QuestionRoutes.UpdateExamId)]
        public override async Task<ActionResult> HandleAsync(QuestionRequest request, CancellationToken cancellationToken = default)
        {
            if (request == null || request.QuestionId == Guid.Empty || request.ExamId == Guid.Empty)
                throw new ResultException("cannot send null QuestionId or ExamId", ((int)System.Net.HttpStatusCode.BadRequest));
            var service = _bawabtkFactory.Build(nameof(CleanArchitecture.Core.Entites.Question.Question));
            await service.UpdateParentById( request.ExamId,request.QuestionId);
            return Ok();
        }
    
    }
}
