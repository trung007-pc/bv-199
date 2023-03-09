using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Domain.Notifications;

namespace Contract.Notifications
{
    public interface INotificationService
    {
        Task<NotificationDto> CreateAsync(CreateNotificationDto input);

        Task<NotificationDto> UpdateAsync(UpdateNotification input);
        
        Task<NotificationDto> UpdateWithReadingStatusAsync(UpdateNotification input);
        Task<bool> UpdateListWithReadingStatusAsync(List<UpdateNotification> inputs);

        Task DeleteAsync(Guid id);
        
        Task<List<NotificationDto>> CreateListAsync(List<CreateNotificationDto> inputs);
        Task<List<NotificationDto>> UpdateListAsync(List<UpdateNotification> inputs);
        Task<List<NotificationDto>> GetListByFilter(NotificationFilter filter);
        
        Task<int> CountUnreadNotificationOfUser(NotificationFilter filter);

        

    }
}