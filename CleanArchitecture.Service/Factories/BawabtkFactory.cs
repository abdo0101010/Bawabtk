using CleanArchitecture.Core.Entites.CourseLessons;
using CleanArchitecture.Core.Entites.CourseModules;
using CleanArchitecture.Core.Entites.Courses;
using CleanArchitecture.Core.Entites.ParentsProfile;
using CleanArchitecture.Core.Entites.Student;
using CleanArchitecture.Core.Entites.Users;
using CleanArchitecture.Core.Interfaces;
using CleanArchitecture.Service.Services.Course;
using CleanArchitecture.Service.Services.CourseLesson;
using CleanArchitecture.Service.Services.CourseModule;
using CleanArchitecture.Service.Services.Parent;
using CleanArchitecture.Service.Services.Student;
using huzcodes.Persistence.Interfaces.Repositories;
namespace CleanArchitecture.Service.Factories
{
    public class BawabtkFactory<TRequest, TResponse>(IRepository<User> userrepository,IRepository<ParentProfile>parentrepository,IRepository<StudentProfile> stdrepository, IRepository<Course> Courserepo,IRepository<CourseModule> CourseModulerepo, IRepository<CourseLesson> CourseLessonrepo)
    {
        private readonly IRepository<Course> _Courserepo = Courserepo;
        private readonly IRepository<CourseLesson> _CourseLessonrepo = CourseLessonrepo;
        private readonly IRepository<User> _userrepository = userrepository;
        private readonly IRepository<ParentProfile> _parentrepository = parentrepository;
        private readonly IRepository<StudentProfile> _stdrepository = stdrepository;
        private readonly IRepository<CourseModule> _CourseModulerepo = CourseModulerepo;
        public IGenericServices<TRequest, TResponse> Build(string key)
        {
            switch (key)
            {
                case (nameof(StudentProfile)):
                    return new StudentService<TRequest, TResponse>(_userrepository, _stdrepository, _parentrepository);
                case (nameof(ParentProfile)):
                    return new ParentService<TRequest, TResponse>(_userrepository, _stdrepository, _parentrepository);
                case (nameof(Course)):
                    return new CourseService<TRequest, TResponse>(_Courserepo, _userrepository);
                case (nameof(CourseModule)):
                    return new CourseModuleService<TRequest, TResponse>(_CourseModulerepo, _Courserepo);
                case (nameof(CourseLesson)):
                    return new CourseLessonService<TRequest, TResponse>(_CourseLessonrepo, _CourseModulerepo);
                default:
                    throw new ArgumentException($"Invalid service key: {key}");
            }
        }

    }
}
