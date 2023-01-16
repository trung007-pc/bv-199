using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Contract.CustomAttribute;
using Contract.Identity.RoleManager;

namespace Contract.Identity.UserManager
{
    public class CreateUpdateUserWithNavDto
    {
        
        
        public CreateUpdateUserDto User { get; set; } = new CreateUpdateUserDto();

        public List<string> Roles { get; set; } = new List<string>();
    }
}