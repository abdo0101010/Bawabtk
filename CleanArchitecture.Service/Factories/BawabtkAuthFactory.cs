using CleanArchitecture.Core.Entites.ParentsProfile;
using CleanArchitecture.Core.Entites.Student;
using CleanArchitecture.Core.Entites.Users;
using CleanArchitecture.Core.Interfaces;
using CleanArchitecture.Service.Services.User;
using huzcodes.Persistence.Interfaces.Repositories;

namespace CleanArchitecture.Service.Factories
{
    public class BawabtkAuthFactory<TRequest, TResponse>(
        IRepository<User> userrepository)
        
    {
        public IUserService<TRequest, TResponse> BuildAuthService()
        {
            return new Userservice<TRequest, TResponse>(userrepository);
        }
    }
}
