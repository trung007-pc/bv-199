using System.Threading.Tasks;
using Contract.Identity.UserManager;
using Core.Enum;

namespace WebClient.Pages.Admin
{
    public partial class Register
    {
        private CreateUserDto UserDto { get; set;} 
        private string Name { get; set; }
        
        public Register()
        {
            UserDto = new CreateUserDto();
        }

        protected override async Task OnInitializedAsync()
        {
           
        }

        public async Task HandleRegistration()
        {

        await  InvokeAsync(async () =>
         {
            var result = await _userManagerService.SignUpAsync(UserDto);
            _navigationManager.NavigateTo("/login",true);
         }, ActionType.SignUp, true);

        }
        
   

    }
}