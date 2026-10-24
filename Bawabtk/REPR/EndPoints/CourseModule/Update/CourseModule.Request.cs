using Microsoft.AspNetCore.Mvc;

namespace Bawabtk.REPR.EndPoints.CourseModule.Update
{
    public class CourseModuleRequest
    {
        [FromRoute]
        public Guid CourseModuleId { get; set; }
        public Guid CourseId { get; set; }
        public string Title { get; set; } = string.Empty;
        public int SortOrder { get; set; }
    }
}
