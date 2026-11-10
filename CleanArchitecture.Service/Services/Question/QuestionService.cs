using CleanArchitecture.Core.Interfaces;
using CleanArchitecture.Shared.Contract.QuestionImplemention;
using huzcodes.Extensions.Exceptions;
using huzcodes.Persistence.Interfaces.Repositories;
using System.Net;

namespace CleanArchitecture.Service.Services.Question
{
    public class QuestionService<TRequest, TResponse>(IRepository<Core.Entites.Question.Question> QuestionRepository,IRepository<Core.Entites.Exams.Exam> ExamRepository) : IGenericServices<TRequest, TResponse>
    {
        private readonly IRepository<Core.Entites.Question.Question> _QuestionRepository = QuestionRepository;
        private readonly IRepository<Core.Entites.Exams.Exam> _ExamRepository = ExamRepository;
        public async ValueTask<TResponse> Create(TRequest request)
        {
            var oContract = request as QuestionOperationContract;
            if (oContract == null || oContract.ExamId == Guid.Empty)
                throw new ResultException("cannot send null ExamId", ((int)HttpStatusCode.BadRequest));
            var oData = new Core.Entites.Question.Question(oContract.Text, oContract.Points,Guid.Empty, oContract.SortOrder);
            var SpecExam = new Core.Entites.Exams.Specification.ReadByIdSpecification(oContract.ExamId);
            var Exam = await _ExamRepository.FirstOrDefaultAsync(SpecExam);
            if (Exam == null)
                throw new ResultException("cannot found Exam ", ((int)HttpStatusCode.NotFound));
            oData.SetExam(Exam);
            var Question = await _QuestionRepository.AddAsync(oData);
            var oResult = new QuestionContract(Question._QuestionId, Exam._ExamId, oContract.Text, oContract.Points, oContract.SortOrder);
            return oResult as dynamic;
        }
        public async ValueTask Delete(Guid id)
        {
            if (id == Guid.Empty)
                throw new ResultException("cannot send null id", ((int)HttpStatusCode.BadRequest));
            var spec = new Core.Entites.Question.Specification.ReadByIdSpecification(id);
            var Question = await _QuestionRepository.FirstOrDefaultAsync(spec);
            if (Question == null)
                throw new ResultException("cannot found Question ", ((int)HttpStatusCode.NotFound));
            await _QuestionRepository.DeleteAsync(Question);
        }
        public async ValueTask<List<TResponse>> Read()
        {
            var spec = new Core.Entites.Question.Specification.ReadSpecification();
            var Questions = await _QuestionRepository.ListAsync(spec);
            if (Questions == null || Questions.Count == 0)
                throw new ResultException("cannot found Questions ", ((int)HttpStatusCode.NotFound));
            return Questions.Select(e => new QuestionContract(e._QuestionId, e.Exam._ExamId, e.Text, e.Points, e.SortOrder)).ToList() as List<TResponse>;
        }
        public async ValueTask<TResponse> ReadById(Guid id)
        {
            if (id == Guid.Empty)
                throw new ResultException("cannot send null id", ((int)HttpStatusCode.BadRequest));
            var spec = new Core.Entites.Question.Specification.ReadByIdSpecification(id);
            var Question = await _QuestionRepository.FirstOrDefaultAsync(spec);
            if (Question == null)
                throw new ResultException("cannot found Question ", ((int)HttpStatusCode.NotFound));
            var oResult = new QuestionContract(Question._QuestionId, Question.Exam._ExamId, Question.Text, Question.Points, Question.SortOrder);
            return oResult as dynamic;
        }

        public ValueTask<TResponse> ReadSingleByParentId(Guid id, Guid ParentId)
        {
            if (id == Guid.Empty || ParentId == Guid.Empty)
                throw new ResultException("cannot send null id", ((int)HttpStatusCode.BadRequest));
            var spec = new Core.Entites.Question.Specification.ReadSingleByExamIdSpecification(id, ParentId);
            var Question = _QuestionRepository.FirstOrDefaultAsync(spec).Result;
            if (Question == null)
                throw new ResultException("cannot found Question ", ((int)HttpStatusCode.NotFound));
            var oResult = new QuestionContract(Question._QuestionId, Question.Exam._ExamId, Question.Text, Question.Points, Question.SortOrder);
            return oResult as dynamic;

        }

        public ValueTask<TResponse> Update(TRequest request, Guid id)
        {
            if (id == Guid.Empty)
                throw new ResultException("cannot send null id", ((int)HttpStatusCode.BadRequest));
            var oContract = request as QuestionOperationContract;
            var spec = new Core.Entites.Question.Specification.ReadByIdSpecification(id);
            var Question = _QuestionRepository.FirstOrDefaultAsync(spec).Result;
            if (Question == null)
                throw new ResultException("cannot found Question ", ((int)HttpStatusCode.NotFound));
            if (oContract == null)
                throw new ResultException("cannot send null request", ((int)HttpStatusCode.BadRequest));
            Question.UpdateQuestion(oContract.Text, oContract.Points, oContract.SortOrder, Guid.Empty);
            _QuestionRepository.UpdateAsync(Question);
            var oResult = new QuestionContract(Question._QuestionId, Question.Exam._ExamId, Question.Text, Question.Points, Question.SortOrder);
            return oResult as dynamic;

        }

        public async ValueTask UpdateParentById(Guid ParentId, Guid contractid)
        {
            if(ParentId == Guid.Empty || contractid == Guid.Empty)
                throw new ResultException("cannot send null id", ((int)HttpStatusCode.BadRequest));
            var spec = new Core.Entites.Question.Specification.ReadByIdSpecification(contractid);
            var Question = await _QuestionRepository.FirstOrDefaultAsync(spec);
            if (Question == null)
                throw new ResultException("cannot found Question ", ((int)HttpStatusCode.NotFound));
            Question.Exam._ExamId = ParentId;
           await _QuestionRepository.UpdateAsync(Question);

        }
    }

    
}
