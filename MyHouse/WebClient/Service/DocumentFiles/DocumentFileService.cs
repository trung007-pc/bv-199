﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Contract.DocumentFiles;
using WebClient.RequestHttp;

namespace WebClient.Service.DocumentFiles
{
    public class DocumentFileService :  IDocumentFileService
    {
        public DocumentFileService()
        {
            
        }

        public async Task<List<DocumentFileWithNavPropertiesDto>> GetListWithNavPropertiesAsync(DocumentFileFilter filter)
        {
            return await RequestClient.PostAPIAsync<List<DocumentFileWithNavPropertiesDto>>("document-file/get-list-with-nav-properties",filter);

        }

        public async Task<List<DocumentFileDto>> GetListAsync()
        {
            return await RequestClient.GetAPIAsync<List<DocumentFileDto>>("document-file");
        }

        public async Task<DocumentFileDto> CreateAsync(CreateUpdateDocumentFileDto input)
        {
            return await RequestClient.PostAPIAsync<DocumentFileDto>("document-file",input);

        }

        public async Task<DocumentFileDto> UpdateAsync(CreateUpdateDocumentFileDto input, Guid id)
        {
            return await RequestClient.PutAPIAsync<DocumentFileDto>($"document-file/{id}" , input);

        }

        public async Task DeleteAsync(Guid id)
        { 
            await RequestClient.DeleteAPIAsync<Task>($"document-file/{id}");
        }

        public async Task UpdateDownloadCountAsync(Guid id)
        {
            await RequestClient.PostAPIAsync<Task>($"document-file/update-download-count/{id}",null);
        }

        public async Task UpdatePrintCountAsync(Guid id)
        {
            await RequestClient.PostAPIAsync<Task>($"document-file/{id}",null);
        }
    }
}