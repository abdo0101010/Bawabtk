namespace CleanArchitecture.Shared.Contract.CourseModuleImplemention
{
    public class CourseModuleOPerationContract
    {
        public Guid CourseId { get; set; }
        public string Title { get; set; } = string.Empty;
        public int SortOrder { get; set; }
    }
}
