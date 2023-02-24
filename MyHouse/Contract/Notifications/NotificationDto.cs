using System;
using Core.Enum;

namespace Contract.Notifications
{
    public class NotificationDto
    {
        public Guid Id { get; set; } 
        public Guid ReceiverId { get; set; }
        public bool Status { get; set; }
        public Guid DestinationCode { get; set; }
        public DateTime SentDate { get; set; } = DateTime.Now;
        public string? Title { get; set; }
        public NotificationType Type { get; set; } = NotificationType.Document;
        
        
        
        //UI - Tier

        public bool Visible { get; set; } = false;
    }
}