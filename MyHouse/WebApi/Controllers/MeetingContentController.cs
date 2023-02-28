using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Application.MeetingContents;
using Contract.MeetingContents;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("api/meeting-content/")]
    [Authorize]
    public class MeetingContentController : IMeetingContentService
    {
        
        private readonly MeetingContentService _meetingContentService;
        
        public MeetingContentController(MeetingContentService meetingContentService)
        {
            _meetingContentService = meetingContentService;
        }
        
        
        [HttpGet]
        public async Task<List<MeetingContentDto>> GetListAsync()
        {
            return await _meetingContentService.GetListAsync();
        } 

        [HttpPost]
        public async Task<MeetingContentDto> CreateAsync(CreateUpdateMeetingContentDto input)
        {
            return await _meetingContentService.CreateAsync(input);

        }

        [HttpPut]
        [Route("{id}")]
        public async Task<MeetingContentDto> UpdateAsync(CreateUpdateMeetingContentDto input, Guid id)
        {
            return await _meetingContentService.UpdateAsync(input,id);

        }

        [HttpDelete]
        [Route("{id}")]
        public async Task DeleteAsync(Guid id)
        {
            await _meetingContentService.DeleteAsync(id);
        }
    }
}