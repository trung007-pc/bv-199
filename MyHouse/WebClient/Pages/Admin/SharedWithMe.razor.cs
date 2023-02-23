using System.Collections.Generic;
using System.Threading.Tasks;
using Contract.DocumentFiles;

namespace WebClient.Pages.Admin
{
    public partial class SharedWithMe
    {
        public List<DocumentFileWithNavPropertiesDto> DocumentFileWithNavProperties { get; set; } = new List<DocumentFileWithNavPropertiesDto>();
        public string HeaderTitle = "Shared With Me";

        
        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                await GetDocumentFiles();
                StateHasChanged();
            }
        }
        
        public async Task GetDocumentFiles()
        {
            var userId = await GetUserIdAsync();
            DocumentFileWithNavProperties = await _documentFileService.GetSharedListWithMeAsync(userId);
        }
        
        
    }
}