using Ardalis.Specification;

namespace CleanArchitecture.Core.Entites.Users.Specefication
{
    public class ReadSpecefication: Specification<User>
    {
        public ReadSpecefication()
        {
            Query.Where(d => d._UserId != Guid.Empty);
        }
    }
}
