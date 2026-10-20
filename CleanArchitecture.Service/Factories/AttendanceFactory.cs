using CleanArchitecture.Core.Entites.AttendanceRecord;
using CleanArchitecture.Core.Entites.Enrollment;
using CleanArchitecture.Core.Entites.OfflineSession;
using CleanArchitecture.Core.Entites.Student;
using CleanArchitecture.Core.Entites.Users;
using CleanArchitecture.Core.Interfaces;
using CleanArchitecture.Service.Services.Attendance;
using huzcodes.Persistence.Interfaces.Repositories;
namespace CleanArchitecture.Service.Factories
{
    public class AttendanceFactory<TRequest, TResponse>(IRepository<OfflineSession> sessionRepo,
        IRepository<StudentProfile> studentRepo,
        IRepository<AttendanceRecord> attendanceRepo,
        IRepository<Enrollment> enrollmentRepo,
        IRepository<User> userRepo)
    {
        private readonly IRepository<OfflineSession> _sessionRepo = sessionRepo;
        private readonly IRepository<StudentProfile> _studentRepo = studentRepo;
        private readonly IRepository<AttendanceRecord> _attendanceRepo = attendanceRepo;
        private readonly IRepository<Enrollment> _enrollmentRepo = enrollmentRepo;
        private readonly IRepository<User> _userRepo = userRepo; 
        public IAttendanceService<TRequest, TResponse> Build()
        {
            return new AttendanceService<TRequest, TResponse>(
                _sessionRepo,
                _studentRepo,
                _attendanceRepo,
                _enrollmentRepo,
                _userRepo);
        }
    }
}
