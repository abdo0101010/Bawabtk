using Ardalis.Specification;
using System;
using System.Collections.Generic;
using System.Text;
using static CleanArchitecture.Core.Entites.Enums;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace CleanArchitecture.Core.Entites.Enrollment.Specefication
{
    public class ReadAbsentStudentsSpecification : Specification<Enrollment>
    {
        public ReadAbsentStudentsSpecification(int courseId, List<int> attendedStudentIds)
        {
            Query.Where(e => e.CourseId == courseId &&
                             e.Status == EnrollmentStatus.Active &&
                             !attendedStudentIds.Contains(e.StudentId));
        }
    
    }
}
