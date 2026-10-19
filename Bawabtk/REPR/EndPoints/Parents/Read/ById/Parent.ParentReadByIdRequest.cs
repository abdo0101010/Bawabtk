using Microsoft.AspNetCore.Mvc;

namespace Bawabtk.REPR.EndPoints.Parents.Read.ById
{
    public class ParentRequest
    {
        [FromRoute]
        public Guid ParentId { get; set; }
    }
}
