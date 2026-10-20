using CleanArchitecture.Core.Entites.AttendanceRecord;
using CleanArchitecture.Core.Entites.AttendanceRecord.Specification;
using CleanArchitecture.Core.Entites.Enrollment;
using CleanArchitecture.Core.Entites.Enrollment.Specefication;
using CleanArchitecture.Core.Entites.OfflineSession;
using CleanArchitecture.Core.Entites.OfflineSession.Specefication;
using CleanArchitecture.Core.Entites.Student;
using CleanArchitecture.Core.Entites.Student.Specefication;
using CleanArchitecture.Core.Entites.Users.Specefication;
using CleanArchitecture.Core.Interfaces;
using CleanArchitecture.Shared.Contract.AttendanceImplemention;
using huzcodes.Extensions.Exceptions;
using huzcodes.Persistence.Interfaces.Repositories;
using System.Net;
using static CleanArchitecture.Core.Entites.Enums;

namespace CleanArchitecture.Service.Services.Attendance
{
    public class AttendanceService<TRequest, TResponse>(IRepository<OfflineSession> sessionRepo,
        IRepository<StudentProfile> studentRepo,
        IRepository<AttendanceRecord> attendanceRepo,
        IRepository<Enrollment> enrollmentRepo,
        IRepository<Core.Entites.Users.User> userRepo) : IAttendanceService<TRequest, TResponse>
    {
        private readonly IRepository<OfflineSession> _sessionRepo = sessionRepo;
        private readonly IRepository<StudentProfile> _studentRepo = studentRepo;
        private readonly IRepository<AttendanceRecord> _attendanceRepo = attendanceRepo;
        private readonly IRepository<Enrollment> _enrollmentRepo = enrollmentRepo;
        private readonly IRepository<Core.Entites.Users.User> _userRepo = userRepo;
        public async ValueTask<TResponse> CloseSessionAndMarkAbsentAsync(Guid sessionId, Guid closedByUserId)
        {
            var sessionSpec = new Core.Entites.OfflineSession.Specefication.ReadByIdSpecification(sessionId);
            var session = await _sessionRepo.FirstOrDefaultAsync(sessionSpec);

            if (session == null)
                throw new ResultException("the session is not found", (int)HttpStatusCode.NotFound);

            if (session.Status == SessionStatus.Closed)
                throw new ResultException("the sesssion is aleardy closed", (int)HttpStatusCode.BadRequest);

            var assistantSpec = new Core.Entites.Users.Specefication.ReadByIdSpecefication(closedByUserId);
            var closedByUser = await _userRepo.FirstOrDefaultAsync(assistantSpec);
            var sessionAttendanceSpec = new ReadSessionAttendanceSpecification(session.Id);
            var attendedRecords = await _attendanceRepo.ListAsync(sessionAttendanceSpec);
            var attendedStudentIds = attendedRecords.Select(a => a.StudentId).ToList();

            var absentSpec = new ReadAbsentStudentsSpecification(session.CourseId, attendedStudentIds);
            var absentEnrollments = await _enrollmentRepo.ListAsync(absentSpec);

            foreach (var enrollment in absentEnrollments)
            {
                var absentRecord = new AttendanceRecord(
                    sessionId: session.Id,
                    studentId: enrollment.StudentId, 
                    scannedByAssistantId: closedByUser?.Id,
                    status: AttendanceStatus.Absent,
                    createdBy: closedByUserId,
                    notes: "Automatic absence recording when the session is closed"
                );

                await _attendanceRepo.AddAsync(absentRecord);
            }

            session.CloseSession(closedByUser?.Id ?? 0, closedByUserId);
            await _sessionRepo.UpdateAsync(session);

            return new AttendanceContract
            {
                SessionTitle = session.Title,
                ScannedAt = DateTimeOffset.Now,
                Message = $"تم إغلاق الجلسة بنجاح، وتم تسجيل غياب لـ {absentEnrollments.Count} طالب."
            } as dynamic;
        }
        public async ValueTask<TResponse> ScanBarcodeAsync(TRequest request)
        {
            var oContract=request as AttendanceOperationContract;
            if(string.IsNullOrWhiteSpace(oContract.UnifiedBarcode))
                throw new ResultException("must send barcode for student", (int)HttpStatusCode.BadRequest);
            var sessionSpec = new Core.Entites.OfflineSession.Specefication.ReadByIdSpecification(oContract.SessionId);
            var session = await _sessionRepo.FirstOrDefaultAsync(sessionSpec);
            if (session == null)
                throw new ResultException("the session is not found", (int)HttpStatusCode.NotFound);
            if (session.Status == SessionStatus.Closed)
                throw new ResultException("the session is aleardy closed", (int)HttpStatusCode.BadRequest);
         
            if (session.Status == SessionStatus.Scheduled)
            {
                session.OpenScanning(oContract.AssistantUserId);
                await _sessionRepo.UpdateAsync(session);
            }
            var stdspec = new ReadByBarcodeSpecification(oContract.UnifiedBarcode,session.CourseId);
            var std =await _studentRepo.FirstOrDefaultAsync(stdspec);
            if (std == null)
                throw new ResultException("the student is not subcribe for this course or not subcribe in lms", (int)HttpStatusCode.NotFound);

            var CheckDuplicatespec = new CheckDuplicateAttendanceSpecification(session.Id, std.Id);
            var existingRecord = await _attendanceRepo.FirstOrDefaultAsync(CheckDuplicatespec);
            if (existingRecord != null)
                throw new ResultException($"the student ({std.User.FullName}) is asgined really ", (int)HttpStatusCode.Conflict);
            var assistantSpec = new ReadByIdSpecefication(oContract.AssistantUserId);
            var assistantUser = await _userRepo.FirstOrDefaultAsync(assistantSpec);
            var now = DateTimeOffset.Now;
            var attendanceStatus = now > session.StartTime.AddMinutes(15)
                ? AttendanceStatus.Late
                : AttendanceStatus.Present;
            var newRecord = new AttendanceRecord(
                sessionId: session.Id,
                studentId: std.Id,
                scannedByAssistantId: assistantUser?.Id,
                status: attendanceStatus,
                createdBy: oContract.AssistantUserId,
                notes: oContract.Notes
            );
            await _attendanceRepo.AddAsync(newRecord);
            var oResult= new AttendanceContract
            {
                AttendanceId = newRecord._AttendanceId,
                StudentProfileId = std._StudentProfileId,
                StudentName = std.User.FullName,
                UnifiedBarcode = std.UnifiedBarcode,
                CourseTitle = session.Course.Title,
                SessionTitle = session.Title,
                Status = newRecord.Status,
                ScannedAt = newRecord.ScannedAt,
                Message = attendanceStatus == AttendanceStatus.Present
                    ? $"تم تسجيل حضور {std.User.FullName} بنجاح"
                    : $"تم تسجيل حضور {std.User.FullName} (متأخر)"
            };
            return oResult as dynamic;
        }
    }
}
