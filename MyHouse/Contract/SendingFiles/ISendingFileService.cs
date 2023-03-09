using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Contract.SendingFiles
{
    public interface ISendingFileService
    {
        Task<SendingFileDto> CreateAsync(CreateUpdateSendingFileDto input);

        Task<SendingFileDto> UpdateAsync(CreateUpdateSendingFileDto input,Guid id);
        Task DeleteAsync(Guid id);
        
        Task<List<SendingFileDto>> CreateListAsync(List<CreateUpdateSendingFileDto> inputs);

        Task<List<SendingFileDto>> SendNotificationForDepartmentUsersAndDefineUsers(SendingFileRequest request);

        Task<SendingFileDto> GetAsync(Guid id);



    }
}