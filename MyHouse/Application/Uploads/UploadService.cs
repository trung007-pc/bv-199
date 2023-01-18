using System;
using System.IO;
using System.Threading.Tasks;
using Contract;
using Contract.Uploads;
using Core.Helper;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Volo.Abp.DependencyInjection;

namespace Application.Uploads
{
    public class UploadService : IUploadService,ITransientDependency
    {
        private IConfiguration _configuration;
        private IWebHostEnvironment _environment;
        
        public UploadService(IConfiguration configuration, IWebHostEnvironment environment)
        {
            _configuration = configuration;
            _environment = environment;
        }

        public void InitConfig(IConfiguration configuration)
        {
            
        }


        public async Task<FileDto> UploadFile(IFormFile file)
        {
            throw new Exception();
        }

        public Task<FileDto> UploadImage(IFormFile file)
        {
            string pathBase = Path.Combine(_environment.WebRootPath,_configuration["Media:BASE_IMAGE_PATH"]);
            return FileHelper.UploadImage(file,pathBase,_configuration["Media:BASE_IMAGE_URL"]);
        }
    }
}