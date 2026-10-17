namespace Bawabtk
{
    public class Route
    {
        public static class StudentRoutes
        {
            public const string BaseUrl = "/Student";
            public const string Delete = BaseUrl + "/{StudentId}";
            public const string ReadById = BaseUrl + "/{StudentId}";
            public const string Update = BaseUrl + "/{StudentId}";
            public const string Create = BaseUrl + "/Create";
        }
    }
}
