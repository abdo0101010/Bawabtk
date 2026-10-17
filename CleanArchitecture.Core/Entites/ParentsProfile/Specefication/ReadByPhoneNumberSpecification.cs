using Ardalis.Specification;
using System;
using System.Collections.Generic;
using System.Text;

namespace CleanArchitecture.Core.Entites.ParentsProfile.Specefication
{
    public class ReadByPhoneNumberSpecification:SingleResultSpecification<ParentProfile>
    {
        public ReadByPhoneNumberSpecification(string PhoneNumber)
        {
            Query.Where(p => p.WhatsAppNumber == PhoneNumber);
        }
    }
}
