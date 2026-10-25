using CleanArchitecture.Core.Entites.CourseLessons.Specification;
using CleanArchitecture.Core.Entites.CourseModules.Specification;
using CleanArchitecture.Core.Interfaces;
using CleanArchitecture.Shared.Contract.CourseLessonImplemention;
using huzcodes.Extensions.Exceptions;
using huzcodes.Persistence.Interfaces.Repositories;
using System.Net;
namespace CleanArchitecture.Service.Services.CourseLesson
{
    public class CourseLessonService<TRequest, TResponse>(IRepository<Core.Entites.CourseLessons.CourseLesson> Courselessonrepo, IRepository<Core.Entites.CourseModules.CourseModule> CourseModulerepo) :IGenericServices<TRequest,TResponse>
    {
        private readonly IRepository<Core.Entites.CourseLessons.CourseLesson> _Courselessonrepo = Courselessonrepo;
        private readonly IRepository<Core.Entites.CourseModules.CourseModule> _CourseModulerepo = CourseModulerepo;

        public async ValueTask<TResponse> Create(TRequest request)
        {
            var oContrct = request as CourseLessonOperationContract;
            if (oContrct == null)
                throw new ResultException("Cannot send null Request", ((int)HttpStatusCode.BadRequest));
            var SpecModule = new Core.Entites.CourseModules.Specification.ReadByIdSpecification(oContrct.ModuleId);
            var Module = await _CourseModulerepo.FirstOrDefaultAsync(SpecModule);
            if (Module == null)
                throw new ResultException("Cannot Create course moudle becuase not found Module", ((int)HttpStatusCode.BadRequest));
            var oData = new Core.Entites.CourseLessons.CourseLesson(oContrct.Title,oContrct.VideoUrl,oContrct.PdfUrl,oContrct.DurationInMinutes,oContrct.SortOrder,oContrct.IsFreePreview,Guid.Empty);
            oData.SetModule(Module);
            var oResult = new CourseLessonContract()
            {
                DurationInMinutes=oData.DurationInMinutes,
                IsFreePreview=oData.IsFreePreview,
                ModuleId=Module._ModuleId,
                PdfUrl=oData.PdfUrl,
                SortOrder=oData.SortOrder,
                Title=oData.Title,
                VideoUrl=oData.VideoUrl,
                _LessonId=oData._LessonId
            };
            return oResult as dynamic;
        }

        public async ValueTask Delete(Guid id)
        {
            if (id == Guid.Empty)
                throw new ResultException("Cannot send null id", ((int)HttpStatusCode.BadRequest));

            var spec = new Core.Entites.CourseLessons.Specification. ReadByIdSpecification(id);
            var CourseModule = await _Courselessonrepo.FirstOrDefaultAsync(spec);
            if (CourseModule == null)
                throw new ResultException("Cannot Delete course moudle becuase not found", ((int)HttpStatusCode.NotFound));
            await _Courselessonrepo.DeleteAsync(CourseModule);
        }

        public async ValueTask<List<TResponse>> Read()
        {
            var Spec = new Core.Entites.CourseLessons.Specification.ReadSpecification();
            var CourseLesson = await _Courselessonrepo.ListAsync(Spec);
            if (!CourseLesson.Any())
                throw new ResultException("no course Lesson to view", ((int)HttpStatusCode.NoContent));
            var oResult = CourseLesson.Select(c => new CourseLessonContract()
            {
                DurationInMinutes = c.DurationInMinutes,
                IsFreePreview = c.IsFreePreview,
                ModuleId = c.Module._ModuleId,
                PdfUrl = c.PdfUrl,
                SortOrder = c.SortOrder,
                Title = c.Title,
                VideoUrl = c.VideoUrl,
                _LessonId = c._LessonId
            }).ToList;
            return oResult as dynamic;
        }

        public async ValueTask<TResponse> ReadById(Guid id)
        {
            if (id == Guid.Empty)
                throw new ResultException("Cannot send null id", ((int)HttpStatusCode.BadRequest));

            var spec = new Core.Entites.CourseLessons.Specification.ReadByIdSpecification(id);
            var oData = await _Courselessonrepo.FirstOrDefaultAsync(spec);
            if (oData == null)
                throw new ResultException("Cannot Found course Lesson becuase not found", ((int)HttpStatusCode.NotFound));

            var oResult = new CourseLessonContract()
            {
                DurationInMinutes = oData.DurationInMinutes,
                IsFreePreview = oData.IsFreePreview,
                ModuleId = oData.Module._ModuleId,
                PdfUrl = oData.PdfUrl,
                SortOrder = oData.SortOrder,
                Title = oData.Title,
                VideoUrl = oData.VideoUrl,
                _LessonId = oData._LessonId
            };
            return oResult as dynamic;
        }

        public async ValueTask<TResponse> ReadSingleByParentId(Guid id, Guid ParentId)
        {
            if (id == Guid.Empty || ParentId == Guid.Empty)
                throw new ResultException("Cannot send null id", ((int)HttpStatusCode.BadRequest));
            var CourseModuleSpec = new Core.Entites.CourseModules.Specification. ReadByIdSpecification(id);
            var oDataCourseModule = await _CourseModulerepo.FirstOrDefaultAsync(CourseModuleSpec);
            if (oDataCourseModule == null)
                throw new ResultException("Cannot Found course moudle becuase not found", ((int)HttpStatusCode.NotFound));
            var LessonSpec = new Core.Entites.CourseLessons.Specification.ReadByIdSpecification(ParentId);
            var oDataCourse = await _Courselessonrepo.FirstOrDefaultAsync(LessonSpec);
            if (oDataCourse == null)
                throw new ResultException("Cannot Found Leesson becuase not found", ((int)HttpStatusCode.NotFound));
            var CourseLessonSpec = new ReadSingleByCourseModuleIdSpceification (ParentId, id);
            var CourseLesson = await _Courselessonrepo.FirstOrDefaultAsync(CourseLessonSpec);
            if (CourseLesson == null)
                throw new ResultException("this course Lesson not relation to this course Module ", ((int)HttpStatusCode.NotFound));
            var oResult = new CourseLessonContract()
            {
                DurationInMinutes = CourseLesson.DurationInMinutes,
                IsFreePreview = CourseLesson.IsFreePreview,
                ModuleId = CourseLesson.Module._ModuleId,
                PdfUrl = CourseLesson.PdfUrl,
                SortOrder = CourseLesson.SortOrder,
                Title = CourseLesson.Title,
                VideoUrl = CourseLesson.VideoUrl,
                _LessonId = CourseLesson._LessonId
            };
            return oResult as dynamic;
        }

        public async ValueTask<TResponse> Update(TRequest request, Guid id)
        {
            var oContract = request as CourseLessonOperationContract ;
            if (oContract == null)
                throw new ResultException("Cannot send null Request", ((int)HttpStatusCode.BadRequest));
            if (id == Guid.Empty)
                throw new ResultException("Cannot send null id", ((int)HttpStatusCode.BadRequest));

            var spec = new Core.Entites.CourseLessons.Specification.ReadByIdSpecification(id);
            var oData = await _Courselessonrepo.FirstOrDefaultAsync(spec);
            if (oData == null)
                throw new ResultException("Cannot Found course Lesson becuase not found", ((int)HttpStatusCode.NotFound));
            oData.Update(oContract.Title, oContract.VideoUrl, oContract.PdfUrl, oContract.DurationInMinutes, oContract.DurationInMinutes, oContract.IsFreePreview);
           await _Courselessonrepo.UpdateAsync(oData);
            var oResult = new CourseLessonContract()
            {
                DurationInMinutes=oContract.DurationInMinutes,
                IsFreePreview=oContract.IsFreePreview,
                ModuleId=oContract.ModuleId,
                PdfUrl=oContract.PdfUrl,
                SortOrder=oContract.SortOrder,
                Title=oContract.Title,
                VideoUrl=oContract.VideoUrl,
                _LessonId=id
            };
            return oResult as dynamic;
        }

        public  async ValueTask UpdateParentById(Guid ParentId, Guid contractid)
        {
            if (contractid == Guid.Empty || ParentId == Guid.Empty)
                throw new ResultException("Cannot send null id", ((int)HttpStatusCode.BadRequest));
            var CourseModuleSpec = new Core.Entites.CourseModules.Specification. ReadByIdSpecification(contractid);
            var oDataCourseModule = await _CourseModulerepo.FirstOrDefaultAsync(CourseModuleSpec);
            if (oDataCourseModule == null)
                throw new ResultException("Cannot Found course moudle becuase not found", ((int)HttpStatusCode.NotFound));
            var LessonSpec = new Core.Entites.CourseLessons.Specification.ReadByIdSpecification(ParentId);
            var Lesson = await _Courselessonrepo.FirstOrDefaultAsync(LessonSpec);
            if (Lesson == null)
                throw new ResultException("Cannot Found lesson becuase not found", ((int)HttpStatusCode.NotFound));
            Lesson.SetModule(oDataCourseModule);
            await _Courselessonrepo.UpdateAsync(Lesson);
        }
    }
}
