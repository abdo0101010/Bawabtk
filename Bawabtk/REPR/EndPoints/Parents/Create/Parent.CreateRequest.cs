namespace Bawabtk.REPR.EndPoints.Parents.Create
{
    public class ParentRequest
    {
        public string FullName { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        public string? Email { get; set; }
        public string Password { get; set; } = string.Empty; public Guid ParentProfileId { get; set; }
        public string WhatsAppNumber { get; set; } = string.Empty;
        public string? JobTitle { get; set; }
        public string? Address { get; set; }
        public string? ProfilePictureUrl { get; set; }
    }
}
