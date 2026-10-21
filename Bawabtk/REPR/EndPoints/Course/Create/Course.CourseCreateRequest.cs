using static CleanArchitecture.Core.Entites.Enums;

namespace Bawabtk.REPR.EndPoints.Course.Create
{
    public class CourseRequest
    {
        public Guid UserId { get; set; }
        public string Title { get; set; } = string.Empty;
        public string? Description { get; set; }
        public string? ThumbnailUrl { get; set; }
        public CourseType CourseType { get; set; }
        public decimal Price { get; set; }
        public string GradeLevel { get; set; } = string.Empty;
        public bool IsPublished { get; set; }
    }
}
