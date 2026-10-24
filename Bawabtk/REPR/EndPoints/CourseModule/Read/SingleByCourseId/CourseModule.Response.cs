namespace Bawabtk.REPR.EndPoints.CourseModule.Read.SingleByCourseId
{
    public class CourseModuleResponse
    {
        public Guid _ModuleId { get; set; }
        public Guid CourseId { get; set; }
        public string Title { get; set; } = string.Empty;
        public int SortOrder { get; set; }
    }
}
