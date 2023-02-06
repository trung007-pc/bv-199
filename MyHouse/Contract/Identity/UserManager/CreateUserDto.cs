using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using Contract.CustomAttribute;
using Core.Const;
using Core.Enum;

// using Microsoft.AspNetCore.Http;

namespace Contract.Identity.UserManager
{
    public  class CreateUserDto 
    {
        [MinLength(4)]
        [Required(ErrorMessage = "UserName is required")]
        [RegularExpression(ContentRegularExpression.USER_NAME,ErrorMessage = "Username must not have spaces")]

        public string UserName { get; set; }
        
        [Required(ErrorMessage = "First Name is required")]
        [RegularExpression(ContentRegularExpression.NAME,ErrorMessage = "First Name must be in text format")]
        [MinLength(2,ErrorMessage = "First Name is at least 2 character")]
        public string FirstName { get; set; }
        
        [Required(ErrorMessage = "Last Name is required")]
        [RegularExpression(ContentRegularExpression.NAME,ErrorMessage = "Last Name must be in text format")]
        [MinLength(2,ErrorMessage = "Last Name is at least 2 character")]
        public string LastName { get; set; }
        
        [MinLength(6)]
        [Required(ErrorMessage = "Password is required")]
        [RegularExpression(ContentRegularExpression.PASSWORD,ErrorMessage = "Password must not have spaces")]
        public string Password { get; set; }

        [Compare(nameof(Password))]
        [Required(ErrorMessage = "Password is required")]
        [RegularExpression(ContentRegularExpression.PASSWORD,ErrorMessage = "PasswordConfirm must not have spaces")]
        public string PasswordConfirm { get; set; }
        
        
        [Required(ErrorMessage = "Employee Code is required")]
        [MinLength(2,ErrorMessage = "Employee Code is at least 2 character")]
        public string EmployeeCode { get; set;}
        

        
        public Gender Gender { get; set; } 
        public DateTime DOB { get; set; }
        
        [Required(ErrorMessage = "Phone Number is required")]
        [RegularExpression(ContentRegularExpression.NUMBER_PHONE,ErrorMessage = "Phone Number has to 10 number")]
        public string PhoneNumber { get; set; }
        
        
  
        [RegularExpression(ContentRegularExpression.EMAIL,ErrorMessage = "Email has to @gmail.com format")]
        public string? Email { get; set; }
        
        public List<string> Roles { get; set; } = new List<string>();

        
        
    }
}