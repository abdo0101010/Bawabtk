using Ardalis.Specification;
namespace CleanArchitecture.Core.Entites.QuestionOption.Specification
{
    public class ReadQuestionOptionByQuestionId: Specification<QuestionOption>
    {
        public ReadQuestionOptionByQuestionId(Guid questionoptionId,Guid questionId)
        {
            Query.Where(e => e._OptionId == questionoptionId && e.Question._QuestionId == questionId);
        }
       
    }
}
