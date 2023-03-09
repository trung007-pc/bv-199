using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;

namespace WebClient.Components
{
    public partial class RZModel
    {
        [Parameter] public RenderFragment ChildContent { get; set; }

        [Parameter] public string Width { get; set; }
        [Parameter] public string Height { get; set; }
        [Parameter] public string Style { get; set; }
        [Parameter] public string Title { get; set; }
        [Parameter] public bool ShowTitle { get; set; }


        public RZModel()
        {
          
        }

      
        
     




    }
}