using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Application.DocumentFiles;
using Contract.DocumentFiles;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("api/document-file/")]
    [Authorize]
    public class DocumentFileController : IDocumentFileService
    {
        private DocumentFileService _documentFileService;

        public DocumentFileController(DocumentFileService fileFolderService)
        {
            _documentFileService = fileFolderService;
        }

        
        [HttpPost]
        public async Task<DocumentFileDto> CreateAsync(CreateUpdateDocumentFileDto input)
        {
            return await _documentFileService.CreateAsync(input);
        }

        [HttpPut]
        [Route("{id}")]
        public async Task<DocumentFileDto> UpdateAsync(CreateUpdateDocumentFileDto input, Guid id)
        {
            return  await _documentFileService.UpdateAsync(input,id);
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task DeleteAsync(Guid id)
        {
            await _documentFileService.DeleteAsync(id);
        }

        [HttpPost]
        [Route("get-list-with-nav-properties")]
        public async Task<List<DocumentFileWithNavPropertiesDto>> GetListWithNavPropertiesAsync(DocumentFileFilter filter)
        {
           return await _documentFileService.GetListWithNavPropertiesAsync(filter);
        }

        [HttpGet]
        public async Task<List<DocumentFileDto>> GetListAsync()
        {
            return await _documentFileService.GetListAsync();
        }
    }
}