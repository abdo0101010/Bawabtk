using Microsoft.AspNetCore.Mvc;

namespace Bawabtk.REPR.EndPoints.User.CloseSession
{
    public class UserRequest
    {
        [FromRoute(Name = "SessionId")]
        public Guid SessionId { get; set; }

        [FromBody]
        public Guid ClosedByUserId { get; set; }
    }
}
