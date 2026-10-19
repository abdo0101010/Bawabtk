using Microsoft.AspNetCore.Mvc;

namespace Bawabtk.REPR.EndPoints.Parents.Update
{
    public class ParentRequest
    {
        [FromRoute]
        public Guid ParentId { get; set; }
        public string FullName { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        public string? Email { get; set; }
        public string Password { get; set; } = string.Empty; 
        public string WhatsAppNumber { get; set; } = string.Empty;
        public string? JobTitle { get; set; }
        public string? Address { get; set; }
        public string? ProfilePictureUrl { get; set; }
        public string? ParentJobTitle { get; set; }
    }
}
