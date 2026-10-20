using CleanArchitecture.Shared.Contract.AttendanceImplemention;

namespace Bawabtk.REPR.EndPoints.User.CloseSession
{
    public class UserResponse
    {
        public AttendanceContract AbsentStudentsCount { get; set; }
        public string Message { get; set; } = string.Empty;

        public UserResponse(AttendanceContract absentCount)
        {
            AbsentStudentsCount = absentCount;
            Message = $"تم إغلاق الجلسة بنجاح، وتم تسجيل غياب لـ {absentCount} طالب.";
        }
    }
}
