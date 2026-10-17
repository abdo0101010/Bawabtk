using Ardalis.Specification;
using System;
using System.Collections.Generic;
using System.Text;

namespace CleanArchitecture.Core.Entites.Users.Specefication
{
    public class ReadByPhoneSpecification:SingleResultSpecification<User>
    {
        public ReadByPhoneSpecification(string PhoneNumber)
        {
            Query.Where(s => s.PhoneNumber == PhoneNumber);
            
        }
    }
}
