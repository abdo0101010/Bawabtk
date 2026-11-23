using CleanArchitecture.Service.Factories;
using CleanArchitecture.Shared.Contract.ExamResultImplemention;
using huzcodes.Endpoints.Abstractions;
using Microsoft.AspNetCore.Mvc;

namespace Bawabtk.REPR.EndPoints.ExamResult.Read
{
    public class ExamResult(BawabtkFactory<ExamResultOperationContract, ExamResultContract> bawabtkFactory):EndpointsAsync.WithoutRequest.WithActionResult<List<ExamResultResponse>>
    {
        private readonly BawabtkFactory<ExamResultOperationContract, ExamResultContract> _bawabtkFactory = bawabtkFactory;
        [HttpGet(Route.ExamResultRoutes.BaseUrl)]
        public override async Task<ActionResult<List<ExamResultResponse>>> HandleAsync(CancellationToken cancellationToken = default)
        {
            var Service = _bawabtkFactory.Build(nameof(CleanArchitecture.Core.Entites.ExamResults.ExamResult));
            var oData = await Service.Read();
            var oResult = oData.Select(e => new ExamResultResponse()
            {
                ExamId=e.ExamId,
                 ExamResultId=e.ExamResultId,
                 GradedByUserId=e.GradedByUserId,
                 IsLowGradeAlertSent=e.IsLowGradeAlertSent,
                 Score=e.Score,
                 StudentId=e.StudentId,
                 SubmittedAt=e.SubmittedAt,
                 TeacherFeedback=e.TeacherFeedback
            });
            return Ok(oResult);
        }
    }
}
