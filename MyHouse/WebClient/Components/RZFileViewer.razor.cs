using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace WebClient.Components
{
    public partial class RZFileViewer
    {
        [Parameter]
        public string URL { get; set; }
        
        [Parameter]
        public string Extension { get; set; }
        
        [Parameter]
        public string Name { get; set; }
        
        
        async Task DownloadFile()
        {
            await JS.InvokeVoidAsync("downloadURI", URL,$"{Name}_{DateTime.Now.ToString()}");
            StateHasChanged();
        }
        
        async  Task PrintFile()
        {
            await JS.InvokeVoidAsync("printAsFile", "bv-199");
        }
    }
}