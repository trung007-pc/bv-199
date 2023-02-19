using System;
using Domain.IssuingAgencys;

namespace Contract.FileTypes
{
    public class CreateUpdateFileTypeDto
    {
        public Guid Id { get; set; }
        public string? Code { get; set; }
        public string Name { get; set; }
        public string? Note { get; set; }
        public int ODX { get; set; }
        public Guid? ParentCode { get; set; }
        
        //foreign key

        public Guid? IssuingAgencyId { get; set; }
    }
}