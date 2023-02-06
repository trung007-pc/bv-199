using System;
using Domain.Identity.Users;
using Domain.Units;
using JetBrains.Annotations;
using SqlServ4r.EntityFramework;
using SqlServ4r.RepGenerationPatten;
using SqlServ4r.Repository.Units;
using Volo.Abp.DependencyInjection;
using System.Linq;

namespace SqlServ4r.Repository.Users
{
    public  class UserRepository: GenericRepository<User, Guid>, ITransientDependency, IUserRepository
    {
        public UserRepository([NotNull] DreamContext context) : base(context)
        {
        }

        public (bool ExistEmail, bool ExistPhoneNumber,bool ExistEmployeeCode) CheckDuplicateInformation(string email,string phone,string employeeCode)
        {
            var query = from user in _context.Users
                select new ValueTuple<bool, bool,bool>(_context.Users.Any(x => x.Email==email && x.Email!=null),
                    _context.Users.Any(x => x.PhoneNumber==phone),
                    _context.Users.Any(x=>x.EmployeeCode == employeeCode));

            return query.FirstOrDefault();
        }

        public (bool ExistEmail, bool ExistPhoneNumber,bool ExistEmployeeCode) CheckDuplicateInformation(string email, string phone,string employeeCode, Guid id)
        {
            var query = from user in _context.Users
                select new ValueTuple<bool, bool,bool>(_context.Users.Where(x => x.Id != id).Any(x => x.Email==email && x.Email!=null),
                    _context.Users.Where(x => x.Id != id).Any(x => x.PhoneNumber==phone),
                    _context.Users.Where(x => x.Id != id).Any(x=>x.EmployeeCode == employeeCode));
        
            return query.FirstOrDefault();
        }
    }
}