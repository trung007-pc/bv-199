using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Hosting;
using WebClient.Setting;

namespace WebClient.Components
{
    public partial class PdfViewer : ComponentBase
    {
        
        public string TempPDfURL { get; set; }
        
        [Parameter]
        
        public IBrowserFile? File { get; set; }
        [Inject] private  IWebHostEnvironment _environment { get; set; }
        
        


        
        
        protected override async Task OnParametersSetAsync()
        {
            await MyFunctionAsync();
        }
        
        private async Task MyFunctionAsync()
        {
            
            string path = Path.Combine(_environment.WebRootPath,"temp.pdf");

            if (File != null)
            {
                using (var ms = new MemoryStream())
                {
                    await File.OpenReadStream(BlazorSetting.Document_FILE_LENGTH_LIMIT).
                        CopyToAsync(ms);
                    ms.Seek(0, SeekOrigin.Begin);
              
                    using (MemoryStream ms1 = new (ms.GetAllBytes()))
                    {
                        await System.IO.File.WriteAllBytesAsync(path, ms1.ToArray());
                    }
                    var iframeRandomId = Guid.NewGuid();

                    TempPDfURL = Path.Combine(_navigationManager.BaseUri,$"temp.pdf?id={iframeRandomId}");
                }
            }
            
        }
        
        
    }
}