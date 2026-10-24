namespace Bawabtk.REPR.EndPoints.CourseModule.Create
{
    public class CourseModuleRequest
    {
        public Guid CourseId { get; set; }
        public string Title { get; set; } = string.Empty;
        public int SortOrder { get; set; }
    }
}
