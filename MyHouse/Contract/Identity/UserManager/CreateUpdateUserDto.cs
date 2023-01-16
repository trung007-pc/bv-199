using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
// using Microsoft.AspNetCore.Http;

namespace Contract.Identity.UserManager
{
    public  class CreateUpdateUserDto 
    {
        [MinLength(6)]
        [Required(ErrorMessage = "UserName is required")]
        public string UserName { get; set; }
        
        [MinLength(6)]
        [Required(ErrorMessage = "Password is required")]
        public string Password { get; set; }

        [Compare(nameof(Password))]
        [Required(ErrorMessage = "Password is required")]
        public string PasswordConfirm { get; set; }
        
    }
}