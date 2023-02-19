using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Application.FileForders;
using Application.FileTypes;
using Contract.FileTypes;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("api/file-type/")]
    [Authorize]
    public class FileTypeController
    {
        private FileTypeService _fileTyperService;
        
        public FileTypeController(FileTypeService fileFolderService)
        {
            _fileTyperService = fileFolderService;
        }
        
        [HttpPost]
        public async Task<FileTypeDto> CreateAsync(CreateUpdateFileTypeDto input)
        {
            return await _fileTyperService.CreateAsync(input);
        }

        [HttpPut]
        [Route("{id}")]
        public async Task<FileTypeDto> UpdateAsync(CreateUpdateFileTypeDto input, Guid id)
        {
            return  await _fileTyperService.UpdateAsync(input,id);
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task DeleteAsync(Guid id)
        {
            await _fileTyperService.DeleteAsync(id);
        }

        [HttpGet]
        public async Task<List<FileTypeDto>> GetListAsync()
        {
            return await _fileTyperService.GetListAsync();
        }
    }
}