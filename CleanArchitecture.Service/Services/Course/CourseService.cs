using CleanArchitecture.Core.Entites.Courses.Specification;
using CleanArchitecture.Core.Entites.Users.Specefication;
using CleanArchitecture.Core.Interfaces;
using CleanArchitecture.Shared.Contract.CourseImplemention;
using huzcodes.Extensions.Exceptions;
using huzcodes.Persistence.Interfaces.Repositories;
using System.Net;
namespace CleanArchitecture.Service.Services.Course
{
    public class CourseService<TRequest,TResponse>(IRepository<Core.Entites.Courses. Course> Courserepo,IRepository<Core.Entites.Users.User>userrepo):IGenericServices<TRequest,TResponse>
    {
        private readonly IRepository<Core.Entites.Courses.Course> _Courserepo = Courserepo;
        private readonly IRepository<Core.Entites.Users.User> _userrepo = userrepo;

        public async ValueTask<TResponse> Create(TRequest request)
        {
            var oContract = request as CourseOperationContract;
            if (oContract == null)
                throw new ResultException("cannot send null course ", ((int)HttpStatusCode.BadRequest));
            if (oContract.UserId == Guid.Empty)
                throw new ResultException("you must be logo in or you unauthorize", ((int)HttpStatusCode.Unauthorized));
            var specuser = new ReadByIdSpecefication(oContract.UserId);
            var user = await _userrepo.FirstOrDefaultAsync(specuser);
            if(user==null)
                throw new ResultException("cannot founded user", ((int)HttpStatusCode.NotFound));

            var oData = new Core.Entites.Courses.Course(oContract.UserId, oContract.Title, oContract.CourseType, oContract.Price, oContract.GradeLevel, oContract.Description, oContract.ThumbnailUrl);
             oData.SetTeacher(user);
            var Course = await _Courserepo.AddAsync(oData);
            var oResult = new CourseContract()
            {
                _CourseId = Course._CourseId,
                CourseType = oContract.CourseType,
                Description = oContract.Description,
                GradeLevel = oContract.GradeLevel,
                IsPublished = oContract.IsPublished,
                Price = oContract.Price,
                ThumbnailUrl = oContract.ThumbnailUrl,
                Title = oContract.Title,

            };
            return oResult as dynamic;

        }

        public async ValueTask Delete(Guid id)
        {
            var spec = new ReadByIdSpecification(id);
            var Course = await _Courserepo.FirstOrDefaultAsync(spec);
            if(Course==null)
                throw new ResultException("cannot found course ", ((int)HttpStatusCode.NotFound));
            await _Courserepo.DeleteAsync(Course);

        }


        public async ValueTask<List<TResponse>> Read()
        {
            var spec = new ReadSpecification();
            var Coursers = await _Courserepo.ListAsync(spec);
            var oResult = Coursers.Select(c => new CourseContract()
            {
                _CourseId = c._CourseId,
                CourseType = c.CourseType,
                Description = c.Description,
                GradeLevel = c.GradeLevel,
                IsPublished = c.IsPublished,
                Price = c.Price,
                ThumbnailUrl = c.ThumbnailUrl,
                Title = c.Title,
            }).ToList();
            return oResult as dynamic;

        }

        public async ValueTask<TResponse> ReadById(Guid id)
        {
            var spec = new ReadByIdSpecification(id);
            var Course = await _Courserepo.FirstOrDefaultAsync(spec);
            if (Course == null)
                throw new ResultException("cannot found course ", ((int)HttpStatusCode.NotFound));
            var oResult = new CourseContract()
            {
                _CourseId = Course._CourseId,
                CourseType = Course.CourseType,
                Description = Course.Description,
                GradeLevel = Course.GradeLevel,
                IsPublished = Course.IsPublished,
                Price = Course.Price,
                ThumbnailUrl = Course.ThumbnailUrl,
                Title = Course.Title,
            };
            return oResult as dynamic;

        }

        public ValueTask<TResponse> ReadSingleByParentId(Guid id, Guid ParentId)
        {
            throw new NotImplementedException();
        }

        public async ValueTask<TResponse> Update(TRequest request, Guid id)
        {
            var oContract = request as CourseOperationContract;
            if (oContract == null)
                throw new ResultException("cannot send null course ", ((int)HttpStatusCode.BadRequest));
            var spec = new ReadByIdSpecification(id);
            var Course = await _Courserepo.FirstOrDefaultAsync(spec);
            if (Course == null)
                throw new ResultException("cannot found course ", ((int)HttpStatusCode.NotFound));
           Course.UpdateCourse(oContract.Title, oContract.Description, oContract.Price,oContract.IsPublished, Guid.Empty);
            await _Courserepo.UpdateAsync(Course);
            var oResult = new CourseContract()
            {
                _CourseId = Course._CourseId,
                CourseType = oContract.CourseType,
                Description = oContract.Description,
                GradeLevel = oContract.GradeLevel,
                IsPublished = oContract.IsPublished,
                Price = oContract.Price,
                ThumbnailUrl = oContract.ThumbnailUrl,
                Title = oContract.Title,
            };
            return oResult as dynamic;

        }

        public ValueTask UpdateParentById(Guid ParentId, Guid contractid)
        {
            throw new NotImplementedException();
        }
    }
}
