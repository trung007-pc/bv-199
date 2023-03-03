using System.Threading.Tasks;
using Microsoft.JSInterop;

namespace WebClient.Components
{
    public partial class RZPdfViewer
    {

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
               await JS.InvokeVoidAsync("RunPdf","https://localhost:7093/StaticFiles/Document-Files/26-02-2023/20230226155548_20230222145519_Hieu-Minh-Le-Java-Full-stack-Engineer.pdf");
            }
        }


        public async Task Next()
        {
            await JS.InvokeVoidAsync("go_next");

        }
        
        public async Task Back()
        {
            await JS.InvokeVoidAsync("go_previous");

        }
        
        public async Task Rotate()
        {
            await JS.InvokeVoidAsync("routeNext");

        }
        public async Task ZoomIn()
        {
            await JS.InvokeVoidAsync("zoom_in");

        }
        
        public async Task ZoomOut()
        {
            await JS.InvokeVoidAsync("zoom_out");

        }
    }
}