using Ardalis.Specification;
using System;
using System.Collections.Generic;
using System.Text;

namespace CleanArchitecture.Core.Entites.ParentsProfile.Specefication
{
    public class ReadByIdSpecefication: SingleResultSpecification<ParentProfile>
    {
        public ReadByIdSpecefication(Guid id)
        {
            Query.Where(p => p._ParentProfileId == id).Include(u=>u.User);
            
        }
    }
}
