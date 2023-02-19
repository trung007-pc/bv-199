using System;
using System.Collections.Generic;

namespace Contract.IssuingAgencys
{
    public class IssuingAgencyDto
    {
        public Guid Id { get; set; }
        public string? Code { get; set; }
        public string Name { get; set; }
        public string? Note { get; set; }
        public int ODX { get; set; }
        public Guid? ParentCode { get; set; }
        
        public List<IssuingAgencyDto> ChildAgencies { get; set; }
    }
}