using Ardalis.Specification;

namespace CleanArchitecture.Core.Entites.Users.Specefication
{
    public class ReadByEmailSpecifition:SingleResultSpecification<User>
    {
        public ReadByEmailSpecifition(string email)
        {
            Query.Where(u => u.Email == email).Include(s => s.StudentProfile).Include(p => p.ParentProfile);
        }
    }
}
