﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Contract.Identity.RoleManager
{
    public interface IRoleManagerService
    {
        //Role
        Task<List<RoleDto>> GetListAsync();
        Task<RoleDto> CreateAsync(CreateUpdateRoleDto input);
        Task<RoleDto> UpdateAsync(CreateUpdateRoleDto input,Guid id);
        Task DeleteAsync(Guid id);
        
        
        
        
        //role-claim
        public Task<List<RoleClaimDto>> GetClaimListAsync(Guid roleId);
        public Task<CreateUpdateClaimRole> CreateClaimAsync(CreateUpdateClaimRole input);
        public Task DeleteClaimAsync(RoleClaimModel input);
    }
}