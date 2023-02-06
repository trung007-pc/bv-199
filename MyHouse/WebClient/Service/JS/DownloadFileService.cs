using System;
using System.Threading.Tasks;
using Microsoft.JSInterop;

namespace WebClient.Service.JS
{
    public class DownloadFileService
    {
        private readonly IJSRuntime _jsInterop;
        
        public DownloadFileService(IJSRuntime jsInterop)
        {
            _jsInterop = jsInterop;
        }
        
        public async Task DownloadFileAsync(Byte[] bytes,string extension)
        {
            await _jsInterop.InvokeVoidAsync("saveAsFile", $"file-{DateTime.Now:yyyyMMddHHmmss}.{extension}", Convert.ToBase64String(bytes));
        }
    }
}