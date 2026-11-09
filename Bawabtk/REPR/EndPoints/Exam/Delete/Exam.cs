using CleanArchitecture.Service.Factories;
using CleanArchitecture.Shared.Contract.ExamImplemention;
using huzcodes.Endpoints.Abstractions;
using Microsoft.AspNetCore.Mvc;

namespace Bawabtk.REPR.EndPoints.Exam.Delete
{
    public class Exam(BawabtkFactory<ExamOperationContract,ExamContract> bawabtkFactory): EndpointsAsync.WithRequest<ExamRequest>.WithActionResult
    {
        private readonly BawabtkFactory<ExamOperationContract, ExamContract> _bawabtkFactory = bawabtkFactory;
        [HttpDelete(Route.ExamRoutes.Delete)]
        public override async Task<ActionResult> HandleAsync(ExamRequest request, CancellationToken cancellationToken = default)
        {
            if (request == null)
                return BadRequest("Request cannot be null.");
            var service = _bawabtkFactory.Build(nameof(Exam));
            await service.Delete(request.ExamId);
            
            return Ok();
        }
    }
    
    }

