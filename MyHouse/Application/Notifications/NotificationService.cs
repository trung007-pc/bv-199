using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Contract.FileTypes;
using Contract.Notifications;
using Core.Const;
using Core.Exceptions;
using Domain.Notifications;
using SqlServ4r.Repository.Notifications;
using Volo.Abp.DependencyInjection;

namespace Application.Notifications
{
    public class NotificationService : ServiceBase,INotificationService,ITransientDependency
    {
        public NotificationRepository _notificationRepository;
        public NotificationService(NotificationRepository notificationRepository)
        {
            _notificationRepository = notificationRepository;
        }
        public Task<NotificationDto> CreateAsync(CreateNotificationDto input)
        {
            throw new NotImplementedException();
        }

        public async Task<NotificationDto> UpdateAsync(UpdateNotification input)
        {
            var item = await _notificationRepository.
                FirstOrDefaultAsync(x => x.Id == input.Id);

            if (item is null)
            {
                throw new GlobalException(HttpMessage.NotFound, HttpStatusCode.BadRequest);
            }
            
            var notification = ObjectMapper.Map(input,item);
            await _notificationRepository.UpdateAsync(notification);

            return ObjectMapper.Map<Notification,NotificationDto>(notification);
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
            var notifications = ObjectMapper
                .Map<List<UpdateNotification>, List<Notification>>(inputs);
            
            _notificationRepository.UpdateRange(notifications);
            return ObjectMapper.Map<List<Notification>, List<NotificationDto>>(notifications);
        }
    }
}