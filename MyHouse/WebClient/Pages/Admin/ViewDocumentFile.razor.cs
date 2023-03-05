using System;
using System.Threading.Tasks;
using Contract.DocumentFiles;
using Core.Enum;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace WebClient.Pages.Admin
{
    public partial class ViewDocumentFile 
    {
        
        [Parameter]
        [SupplyParameterFromQuery(Name = "fileId")]
        public Guid FileID { get; set; }

        public DocumentFileDto DocumentFile { get; set; } = new DocumentFileDto();
        
        
        public ViewDocumentFile()
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
            DocumentFile.DownloadCount += 1;
         
            await JS.InvokeVoidAsync("downloadURI", url,DateTime.Now.Date.ToString());
            await _documentFileService.UpdateDownloadCountAsync(documentFileId);
            StateHasChanged();
        }
        
        async  Task PrintFile(Guid documentFileId)
        {
            DocumentFile.PrintCount += 1;
            await _documentFileService.UpdatePrintCountAsync(documentFileId);
            await JS.InvokeVoidAsync("printAsFile", "bv-199");
        }
    }
}