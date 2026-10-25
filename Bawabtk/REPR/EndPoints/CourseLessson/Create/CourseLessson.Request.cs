namespace Bawabtk.REPR.EndPoints.CourseLessson.Create
{
    public class CourseLesssonRequest
    {
        public Guid ModuleId { get; set; }
        public string Title { get; set; } = string.Empty;
        public string? VideoUrl { get; set; }
        public string? PdfUrl { get; set; }
        public int DurationInMinutes { get; set; }
        public int SortOrder { get; set; }
        public bool IsFreePreview
        {
            get; set;
        }
    }
}
