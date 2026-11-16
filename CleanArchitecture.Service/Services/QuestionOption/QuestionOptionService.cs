using CleanArchitecture.Core.Entites.Question.Specification;
using CleanArchitecture.Core.Interfaces;
using CleanArchitecture.Shared.Contract.QuestionOptionContract;
using huzcodes.Extensions.Exceptions;
using huzcodes.Persistence.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

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

        public ValueTask Delete(Guid id)
        {
            throw new NotImplementedException();
        }

        public ValueTask<List<TRequest>> Read()
        {
            throw new NotImplementedException();
        }

        public ValueTask<TRequest> ReadById(Guid id)
        {
            throw new NotImplementedException();
        }

        public ValueTask<TRequest> ReadSingleByParentId(Guid id, Guid ParentId)
        {
            throw new NotImplementedException();
        }

        public ValueTask<TRequest> Update(TResponse request, Guid id)
        {
            throw new NotImplementedException();
        }

        public ValueTask UpdateParentById(Guid ParentId, Guid contractid)
        {
            throw new NotImplementedException();
        }
    }
}
