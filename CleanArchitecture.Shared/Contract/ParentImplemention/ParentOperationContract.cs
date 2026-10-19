using System;
using System.Collections.Generic;
using System.Text;

namespace CleanArchitecture.Shared.Contract.ParentImplemention
{
    public class ParentOperationContract
    {
        public string FullName { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        public string? Email { get; set; }
        public string Password { get; set; } = string.Empty; public Guid ParentProfileId { get; set; }
        public string WhatsAppNumber { get; set; } = string.Empty;
        public string? JobTitle { get; set; }
        public string? Address { get; set; }
        public string? ProfilePictureUrl { get; set; }
        public string? ParentJobTitle { get; set; }

    }
}
