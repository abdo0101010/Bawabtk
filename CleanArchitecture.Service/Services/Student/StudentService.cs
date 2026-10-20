using Azure.Core;
using BCrypt.Net;
using CleanArchitecture.Core.Entites.ParentsProfile;
using CleanArchitecture.Core.Entites.ParentsProfile.Specefication;
using CleanArchitecture.Core.Entites.Student;
using CleanArchitecture.Core.Entites.Users;
using CleanArchitecture.Core.Entites.Users.Specefication;
using CleanArchitecture.Core.Interfaces;
using CleanArchitecture.Shared.Contract.Student;
using huzcodes.Extensions.Exceptions;
using huzcodes.Persistence.Interfaces.Repositories;
using System.Diagnostics.Contracts;
using System.Net;
using static CleanArchitecture.Core.Entites.Enums;

namespace CleanArchitecture.Service.Services.Student
{
    public class StudentService<TRequest, TResponse>(IRepository<Core.Entites.Users.User> userrepo,IRepository<StudentProfile>stdrepo,IRepository<ParentProfile>parentrepo) : IGenericServices<TRequest, TResponse>
    {
        private readonly IRepository<Core.Entites.Users.User> _userrepo = userrepo;
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

            var NewUser = new Core.Entites.Users.User(
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
                var parentUser = new Core.Entites.Users.User(
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
        public async ValueTask Delete(Guid id)
        {
            var spec = new Core.Entites.Student.Specefication.ReadByIdSpecification(id);
            var oData = await _stdrepo.FirstOrDefaultAsync(spec);
            if (oData == null)
                throw new ResultException("cannot found this user", ((int)HttpStatusCode.NotFound));
            await _stdrepo.DeleteAsync(oData);
        }

        public async ValueTask<List<TResponse>> Read()
        {
            var Spec = new Core.Entites.Student.Specefication.ReadSpecefication();
            var oData =await _stdrepo.ListAsync(Spec);
            if (oData == null || !oData.Any())
                return new List<TResponse>();
            var oResult = oData.Select(s => new StudentContract
            {
                StudentUserGuid = s.User != null ? s.User._UserId : Guid.Empty,
                StudentProfileGuid = s._StudentProfileId,
                ParentProfileGuid = s.Parent != null ? s.Parent._ParentProfileId : Guid.Empty,
                StudentName = s.User != null ? s.User.FullName : string.Empty,
                UnifiedBarcode = s.UnifiedBarcode,
                GradeLevel = s.GradeLevel
            }).ToList();
            return oResult as dynamic;
        }

        public async ValueTask<TResponse> ReadById(Guid id)
        {
            var spec = new Core.Entites.Student.Specefication.ReadByIdSpecification(id);
            var oData =await _stdrepo.FirstOrDefaultAsync(spec);
            if (oData == null)
                throw new ResultException("cannot found this user", ((int)HttpStatusCode.NotFound));
            var oResult = new StudentContract()
            {
                GradeLevel = oData.GradeLevel,
                ParentProfileGuid = oData.Parent._ParentProfileId,
                StudentName = oData.User.FullName,
                StudentProfileGuid = oData._StudentProfileId,
                StudentUserGuid = oData.User._UserId,
                UnifiedBarcode = oData.UnifiedBarcode
            };
            return oResult as dynamic;
        }

        public async ValueTask<TResponse> ReadSingleByParentId(Guid id, Guid ParentId)
        {
            if (ParentId == Guid.Empty || id == Guid.Empty)
                throw new ResultException("cannot send null id", ((int)HttpStatusCode.BadRequest));
            var specparent = new Core.Entites.ParentsProfile.Specefication.ReadByIdSpecefication(ParentId);
            var parent = await _parentrepo.FirstOrDefaultAsync(specparent);
            if (parent == null)
                throw new ResultException("cannot found parent", ((int)HttpStatusCode.NotFound));
            var specstd = new Core.Entites.Student.Specefication.ReadByIdSpecification(id);
            var std = await _stdrepo.FirstOrDefaultAsync(specstd);
            var specresult = new Core.Entites.Student.Specefication.ReadSingleByParentId(ParentId, id);
            var oData = await _stdrepo.FirstOrDefaultAsync(specresult);
            if (oData == null)
                throw new ResultException("cannot found student for this parent", ((int)HttpStatusCode.NotFound));
            var oResult = new StudentContract()
            {
                GradeLevel=oData.GradeLevel,
                ParentProfileGuid=oData.Parent._ParentProfileId,
                StudentName=oData.User.FullName,
                UnifiedBarcode=oData.UnifiedBarcode,
                StudentProfileGuid=oData._StudentProfileId,
                StudentUserGuid=oData.User._UserId
            };
            return oResult as dynamic;
        }

        public async ValueTask<TResponse> Update(TRequest request, Guid id)
        {
            var oContract = request as StudentOperationContract;
            var spec = new Core.Entites.Student.Specefication.ReadByIdSpecification(id);
            var oData = await _stdrepo.FirstOrDefaultAsync(spec);
            if (oData == null)
                throw new ResultException("cannot found this user", ((int)HttpStatusCode.NotFound));
            oData.GradeLevel = oContract.GradeLevel;
            oData.User.FullName = oContract.StudentFullName;
            oData.SchoolName = oContract.SchoolName;
            oData.User.ProfilePictureUrl = oContract.ProfilePictureUrl;
            oData.User.PasswordHash = BCrypt.Net.BCrypt.HashPassword(oContract.Password);
            oData.Parent.WhatsAppNumber = oContract.ParentWhatsAppNumber;
            oData.User.PhoneNumber = oContract.StudentPhoneNumber;
            
             
             await _stdrepo.UpdateAsync(oData);
            var oResult = new StudentContract()
            {
                GradeLevel = oData.GradeLevel,
                StudentName = oData.User.FullName,
                ParentProfileGuid = oData.Parent._ParentProfileId,
                StudentProfileGuid = oData._StudentProfileId,
                StudentUserGuid = oData.User._UserId,
                UnifiedBarcode = oData.UnifiedBarcode

            };
            return oResult as dynamic;
        }

        public async ValueTask UpdateParentById(Guid ParentId,Guid contractid)
        {
            if (ParentId == Guid.Empty|| contractid == Guid.Empty)
                throw new ResultException("cannot send null id", ((int)HttpStatusCode.BadRequest));
            var specparent = new Core.Entites.ParentsProfile.Specefication.ReadByIdSpecefication(ParentId);
            var parent =await _parentrepo.FirstOrDefaultAsync(specparent);
            if(parent==null)
                throw new ResultException("cannot found parent", ((int)HttpStatusCode.NotFound));
            var specstd= new Core.Entites.Student.Specefication.ReadByIdSpecification(contractid);
            var std =await _stdrepo.FirstOrDefaultAsync(specstd);
            if (std == null)
                throw new ResultException("cannot found Student", ((int)HttpStatusCode.NotFound));
             std.SetParent(parent);
            await _stdrepo.UpdateAsync(std);
        }
    }
}
