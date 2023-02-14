﻿using System;
using System.Collections.Generic;

namespace Contract.Departments
{
    public class DepartmentDto
    {
        public Guid Id { get; set; }
        public string? Code { get; set; }
        public string Name { get; set; }
        public string? Note { get; set; }
        public int ODX { get; set; }
        public Guid? ParentCode { get; set; }
        
        
        
        //model
        public List<DepartmentDto> ChildDepartment { get; set; } = new List<DepartmentDto>();

      
    }
}