using CleanArchitecture.Core.Entites;
using CleanArchitecture.Core.Entites.ParentsProfile;
using CleanArchitecture.Core.Entites.ParentsProfile.Specefication;
using CleanArchitecture.Core.Entites.Student;
using CleanArchitecture.Core.Entites.Users;
using CleanArchitecture.Core.Interfaces;
using CleanArchitecture.Shared.Contract.ParentImplemention;
using huzcodes.Extensions.Exceptions;
using huzcodes.Persistence.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using static CleanArchitecture.Core.Entites.Enums;

namespace CleanArchitecture.Service.Services.Parent
{
    public class ParentService<TRequest, TResponse>(IRepository<Core.Entites.Users.User> userrepo, IRepository<StudentProfile> stdrepo, IRepository<ParentProfile> parentrepo) : IGenericServices<TRequest, TResponse>
    {
        private readonly IRepository<Core.Entites.Users.User> _userrepo = userrepo;
        private readonly IRepository<StudentProfile> _stdrepo = stdrepo;
        private readonly IRepository<ParentProfile> _parentrepo = parentrepo;
        public async ValueTask<TResponse> Create(TRequest request)
        {
            var oContract = request as ParentOperationContract;
            if (oContract == null)
                throw new ResultException("cannot send null request", ((int)HttpStatusCode.BadRequest));
            var specparent = new ReadByPhoneNumberSpecification(oContract.WhatsAppNumber);
            var parentnumberexist = await _parentrepo.FirstOrDefaultAsync(specparent);
            if (parentnumberexist != null)
                throw new ResultException("the number is exist please try with anthor number",((int)HttpStatusCode.BadRequest));
            var user = new Core.Entites.Users.User(oContract.FullName, oContract.PhoneNumber, BCrypt.Net.BCrypt.HashPassword(oContract.Password), UserRole.Parent, oContract.ProfilePictureUrl, oContract.Email);
            var newuser = await _userrepo.AddAsync(user);

            var oData = new ParentProfile(newuser.Id, newuser._UserId, newuser.PhoneNumber, address: oContract.Address,jobTitle:oContract.JobTitle);
            var newparent = await _parentrepo.AddAsync(oData);
            var oResult = new ParentContract()
            {
                Address = newparent.Address,
                FullName = newuser.FullName,
                JobTitle = newparent.JobTitle,
                WhatsAppNumber = newuser.PhoneNumber,
                ParentProfileGuid=newparent._ParentProfileId
            };
            return oResult as dynamic;
        
        }

        public async ValueTask Delete(Guid id)
        {
            var spec = new ReadByIdSpecefication(id);
            var oData =await _parentrepo.FirstOrDefaultAsync(spec);
            if (oData == null)
                throw new ResultException("not found parent", ((int)HttpStatusCode.NotFound));
            await _parentrepo.DeleteAsync(oData);
        }

        public async ValueTask<List<TResponse>> Read()
        {
            var spec = new ReadSpecefication();
            var oData =await _parentrepo.ListAsync(spec);
            if(oData==null||!oData.Any())
                throw new ResultException("not found any parent", ((int)HttpStatusCode.NotFound));
            var oResult = oData.Select(p => new ParentContract()
            {
                Address = p.Address,
                FullName = p.User.FullName,
                WhatsAppNumber = p.WhatsAppNumber,
                JobTitle = p.JobTitle,
                ParentProfileGuid = p._ParentProfileId
            }).ToList();
            return oResult as dynamic;
        }

        public async ValueTask<TResponse> ReadById(Guid id)
        {
            var spec = new ReadByIdSpecefication(id);
            var oData = await _parentrepo.FirstOrDefaultAsync(spec);
            if (oData == null)
                throw new ResultException("not found any parent", ((int)HttpStatusCode.NotFound));
            var oResult =new ParentContract()
            {
                Address = oData.Address,
                FullName = oData.User.FullName,
                WhatsAppNumber = oData.WhatsAppNumber,
                JobTitle = oData.JobTitle,
                ParentProfileGuid=oData._ParentProfileId
            };
            return oResult as dynamic;


        }

        public ValueTask<TResponse> ReadSingleByParentId(Guid id, Guid ParentId)
        {
            throw new NotImplementedException();
        }

        public async ValueTask<TResponse> Update(TRequest request, Guid id)
        {
            var oContract = request as ParentOperationContract;
            if (oContract == null)
                throw new ResultException("cannot send null request", ((int)HttpStatusCode.BadRequest));
            var spec = new ReadByIdSpecefication(id);
            var oData = await _parentrepo.FirstOrDefaultAsync(spec);
            if (oData == null)
                throw new ResultException("not found any parent", ((int)HttpStatusCode.NotFound));
            oData.Address = oContract.Address;
            oData.JobTitle = oContract.JobTitle;
            oData.User.FullName = oContract.FullName;
            oData.WhatsAppNumber = oContract.WhatsAppNumber;
            await _parentrepo.UpdateAsync(oData);
            var oResult = new ParentContract()
            {
                Address=oData.Address,
                FullName = oData.User.FullName,
                JobTitle=oData.JobTitle,
                WhatsAppNumber=oData.WhatsAppNumber,
                ParentProfileGuid=oData._ParentProfileId
            };
            return oResult as dynamic;
        }

        public ValueTask UpdateParentById(Guid ParentId, Guid contractid)
        {
            throw new NotImplementedException();
        }
    }
}
