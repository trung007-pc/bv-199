using System;

namespace Contract.IssuingAgencys
{
    public class CreateUpdateIssuingAgencyDto
    {
        public string? Code { get; set; }
        public string Name { get; set; }
        public string? Note { get; set; }
        public int ODX { get; set; }
        public Guid? ParentCode { get; set; }
    }
}