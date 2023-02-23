using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Contract.FileTypes;
using Contract.Notifications;
using Core.Const;
using Core.Exceptions;
using Domain.Identity.Users;
using Domain.Notifications;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SqlServ4r.Repository.Notifications;
using Volo.Abp.DependencyInjection;

namespace Application.Notifications
{
    public class NotificationService : ServiceBase,INotificationService,ITransientDependency
    {
        public NotificationRepository _notificationRepository;
        public UserManager<User> _userManager;
        public NotificationService(NotificationRepository notificationRepository,
            UserManager<User> userManager)
        {
            _notificationRepository = notificationRepository;
            _userManager = userManager;
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

        public async Task<List<NotificationDto>> GetListByFilter(NotificationFilter filter)
        {
            var notifications = new List<Notification>();
            var user  =  await _userManager.FindByNameAsync(filter.UserName);
            if (user is null) throw new GlobalException(HttpMessage.NotFound, HttpStatusCode.BadRequest);
            
            if (filter.Status == NotificationStatus.All)
            {
                notifications = await _notificationRepository.GetQueryable()
                    .Where(x => x.ReceiverId == user.Id).OrderBy(x => x.Status == false)
                    .ThenByDescending(x => x.SentDate).ToListAsync();
            }
            else
            {
                notifications = await _notificationRepository.GetQueryable()
                    .Where(x => x.ReceiverId == user.Id && x.Status == false)
                    .OrderBy(x=>x.SentDate)
                    .ToListAsync();
            }
           
            
            return ObjectMapper.Map<List<Notification>, List<NotificationDto>>(notifications);

        }

        public async Task<int> CountUnreadNotificationOfUser(NotificationFilter filter)
        {
            var notifications = new List<Notification>();
            var user  =  await _userManager.FindByNameAsync(filter.UserName);
            if (user is null) throw new GlobalException(HttpMessage.NotFound, HttpStatusCode.BadRequest);

           var count = await  _notificationRepository.
                GetQueryable().CountAsync(x =>x.ReceiverId == user.Id && x.Status == false);
           return count;    
        }
    }
}