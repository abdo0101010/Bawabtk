using CleanArchitecture.Core.Entites.Student;
using CleanArchitecture.Service.Factories;
using CleanArchitecture.Shared.Contract.Student;
using huzcodes.Endpoints.Abstractions;
using Microsoft.AspNetCore.Mvc;
namespace Bawabtk.REPR.EndPoints.Students.Read
{
    public class Student(BawabtkFactory<StudentOperationContract, StudentContract> bawabtkFactory) : EndpointsAsync.WithoutRequest.WithActionResult<List<StudentResponse>>
    {
        private readonly BawabtkFactory<StudentOperationContract, StudentContract> _bawabtkFactory = bawabtkFactory;
        [HttpGet(Route.StudentRoutes.BaseUrl)]
        public override async Task<ActionResult<List<StudentResponse>>> HandleAsync(CancellationToken cancellationToken = default)
        {
            var service = _bawabtkFactory.Build(nameof(StudentProfile));
            var oData = await service.Read();
            var oResult = oData.Select(s => new StudentResponse()
            {
                GradeLevel = s.GradeLevel,
                Message = s.Message,
                ParentProfileGuid = s.ParentProfileGuid,
                StudentName = s.StudentName,
                StudentProfileGuid = s.StudentProfileGuid,
                StudentUserGuid = s.StudentUserGuid,
                UnifiedBarcode = s.UnifiedBarcode
            }).ToList();
            return oResult as dynamic;

        }
    }
}
