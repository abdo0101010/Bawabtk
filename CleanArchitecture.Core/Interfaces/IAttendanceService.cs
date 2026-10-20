namespace CleanArchitecture.Core.Interfaces
{
    public interface IAttendanceService<TRequest,TResponse>
    {
        public ValueTask<TResponse> ScanBarcodeAsync(TRequest request);
        public ValueTask<TResponse> CloseSessionAndMarkAbsentAsync(Guid sessionId, Guid closedByUserId);
    }
}
