using CleanArchitecture.Core.Entites.Courses;
using CleanArchitecture.Core.Interfaces;
using CleanArchitecture.Shared.Contract.CourseImplemention;
using huzcodes.Persistence.Interfaces.Repositories;

namespace CleanArchitecture.Service.Services.Course
{
    public class CourseService<TRequest,TResponse>(IRepository<Core.Entites.Courses. Course> Courserepo):IGenericServices<TRequest,TResponse>
    {
        private readonly IRepository<Core.Entites.Courses.Course> _Courserepo = Courserepo;

        public ValueTask<TResponse> Create(TRequest request)
        {
            var oContract = request as CourseOperationContract;

        }

        public ValueTask Delete(Guid id)
        {
            throw new NotImplementedException();
        }

        public ValueTask<List<TResponse>> Read()
        {
            throw new NotImplementedException();
        }

        public ValueTask<TResponse> ReadById(Guid id)
        {
            throw new NotImplementedException();
        }

        public ValueTask<TResponse> ReadSingleByParentId(Guid id, Guid ParentId)
        {
            throw new NotImplementedException();
        }

        public ValueTask<TResponse> Update(TRequest request, Guid id)
        {
            throw new NotImplementedException();
        }

        public ValueTask UpdateParentById(Guid ParentId, Guid contractid)
        {
            throw new NotImplementedException();
        }
    }
}
