using Ardalis.Specification;
using System;
using System.Collections.Generic;
using System.Text;

namespace CleanArchitecture.Core.Entites.Student.Specefication
{
    public class ReadSingleByParentId:SingleResultSpecification<StudentProfile>
    {
        public ReadSingleByParentId(Guid parentid,Guid contractid)
        {
            Query.Where(s => s._StudentProfileId == contractid && s.Parent._ParentProfileId == parentid).Include(p => p.Parent).Include(u=>u.User);
        }
    }
}
