using System;
using System.Collections.Generic;

namespace Contract.SendingFiles
{
    public class SendingFileRequest
    {
        public List<Guid> DepartmentIds { get; set; }
        public List<Guid> DefineUsers { get; set; }
        
        public Guid Sender { get; set; }
        
        public Guid FileId { get; set; }
    }
}