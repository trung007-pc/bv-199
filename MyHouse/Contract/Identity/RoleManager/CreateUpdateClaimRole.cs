﻿using System;
using System.Security.Claims;

namespace Contract.Identity.RoleManager
{
    public class CreateUpdateClaimRole
    {
        public Guid RoleId { get; set; }

        public string ClaimType { get; set; }
        
        public  string ClaimValue { get; set; }
    }
}