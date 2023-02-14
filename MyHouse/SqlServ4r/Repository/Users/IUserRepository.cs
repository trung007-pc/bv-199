using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Domain.Identity.Users;

namespace SqlServ4r.Repository.Users
{
    public interface IUserRepository
    {
        (bool ExistEmail, bool ExistPhoneNumber,bool ExistEmployeeCode) CheckDuplicateInformation(string email,string phone,string employeeCode);
        (bool ExistEmail, bool ExistPhoneNumber,bool ExistEmployeeCode) CheckDuplicateInformation(string email,string phone,string employeeCode,Guid id);

        Task<List<UserWithNavigationProperties>> GetListWithNavigationProperties();

    }
}