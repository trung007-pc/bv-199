using System;
using System.Collections.Generic;
using Core.Enum;
using Domain.DocumentFiles;
using Domain.Identity.Users;

namespace Domain.Notifications
{
    public class Notification
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public Guid ReceiverId { get; set; }
        public bool Status { get; set; }
        public Guid DestinationCode { get; set; }
        public DateTime SentDate { get; set; } = DateTime.Now;
        public string? Title { get; set; }
        public NotificationType Type { get; set; } = NotificationType.Document;

        
        
        public User User { get; set; }
        public DocumentFile DocumentFile { get; set;} 

    }
}