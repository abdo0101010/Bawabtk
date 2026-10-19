namespace Bawabtk.REPR.EndPoints.Parents.Read
{
    public class ParentResponse
    {
        public string FullName { get; set; } = string.Empty;
        public Guid ParentProfileGuid { get; set; }
        public string WhatsAppNumber { get; set; } = string.Empty;
        public string? JobTitle { get; set; }
        public string? Address { get; set; }
    }
}
