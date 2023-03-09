using System;
using System.Collections;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Application.Helpers;
using Contract;
using Contract.Common.Excels;
using Contract.Identity.RoleManager;
using Contract.Identity.UserManager;
using Contract.Uploads;
using Core.Const;
using Core.Enum;
using Core.Exceptions;
using Domain.Identity.Roles;
using Domain.Identity.Users;
using Domain.UserDepartments;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using OfficeOpenXml;
using SqlServ4r.Repository.Departments;
using SqlServ4r.Repository.Positions;
using SqlServ4r.Repository.RoleClaims;
using SqlServ4r.Repository.UserDepartments;
using SqlServ4r.Repository.UserRoles;
using SqlServ4r.Repository.Users;
using Volo.Abp.DependencyInjection;

namespace Application.Identity.UserManager
{
    public partial class UserManagerService : ServiceBase, IUserManagerService, ITransientDependency
    {
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<Role> _roleManager;
        private readonly RoleClaimRepository _roleClaimRepository;
        private readonly IConfiguration _configuration;
        private readonly UserRoleRepository _userRoleRepository;
        private readonly UserRepository _userRepository;
        private readonly UserDepartmentRepository _userDepartmentRepository;
        private readonly PositionRepository _positionRepository;
        private readonly DepartmentRepository _departmentRepository;

        public UserManagerService(UserManager<User> userManager,
            RoleManager<Role> roleManager,
            RoleClaimRepository roleClaimRepository,
            UserRoleRepository userRoleRepository,
            UserRepository userRepository,
            UserDepartmentRepository userDepartmentRepository,
            PositionRepository positionRepository,
            DepartmentRepository departmentRepository,
            IConfiguration configuration
        )
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _configuration = configuration;
            _roleClaimRepository = roleClaimRepository;
            _userRoleRepository = userRoleRepository;
            _userRepository = userRepository;
            _userDepartmentRepository = userDepartmentRepository;
            _positionRepository = positionRepository;
            _departmentRepository = departmentRepository;
        }

        public async Task<List<UserWithNavigationPropertiesDto>> GetListWithNavigationAsync()
        {
            var users = await _userRepository.GetListWithNavigationProperties();
            return ObjectMapper.Map<List<UserWithNavigationProperties>,List<UserWithNavigationPropertiesDto>>(users);
        }

        public async Task<UserWithNavigationPropertiesDto> GetWithNavigationProperties(Guid id)
        {
           return  ObjectMapper.Map<UserWithNavigationProperties,UserWithNavigationPropertiesDto>(await _userRepository.GetWithNavigationProperties(id));
        }

        public async Task<UserDto> CreateUserWithNavigationPropertiesAsync(CreateUserDto input)
        {
            var dto = await CreateAsync(input);
            await UpdateRolesForUser(input.UserName, input.Roles);
            var userDepartments = new List<UserDepartment>();
            foreach (var item in input.DepartmentIds)
            {
                userDepartments.Add(new UserDepartment(){UserId = dto.Id,DepartmentId = item});
            }
            await _userDepartmentRepository.AddRangeAsync(userDepartments);
            
            return dto;
        }


        public async Task<UserDto> UpdateUserWithNavigationPropertiesAsync(UpdateUserDto input, Guid id)
        {
            var item = await _userManager.FindByIdAsync(id.ToString());
            
            if (item == null)
            {
                throw new GlobalException(HttpMessage.NotFound, HttpStatusCode.BadRequest);
            }
            
            var user = ObjectMapper.Map(input, item);
            
            var result = await _userManager.UpdateAsync(user);
            
            if (!result.Succeeded)
            {
                throw new GlobalException(result.Errors?.FirstOrDefault().Description, HttpStatusCode.BadRequest);
            }
            
            
            if (input.IsSetPassword)
            {
                var token = await _userManager.GeneratePasswordResetTokenAsync(user);
                var reuslt = await _userManager.ResetPasswordAsync(user, token, input.Password);
                if (!reuslt.Succeeded)
                {
                    throw new GlobalException(reuslt.Errors?.FirstOrDefault().Description, HttpStatusCode.BadRequest);
                }
            }
            
            await UpdateRolesForUser(user.UserName, input.Roles);
            await _updateUserDepartmentForUser(user.Id,input.DepartmentIds);
            return ObjectMapper.Map<User,UserDto>(item);
        }

  


        public async Task DeleteWithNavigationAsync(Guid id)
        {
            var item = await _userManager.FindByIdAsync(id.ToString());
            if (item == null) throw new GlobalException(HttpMessage.NotFound, HttpStatusCode.BadRequest);


            item.IsActive = false;
            var userResult = await _userManager.UpdateAsync(item);
            if (!userResult.Succeeded)
            {
                throw new GlobalException(HttpMessage.CheckInformation, HttpStatusCode.BadRequest);
            }
        }

        public async Task<List<UserDto>> GetListAsync()
        {
            var users = await _userManager.Users.ToListAsync();
            
            return ObjectMapper.Map<List<User>, List<UserDto>>(users);
        }

        public async Task<UserDto> CreateAsync(CreateUserDto input)
        {
            (input.PhoneNumber, input.EmployeeCode, input.Email) =
                TrimText(input.PhoneNumber, input.EmployeeCode, input.Email);
            
            var user = ObjectMapper.Map<CreateUserDto, User>(input);
            
            var result = await _userManager.CreateAsync(user, input.Password);
            if (!result.Succeeded)
            {
                throw new GlobalException(result.Errors?.FirstOrDefault().Description, HttpStatusCode.BadRequest);
            }

            return ObjectMapper.Map<User, UserDto>(user);
        }
        
        

        public async Task<UserDto> UpdateAsync(UpdateUserDto input, Guid id)
        {
            
            (input.PhoneNumber, input.EmployeeCode, input.Email) =
                TrimText(input.PhoneNumber, input.EmployeeCode, input.Email);
            
            var item = await _userManager.FindByIdAsync(id.ToString());

            if (item == null)
            {
                throw new GlobalException(HttpMessage.NotFound, HttpStatusCode.BadRequest);
            }
            
            var user = ObjectMapper.Map(input, item);
            var result = await _userManager.UpdateAsync(user);
            if (!result.Succeeded)
            {
                throw new GlobalException(result.Errors?.FirstOrDefault().Description, HttpStatusCode.BadRequest);
            }

            return ObjectMapper.Map<User, UserDto>(user);
            ;
        }

        private (string Phone, string Code, string? Email) TrimText(string phone, string code, string email)
        {
            return (phone.Trim(), code.Trim(), email?.Trim());
        }



        public async Task DeleteAsync(Guid id)
        {
            var item = await _userManager.FindByIdAsync(id.ToString());
            if (item == null)
            {
                throw new GlobalException(HttpMessage.NotFound, HttpStatusCode.BadRequest);
            }
            item.IsDelete = true;
            await _userRepository.UpdateAsync(item);
        }

        public async Task<TokenDto> SignInAsync(UserModel input)
        {
            var user = await _userManager.FindByNameAsync(input.UserName);
            if (user == null|| !user.IsActive|| user.IsDelete)
            {
                throw new GlobalException(HttpMessage.CheckInformation, HttpStatusCode.BadRequest);
            }


            var result = await _userManager.CheckPasswordAsync(user, input.Password);
            if (!result)
            {
                throw new GlobalException(HttpMessage.CheckInformation, HttpStatusCode.BadRequest);
            }

            string accessToken = await GenerateTokenByUser(user);
            string refreshToken = GenerateRefreshToken();

            user.RefreshToken = refreshToken; await _userManager.UpdateAsync(user);

            return new TokenDto() {AccessToken = accessToken, RefreshToken = refreshToken};
        }


        public async Task<UserDto> SignUpAsync(CreateUserDto input)
        {
            var user = ObjectMapper.Map<CreateUserDto, User>(input);

            var result = await _userManager.CreateAsync(user, input.Password);
            if (!result.Succeeded)
            {
                throw new GlobalException(result.Errors.FirstOrDefault().Description, HttpStatusCode.BadRequest);
            }

            return ObjectMapper.Map<User, UserDto>(user);
        }

        public async Task<bool> SetNewPasswordAsync(NewUserPasswordDto input)
        {
            var user = await _userManager.FindByNameAsync(input.UserName);
            if (user == null)
            {
                throw new GlobalException(HttpMessage.CheckInformation, HttpStatusCode.BadRequest);
            }
            
            var token  = await _userManager.GeneratePasswordResetTokenAsync(user);
            var result =   await _userManager.ResetPasswordAsync(user, token, input.NewPassword);
            if (!result.Succeeded)
            {
                throw new GlobalException(result.Errors.FirstOrDefault().Description, HttpStatusCode.BadRequest);
            }
            
            return true;
        }

        public async Task UpdateRolesForUser(string userName, List<string> roles)
        {
            var user = await _userManager.FindByNameAsync(userName);
            var oldRoles = await _userManager.GetRolesAsync(user);
            await _userManager.RemoveFromRolesAsync(user, oldRoles);
            await _userManager.AddToRolesAsync(user, roles);
        }

        private async Task _updateUserDepartmentForUser(Guid userId,List<Guid> newDepartmentId)
        {
           var oldDepartmentUsers = await _userDepartmentRepository
               .GetListAsync(x => x.UserId == userId);
           _userDepartmentRepository.RemoveRange(oldDepartmentUsers);
           
           var newUserDepartments = new List<UserDepartment>();
           foreach (var item in newDepartmentId)
           {
               newUserDepartments.Add(new UserDepartment(){UserId = userId,DepartmentId = item});
           }

           await _userDepartmentRepository.AddRangeAsync(newUserDepartments);
        }

        public Task<UserProfileModel> UpdateUserProfileAsync(UserProfileModel userProfileModel)
        {
            throw new NotImplementedException();
        }

    
        


        public async Task<TokenDto> RefreshTokenAsync(TokenModel token)
        {
            
                if (token is null) throw new GlobalException(HttpMessage.Unauthorized, HttpStatusCode.Unauthorized);


                var principal = GetPrincipalFromExpiredToken(token.AccessToken);
                var userName = principal.Identity.Name;

                var user = await _userManager.FindByNameAsync(userName);

                if (user == null || user.RefreshToken != token.RefreshToken)
                    throw new GlobalException(HttpMessage.Unauthorized, HttpStatusCode.Unauthorized);

                var refreshToken = GenerateRefreshToken();
                var accessToken = await GenerateTokenByUser(user);
                user.RefreshToken = refreshToken;
                var result = await _userManager.UpdateAsync(user);

                if (!result.Succeeded)
                {
                     throw new GlobalException(HttpMessage.Conflict, HttpStatusCode.TooManyRequests);
                }

                return new TokenDto()
                {
                    AccessToken = accessToken,
                    RefreshToken = refreshToken
                };
        }

        public void Logout()
        {
            throw new NotImplementedException();
        }

        private async Task<string> GenerateTokenByUser(User user)
        {
            var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var signinCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);
            var userRoles = await _userManager.GetRolesAsync(user);
            var userRole = await _userRoleRepository.GetListAsync(x => x.UserId == user.Id);

            var userRoleClaims = _roleClaimRepository.GetRoleClaimsByRoles(userRole.Select(x => x.RoleId).ToList());
            var userClaims = userRoleClaims.Select(x => new Claim(x.ClaimType, x.ClaimValue));

            List<Claim> claims = new List<Claim>();

            claims.AddRange(userClaims);
            foreach (var item in userRoles)
            {
                claims.Add(new Claim(ClaimTypes.Role, item));
            }
            
            claims.Add(new Claim(ClaimTypes.PrimarySid, user.Id.ToString()));
            claims.Add(new Claim(ClaimTypes.Name, user.UserName));
            claims.Add(new Claim(ClaimTypes.Surname,user.FirstName +" "+ user.LastName));

            var tokeOptions = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.Now.AddHours(4),
                signingCredentials: signinCredentials
            );
            var tokenString = new JwtSecurityTokenHandler().WriteToken(tokeOptions);

            return tokenString;
        }

        public string GenerateRefreshToken()
        {
            var randomNumber = new byte[32];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(randomNumber);
                return Convert.ToBase64String(randomNumber);
            }
        }

        public ClaimsPrincipal GetPrincipalFromExpiredToken(string token)
        {
            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateAudience =
                    false, //you might want to validate the audience and issuer depending on your use case
                ValidateIssuer = false,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"])),
                ValidateLifetime = false //here we are saying that we don't care about the token's expiration date
            };
            var tokenHandler = new JwtSecurityTokenHandler();
            SecurityToken securityToken;
            var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out securityToken);
            var jwtSecurityToken = securityToken as JwtSecurityToken;
            if (jwtSecurityToken == null || !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256,
                StringComparison.InvariantCultureIgnoreCase))
                throw new SecurityTokenException("Invalid token");
            return principal;
        }
    }
}