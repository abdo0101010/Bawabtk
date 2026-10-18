using Ardalis.Specification;
using System;
using System.Collections.Generic;
using System.Text;

namespace CleanArchitecture.Core.Entites.Student.Specefication
{
    public class ReadByIdSpecification:SingleResultSpecification<StudentProfile>
    {
        public ReadByIdSpecification(Guid id)
        {
            Query.Where(s => s._StudentProfileId == id).Include(u => u.User).Include(p => p.Parent);
        }
    }
}
