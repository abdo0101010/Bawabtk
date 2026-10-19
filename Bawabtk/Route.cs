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
    }
}
