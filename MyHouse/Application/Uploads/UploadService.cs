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
            var fileModel = await FileHelper.UploadFile(file, pathBase,new List<string>(){".jpg,.png"}, _configuration["Media:BASE_IMAGE_URL"]);
            return new FileDto(){FileName = fileModel.FileName , Path = fileModel.Path,Url = fileModel.Url};
        }

        public async Task<FileDto> UploadExcelFileOfUsers(IFormFile file)
        {
            string pathBase = Path.Combine(_environment.WebRootPath,_configuration["Media:BASE_EXCEL_USER_FILE_PATH"]);
            var fileModel = await FileHelper.UploadFile(file, pathBase,new List<string>(){".csv, application/vnd.openxmlformats-officedocument.spreadsheetml.sheet, application/vnd.ms-excel"});
            return new FileDto(){FileName = fileModel.FileName , Path = fileModel.Path,Url = fileModel.Url};
        }

        public async Task<FileDto> UploadDocumentFile(IFormFile file)
        {
            string pathBase = Path.Combine(_environment.WebRootPath,_configuration["Media:DOCUMENT_FILE"]);
            var fileModel = await FileHelper.UploadFile(file, pathBase,new List<string>()
            {
                ".xlsx,.pdf, application/vnd.ms-excel, application/vnd.openxmlformats-officedocument.spreadsheetml.sheet , application/msword, application/vnd.openxmlformats-officedocument.wordprocessingml.document"
            }, _configuration["Media:BASE_DOCUMENT_FILE_URL"]);
            return new FileDto(){FileName = fileModel.FileName , Path = fileModel.Path,Url = fileModel.Url,Extension= fileModel.Extension};
        }
    }
}