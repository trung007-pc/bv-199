using System;
using Core.Enum;

namespace Contract.Notifications
{
    public class CreateNotificationDto
    {
        public Guid ReceiverId { get; set; }
        public bool Status { get; set; }
        public Guid DestinationCode { get; set; }
        public DateTime SentDate { get; set; } = DateTime.Now;
        public string? Title { get; set; }
        public NotificationType Type { get; set; } = NotificationType.Document;
    }
}