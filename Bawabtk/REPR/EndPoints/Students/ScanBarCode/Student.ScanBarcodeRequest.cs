namespace Bawabtk.REPR.EndPoints.Students.ScanBarCode
{
    public class StudentRequest
    {
        public Guid SessionId { get; set; }
        public string UnifiedBarcode { get; set; } = string.Empty;
        public Guid AssistantUserId { get; set; }
        public string? Notes { get; set; }
    }
}
