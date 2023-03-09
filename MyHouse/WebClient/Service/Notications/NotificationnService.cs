using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Contract.Notifications;
using WebClient.RequestHttp;

namespace WebClient.Service.Notications
{
    public class NotificationnService : INotificationService
    {
        public NotificationnService()
        {
            
        }


        public Task<NotificationDto> CreateAsync(CreateNotificationDto input)
        {
            throw new NotImplementedException();
        }

        public async Task<NotificationDto> UpdateAsync(UpdateNotification input)
        {
            return await RequestClient.PutAPIAsync<NotificationDto>("notification",input);
        }

        public async Task<NotificationDto> UpdateWithReadingStatusAsync(UpdateNotification input)
        {
            return await RequestClient.PutAPIAsync<NotificationDto>("notification/update-with-reading-status",input);
        }

        public async Task<bool> UpdateListWithReadingStatusAsync(List<UpdateNotification> inputs)
        {
            return await RequestClient.PutAPIAsync<bool>("notification/update-list-reading-status",inputs);
        }

        public Task DeleteAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<List<NotificationDto>> CreateListAsync(List<CreateNotificationDto> inputs)
        {
            throw new NotImplementedException();
        }

        public async Task<List<NotificationDto>> UpdateListAsync(List<UpdateNotification> inputs)
        {
            return await RequestClient.PutAPIAsync<List<NotificationDto>>("notification/update-list",inputs);
        }
        
        public async Task<List<NotificationDto>> GetListByFilter(NotificationFilter filter)
        {   
            return await RequestClient.PostAPIAsync<List<NotificationDto>>("notification/get-list-by-filter",filter);
        }

        public async Task<int> CountUnreadNotificationOfUser(NotificationFilter filter)
        {
            return await RequestClient.PostAPIAsync<int>("notification/count-unread-notification-of-user",filter);
        }
    }
}