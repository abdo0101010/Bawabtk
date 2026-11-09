using CleanArchitecture.Service.Factories;
using CleanArchitecture.Shared.Contract.ExamImplemention;
using huzcodes.Endpoints.Abstractions;
using huzcodes.Extensions.Exceptions;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Bawabtk.REPR.EndPoints.Exam.Update
{
    public class Exam(BawabtkFactory<ExamOperationContract,ExamContract> bawabtkFactory) : EndpointsAsync.WithRequest<ExamRequest>.WithActionResult<ExamResponse>
    {
        private readonly BawabtkFactory<ExamOperationContract, ExamContract> _bawabtkFactory = bawabtkFactory;
        [HttpPut(Route.ExamRoutes.Update)]
        public override async Task<ActionResult<ExamResponse>> HandleAsync(ExamRequest request, CancellationToken cancellationToken = default)
        {
            if (request == null)
                throw new ResultException( "Request cannot be null.",((int)HttpStatusCode.BadRequest));
            var service = _bawabtkFactory.Build(nameof(Exam));
            var oData = new ExamOperationContract(request.CourseId, request.Title, request.MaxScore, request.PassingScore, request.LowGradeAlertThreshold, request.ExamDate, request.IsOnline);
            var oResult = await service.Update(oData,request.ExamId);
            var response = new ExamResponse()
            {
                ExamId = oResult.ExamId,
                CourseId = oResult.CourseId,
                Title = oResult.Title,
                MaxScore = oResult.MaxScore,
                PassingScore = oResult.PassingScore,
                LowGradeAlertThreshold = oResult.LowGradeAlertThreshold,
                ExamDate = oResult.ExamDate,
                IsOnline = oResult.IsOnline
            };
            return Ok(response as dynamic);
        }
    }
    
    
}
