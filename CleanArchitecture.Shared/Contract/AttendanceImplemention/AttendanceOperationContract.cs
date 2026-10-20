namespace CleanArchitecture.Shared.Contract.AttendanceImplemention
{
    public class AttendanceOperationContract
    {
        public Guid SessionId { get; set; }
        public string UnifiedBarcode { get; set; } = string.Empty;
        public string Notes { get; set; } = string.Empty;
        public Guid AssistantUserId { get; set; }
    }
}
