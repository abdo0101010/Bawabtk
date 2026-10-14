
namespace CleanArchitecture.Core.Entites
{
    public class CourseLesson:BaseEntity
    {
        public Guid _LessonId { get; set; }
        public int ModuleId { get; set; } // FK -> CourseModule.Id
        public string Title { get; set; } = string.Empty;
        public string? VideoUrl { get; set; }
        public string? PdfUrl { get; set; }
        public int DurationInMinutes { get; set; }
        public int SortOrder { get; set; }
        public bool IsFreePreview { get; set; }

        // Relations
        public CourseModule Module { get; set; } = null!;

        public CourseLesson() { } // ef core ctor

        public CourseLesson(int moduleId,
                            string title,
                            string? videoUrl,
                            string? pdfUrl,
                            int durationInMinutes,
                            int sortOrder,
                            bool isFreePreview,
                            Guid createdBy)
        {
            _LessonId = Guid.NewGuid();
            ModuleId = moduleId;
            Title = title;
            VideoUrl = videoUrl;
            PdfUrl = pdfUrl;
            DurationInMinutes = durationInMinutes;
            SortOrder = sortOrder;
            IsFreePreview = isFreePreview;
            CreatedBy = createdBy;
            CreatedOn = DateTimeOffset.Now;
            ModifiedBy = Guid.Empty;
            ModifiedOn = DateTimeOffset.Now;
        }
    }
}
