using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Blazored.LocalStorage;
using Contract;
using Contract.Identity.UserManager;
using Contract.Uploads;
using Microsoft.AspNetCore.Components.Authorization;
using WebClient.Identity;
using WebClient.RequestHttp;

namespace WebClient.Service.Users
{
    public class UserManagerService : IUserManagerService
    {
        private ILocalStorageService _localStorage;
        private readonly AuthenticationStateProvider  _authenticationStateProvider;


        public UserManagerService(
            ILocalStorageService localStorage,
            AuthenticationStateProvider  authenticationStateProvider)
        {
            _localStorage = localStorage;
            _authenticationStateProvider = authenticationStateProvider;
        }


        public async Task<List<UserWithNavigationPropertiesDto>> GetListWithNavigationAsync()
        {   
             return await RequestClient.GetAPIAsync<List<UserWithNavigationPropertiesDto>>("user/get-list-with-nav");
        }

        public async Task<UserDto> CreateUserWithNavigationPropertiesAsync(CreateUserDto input)
        {
            return await RequestClient.PostAPIAsync<UserDto>("user/create-user-with-roles", input);

        }

        public async Task<UserDto> UpdateUserWithNavigationPropertiesAsync(UpdateUserDto input, Guid id)
        {
            return await RequestClient.PostAPIAsync<UserDto>($"user/update-user-with-roles/{id}", input);
        }

        public Task<UserDto> UpdateUserWithRolesByPhoneNumberAsync(UpdateUserDto input, string phoneNumber)
        {
            throw new NotImplementedException();
        }
        
        public async Task<UserValidatorExcel> CreateUsersFromCSVFileAndDefineRoles(FileDto file)
        {
            return await RequestClient.PostAPIAsync<UserValidatorExcel>($"user/create-users-from-csv-file", file);
        }

        public async Task DeleteWithNavigationAsync(Guid id)
        {
             await RequestClient.PostAPIAsync<Task>($"user/delete-with-nav/{id}",null);
        }

        public async Task<List<UserDto>> GetListAsync()
        {
            return await RequestClient.GetAPIAsync<List<UserDto>>("user");
        }

        public async Task<UserDto> CreateAsync(CreateUserDto input)
        {
            return await RequestClient.PostAPIAsync<UserDto>("user",input);
        }

        public async Task<UserDto> UpdateAsync(UpdateUserDto input, Guid id)
        {
            return await RequestClient.PutAPIAsync<UserDto>($"user/{id}",input);

        }

        public async Task DeleteAsync(Guid id)
        {
             await RequestClient.DeleteAPIAsync<Task>($"user/{id}");
        }

        public async Task<TokenDto> SignInAsync(UserModel input)
        {
            var response = await RequestClient.PostAPIAsync<TokenDto>("user/sign-in", input);
            RequestClient.AttachToken(response.AccessToken);
                 RequestClient.SetLocalStorageService(_localStorage);
                await _localStorage.SetItemAsync("my-access-token", response.AccessToken);
                await _localStorage.SetItemAsync("my-refresh-token", response.RefreshToken);
                ((ApiAuthenticationStateProvider) _authenticationStateProvider).MarkUserAsAuthenticated(input.UserName);
       
            return response;
        }
        

        public async Task<UserDto> SignUpAsync(CreateUserDto input)
        {
            return await RequestClient.PostAPIAsync<UserDto>("user/sign-up", input);
        }



        public Task<UserProfileModel> UpdateUserProfileAsync(UserProfileModel userProfileModel)
        {
            throw new NotImplementedException();
        }

        public Task<UserPasswordUpdateModel> ChangePasswordAsync(UserPasswordUpdateModel user)
        {
            throw new NotImplementedException();
        }

        public Task<UserDto> SetPasswordAsync(UserModel input)
        {
            throw new NotImplementedException();
        }

        public Task<TokenDto> RefreshTokenAsync(TokenModel token)
        {
            throw new NotImplementedException();
        }

        public void Logout()
        {
            ((ApiAuthenticationStateProvider)_authenticationStateProvider).MarkUserAsLoggedOut();
        }


        public async Task test()
        { 
            await RequestClient.GetAPIAsync<Task>("test/alo");
        }
    }
}