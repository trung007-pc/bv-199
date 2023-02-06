using System;
using Core.Enum;

namespace Contract.Identity.UserManager
{
    public class UserDto
    {
        public Guid Id { get; set; }
        public string UserName { get; set; }
        public string EmployeeCode { get; set;}
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public Gender Gender { get; set; } 
        public DateTime DOB { get; set; } 
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        
    }
}