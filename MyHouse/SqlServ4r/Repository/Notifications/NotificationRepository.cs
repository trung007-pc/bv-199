using System;
using System.Collections.Generic;
using Domain.Notifications;
using JetBrains.Annotations;
using SqlServ4r.EntityFramework;
using SqlServ4r.RepGenerationPatten;
using Volo.Abp.DependencyInjection;

namespace SqlServ4r.Repository.Notifications
{
    public class NotificationRepository : GenericRepository<Notification, Guid>, ITransientDependency
    {
        public NotificationRepository([NotNull] DreamContext context) : base(context)
        {
        }
        
       
    }
}