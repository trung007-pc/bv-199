using System.Threading.Tasks;
using Contract.Identity.UserManager;
using Core.Enum;

namespace WebClient.Pages.Admin
{
    public partial class Login
    {
        
        private UserModel UserModel { get; set; } = new UserModel();


        public Login()
        {
            
        }

        private async Task SignIn()
        {
            await InvokeAsync(async () =>
            {
                await  _userManagerService.SignInAsync(UserModel);
                _navigationManager.NavigateTo("/",true);
            }, ActionType.SignIn, true);
   
            
        }

        private void RedirectToRegister()
        {
            _navigationManager.NavigateTo("register");
        }
    }
}