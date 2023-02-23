using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Contract.SendingFiles;
using WebClient.RequestHttp;

namespace WebClient.Service.SendingFiles
{
    public class SendingFileService : ISendingFileService
    {
        
        public Task<SendingFileDto> CreateAsync(CreateUpdateSendingFileDto input)
        {
            throw new NotImplementedException();
        }

        public async Task<SendingFileDto> UpdateAsync(CreateUpdateSendingFileDto input, Guid id)
        {
            return await RequestClient.PutAPIAsync<SendingFileDto>($"sending-file/{id}",input);
        }

        public Task DeleteAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public async Task<List<SendingFileDto>> CreateListAsync(List<CreateUpdateSendingFileDto> inputs)
        {
            return await RequestClient.PostAPIAsync<List<SendingFileDto>>("sending-file/create-list",inputs);
        }

        public async Task<List<SendingFileDto>> SendNotificationForDepartmentUsersAndDefineUsers(SendingFileRequest request)
        {
            return await RequestClient.PostAPIAsync<List<SendingFileDto>>("sending-file/send-notifications-user",request);
        }
    }
}