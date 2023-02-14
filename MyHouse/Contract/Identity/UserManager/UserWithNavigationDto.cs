using System.Collections.Generic;
using Contract.Departments;
using Contract.Identity.RoleManager;
using Contract.Positions;

namespace Contract.Identity.UserManager
{
    public class UserWithNavigationPropertiesDto
    {
        public int Index { get; set;}
        public UserDto User { get; set; }

        public List<string> RoleNames { get; set; }
        
        public PositionDto Position { get; set; }
        public List<DepartmentDto> Departments { get; set; }

    }
}