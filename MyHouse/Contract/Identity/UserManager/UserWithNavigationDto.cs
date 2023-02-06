using System.Collections.Generic;
using Contract.Identity.RoleManager;

namespace Contract.Identity.UserManager
{
    public class UserWithNavigationPropertiesDto
    {
        public int Index { get; set;}
        public UserDto UserDto { get; set; }

        public List<string> RoleNames { get; set; }
    }
}