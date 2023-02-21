using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Contract.Notifications
{
    public interface INotificationService
    {
        Task<NotificationDto> CreateAsync(CreateNotificationDto input);

        Task<NotificationDto> UpdateAsync(UpdateNotification input);
        Task DeleteAsync(Guid id);
        
        Task<List<NotificationDto>> CreateListAsync(List<CreateNotificationDto> inputs);
        Task<List<NotificationDto>> UpdateListAsync(List<UpdateNotification> inputs);

    }
}