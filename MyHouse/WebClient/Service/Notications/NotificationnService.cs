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
    }
}