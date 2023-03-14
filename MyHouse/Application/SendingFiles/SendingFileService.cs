using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Contract.SendingFiles;
using Contract.UnitReviews;
using Core.Const;
using Core.Exceptions;
using Domain.Notifications;
using Domain.SendingFiles;
using Domain.UserDepartments;
using SqlServ4r.Repository.FileDocuments;
using SqlServ4r.Repository.Notifications;
using SqlServ4r.Repository.SendingFiles;
using SqlServ4r.Repository.UserDepartments;
using Volo.Abp.DependencyInjection;

namespace Application.SendingFiles
{
    public class SendingFileService : ServiceBase, ISendingFileService, ITransientDependency
    {
        private readonly SendingFileRepository _sendingFileRepository;
        private readonly NotificationRepository _notificationRepository;
        private readonly DocumentFileRepository _documentFileRepository;
        private readonly UserDepartmentRepository _userDepartmentRepository;
        public SendingFileService(SendingFileRepository SendingFileRepository,
            NotificationRepository notificationRepository
            , DocumentFileRepository documentFileRepository,
            UserDepartmentRepository userDepartmentRepository)
        {
            _sendingFileRepository = SendingFileRepository;
            _notificationRepository = notificationRepository;
            _documentFileRepository = documentFileRepository;
            _userDepartmentRepository = userDepartmentRepository;
        }
        public Task<SendingFileDto> CreateAsync(CreateUpdateSendingFileDto input)
        {
            throw new NotImplementedException();

        }

        public async Task<SendingFileDto> UpdateAsync(CreateUpdateSendingFileDto input, Guid id)
        {
            var item = await _sendingFileRepository
                .FirstOrDefaultAsync(x => x.Id == id);
            if (item == null) throw new GlobalException(HttpMessage.NotFound, HttpStatusCode.BadRequest);
            var sendingFile = ObjectMapper.Map(input,item);
            await _sendingFileRepository.UpdateAsync(sendingFile);
            return ObjectMapper.Map<SendingFile,SendingFileDto>(sendingFile);
        }

        public Task DeleteAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public async Task<List<SendingFileDto>> CreateListAsync(List<CreateUpdateSendingFileDto> inputs)
        {
            
            return null;
        }

        public async Task<List<SendingFileDto>> SendNotificationForDepartmentUsersAndDefineUsers(SendingFileRequest request)
        {

            var userDepartments = await _userDepartmentRepository.GetListAsync(x => request
                .DepartmentIds.Contains(x.DepartmentId));
            var userIds = userDepartments.Select(x => x.UserId).ToList();
            userIds.AddRange(request.DefineUsers);
            userIds = userIds.Where(x => x != request.Sender).Distinct().ToList();
            
            
            var sendingFiles = new List<SendingFile>();
            
            foreach (var item in userIds)
            {
                sendingFiles.Add(new SendingFile()
                {
                    Status = false,
                    FileId = request.FileId,
                    IsRevoked = false,
                    SentDate = DateTime.Now,
                    ReceiverId = item,
                    SenderId = request.Sender
                });
            }
            
            await _sendingFileRepository.AddRangeAsync(sendingFiles);
            var notifications = new List<Notification>();
            var file = await _documentFileRepository
                .FirstOrDefaultAsync(x => x.Id == request.FileId);
            foreach (var item in sendingFiles)
            {
                
                notifications.Add(new Notification()
                {
                    ReceiverId = item.ReceiverId,
                    DestinationCode = item.Id,
                    Title = $"{file?.Code}"
                            
                });
            }

            await _notificationRepository.AddRangeAsync(notifications);

            return ObjectMapper.Map<List<SendingFile>, List<SendingFileDto>>(sendingFiles);
        }

        public async Task<SendingFileDto> GetAsync(Guid id)
        {
           
            var item = await _sendingFileRepository
                .FirstOrDefaultAsync(x => x.Id == id);
            if (item == null) throw new GlobalException(HttpMessage.NotFound, HttpStatusCode.BadRequest);
            return ObjectMapper.Map<SendingFile,SendingFileDto>(item);
        }
    }
}