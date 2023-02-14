﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Contract.Uploads;

namespace Contract.Identity.UserManager
{
    public interface IUserManagerService
    {
        public Task<List<UserWithNavigationPropertiesDto>> GetListWithNavigationAsync();
        public Task<UserDto> CreateUserWithNavigationPropertiesAsync(CreateUserDto input);
        public Task<UserDto> UpdateUserWithNavigationPropertiesAsync(UpdateUserDto input,Guid id);
        
        public Task<UserValidatorExcel> CreateUsersFromCSVFileAndDefineRoles(FileDto file);
        public Task DeleteWithNavigationAsync(Guid id);
        public Task<List<UserDto>> GetListAsync();
        public Task<UserDto> CreateAsync(CreateUserDto input);
        public Task<UserDto> UpdateAsync(UpdateUserDto input,Guid id);
        public Task DeleteAsync(Guid id);
        public Task<TokenDto> SignInAsync(UserModel input);
        public Task<UserDto> SignUpAsync(CreateUserDto input);
        public Task<UserProfileModel> UpdateUserProfileAsync(UserProfileModel userProfileModel);
        public Task<UserPasswordUpdateModel> ChangePasswordAsync(UserPasswordUpdateModel user);
        public Task<UserDto> SetPasswordAsync(UserModel input);
        public Task<TokenDto> RefreshTokenAsync(TokenModel token);

        public void Logout();

    }
}