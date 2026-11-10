using CleanArchitecture.Core.Interfaces;
using CleanArchitecture.Shared.Contract.ExamImplemention;
using DnsClient.Protocol;
using huzcodes.Extensions.Exceptions;
using huzcodes.Persistence.Interfaces.Repositories;
using System.Net;

namespace CleanArchitecture.Service.Services.Exam
{
    public class ExamService<TRequest, TResponse>(IRepository<Core.Entites.Exams.Exam> ExamRepository,IRepository<Core.Entites.Courses.Course> CourseRepository) : IGenericServices<TRequest, TResponse>
    {
        private readonly IRepository<Core.Entites.Exams.Exam> _ExamRepository = ExamRepository;
        private readonly IRepository<Core.Entites.Courses.Course> _CourseRepository = CourseRepository;
        public async ValueTask<TResponse> Create(TRequest request)
        {
            var oContract = request as ExamOperationContract;
            var SpecCourse = new Core.Entites.Courses.Specification.ReadByIdSpecification(oContract.CourseId);
            var Course = await _CourseRepository.FirstOrDefaultAsync(SpecCourse);
            if (Course==null)
                throw new ResultException("cannot found course ", ((int)HttpStatusCode.NotFound));
            var oData = new Core.Entites.Exams.Exam(oContract.Title, oContract.MaxScore, oContract.PassingScore, oContract.LowGradeAlertThreshold, oContract.ExamDate, oContract.IsOnline,Guid.Empty);
             oData.SetCourse(Course);
            var Exam =await _ExamRepository.AddAsync(oData);
            var oResult = new ExamContract(Exam._ExamId, oContract.CourseId, oContract.Title, oContract.MaxScore, oContract.PassingScore, oContract.LowGradeAlertThreshold, oContract.ExamDate, oContract.IsOnline);
            return oResult as dynamic;
        }

        public async ValueTask Delete(Guid id)
        {
            if (id == Guid.Empty)
                throw new ResultException("cannot send null id", ((int)HttpStatusCode.BadRequest));
            var spec = new Core.Entites.Exams.Specification.ReadByIdSpecification(id);
            var Exam =await _ExamRepository.FirstOrDefaultAsync(spec);
            if (Exam == null)
                throw new ResultException("cannot found Exam ", ((int)HttpStatusCode.NotFound));
           await _ExamRepository.DeleteAsync(Exam);

        }

        public async ValueTask<List<TResponse>> Read()
        {
            var spec = new Core.Entites.Exams.Specification.ReadSpecification();
            var Exams = await _ExamRepository.ListAsync(spec);
            if (Exams == null || Exams.Count == 0)
                throw new ResultException("cannot found Exams ", ((int)HttpStatusCode.NotFound));
            return Exams.Select(e => new ExamContract(e._ExamId, e.Course._CourseId, e.Title, e.MaxScore, e.PassingScore, e.LowGradeAlertThreshold, e.ExamDate, e.IsOnline)).ToList() as List<TResponse>;

        }

        public async ValueTask<TResponse> ReadById(Guid id)
        {
            if (id == Guid.Empty)
                throw new ResultException("cannot send null id", ((int)HttpStatusCode.BadRequest));
            var spec = new Core.Entites.Exams.Specification.ReadByIdSpecification(id);
            var Exam = await _ExamRepository.FirstOrDefaultAsync(spec);
            if (Exam == null)
                throw new ResultException("cannot found Exam ", ((int)HttpStatusCode.NotFound));
            var oResult = new ExamContract(Exam._ExamId, Exam.Course._CourseId, Exam.Title, Exam.MaxScore, Exam.PassingScore, Exam.LowGradeAlertThreshold, Exam.ExamDate, Exam.IsOnline);
            return oResult as dynamic;

        }

        public async ValueTask<TResponse> ReadSingleByParentId(Guid id, Guid ParentId)
        {
            throw new NotImplementedException();
        }

        public async ValueTask<TResponse> Update(TRequest request, Guid id)
        {
            var oContract = request as ExamOperationContract;


            if (id == Guid.Empty)
                throw new ResultException("cannot send null id", ((int)HttpStatusCode.BadRequest));
            var spec = new Core.Entites.Exams.Specification.ReadByIdSpecification(id);
            var Exam = await _ExamRepository.FirstOrDefaultAsync(spec);
            if (Exam == null)
                throw new ResultException("cannot found Exam ", ((int)HttpStatusCode.NotFound));
            if(oContract == null)
                throw new ResultException("cannot send null request", ((int)HttpStatusCode.BadRequest));
            Exam.UpdateExam(oContract.Title, oContract.MaxScore, oContract.PassingScore, oContract.LowGradeAlertThreshold, oContract.ExamDate, oContract.IsOnline, Guid.Empty);
            await _ExamRepository.UpdateAsync(Exam);
           var oResult = new ExamContract(Exam._ExamId, Exam.Course._CourseId, Exam.Title, Exam.MaxScore, Exam.PassingScore, Exam.LowGradeAlertThreshold, Exam.ExamDate, Exam.IsOnline);

            return oResult as dynamic;
        }

        public async ValueTask UpdateParentById(Guid ParentId, Guid contractid)
        {
            throw new NotImplementedException();
        }
    }
}
