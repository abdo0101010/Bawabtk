using Microsoft.AspNetCore.Mvc;

namespace Bawabtk.REPR.EndPoints.Parents.Delete
{
    public class ParentRequest
    {
        [FromRoute]
        public Guid ParentId { get; set; }
    }
}
