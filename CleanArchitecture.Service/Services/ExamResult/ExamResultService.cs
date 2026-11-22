using CleanArchitecture.Core.Entites.ExamResults;
using CleanArchitecture.Core.Entites.Exams;
using CleanArchitecture.Core.Entites.Student;
using CleanArchitecture.Core.Entites.Student.Specefication;
using CleanArchitecture.Core.Entites.Users;
using CleanArchitecture.Core.Interfaces;
using CleanArchitecture.Shared.Contract.ExamResultImplemention;
using huzcodes.Extensions.Exceptions;
using huzcodes.Persistence.Interfaces.Repositories;
using System.Net;

namespace CleanArchitecture.Service.Services.ExamResult
{
    public class ExamResultService<TRequest, TResponse>(IRepository<Core.Entites.ExamResults.ExamResult> ExamResultRepository,IRepository<StudentProfile> StudentRepository,IRepository<Core.Entites.Users. User>UserRepository,IRepository<Core.Entites.Exams.Exam> ExamRepository ) : IGenericServices<TRequest, TResponse>

    {
        private readonly IRepository<Core.Entites.ExamResults.ExamResult> _ExamResultRepository = ExamResultRepository;
        private readonly IRepository<StudentProfile> _StudentRepository = StudentRepository;
        private readonly IRepository<Core.Entites.Users.User> _UserRepository = UserRepository;
        private readonly IRepository<Core.Entites.Exams.Exam> _ExamRepository = ExamRepository;
        public async ValueTask<TResponse> Create(TRequest request)
        {
            var oContract = request as ExamResultOperationContract;
            if (oContract == null)
                throw new ResultException("cannot send null request", ((int)HttpStatusCode.BadRequest));
            var oData = new Core.Entites.ExamResults.ExamResult(oContract.Score, Guid.Empty, oContract.TeacherFeedback);
            var specStudent = new ReadByIdSpecification (oContract.StudentId);

            var Student = await _StudentRepository.FirstOrDefaultAsync(specStudent);
            if (Student == null)
                throw new ResultException("Cannot Found this Student ", ((int)HttpStatusCode.NotFound));
            var SpecUser = new Core.Entites.Users.Specefication.ReadByIdSpecefication(oContract.GradedByUserId);
            var User=await _UserRepository.FirstOrDefaultAsync(SpecUser);
            if (User == null)
                throw new ResultException("Cannot Found this User ", ((int)HttpStatusCode.NotFound));
            var SpecExam = new Core.Entites.Exams.Specification.ReadByIdSpecification(oContract.ExamId);
            var Exam = await _ExamRepository.FirstOrDefaultAsync(SpecExam);
            if (Exam == null)
                throw new ResultException("Cannot Found this Exam ", ((int)HttpStatusCode.NotFound));
            oData.SetStudent(Student);
            oData.SetUser(User);
            oData.SetExam(Exam);
            var OResult = await _ExamResultRepository.AddAsync(oData);
            var Response = new ExamResultContract()
            {
                ExamResultId= OResult._ExamResultId,
                ExamId = oData.Exam._ExamId,
                StudentId = oData.Student._StudentProfileId,
                Score = oData.Score,
                SubmittedAt = oData.SubmittedAt,
                GradedByUserId = oData.GradedByUser._UserId,
                TeacherFeedback = oData.TeacherFeedback,
                IsLowGradeAlertSent = oData.IsLowGradeAlertSent
            };
            return Response as dynamic;

        }

        public async ValueTask Delete(Guid id)
        {
            if (id == Guid.Empty)
                throw new ResultException("cannot send null request", ((int)HttpStatusCode.BadRequest));
            var SpecExamResult = new CleanArchitecture.Core.Entites.ExamResults.Specification.ReadByIdSpecification(id);
            var ExamResult = _ExamResultRepository.FirstOrDefaultAsync(SpecExamResult);
            if (ExamResult == null)
                throw new ResultException("Cannot Found ExamResult", ((int)HttpStatusCode.NotFound));
            await _ExamResultRepository.DeleteAsync(ExamResult.Result);
             

        }

        public ValueTask<List<TResponse>> Read()
        {
            var SpecExamResult = new  Core.Entites.ExamResults.Specification.ReadSpecification();
            var ExamResults = _ExamResultRepository.ListAsync(SpecExamResult);
            if(ExamResults.Result == null)
                throw new ResultException("Cannot Found ExamResults", ((int)HttpStatusCode.NotFound));
            var Response = ExamResults.Result.Select(er => new ExamResultContract
            {
                ExamResultId = er._ExamResultId,
                ExamId = er.Exam._ExamId,
                StudentId = er.Student._StudentProfileId,
                Score = er.Score,
                SubmittedAt = er.SubmittedAt,
                GradedByUserId = er.GradedByUser._UserId,
                TeacherFeedback = er.TeacherFeedback,
                IsLowGradeAlertSent = er.IsLowGradeAlertSent
            }).ToList() as dynamic;
            return Response;


        }

        public ValueTask<TResponse> ReadById(Guid id)
        {
            if(id == Guid.Empty)
                throw new ResultException("cannot send null request", ((int)HttpStatusCode.BadRequest));
            var SpecExamResult = new Core.Entites.ExamResults.Specification.ReadByIdSpecification(id);
            var ExamResult = _ExamResultRepository.FirstOrDefaultAsync(SpecExamResult);
            if (ExamResult.Result == null)
                throw new ResultException("Cannot Found ExamResult", ((int)HttpStatusCode.NotFound));
            var Response = new ExamResultContract()
            {
                ExamResultId = ExamResult.Result._ExamResultId,
                ExamId = ExamResult.Result.Exam._ExamId,
                StudentId = ExamResult.Result.Student._StudentProfileId,
                Score = ExamResult.Result.Score,
                SubmittedAt = ExamResult.Result.SubmittedAt,
                GradedByUserId = ExamResult.Result.GradedByUser._UserId,
                TeacherFeedback = ExamResult.Result.TeacherFeedback,
                IsLowGradeAlertSent = ExamResult.Result.IsLowGradeAlertSent

            };
            return Response as dynamic;

        }

        public ValueTask<TResponse> ReadSingleByParentId(Guid id, Guid ParentId)
        {
            throw new NotImplementedException();
        }

        public async ValueTask<TResponse> Update(TRequest request, Guid id)
        {
            var oContract = request as ExamResultOperationContract; ;

            if (id == Guid.Empty || oContract == null)
                throw new ResultException("cannot send null id and cannot send null  request",((int)HttpStatusCode.BadRequest));
            var spec = new Core.Entites.ExamResults.Specification.ReadByIdSpecification(id);
            var ExamResult = await _ExamResultRepository.FirstOrDefaultAsync(spec);
            if (ExamResult == null)
                throw new ResultException("Caannot found this ExamResult", ((int)HttpStatusCode.NotFound));
            var specStudent = new ReadByIdSpecification(oContract.StudentId);

            var Student = await _StudentRepository.FirstOrDefaultAsync(specStudent);
            if (Student == null)
                throw new ResultException("Cannot Found this Student ", ((int)HttpStatusCode.NotFound));
            var SpecExam = new Core.Entites.Exams.Specification.ReadByIdSpecification(oContract.ExamId);
            var Exam = await _ExamRepository.FirstOrDefaultAsync(SpecExam);
            if (Exam == null)
                throw new ResultException("Cannot Found this Exam ", ((int)HttpStatusCode.NotFound));
            var SpecUser = new Core.Entites.Users.Specefication.ReadByIdSpecefication(oContract.GradedByUserId);
            var User = await _UserRepository.FirstOrDefaultAsync(SpecUser);
            if (User == null)
                throw new ResultException("Cannot Found this User ", ((int)HttpStatusCode.NotFound));
            ExamResult.Update(oContract.Score, oContract.IsLowGradeAlertSent);
            ExamResult.SetStudent(Student);
            ExamResult.SetUser(User);
            ExamResult.SetExam(Exam);
            await _ExamResultRepository.UpdateAsync(ExamResult);
            var Response = new ExamResultContract()
            {
                ExamResultId = ExamResult._ExamResultId,
                ExamId = ExamResult.Exam._ExamId,
                StudentId = ExamResult.Student._StudentProfileId,
                Score = ExamResult.Score,
                SubmittedAt = ExamResult.SubmittedAt,
                GradedByUserId = ExamResult.GradedByUser._UserId,
                TeacherFeedback = ExamResult.TeacherFeedback,
                IsLowGradeAlertSent = ExamResult.IsLowGradeAlertSent

            };
            return Response as dynamic;
        }

        public ValueTask UpdateParentById(Guid ParentId, Guid contractid)
        {
            throw new NotImplementedException();
        }
    }
}
