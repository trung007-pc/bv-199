using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Application.SendingFiles;
using Contract.SendingFiles;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("api/sending-file/")]
    [Authorize]
    public class SendingFileController : ISendingFileService
    {
        private SendingFileService _sendingFileService;
        
        public SendingFileController(SendingFileService sendingFileService)
        {
            _sendingFileService = sendingFileService;
        }
        
        public Task<SendingFileDto> CreateAsync(CreateUpdateSendingFileDto input)
        {
            throw new NotImplementedException();
        }

        [HttpPut]
        [Route("{id}")]
        public async Task<SendingFileDto> UpdateAsync(CreateUpdateSendingFileDto input, Guid id)
        {
            return await _sendingFileService.UpdateAsync(input, id);
        }

        public Task DeleteAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        [HttpPost]
        [Route("create-list")]
        public Task<List<SendingFileDto>> CreateListAsync(List<CreateUpdateSendingFileDto> inputs)
        {
            throw new NotImplementedException();
        }
    }
}