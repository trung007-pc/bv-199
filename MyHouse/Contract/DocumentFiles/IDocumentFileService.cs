using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Contract.UnitTypes;

namespace Contract.DocumentFiles
{
    public interface IDocumentFileService
    {
        Task<List<DocumentFileWithNavPropertiesDto>> GetListWithNavPropertiesAsync(DocumentFileFilter filter);

        Task<List<DocumentFileDto>> GetListAsync();
        Task<List<DocumentFileWithNavPropertiesDto>> GetSharedListWithMeAsync(DocumentFileFilter filter);
        Task<DocumentFileDto> GetAsync(Guid id);
        Task<DocumentFileDto> CreateAsync(CreateUpdateDocumentFileDto input);
        Task<DocumentFileDto> UpdateAsync(CreateUpdateDocumentFileDto input,Guid id);
        Task DeleteAsync(Guid id);
        Task UpdateDownloadCountAsync(Guid id);
        Task UpdatePrintCountAsync(Guid id);

    }
}