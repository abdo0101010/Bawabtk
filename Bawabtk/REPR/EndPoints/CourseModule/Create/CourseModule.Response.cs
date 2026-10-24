namespace Bawabtk.REPR.EndPoints.CourseModule.Create
{
    public class CourseModuleReaponse
    {
        public Guid ModuleId { get; set; }
        public Guid CourseId { get; set; }
        public string Title { get; set; } = string.Empty;
        public int SortOrder { get; set; }
    }
}
