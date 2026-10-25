namespace Bawabtk
{
    public class Route
    {
        public static class ParentRoutes {
            public const string BaseUrl = "/Parent";
            public const string ReadById = BaseUrl + "/{ParentId}";
            public const string Create = BaseUrl + "/Create";
            public const string Delete = BaseUrl + "/{ParentId}";
            public const string Update = BaseUrl + "/{ParentId}";


        }

        public static class StudentRoutes
        {
            public const string BaseUrl = "/Student";
            public const string Delete = BaseUrl + "/{StudentId}";
            public const string ReadById = BaseUrl + "/{StudentId}";
            public const string Update = BaseUrl + "/{StudentId}";
            public const string Create = BaseUrl + "/Create";
            public const string UpdateStudentParentId = ParentRoutes.BaseUrl + ReadById;
            public const string ReadStudentByParentId = ParentRoutes.ReadById + "/{StudentId}";
        }
        public static class userRoutes
        {
            public const string BaseUrl = "/User";
            public const string Login = BaseUrl+"/Login";
        }
        public static class AttendanceRoutes
        {
            public const string BaseUrl = "/Attendance";
            public const string ScanBarcode = BaseUrl + "/Scan";
            public const string CloseSession = BaseUrl + "/Sessions/{SessionId}/Close";
        }
        public static class CourseRoutes
        {
            public const string BaseUrl = "/Course";
            public const string Delete = BaseUrl + "/{CourseId}";
            public const string ReadById = BaseUrl + "/{CourseId}";
            public const string Update = BaseUrl + "/{CourseId}";
            public const string Create = BaseUrl + "/Create";
        }
        public static class CourseModuleRoutes
        {
            public const string BaseUrl = "/CourseModule";
            public const string Delete = BaseUrl + "/{CourseModuleId}";
            public const string ReadById = BaseUrl + "/{CourseModuleId}";
            public const string Update = BaseUrl + "/{CourseModuleId}";
            public const string Create = BaseUrl + "/Create";
            public const string UpdateCourseId = CourseRoutes.BaseUrl + ReadById;
            public const string ReadSingleCoursemoduleByCourseId = CourseRoutes.ReadById + "/{CourseModuleId}";
        }
        public static class CourseLessoneRoutes
        {
            public const string BaseUrl = "/CourseLesson";
            public const string Delete = BaseUrl + "/{CourseLessonId}";
            public const string ReadById = BaseUrl + "/{CourseLessonId}";
            public const string Update = BaseUrl + "/{CourseLessonId}";
            public const string Create = BaseUrl + "/Create";
            public const string UpdateCourseModuleId = CourseModuleRoutes.ReadById + ReadById;
            public const string ReadSingleCourseLessonByCoursemoduleId = CourseModuleRoutes.ReadById + "/{CourseLessonId}";
        }

    }
}
