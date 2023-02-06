using System;

namespace SqlServ4r.Repository.Users
{
    public interface IUserRepository
    {
        (bool ExistEmail, bool ExistPhoneNumber,bool ExistEmployeeCode) CheckDuplicateInformation(string email,string phone,string employeeCode);
        (bool ExistEmail, bool ExistPhoneNumber,bool ExistEmployeeCode) CheckDuplicateInformation(string email,string phone,string employeeCode,Guid id);

    }
}