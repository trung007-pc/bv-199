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

        [HttpGet]   
        [Route("get-with-nav-properties/{id}")]
        public async Task<UserWithNavigationPropertiesDto> GetWithNavigationProperties(Guid id)
        {
            return await _userManagerService.GetWithNavigationProperties(id);
        }

        [HttpPost]
        [Route("create-user-with-roles")]
        public async Task<UserDto> CreateUserWithNavigationPropertiesAsync(CreateUserDto input)
        {
            return await _userManagerService.CreateUserWithNavigationPropertiesAsync(input);
        }
        
        [HttpPost]
        [Route("update-user-with-roles/{id}")]
        public async Task<UserDto> UpdateUserWithNavigationPropertiesAsync(UpdateUserDto input, Guid id)
        {
            return await _userManagerService.UpdateUserWithNavigationPropertiesAsync(input,id);
        }

        [HttpPost]
        [Route("create-users-from-csv-file")]
        [AllowAnonymous]
        public async Task<UserValidatorExcel> CreateUsersFromCSVFileAndDefineRoles(FileDto file)
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
        [Route("set-password")]
        public async Task<bool> SetNewPasswordAsync(NewUserPasswordDto input)
        {
            return await _userManagerService.SetNewPasswordAsync(input);

        }


        [HttpPost]
        [Route("update-profile")]
        public async Task<UserProfileModel> UpdateUserProfileAsync(UserProfileModel userProfileModel)
        {
            return await _userManagerService.UpdateUserProfileAsync(userProfileModel);
        }
        
        

        [HttpPost]
        [Route("refresh-token")]
        [AllowAnonymous]
        public async Task<TokenDto> RefreshTokenAsync(TokenModel token)
        {
            return await _userManagerService.RefreshTokenAsync(token);
        }
        
    }
}