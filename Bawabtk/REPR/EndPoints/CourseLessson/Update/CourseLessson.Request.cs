using Microsoft.AspNetCore.Mvc;

namespace Bawabtk.REPR.EndPoints.CourseLessson.Update
{
    public class CourseLesssonRequest
    {
        [FromRoute]
        public Guid CourseLessonId { get; set; }
        public Guid ModuleId { get; set; }
        public string Title { get; set; } = string.Empty;
        public string? VideoUrl { get; set; }
        public string? PdfUrl { get; set; }
        public int DurationInMinutes { get; set; }
        public int SortOrder { get; set; }
        public bool IsFreePreview { get; set; }
    }
}
