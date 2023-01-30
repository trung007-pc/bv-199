using System;
using System.Collections.Generic;
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
        private readonly IConfiguration _configuration;
        private readonly IWebHostEnvironment _environment;
        
        public UploadService(IConfiguration configuration, IWebHostEnvironment environment)
        {
            _configuration = configuration;
            _environment = environment;
        }

        public  (double latitude, double longitude) InitConfig(IConfiguration configuration)
        {
            (double latitude, double longitude) coordinates = (55.39594, 10.38831);

            return coordinates;
        }


        public async Task<FileDto> UploadFile(IFormFile file)
        {
           
            
            throw new Exception();
        }

        public async Task<FileDto> UploadImage(IFormFile file)
        {
            string pathBase = Path.Combine(_environment.WebRootPath,_configuration["Media:BASE_IMAGE_PATH"]);
            var fileModel = await FileHelper.UploadImage(file, pathBase, _configuration["Media:BASE_IMAGE_URL"]);
            return new FileDto(){FileName = fileModel.FileName , Path = fileModel.Path,Url = fileModel.Url};
        }
    }
}