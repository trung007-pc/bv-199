using System;
using System.Collections.Generic;
using Core.Enum;

namespace Contract.Identity.UserManager
{
    public class CreateUpdateUseDto
    {
        public string UserName { get; set; }
        public string Password { get; set;}
        public string EmployeeCode { get; set;}
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public Gender? Gender { get; set; } = Core.Enum.Gender.Unknown; 
        public DateTime? DOB { get; set; } 
        public string PhoneNumber { get; set; }
        public string? Email { get; set; }
        public int Row { get; set; }
        public List<string> Roles { get; set; } = new List<string>();
    }
}