using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using Application.Identity.UserManager;
using Contract;
using Contract.Identity.UserManager;
using Contract.Uploads;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("api/user/")]
    [Authorize]
    public class UserController : IUserManagerService
    {
        private UserManagerService _userManagerService;

        public UserController(UserManagerService userManagerService)
        {
            _userManagerService = userManagerService;
        }

        [HttpGet]   
        [Route("get-list-with-nav")]
        public async Task<List<UserWithNavigationPropertiesDto>> GetListWithNavigationAsync()
        {
           return  await _userManagerService.GetListWithNavigationAsync();
        }

        [HttpPost]
        [Route("create-user-with-roles")]
        public async Task<UserDto> CreateUserWithRolesAsync(CreateUserDto input)
        {
            return await _userManagerService.CreateUserWithRolesAsync(input);
        }
        
        [HttpPost]
        [Route("update-user-with-roles/{id}")]
        public async Task<UserDto> UpdateUserWithRolesAsync(UpdateUserDto input, Guid id)
        {
            return await _userManagerService.UpdateUserWithRolesAsync(input,id);
        }

        [HttpPost]
        [Route("update-user-with-roles-by-phone-number/{phoneNumber}")]
        public Task<UserDto> UpdateUserWithRolesByPhoneNumberAsync(UpdateUserDto input, string phoneNumber)
        {
            throw new NotImplementedException();
        }

        [HttpPost]
        [Route("create-users-from-csv-file")]
        [AllowAnonymous]
        public async Task<ExcelValidator> CreateUsersFromCSVFileAndDefineRoles(FileDto file)
        {
            return await _userManagerService.CreateUsersFromCSVFileAndDefineRoles(file);
        }


        [HttpPost]
        [Route("delete-with-nav")]
        public async  Task DeleteWithNavigationAsync(Guid id)
        {
             await _userManagerService.DeleteWithNavigationAsync(id);
        }

        [HttpGet]
        public async Task<List<UserDto>> GetListAsync()
        {
            return await _userManagerService.GetListAsync();
        }

        [HttpPost]
        public async Task<UserDto> CreateAsync(CreateUserDto input)
        {
            return await _userManagerService.CreateAsync(input);
        }

        [HttpPut]
        [Route("{id}")]
        public async Task<UserDto> UpdateAsync(UpdateUserDto input, Guid id)
        {
            return await _userManagerService.UpdateAsync(input, id);
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task DeleteAsync(Guid id)
        {
             await _userManagerService.DeleteAsync(id);
        }
        
        
        [HttpPost]
        [Route("sign-in")]
        [AllowAnonymous]
        public async Task<TokenDto> SignInAsync(UserModel input)
        {
            return await _userManagerService.SignInAsync(input);
        }
        

        [HttpPost]
        [Route("sign-up")]
        [AllowAnonymous]
        public async Task<UserDto> SignUpAsync(CreateUserDto input)
        {
            return await _userManagerService.SignUpAsync(input);
        }

        
        [HttpPost]
        [Route("update-profile")]
        public async Task<UserProfileModel> UpdateUserProfileAsync(UserProfileModel userProfileModel)
        {
            return await _userManagerService.UpdateUserProfileAsync(userProfileModel);
        }

        [HttpPost]
        [Route("change-password")]
        public async Task<UserPasswordUpdateModel> ChangePasswordAsync(UserPasswordUpdateModel user)
        {
            return await _userManagerService.ChangePasswordAsync(user);
        }
        

        [HttpPost]
        [Route("set-password")]
        public  async Task<UserDto> SetPasswordAsync(UserModel input)
        {
            return await _userManagerService.SetPasswordAsync(input);
        }

        [HttpPost]
        [Route("refresh-token")]
        [AllowAnonymous]
        public async Task<TokenDto> RefreshTokenAsync(TokenModel token)
        {
            return await _userManagerService.RefreshTokenAsync(token);
        }

        
        [HttpPost]
        [Route("upload-file")]
        public bool UploadFile()
        {
            return true;
        }
        
        [HttpGet]
        [Route("xxxxx")]
        public void Logout()
        {
            throw new NotImplementedException();
        }
    }
}