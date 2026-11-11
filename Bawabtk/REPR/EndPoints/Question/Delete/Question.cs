using CleanArchitecture.Service.Factories;
using CleanArchitecture.Shared.Contract.QuestionImplemention;
using huzcodes.Endpoints.Abstractions;
using huzcodes.Extensions.Exceptions;
using Microsoft.AspNetCore.Mvc;

namespace Bawabtk.REPR.EndPoints.Question.Delete
{
    public class Question(BawabtkFactory<QuestionOperationContract,QuestionContract> bawabtkFactory): EndpointsAsync.WithRequest<QuestionRequest>.WithActionResult
    {
        private readonly BawabtkFactory<QuestionOperationContract, QuestionContract> _bawabtkFactory = bawabtkFactory;
        [HttpDelete(Route.QuestionRoutes.Delete)]
        public override async Task<ActionResult> HandleAsync(QuestionRequest request, CancellationToken cancellationToken = default)
        {
            if (request == null || request.QuestionId == Guid.Empty)
                throw new ResultException("cannot send null QuestionId", ((int)System.Net.HttpStatusCode.BadRequest));
            var service = _bawabtkFactory.Build(nameof(CleanArchitecture.Core.Entites.Question.Question));
             await service.Delete(request.QuestionId);
            return Ok();
        }
    }   
    
    
}
