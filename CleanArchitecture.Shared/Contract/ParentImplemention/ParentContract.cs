using System;
using System.Collections.Generic;
using System.Text;

namespace CleanArchitecture.Shared.Contract.ParentImplemention
{
    public class ParentContract
    {
        public string FullName { get; set; } = string.Empty;
        public Guid ParentProfileGuid { get; set; }
        public string WhatsAppNumber { get; set; } = string.Empty;
        public string? JobTitle { get; set; }
        public string? Address { get; set; }
    }
}
