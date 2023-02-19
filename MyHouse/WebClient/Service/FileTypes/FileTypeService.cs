using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Contract.FileTypes;
using WebClient.RequestHttp;

namespace WebClient.Service.FileTypes
{
    public class FileTypeService
    {
        public FileTypeService()
        {
            
        }
        
        public async Task<List<FileTypeDto>> GetListAsync()
        {
            return await RequestClient.GetAPIAsync<List<FileTypeDto>>("file-type");
        }

        public async Task<FileTypeDto> CreateAsync(CreateUpdateFileTypeDto input)
        {
            return await RequestClient.PostAPIAsync<FileTypeDto>("file-type",input);

        }

        public async Task<FileTypeDto> UpdateAsync(CreateUpdateFileTypeDto input, Guid id)
        {
            return await RequestClient.PutAPIAsync<FileTypeDto>($"file-type/{id}" , input);

        }

        public async Task DeleteAsync(Guid id)
        { 
            await RequestClient.DeleteAPIAsync<Task>($"file-type/{id}");
        }
    }
}