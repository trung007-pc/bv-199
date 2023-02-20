using System;
using Domain.DocumentFiles;
using Domain.Identity.Users;

namespace Domain.SendingFiles
{
    public class SendingFile
    {
        public Guid Id { get; set; }
        
        //id-user
        public Guid ReceiverId { get; set; }
        
        public Guid FileId { get; set; }

        public bool Status { get; set; }
        public bool IsRevoked { get; set; }
        public DateTime SentDate { get; set; }
        public DateTime ViewDate { get; set; }
        
        
        //navaigation
        
        public User User { get; set; }
        public DocumentFile DocumentFile { get; set; }
    }
}