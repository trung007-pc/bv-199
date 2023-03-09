using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Contract.MeetingContents;
using WebClient.RequestHttp;

namespace WebClient.Service.MeetingContents
{
    public class MeetingContentService : IMeetingContentService
    {
        public async Task<List<MeetingContentDto>> GetListAsync()
        {
            return await RequestClient.GetAPIAsync<List<MeetingContentDto>>("meeting-content");

        }

        public async Task<MeetingContentDto> CreateAsync(CreateUpdateMeetingContentDto input)
        {
            return await RequestClient.PostAPIAsync<MeetingContentDto>("meeting-content",input);
        }

        public async  Task<MeetingContentDto> UpdateAsync(CreateUpdateMeetingContentDto input, Guid id)
        {
            return await RequestClient.PutAPIAsync<MeetingContentDto>($"meeting-content/{id}",input);
        }

        public async Task DeleteAsync(Guid id)
        {
            await RequestClient.DeleteAPIAsync<Task>($"meeting-content/{id}");
        }
    }
}