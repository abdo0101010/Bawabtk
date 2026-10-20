using CleanArchitecture.Core.Entites.ParentsProfile;
using CleanArchitecture.Core.Entites.Student;
using CleanArchitecture.Core.Entites.Users;
using CleanArchitecture.Core.Interfaces;
using CleanArchitecture.Service.Services.Parent;
using CleanArchitecture.Service.Services.Student;
using CleanArchitecture.Service.Services.User;
using CleanArchitecture.Shared.Contract.Student;
using huzcodes.Persistence.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace CleanArchitecture.Service.Factories
{
    public class BawabtkFactory<TRequest, TResponse>(IRepository<User> userrepository,IRepository<ParentProfile>parentrepository,IRepository<StudentProfile> stdrepository)
    {
        private readonly IRepository<User> _userrepository = userrepository;
        private readonly IRepository<ParentProfile> _parentrepository = parentrepository;
        private readonly IRepository<StudentProfile> _stdrepository = stdrepository;
        public IGenericServices<TRequest, TResponse> Build(string key)
        {
            switch (key)
            {
                case (nameof(StudentProfile)):
                    return new StudentService<TRequest, TResponse>(_userrepository, _stdrepository, _parentrepository);
                case (nameof(ParentProfile)):
                    return new ParentService<TRequest, TResponse>(_userrepository, _stdrepository, _parentrepository);
               
                default:
                    throw new ArgumentException($"Invalid service key: {key}");
            }
        }

    }
}
