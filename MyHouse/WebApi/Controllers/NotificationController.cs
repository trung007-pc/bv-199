using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Application.Notifications;
using Contract.Notifications;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SqlServ4r.Repository.Notifications;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("api/notification/")]
    [Authorize]
    public class NotificationController : INotificationService
    {
        private NotificationService _notificationService;
        public NotificationController(NotificationService notificationService)
        {
            _notificationService = notificationService;
        }


        public Task<NotificationDto> CreateAsync(CreateNotificationDto input)
        {
            throw new NotImplementedException();
        }

        [HttpPut]
        public async Task<NotificationDto> UpdateAsync(UpdateNotification input)
        {
             return await _notificationService.UpdateAsync(input);
        }

        [HttpPut]
        [Route("update-with-reading-status")]
        public async Task<NotificationDto> UpdateWithReadingStatusAsync(UpdateNotification input)
        {
            return await _notificationService.UpdateWithReadingStatusAsync(input);
        }

        [HttpPut]
        [Route("update-list-reading-status")]
        public async Task<bool> UpdateListWithReadingStatusAsync(List<UpdateNotification> inputs)
        {
            return await _notificationService.UpdateListWithReadingStatusAsync(inputs);
        }

        public Task DeleteAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<List<NotificationDto>> CreateListAsync(List<CreateNotificationDto> inputs)
        {
            throw new NotImplementedException();
        }

        [HttpPut]
        [Route("update-list")]
        public async Task<List<NotificationDto>> UpdateListAsync(List<UpdateNotification> inputs)
        {
            return await _notificationService.UpdateListAsync(inputs);
        }

        
        [HttpPost]
        [Route("get-list-by-filter")]
        public async Task<List<NotificationDto>> GetListByFilter(NotificationFilter filter)
        {
            return await _notificationService.GetListByFilter(filter);
        }

        [HttpPost]
        [Route("count-unread-notification-of-user")]
        public async Task<int> CountUnreadNotificationOfUser(NotificationFilter filter)
        {
            return await _notificationService.CountUnreadNotificationOfUser(filter);
        }
    }
}