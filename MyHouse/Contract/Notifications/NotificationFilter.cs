using System;

namespace Contract.Notifications
{
    public class NotificationFilter
    {
        public string UserName { get; set; }
        public NotificationStatus Status { get; set; } = NotificationStatus.All;
        
    }
    public enum NotificationStatus
    {
        All,
        Unread,
    }
}