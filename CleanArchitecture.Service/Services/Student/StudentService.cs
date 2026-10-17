using Azure.Core;
using BCrypt.Net;
using CleanArchitecture.Core.Entites;
using CleanArchitecture.Core.Entites.ParentsProfile;
using CleanArchitecture.Core.Entites.ParentsProfile.Specefication;
using CleanArchitecture.Core.Entites.Users;
using CleanArchitecture.Core.Entites.Users.Specefication;
using CleanArchitecture.Core.Interfaces;
using CleanArchitecture.Shared.Contract.Student;
using huzcodes.Extensions.Exceptions;
using huzcodes.Persistence.Interfaces.Repositories;
using System.Net;
using static CleanArchitecture.Core.Entites.Enums;

namespace CleanArchitecture.Service.Services.Student
{
    public class StudentService<TRequest, TResponse>(IRepository<User> userrepo,IRepository<StudentProfile>stdrepo,IRepository<ParentProfile>parentrepo) : GenericServices<TRequest, TResponse>
    {
        private readonly IRepository<User> _userrepo = userrepo;
        private readonly IRepository<StudentProfile> _stdrepo = stdrepo;
        private readonly IRepository<ParentProfile> _parentrepo = parentrepo;

        public async ValueTask<TResponse> Create(TRequest request)
        {
            var oContract = request as StudentOperationContract;
            if (oContract == null)
                throw new ResultException("you must complete your information", (int)HttpStatusCode.BadRequest);

            if (oContract.StudentPhoneNumber == oContract.ParentWhatsAppNumber)
                throw new ResultException("Student phone number cannot be the same as Parent WhatsApp number", (int)HttpStatusCode.BadRequest);

            var specstd = new ReadByPhoneSpecification(oContract.StudentPhoneNumber);
            var stdnumcheck = await _userrepo.FirstOrDefaultAsync(specstd);
            if (stdnumcheck != null)
                throw new ResultException("The student phone number already exists", (int)HttpStatusCode.BadRequest);

            var specparent = new ReadByPhoneNumberSpecification(oContract.ParentWhatsAppNumber);
            var parentnumberexist = await _parentrepo.FirstOrDefaultAsync(specparent);

            var NewUser = new User(
                oContract.StudentFullName,
                oContract.StudentPhoneNumber,
                BCrypt.Net.BCrypt.HashPassword(oContract.Password),
                UserRole.Student,
                oContract.ProfilePictureUrl,
                oContract.StudentEmail
            );
            var UStd = await _userrepo.AddAsync(NewUser);
            Guid parentProfileGuid;
            StudentProfile createdStudent;

            if (parentnumberexist == null)
            {
                var parentUser = new User(
                    fullName: oContract.ParentFullName,
                    phoneNumber: oContract.ParentWhatsAppNumber,
                    passwordHash: BCrypt.Net.BCrypt.HashPassword(oContract.ParentWhatsAppNumber),
                    role: UserRole.Parent
                );
                await _userrepo.AddAsync(parentUser);

                var NewParent = new ParentProfile(
                    parentUser.Id,
                    parentUser._UserId,
                    oContract.ParentWhatsAppNumber,
                    oContract.RelationType,
                    oContract.ParentJobTitle,
                    oContract.Address
                );
                var parent = await _parentrepo.AddAsync(NewParent);

                parentProfileGuid = parent._ParentProfileId;
                var NewStd = new StudentProfile(UStd.Id, parent.Id, UStd._UserId, oContract.GradeLevel, oContract.SchoolName);
                createdStudent = await _stdrepo.AddAsync(NewStd);
            }
            else
            {
                parentProfileGuid = parentnumberexist._ParentProfileId;
                var NewStd = new StudentProfile(UStd.Id, parentnumberexist.Id, UStd._UserId, oContract.GradeLevel, oContract.SchoolName);
                createdStudent = await _stdrepo.AddAsync(NewStd); 
            }

            var studentResponse = new StudentContract()
            {
                StudentUserGuid = UStd._UserId,
                StudentProfileGuid = createdStudent._StudentProfileId,
                StudentName = UStd.FullName,
                ParentProfileGuid = parentProfileGuid,
                 UnifiedBarcode = createdStudent.UnifiedBarcode,
                GradeLevel = createdStudent.GradeLevel
            };

            if (studentResponse is TResponse response)
                return response;

            throw new ResultException("Invalid response type", (int)HttpStatusCode.InternalServerError);
        }
        public ValueTask Delete(Guid id)
        {
            throw new NotImplementedException();
        }

        public ValueTask<List<TResponse>> Read()
        {
            throw new NotImplementedException();
        }

        public ValueTask<TResponse> ReadById(Guid id)
        {
            throw new NotImplementedException();
        }

        public ValueTask<TResponse> ReadSingleByParentId(Guid id, Guid ParentId)
        {
            throw new NotImplementedException();
        }

        public ValueTask<TResponse> Update(TRequest request, Guid id)
        {
            throw new NotImplementedException();
        }

        public ValueTask UpdateParentById(Guid ParentId)
        {
            throw new NotImplementedException();
        }
    }
}
