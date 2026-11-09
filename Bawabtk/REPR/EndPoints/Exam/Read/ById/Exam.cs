using CleanArchitecture.Service.Factories;
using CleanArchitecture.Shared.Contract.ExamImplemention;
using huzcodes.Endpoints.Abstractions;
using Microsoft.AspNetCore.Mvc;

namespace Bawabtk.REPR.EndPoints.Exam.Read.ById
{
    public class Exam(BawabtkFactory<ExamOperationContract, ExamContract> bawabtkFactory) : EndpointsAsync.WithRequest<ExamRequest>.WithActionResult<ExamResponse>
    {
        private readonly BawabtkFactory<ExamOperationContract, ExamContract> _bawabtkFactory = bawabtkFactory;
      
        [HttpGet(Route.ExamRoutes.ReadById)]
        public override async Task<ActionResult<ExamResponse>> HandleAsync(ExamRequest request, CancellationToken cancellationToken = default)
        {
            var service = _bawabtkFactory.Build(nameof(Exam));
            var oResult = await service.ReadById(request.ExamId);
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
