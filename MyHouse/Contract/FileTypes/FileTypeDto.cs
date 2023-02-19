using System;
using System.Collections.Generic;
using Domain.IssuingAgencys;

namespace Contract.FileTypes
{
    public class FileTypeDto
    {
        public Guid Id { get; set; }
        public string? Code { get; set; }
        public string Name { get; set; }
        public string? Note { get; set; }
        public int ODX { get; set; }
        public Guid? ParentCode { get; set; }
        
        //navigation
        public IssuingAgency IssuingAgency { get; set; }
        
        
        //UI
        public List<FileTypeDto> ChildTypes { get; set; }
    }
}