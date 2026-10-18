using Ardalis.Specification;
using System;
using System.Collections.Generic;
using System.Text;

namespace CleanArchitecture.Core.Entites.Student.Specefication
{
    public class ReadSpecefication:Specification<StudentProfile>
    {
        public ReadSpecefication()
        {
            Query.Where(s => s._StudentProfileId != Guid.Empty).Include(s => s.User).Include(s => s.Parent);

        }
    }
}
