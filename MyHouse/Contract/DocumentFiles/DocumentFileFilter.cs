using System;
using Contract.Base;

namespace Contract.DocumentFiles
{
    public class DocumentFileFilter : FilterBase
    {
        public Guid? DocumentFolderId { get; set; }
        public Guid? IssuingAgencyId { get; set; }
        public Guid? FileTypeId { get; set; }
    }
}