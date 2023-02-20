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
using SqlServ4r.Repository.FileDocuments;
using SqlServ4r.Repository.Notifications;
using SqlServ4r.Repository.SendingFiles;
using Volo.Abp.DependencyInjection;

namespace Application.SendingFiles
{
    public class SendingFileService : ServiceBase, ISendingFileService, ITransientDependency
    {
        private readonly SendingFileRepository _sendingFileRepository;
        private readonly NotificationRepository _notificationRepository;
        private readonly DocumentFileRepository _documentFileRepository;
        public SendingFileService(SendingFileRepository SendingFileRepository,
            NotificationRepository notificationRepository
            , DocumentFileRepository documentFileRepository)
        {
            _sendingFileRepository = SendingFileRepository;
            _notificationRepository = notificationRepository;
            _documentFileRepository = documentFileRepository;
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
            var sendingFiles = ObjectMapper.
                Map<List<CreateUpdateSendingFileDto>, List<SendingFile>>(inputs);

            await _sendingFileRepository.AddRangeAsync(sendingFiles);

            var fileIds = sendingFiles.Select(x => x.FileId);
            
            
            
            
            var notifications = new List<Notification>();

            foreach (var item in sendingFiles)
            {
                
                notifications.Add(new Notification()
                {
                  ReceiverId = item.ReceiverId,
                  DestinationCode = item.FileId,
                  Title = "You've just received documentary number:"
                });
            }

            await _notificationRepository.AddRangeAsync(notifications);
            
            
            
            return ObjectMapper.Map<List<SendingFile>,List<SendingFileDto>>(sendingFiles);
        }
    }
}