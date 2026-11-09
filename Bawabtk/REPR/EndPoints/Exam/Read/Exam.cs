using CleanArchitecture.Service.Factories;
using CleanArchitecture.Shared.Contract.ExamImplemention;
using huzcodes.Endpoints.Abstractions;
using Microsoft.AspNetCore.Mvc;

namespace Bawabtk.REPR.EndPoints.Exam.Read
{
    public class Exam(BawabtkFactory<ExamOperationContract,ExamContract> bawabtkFactory):EndpointsAsync.WithoutRequest.WithActionResult<ExamResponse>
    {
        private readonly BawabtkFactory<ExamOperationContract, ExamContract> _bawabtkFactory = bawabtkFactory;
        [HttpGet(Route.ExamRoutes.BaseUrl)]
        public override async Task<ActionResult<ExamResponse>> HandleAsync(CancellationToken cancellationToken = default)
        {
            var service = _bawabtkFactory.Build(nameof(Exam));
            var oResult = await service.Read();
            var listresponse=new List<ExamResponse>();
            foreach (var exam in oResult)
            {
                var response = new ExamResponse()
                {
                    ExamId = exam.ExamId,
                    CourseId = exam.CourseId,
                    Title = exam.Title,
                    MaxScore = exam.MaxScore,
                    PassingScore = exam.PassingScore,
                    LowGradeAlertThreshold = exam.LowGradeAlertThreshold,
                    ExamDate = exam.ExamDate,
                    IsOnline = exam.IsOnline
                };
                listresponse.Add(response);
            }
            return Ok(listresponse as dynamic);
        }
    
    }
}
