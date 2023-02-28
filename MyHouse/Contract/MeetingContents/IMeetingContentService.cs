using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Contract.MeetingContents
{
    public interface IMeetingContentService
    {
        Task<MeetingContentDto> CreateAsync(CreateUpdateMeetingContentDto input);
        Task<MeetingContentDto> UpdateAsync(CreateUpdateMeetingContentDto input,Guid id);
        Task DeleteAsync(Guid id);
        Task<List<MeetingContentDto>> GetListAsync();
    }
}