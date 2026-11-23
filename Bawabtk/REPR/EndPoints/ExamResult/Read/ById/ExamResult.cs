using CleanArchitecture.Service.Factories;
using CleanArchitecture.Shared.Contract.ExamResultImplemention;
using huzcodes.Endpoints.Abstractions;
using huzcodes.Extensions.Exceptions;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Xml.Linq;

namespace Bawabtk.REPR.EndPoints.ExamResult.Read.ById
{
    public class ExamResult(BawabtkFactory<ExamResultOperationContract, ExamResultContract> bawabtkFactory) : EndpointsAsync.WithRequest<ExamResultRequest>.WithActionResult<ExamResultResponse>
    {
        private readonly BawabtkFactory<ExamResultOperationContract, ExamResultContract> _bawabtkFactory = bawabtkFactory;
        [HttpGet(Route.ExamResultRoutes.ReadById)]
        public override async Task<ActionResult<ExamResultResponse>> HandleAsync(ExamResultRequest request, CancellationToken cancellationToken = default)
        {
            if (request.ExamResultId == Guid.Empty)
                throw new ResultException("cannot must null id ", ((int)HttpStatusCode.BadRequest));
            var Service = _bawabtkFactory.Build(nameof(CleanArchitecture.Core.Entites.ExamResults.ExamResult));
            var oData = await Service.ReadById(request.ExamResultId);
            var oResult = new ExamResultResponse()
            {
                ExamId = oData.ExamId,
                ExamResultId = oData.ExamResultId,
                GradedByUserId = oData.GradedByUserId,
                IsLowGradeAlertSent = oData.IsLowGradeAlertSent,
                Score = oData.Score,
                StudentId = oData.StudentId,
                SubmittedAt = oData.SubmittedAt,
                TeacherFeedback =oData.TeacherFeedback
            };
            return Ok(oResult);
        }
    }
}
