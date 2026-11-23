using CleanArchitecture.Service.Factories;
using CleanArchitecture.Shared.Contract.ExamResultImplemention;
using huzcodes.Endpoints.Abstractions;
using huzcodes.Extensions.Exceptions;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Bawabtk.REPR.EndPoints.ExamResult.Update
{
    public class ExamResult(BawabtkFactory<ExamResultOperationContract, ExamResultContract> bawabtkFactory) : EndpointsAsync.WithRequest<ExamResultRequest>.WithActionResult<ExamResultResponse>
    {
        private readonly BawabtkFactory<ExamResultOperationContract, ExamResultContract> _bawabtkFactory = bawabtkFactory;
        [HttpPut(Route.ExamResultRoutes.Update)]
        public override async Task<ActionResult<ExamResultResponse>> HandleAsync(ExamResultRequest request, CancellationToken cancellationToken = default)
        {
            if (request.ExamResultId == Guid.Empty||request==null)
                throw new ResultException("cannot must null id ", ((int)HttpStatusCode.BadRequest));
            var Service = _bawabtkFactory.Build(nameof(CleanArchitecture.Core.Entites.ExamResults.ExamResult));
            var oData = new ExamResultOperationContract()
            {
                ExamId=request.ExamId,
                GradedByUserId=request.GradedByUserId,
                IsLowGradeAlertSent=request.IsLowGradeAlertSent,
                Score=request.Score,
                StudentId=request.StudentId,
                SubmittedAt=request.SubmittedAt,
                TeacherFeedback=request.TeacherFeedback

            };
            await Service.Update(oData, request.ExamResultId);
            var oResult = new ExamResultResponse()
            {
                ExamId = oData.ExamId,
                ExamResultId = request.ExamResultId,
                GradedByUserId = oData.GradedByUserId,
                IsLowGradeAlertSent = oData.IsLowGradeAlertSent,
                Score = oData.Score,
                StudentId = oData.StudentId,
                SubmittedAt = oData.SubmittedAt,
                TeacherFeedback = oData.TeacherFeedback
            };
            return Ok(oResult);
        }
    }
}
