using System;
using System.Threading.Tasks;
using Contract;
using Contract.Uploads;
using Core.Helper;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Volo.Abp.DependencyInjection;

namespace Application.Uploads
{
    public class UploadService : IUploadService,ITransientDependency
    {
        private IConfiguration _configuration;
        
        public UploadService(IConfiguration configuration)
        {
            _configuration = configuration;
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
            return FileHelper.UploadImage(file,_configuration["Media:BASE_IMAGE_PATH"],_configuration["Media:BASE_IMAGE_URL"]);
        }
    }
}