using System;
using System.ComponentModel.DataAnnotations;
using Core.Enum;
using Microsoft.AspNetCore.Identity;

namespace Domain.Identity.Users
{
    public class User : IdentityUser<Guid>
    {
        public string EmployeeCode { get; set;}
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public Gender Gender { get; set; } = Gender.Unknown;
        public DateTime DOB { get; set; } = DateTime.Now;
        
        public string PhoneNumber { get; set; }
        public bool IsActive { get; set; } = true;
        public string? RefreshToken { get; set;}
        
    }
}