using CleanArchitecture.Service.Factories;
using CleanArchitecture.Shared.Contract.ExamResultImplemention;
using huzcodes.Endpoints.Abstractions;
using huzcodes.Extensions.Exceptions;
using Microsoft.AspNetCore.Mvc;
using System.Net;
namespace Bawabtk.REPR.EndPoints.ExamResult.Delete
{
    public class ExamResult (BawabtkFactory<ExamResultOperationContract,ExamResultContract> bawabtkFactory) : EndpointsAsync.WithRequest<ExamResultRequest>.WithActionResult
    {
        private readonly BawabtkFactory<ExamResultOperationContract, ExamResultContract> _bawabtkFactory = bawabtkFactory;
        [HttpDelete(Route.ExamResultRoutes.Delete)]
        public override async Task<ActionResult> HandleAsync(ExamResultRequest request, CancellationToken cancellationToken = default)
        {
            if (request.ExamResultId == Guid.Empty)
                throw new ResultException("cannot must null id ", ((int)HttpStatusCode.BadRequest));
            var Service = _bawabtkFactory.Build(nameof(CleanArchitecture.Core.Entites.ExamResults.ExamResult));
            await Service.Delete(request.ExamResultId);
            return Ok();
        }
    }
}
