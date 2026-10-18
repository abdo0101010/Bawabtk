using Microsoft.AspNetCore.Mvc;

namespace Bawabtk.REPR.EndPoints.Students.Update
{
    public class StudentRequest
    {
        [FromRoute]
        public Guid StudentId { get; set; }
        public string StudentFullName { get; set; } = string.Empty;
        public string StudentPhoneNumber { get; set; } = string.Empty;
        public string? StudentEmail { get; set; }
        public string Password { get; set; } = string.Empty;
        public string GradeLevel { get; set; } = string.Empty;
        public string? SchoolName { get; set; }
        public string ParentWhatsAppNumber { get; set; } = string.Empty; 

    }
}
