using CleanArchitecture.Core.Entites.Question.Specification;
using CleanArchitecture.Core.Interfaces;
using CleanArchitecture.Shared.Contract.QuestionOptionImplemention;
using huzcodes.Extensions.Exceptions;
using huzcodes.Persistence.Interfaces.Repositories;
using System.Net;
namespace CleanArchitecture.Service.Services.QuestionOption
{
    class QuestionOptionService<TResponse, TRequest>(IRepository<Core.Entites.QuestionOption.QuestionOption> QuestionOptionRepository,IRepository<Core.Entites.Question.Question> QuestionRepository) : IGenericServices<TResponse, TRequest>
    {
        private readonly IRepository<Core.Entites.QuestionOption.QuestionOption> _QuestionOptionRepository = QuestionOptionRepository;
        private readonly IRepository<Core.Entites.Question.Question> _QuestionRepository = QuestionRepository;
        public async ValueTask<TRequest> Create(TResponse request)
        {
            if (request == null)
                throw new ResultException("cannot send null request", ((int)System.Net.HttpStatusCode.BadRequest));
                var oContract = request as QuestionOptionOperationContract;
            if (oContract == null || oContract.QuestionId == Guid.Empty)
                throw new ResultException("cannot send null QuestionId", ((int)System.Net.HttpStatusCode.BadRequest));
            var SpecQuestion = new ReadByIdSpecification(oContract.QuestionId);
            var Question = await _QuestionRepository.FirstOrDefaultAsync(SpecQuestion);
            if (Question == null)
                throw new ResultException("Cannot Found Question", ((int)HttpStatusCode.NotFound));
            var oData = new Core.Entites.QuestionOption.QuestionOption( oContract.Text, oContract.IsCorrect, Guid.Empty);
            oData.SetQuestion(Question);
            var OResult = await _QuestionOptionRepository.AddAsync(oData);
            var
                Response = new QuestionOptionContract()
                {
                    _OptionId = oData._OptionId,
                    QuestionId = oData.Question._QuestionId,
                    Text = oData.Text,
                    IsCorrect = oData.IsCorrect
                };
            return Response as dynamic;

        }

        public async ValueTask Delete(Guid id)
        {
         if(id == Guid.Empty)
                throw new ResultException("cannot send null request", ((int)System.Net.HttpStatusCode.BadRequest));
            var SpecQuestionOption = new Core.Entites.QuestionOption.Specification.ReadByIdSpecification(id);
            var QuestionOption = await _QuestionOptionRepository.FirstOrDefaultAsync(SpecQuestionOption);
            if (QuestionOption == null)
                throw new ResultException("Cannot Found QuestionOption", ((int)HttpStatusCode.NotFound));
            await _QuestionOptionRepository.DeleteAsync(QuestionOption);
           
        }

        public async ValueTask<List<TRequest>> Read()
        {
            var SpecQuestionOption = new Core.Entites.QuestionOption.Specification.ReadSpecification();
            var QuestionOptions = await _QuestionOptionRepository.ListAsync(SpecQuestionOption);
            return QuestionOptions.Select(qo => new QuestionOptionContract
            {
                _OptionId = qo._OptionId,
                QuestionId = qo.Question._QuestionId,
                Text = qo.Text,
                IsCorrect = qo.IsCorrect
            }).ToList()as dynamic;
        }

        public async ValueTask<TRequest> ReadById(Guid id)
        {
            var SpecQuestionOption = new Core.Entites.QuestionOption.Specification.ReadByIdSpecification(id);
            var QuestionOption = await _QuestionOptionRepository.FirstOrDefaultAsync(SpecQuestionOption);
            if (QuestionOption == null)
                throw new ResultException("Cannot Found QuestionOption", ((int)HttpStatusCode.NotFound));
            return new QuestionOptionContract
            {
                _OptionId = QuestionOption._OptionId,
                QuestionId = QuestionOption.Question._QuestionId,
                Text = QuestionOption.Text,
                IsCorrect = QuestionOption.IsCorrect
            }as dynamic;
        }


        public async ValueTask<TRequest> ReadSingleByParentId(Guid id, Guid parentId)
        {
            var specQuestion = new ReadByIdSpecification(parentId);
            var question = await _QuestionRepository.FirstOrDefaultAsync(specQuestion);
            if (question == null)
                throw new ResultException("Cannot Found Question", (int)HttpStatusCode.NotFound);

            var QuestionOptionByParent = new Core.Entites.QuestionOption.Specification.ReadQuestionOptionByQuestionId(id, parentId);
            var questionOption = await _QuestionOptionRepository.FirstOrDefaultAsync(QuestionOptionByParent);
            if (questionOption == null)
                throw new ResultException("Cannot Found QuestionOption", (int)HttpStatusCode.NotFound);

            return new QuestionOptionContract
            {
                _OptionId = questionOption._OptionId,
                QuestionId = question._QuestionId, 
                Text = questionOption.Text,
                IsCorrect = questionOption.IsCorrect
            } as dynamic;
        }

        public async ValueTask<TRequest> Update(TResponse request, Guid id)
        {
            if (request == null)
                throw new ResultException("cannot send null request", ((int)System.Net.HttpStatusCode.BadRequest));
            var oContract = request as QuestionOptionOperationContract;
            if (oContract == null || oContract.QuestionId == Guid.Empty)
                throw new ResultException("cannot send null QuestionId", ((int)System.Net.HttpStatusCode.BadRequest));
            var SpecQuestionOption = new Core.Entites.QuestionOption.Specification.ReadByIdSpecification(id);
            var QuestionOption = await _QuestionOptionRepository.FirstOrDefaultAsync(SpecQuestionOption);
            if (QuestionOption == null)
                throw new ResultException("Cannot Found QuestionOption", ((int)HttpStatusCode.NotFound));
            var SpecQuestion = new ReadByIdSpecification(oContract.QuestionId);
            var Question = await _QuestionRepository.FirstOrDefaultAsync(SpecQuestion);
            if (Question == null)
                throw new ResultException("Cannot Found Question", ((int)HttpStatusCode.NotFound));
            QuestionOption.Update(oContract.Text, oContract.IsCorrect, Guid.Empty);
            QuestionOption.SetQuestion(Question);
            await _QuestionOptionRepository.UpdateAsync(QuestionOption);
            return new QuestionOptionContract()
            {
                _OptionId = QuestionOption._OptionId,
                QuestionId = QuestionOption.Question._QuestionId,
                Text = QuestionOption.Text,
                IsCorrect = QuestionOption.IsCorrect
            } as dynamic;   

        }

        public async ValueTask UpdateParentById(Guid ParentId, Guid contractid)
        {
            if(ParentId==Guid.Empty || contractid == Guid.Empty)
                throw new ResultException("cannot send null request", ((int)System.Net.HttpStatusCode.BadRequest));
            var SpecQuestionOption = new Core.Entites.QuestionOption.Specification.ReadByIdSpecification(contractid);
            var QuestionOption = await _QuestionOptionRepository.FirstOrDefaultAsync(SpecQuestionOption);
            if(QuestionOption == null)
                throw new ResultException("Cannot Found QuestionOption", ((int)HttpStatusCode.NotFound));
            var SpecQuestion = new ReadByIdSpecification(ParentId);
            var Question = await _QuestionRepository.FirstOrDefaultAsync(SpecQuestion);
            if(Question == null)
                throw new ResultException("Cannot Found Question", ((int)HttpStatusCode.NotFound));
            QuestionOption.SetQuestion(Question);
            await _QuestionOptionRepository.UpdateAsync(QuestionOption);
        }
    }
}
