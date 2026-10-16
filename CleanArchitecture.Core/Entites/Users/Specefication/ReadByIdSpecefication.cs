
using Ardalis.Specification;

namespace CleanArchitecture.Core.Entites.Users.Specefication
{
    public class ReadByIdSpecefication: SingleResultSpecification<User>
    {
        public ReadByIdSpecefication(Guid id)
        {
            Query.Where(d => d._UserId == id);
            
        }

    }
}
