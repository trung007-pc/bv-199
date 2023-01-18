using System;
using System.ComponentModel.DataAnnotations;

namespace Contract.Identity.UserManager
{
    public class UserModel
    {
        [MinLength(6)]
        [Required(ErrorMessage = "UserName is required")]
        public string UserName { get; set; }
        public string Password { get; set; }

    }
}