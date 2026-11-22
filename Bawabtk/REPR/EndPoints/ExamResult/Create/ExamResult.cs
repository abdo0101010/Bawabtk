using CleanArchitecture.Service.Factories;
using CleanArchitecture.Shared.Contract.ExamResultImplemention;
using huzcodes.Endpoints.Abstractions;
using huzcodes.Extensions.Exceptions;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Bawabtk.REPR.EndPoints.ExamResult.Create
{
    public class ExamResult(BawabtkFactory<ExamResultOperationContract,ExamResultContract> bawabtkFactory):EndpointsAsync.WithRequest<ExamResultRquest>.WithActionResult<ExamResultResponse>
    {
        private readonly BawabtkFactory<ExamResultOperationContract, ExamResultContract> _bawabtkFactory = bawabtkFactory;
        [HttpPost(Route.ExamResultRoutes.Create)]
        public override async Task<ActionResult<ExamResultResponse>> HandleAsync(ExamResultRquest request, CancellationToken cancellationToken = default)
        {
            if (request == null)
                throw new ResultException("cannot Send null Request", ((int)HttpStatusCode.BadRequest));
            var Service = _bawabtkFactory.Build(nameof(CleanArchitecture.Core.Entites.ExamResults.ExamResult));
            var oData = new ExamResultOperationContract()
            {
                ExamId=request.ExamId,
               StudentId=request.StudentId,
              GradedByUserId=request.GradedByUserId,
              IsLowGradeAlertSent=request.IsLowGradeAlertSent,
              Score=request.Score,
              SubmittedAt=request.SubmittedAt,
              TeacherFeedback=request.TeacherFeedback
            };
            var oResult = await Service.Create(oData);
            var Response = new ExamResultContract()
            {
                ExamResultId= oResult.ExamResultId,
                ExamId = oResult.ExamId,
                StudentId = oResult.StudentId,
                GradedByUserId = oResult.GradedByUserId,
                IsLowGradeAlertSent = oResult.IsLowGradeAlertSent,
                Score = oResult.Score,
                SubmittedAt = oResult.SubmittedAt,
                TeacherFeedback = oResult.TeacherFeedback

            };
            return Ok();
        }
    }
}
