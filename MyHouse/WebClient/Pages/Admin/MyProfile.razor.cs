using System.Threading.Tasks;
using Contract.Identity.UserManager;
using Core.Enum;
using Radzen;

namespace WebClient.Pages.Admin
{
    public partial class MyProfile
    {



        public MudBlazor.Position Position { get; set; } = MudBlazor.Position.Left;

        public UserWithNavigationPropertiesDto UserDto { get; set; } = new UserWithNavigationPropertiesDto();
        public NewUserPasswordDto NewPassword { get; set; } = new NewUserPasswordDto();


        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                UserDto = await _userDtoManagerService.GetWithNavigationProperties(await GetUserIdAsync());
                StateHasChanged();
            }
        }

        public async Task ResetPassword()
        {
            await InvokeAsync(async () =>
            {
                NewPassword.UserName = await GetUserNameAsync();
                await _userDtoManagerService.SetNewPasswordAsync(NewPassword);
            },ActionType.Reset,true);

            NewPassword = new NewUserPasswordDto();
        }
     
    }
}