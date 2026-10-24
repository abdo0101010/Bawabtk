namespace Bawabtk.REPR.EndPoints.CourseModule.Read.ById
{
    public class CourseModuleResponse
    {
        public Guid ModuleId { get; set; }
        public Guid CourseId { get; set; }
        public string Title { get; set; } = string.Empty;
        public int SortOrder { get; set; }
    }
}
