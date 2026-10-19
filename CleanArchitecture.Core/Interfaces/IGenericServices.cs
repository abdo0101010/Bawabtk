namespace CleanArchitecture.Core.Interfaces
{
    public interface IGenericServices<TRequest,TResponse>
    {
        public ValueTask<TResponse> Create(TRequest request);
        public ValueTask<TResponse> Update(TRequest request,Guid id);
        public ValueTask Delete(Guid id);
        public ValueTask<List<TResponse>> Read();
        public ValueTask<TResponse> ReadById(Guid id);
        public ValueTask UpdateParentById(Guid ParentId,Guid contractid);
        public ValueTask<TResponse> ReadSingleByParentId(Guid id, Guid ParentId);

    }
}
