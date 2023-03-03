using System;
using System.Threading.Tasks;
using Contract.DocumentFiles;
using Core.Enum;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace WebClient.Components
{
    public partial class RZPdfView4r 
    {
        [Parameter]
        public Guid FileID { get; set; }

        public DocumentFileDto DocumentFile { get; set; } = new DocumentFileDto();
        
        
        public RZPdfView4r()
        {
            
        }


        protected override async void OnAfterRender(bool firstRender)
        {
            if (firstRender)
            {
                await InvokeAsync(async () =>
                {
                    await GetDocumentFile();
                    StateHasChanged();
                }, ActionType.GetList, false);
            }
        }

        public async Task GetDocumentFile()
        {
            DocumentFile =  await _documentFileService.GetAsync(FileID);
        }

        async Task DownloadFile(string url,Guid documentFileId)
        {
            await JS.InvokeVoidAsync("downloadURI", url,DateTime.Now.Date.ToString());
            await _documentFileService.UpdateDownloadCountAsync(documentFileId);
            StateHasChanged();
        }
        
        async  Task PrintFile()
        {
            await JS.InvokeVoidAsync("printAsFile", "bv-199");
        }
    }
}