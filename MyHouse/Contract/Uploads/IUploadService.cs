using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Contract.Uploads
{
    public interface IUploadService
    {
        public Task<FileDto> UploadFile(IFormFile file);
        public Task<FileDto>   UploadImage(IFormFile file);
        
        public Task<FileDto>   UploadExcelFileOfUsers(IFormFile file);

        public Task<FileDto> UploadDocumentFile(IFormFile file);


    }
}