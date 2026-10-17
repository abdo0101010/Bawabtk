namespace Bawabtk.REPR.EndPoints.Students.Create
{
    public class StudentRequest
    {
        public string StudentFullName { get; set; } = string.Empty;
        public string StudentPhoneNumber { get; set; } = string.Empty;
        public string? StudentEmail { get; set; }
        public string Password { get; set; } = string.Empty;
        public string GradeLevel { get; set; } = string.Empty;
        public string? SchoolName { get; set; }
        public string? ProfilePictureUrl { get; set; }
        public string ParentFullName { get; set; } = string.Empty;
        public string ParentWhatsAppNumber { get; set; } = string.Empty;
        public string RelationType { get; set; } = "الأب";
        public string? ParentJobTitle { get; set; }
        public string? Address { get; set; }
    }
}
