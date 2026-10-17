using Ardalis.Specification;
namespace CleanArchitecture.Core.Entites.ParentsProfile.Specefication
{
    public class ReadSpecefication: Specification<ParentProfile>
    {
        public ReadSpecefication()
        {
            Query.Where(p => p._ParentProfileId != Guid.Empty);
        }
    }
}
