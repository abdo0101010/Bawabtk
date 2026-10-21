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


    }
}
