using System;
using System.Threading.Tasks;
using Application.Uploads;
using Castle.Core.Configuration;
using Contract;
using Contract.Uploads;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("api/upload/")]
    public class UploadController : IUploadService
    {
        private UploadService _uploadService;

        public UploadController(UploadService uploadService  )
        {
            _uploadService = uploadService;
        }

        [HttpPost]
        [Route("file")]
        public async Task<FileDto> UploadFile([FromForm] IFormFile file)
        {
            return await _uploadService.UploadFile(file);
        }

        [HttpPost]
        [Route("image")]
        public async Task<FileDto> UploadImage([FromForm] IFormFile file)
        {
            return await _uploadService.UploadImage(file);
        }

        [HttpPost]
        [Route("excel-file-of-users")]
        public async Task<FileDto> UploadExcelFileOfUsers(IFormFile file)
        {
            return await _uploadService.UploadExcelFileOfUsers(file);
        }

        [HttpPost]
        [Route("upload-document-file")]
        public async Task<FileDto> UploadDocumentFile(IFormFile file)
        {
            return await _uploadService.UploadDocumentFile(file);
        }
    }
}