using System;
using System.Threading.Tasks;
using Contract.Identity.UserManager;
using Core.Enum;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Radzen;

namespace WebClient.Pages.Admin
{
    public partial class Login
    {
        
        public Login()
        {
            
        }

        protected async override Task OnInitializedAsync()
        {
            if (await IsAuthenticatedAsync())
            {
                _navigationManager.NavigateTo("index");
            }
        }
        

        private void RedirectToRegister()
        {
            _navigationManager.NavigateTo("register");
        }
        
        async Task  OnLogin(LoginArgs args, string name)
        {

            var userModel = new UserModel() {UserName = args.Username, Password = args.Password};
            
            await InvokeAsync(async () =>
            {
                await  _userManagerService.SignInAsync(userModel);
                _navigationManager.NavigateTo("/",true);
            }, ActionType.SignIn, true);

        }
        
    }
}