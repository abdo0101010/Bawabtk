using Ardalis.Specification;

namespace CleanArchitecture.Core.Entites.OfflineSession.Specefication
{
    public class ReadByIdSpecification:SingleResultSpecification<OfflineSession>
    {
        public ReadByIdSpecification(Guid offlinesessionid)
        {
            Query.Where(o => o._SessionId == offlinesessionid)
                .Include(c => c.Course)
                .Include(s => s.AttendanceRecords); ;
            
        }
    }
}
