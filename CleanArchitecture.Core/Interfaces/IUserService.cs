using System;
using System.Collections.Generic;
using System.Text;

namespace CleanArchitecture.Core.Interfaces
{
    public interface IUserService<TRequest, TResponse>
    {
        public ValueTask<TResponse> Auth(TRequest Request);

       
    }
}
