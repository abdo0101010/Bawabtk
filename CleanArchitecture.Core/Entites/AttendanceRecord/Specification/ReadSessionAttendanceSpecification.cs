using Ardalis.Specification;
using System;
using System.Collections.Generic;
using System.Text;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace CleanArchitecture.Core.Entites.AttendanceRecord.Specification
{
    public class ReadSessionAttendanceSpecification : Specification<AttendanceRecord>
    {
        public ReadSessionAttendanceSpecification(int sessionId)
        {
            Query.Where(a => a.SessionId == sessionId);
        }
    
    }
}
