using CleanArchitecture.Core.Entites.CourseModules.Specification;
using CleanArchitecture.Core.Interfaces;
using CleanArchitecture.Shared.Contract.CourseModuleImplemention;
using huzcodes.Extensions.Exceptions;
using huzcodes.Persistence.Interfaces.Repositories;
using System.Net;

namespace CleanArchitecture.Service.Services.CourseModule
{
    public class CourseModuleService<TRequest, TResponse>(IRepository<Core.Entites.CourseModules.CourseModule> CourseModuleRepo, IRepository<Core.Entites.Courses.Course> CourseRepo) : IGenericServices<TRequest, TResponse>
    {
        private readonly IRepository<Core.Entites.CourseModules.CourseModule> _CourseModuleRepo = CourseModuleRepo;
        private readonly IRepository<Core.Entites.Courses.Course> _CourseRepo = CourseRepo;
        public async ValueTask<TResponse> Create(TRequest request)
        {
            var oContract = request as CourseModuleOPerationContract;
            if (oContract == null)
                throw new ResultException("Cannot send null Request", ((int)HttpStatusCode.BadRequest));
            var CourseSpec = new Core.Entites.Courses.Specification.ReadByIdSpecification(oContract.CourseId);
            var Course = await _CourseRepo.FirstOrDefaultAsync(CourseSpec);
            if (Course == null)
                throw new ResultException("Cannot Create course moudle becuase not found course", ((int)HttpStatusCode.BadRequest));
            var oData = new Core.Entites.CourseModules.CourseModule(oContract.Title, oContract.SortOrder, Guid.Empty);
            oData.SetCourse(Course);
            var oResult = await _CourseModuleRepo.AddAsync(oData);
            return new CourseModuleContract()
            {
                _ModuleId = oResult._ModuleId,
                CourseId = oContract.CourseId,
                SortOrder = oResult.SortOrder,
                Title = oResult.Title

            } as dynamic;

        }

        public async ValueTask Delete(Guid id)
        {
            if (id == Guid.Empty)
                throw new ResultException("Cannot send null id", ((int)HttpStatusCode.BadRequest));

            var spec = new ReadByIdSpecification(id);
            var CourseModule = await _CourseModuleRepo.FirstOrDefaultAsync(spec);
            if (CourseModule == null)
                throw new ResultException("Cannot Delete course moudle becuase not found", ((int)HttpStatusCode.NotFound));
            await _CourseModuleRepo.DeleteAsync(CourseModule);

        }

        public async ValueTask<List<TResponse>> Read()
        {
            var Spec = new ReadSpecification();
            var CourseModules = await _CourseModuleRepo.ListAsync(Spec);
            if (!CourseModules.Any())
                throw new ResultException("no course moudle to view", ((int)HttpStatusCode.NoContent));
            var oResult = CourseModules.Select(c => new CourseModuleContract()
            {
                _ModuleId = c._ModuleId,
                CourseId = c.Course._CourseId,
                SortOrder = c.SortOrder,
                Title = c.Title

            }).ToList();
            return oResult as dynamic;
        }

        public async ValueTask<TResponse> ReadById(Guid id)
        {
            if (id == Guid.Empty)
                throw new ResultException("Cannot send null id", ((int)HttpStatusCode.BadRequest));

            var spec = new ReadByIdSpecification(id);
            var oData = await _CourseModuleRepo.FirstOrDefaultAsync(spec);
            if (oData == null)
                throw new ResultException("Cannot Found course moudle becuase not found", ((int)HttpStatusCode.NotFound));
            var oResult = new CourseModuleContract()
            {
                _ModuleId = oData._ModuleId,
                CourseId = id,
                SortOrder = oData.SortOrder,
                Title = oData.Title

            };
            return oResult as dynamic;

        }

        public async ValueTask<TResponse> ReadSingleByParentId(Guid id, Guid ParentId)
        {
            if (id == Guid.Empty||ParentId==Guid.Empty)
                throw new ResultException("Cannot send null id", ((int)HttpStatusCode.BadRequest));
            var CourseModuleSpec = new ReadByIdSpecification(id);
            var oDataCourseModule = await _CourseModuleRepo.FirstOrDefaultAsync(CourseModuleSpec);
            if (oDataCourseModule == null)
                throw new ResultException("Cannot Found course moudle becuase not found", ((int)HttpStatusCode.NotFound));
            var CourseSpec = new Core.Entites.Courses.Specification.ReadByIdSpecification(ParentId);
            var oDataCourse = await _CourseRepo.FirstOrDefaultAsync(CourseSpec);
            if (oDataCourse == null)
                throw new ResultException("Cannot Found course becuase not found", ((int)HttpStatusCode.NotFound));
            var CourseModule = new ReadSingleByCourseId(ParentId, id);
            var Course = await _CourseModuleRepo.FirstOrDefaultAsync(CourseModule);
            if(Course==null)
                throw new ResultException("this course moudle not relation to this course ", ((int)HttpStatusCode.NotFound));
            var oResult = new CourseModuleContract()
            {
                _ModuleId = Course._ModuleId,
                CourseId = id,
                SortOrder = Course.SortOrder,
                Title = Course.Title
            };
            return oResult as dynamic;
        }

        public async ValueTask<TResponse> Update(TRequest request, Guid id)
        {

            var oContract = request as CourseModuleOPerationContract;
            if (oContract == null)
                throw new ResultException("Cannot send null Request", ((int)HttpStatusCode.BadRequest));
            if (id == Guid.Empty)
                throw new ResultException("Cannot send null id", ((int)HttpStatusCode.BadRequest));

            var spec = new ReadByIdSpecification(id);
            var oData = await _CourseModuleRepo.FirstOrDefaultAsync(spec);
            if (oData == null)
                throw new ResultException("Cannot Found course moudle becuase not found", ((int)HttpStatusCode.NotFound));
            oData.Update(oContract.Title);
            await _CourseModuleRepo.UpdateAsync(oData);
            var oResult = new CourseModuleContract()
            {
                _ModuleId = oData._ModuleId,
                CourseId = id,
                SortOrder = oData.SortOrder,
                Title = oData.Title

            };
            return oResult as dynamic;
        }

        public async ValueTask UpdateParentById(Guid ParentId, Guid contractid)
        {
            if (contractid == Guid.Empty || ParentId == Guid.Empty)
                throw new ResultException("Cannot send null id", ((int)HttpStatusCode.BadRequest));
            var CourseModuleSpec = new ReadByIdSpecification(contractid);
            var oDataCourseModule = await _CourseModuleRepo.FirstOrDefaultAsync(CourseModuleSpec);
            if (oDataCourseModule == null)
                throw new ResultException("Cannot Found course moudle becuase not found", ((int)HttpStatusCode.NotFound));
            var CourseSpec = new Core.Entites.Courses.Specification.ReadByIdSpecification(ParentId);
            var oDataCourse = await _CourseRepo.FirstOrDefaultAsync(CourseSpec);
            if (oDataCourse == null)
                throw new ResultException("Cannot Found course becuase not found", ((int)HttpStatusCode.NotFound));
             oDataCourseModule.SetCourse(oDataCourse);
            await _CourseModuleRepo.UpdateAsync(oDataCourseModule);

        }
    }
}
