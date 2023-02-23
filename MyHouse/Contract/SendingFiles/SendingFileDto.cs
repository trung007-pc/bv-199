using System;

namespace Contract.SendingFiles
{
    public class SendingFileDto
    {
        public Guid Id { get; set; }

        //id-user
        public Guid SenderId { get; set; }
        public Guid ReceiverId { get; set; }
        
        public Guid FileId { get; set; }

        public bool Status { get; set; }
        public bool IsRevoked { get; set; }
        public DateTime SentDate { get; set; }
        public DateTime ViewDate { get; set; }
    }
}