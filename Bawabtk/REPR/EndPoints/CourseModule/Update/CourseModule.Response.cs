namespace Bawabtk.REPR.EndPoints.CourseModule.Update
{
    public class CourseModuleResponse
    {
        public Guid _ModuleId { get; set; }
        public Guid CourseId { get; set; }
        public string Title { get; set; } = string.Empty;
        public int SortOrder { get; set; }
    }
}
