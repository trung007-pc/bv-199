using System;
using System.Collections.Generic;
using Domain.Identity.Users;
using Domain.Units;
using JetBrains.Annotations;
using SqlServ4r.EntityFramework;
using SqlServ4r.RepGenerationPatten;
using SqlServ4r.Repository.Units;
using Volo.Abp.DependencyInjection;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;
using Domain.Positions;
using Microsoft.EntityFrameworkCore;

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

        public async Task<List<UserWithNavigationProperties>> GetListWithNavigationProperties()
        {   

            var query = from user in _context.Users.Where(x=>!x.IsDelete)
                select new UserWithNavigationProperties
                {
                    User = user,
                    RoleNames = (from roleUser in _context.UserRoles
                        join role in _context.Roles on roleUser.RoleId equals role.Id
                        where roleUser.UserId == user.Id
                        select role.Name).ToList(),
                  Position  = _context.Positions.FirstOrDefault(x=>x.Id == user.PositionId),
                  Departments = (from department in _context.Departments 
                                join departmentUser in _context.UserDepartments
                                on department.Id equals departmentUser.DepartmentId
                                where departmentUser.UserId == user.Id
                                    select department).ToList()
                  
                };


            return await query.ToListAsync();

        }

        public async Task<UserWithNavigationProperties> GetWithNavigationProperties(Guid id)
        {
            var query = from user in _context.Users.Where(x=>x.Id == id)
                select new UserWithNavigationProperties
                {
                    User = user,
                    RoleNames = (from roleUser in _context.UserRoles
                        join role in _context.Roles on roleUser.RoleId equals role.Id
                        where roleUser.UserId == user.Id
                        select role.Name).ToList(),
                    Position  = _context.Positions.FirstOrDefault(x=>x.Id == user.PositionId),
                    Departments = (from department in _context.Departments 
                        join departmentUser in _context.UserDepartments
                            on department.Id equals departmentUser.DepartmentId
                        where departmentUser.UserId == user.Id
                        select department).ToList()
                  
                };
            
            return await query.FirstOrDefaultAsync() ?? new UserWithNavigationProperties();
        }
    }
}