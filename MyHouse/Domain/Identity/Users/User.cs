using System;
using Core.Enum;
using Microsoft.AspNetCore.Identity;

namespace Domain.Identity.Users
{
    public class User : IdentityUser<Guid>
    {
        // public int UserCode { get; set;}
        // public string? FirstName { get; set; }
        // public string? LastName { get; set; }
        // public Gender Gender { get; set; } = Gender.Unknown;
        // public DateTime? DateTime { get; set; }
        // public string Phone { get; set; }
        // public string Email { get; set; }
        public bool IsActive { get; set; } = true;
        public string? RefreshToken { get; set;}
        
    }
}