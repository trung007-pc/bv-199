using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Contract.FileTypes
{
    public interface IFileTypeService
    {
        Task<FileTypeDto> CreateAsync(CreateUpdateFileTypeDto input);
        Task<FileTypeDto> UpdateAsync(CreateUpdateFileTypeDto input,Guid id);
        Task DeleteAsync(Guid id);
        Task<List<FileTypeDto>> GetListAsync();
    }
}