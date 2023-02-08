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
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using OfficeOpenXml;
using SqlServ4r.Repository.RoleClaims;
using SqlServ4r.Repository.UserRoles;
using SqlServ4r.Repository.Users;
using Volo.Abp.DependencyInjection;

namespace Application.Identity.UserManager
{
    public class UserManagerService : ServiceBase, IUserManagerService, ITransientDependency
    {
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<Role> _roleManager;
        private readonly RoleClaimRepository _roleClaimRepository;
        private readonly IConfiguration _configuration;
        private readonly UserRoleRepository _userRoleRepository;
        private readonly UserRepository _userRepository;
        

        public UserManagerService(UserManager<User> userManager,
            RoleManager<Role> roleManager,
            RoleClaimRepository roleClaimRepository,
            UserRoleRepository userRoleRepository,
            UserRepository userRepository,
            IConfiguration configuration
        )
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _configuration = configuration;
            _roleClaimRepository = roleClaimRepository;
            _userRoleRepository = userRoleRepository;
            _userRepository = userRepository;
        }

        public async Task<List<UserWithNavigationPropertiesDto>> GetListWithNavigationAsync()
        {
            var users = await _userManager.Users.ToListAsync();
            var usersWithNav = new List<UserWithNavigationPropertiesDto>();
            var count = 1;
            foreach (var item in users)
            {
                var roleNames = (List<string>) await _userManager.GetRolesAsync(item);
                count++;
                usersWithNav.Add(new UserWithNavigationPropertiesDto()
                    {RoleNames = roleNames, Index = count, UserDto = ObjectMapper.Map<User, UserDto>(item)});
            }

            return usersWithNav;
        }

        public async Task<UserDto> CreateUserWithRolesAsync(CreateUserDto input)
        {
            var dto = await CreateAsync(input);
            await UpdateRolesForUser(input.UserName, input.Roles);
            return dto;
        }


        public async Task<UserDto> UpdateUserWithRolesAsync(UpdateUserDto input, Guid id)
        {
            var item = await _userManager.FindByIdAsync(id.ToString());

            if (item == null)
            {
                throw new GlobalException(HttpMessage.NotFound, HttpStatusCode.BadRequest);
            }

            await _checkDuplicateByUpdating(input, id);
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
                if (!result.Succeeded)
                {
                    throw new GlobalException(result.Errors?.FirstOrDefault().Description, HttpStatusCode.BadRequest);
                }
            }


            await UpdateRolesForUser(user.UserName, input.Roles);

            return ObjectMapper.Map<User, UserDto>(item);
        }

        public async Task<UserDto> UpdateUserWithRolesByPhoneNumberAsync(UpdateUserDto input, string phoneNumber)
        {
            var item = await _userRepository.FirstOrDefaultAsync(x => x.PhoneNumber == phoneNumber);

            if (item == null)
            {
                throw new GlobalException(HttpMessage.NotFound, HttpStatusCode.BadRequest);
            }

            await _checkDuplicateByUpdating(input, item.Id);
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
                if (!result.Succeeded)
                {
                    throw new GlobalException(result.Errors?.FirstOrDefault().Description, HttpStatusCode.BadRequest);
                }
            }


            await UpdateRolesForUser(user.UserName, input.Roles);

            return ObjectMapper.Map<User, UserDto>(item);
        }

        public async Task<ExcelValidator> CreateUsersFromCSVFileAndDefineRoles(FileDto file)
        {
            if (!File.Exists(file.Path))
            {
                throw new GlobalException(HttpMessage.NotFound, HttpStatusCode.BadRequest);
            }

            var users = await _userRepository.ToListAsync();
            var validUsers = new List<CreateUpdateUseDto>();
            var userRows = new List<UserExcelDto>();


            var excelValidator = new ExcelValidator();

            FileInfo existingFile = new FileInfo(file.Path);
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

            using (ExcelPackage package = new ExcelPackage(existingFile))
            {
                ExcelWorksheet worksheet = package.Workbook.Worksheets[0];

                if (worksheet.Dimension is null)
                {
                    throw new GlobalException(HttpMessage.EmptyContent, HttpStatusCode.BadRequest);
                }

                int rowCount = worksheet.Dimension.End.Row;

                for (int row = 2; row <= rowCount; row++)
                {
                    var userExcel = _readUserRowFromExcel(row, worksheet);
                    if (!_isNullExcelRow(userExcel)) break;
                    userRows.Add(userExcel);

                    var user = new CreateUpdateUseDto();
                    excelValidator.InvalidCells.AddRange(ValidateUserRow(row, userExcel, out user));
                    if (excelValidator.InvalidCells.Count < 1)
                    {
                        validUsers.Add(user);
                    }
                }
            }

            if (excelValidator.InvalidCells.Count == 0)
            {
                var emailGroup = userRows.GroupBy(x => x.Email);
                var codeGroup = userRows.GroupBy(x => x.EmployeeCode);
                var phoneGroup = userRows.GroupBy(x => x.PhoneNumber);

                if (emailGroup.Count() != userRows.Count)
                {
                    foreach (var items in emailGroup.Where(x => x.Count() > 2))
                    {
                        var emailName = items.FirstOrDefault()?.Email;
                        excelValidator.InvalidLogics.Add($"Email :{emailName} In Excel File");
                    }
                }

                if (codeGroup.Count() != userRows.Count)
                {
                    foreach (var items in codeGroup.Where(x => x.Count() > 2))
                    {
                        var codeName = items.FirstOrDefault()?.EmployeeCode;
                        excelValidator.InvalidLogics.Add($"User Code :{codeName} In Excel File");
                    }
                }

                if (phoneGroup.Count() != userRows.Count)
                {
                    foreach (var items in phoneGroup.Where(x => x.Count() > 2))
                    {
                        var phoneName = items.FirstOrDefault()?.PhoneNumber;
                        excelValidator.InvalidLogics.Add($"Phone :{phoneName} In Excel File");
                    }
                }

                var usersToCreate = new List<CreateUpdateUseDto>();
                var usersToUpdate = new List<CreateUpdateUseDto>();
                var roles = new List<Role>();
                if (excelValidator.InvalidLogics.Count == 0)
                {
                     roles = await _roleManager.Roles.ToListAsync();

                    foreach (var item in validUsers.SelectMany(x=>x.Roles).Distinct())
                    {
                        if (!roles.Any(x => x.RoleCode == item))
                        {
                            excelValidator.InvalidLogics.Add($"Role :not exits {item} In Excel File");
                        }   
                    }
                    
                    
                    foreach (var item in validUsers)
                    {
                        
                        if (users.Any(x => x.PhoneNumber == item.PhoneNumber))
                        {
                            var user = users.FirstOrDefault(x => x.PhoneNumber == item.PhoneNumber);

                            if (users.Any(x => x.Email == item.Email && x.Id != user.Id))
                            {
                                excelValidator.InvalidLogics.Add($"Email :{item.Email} In DB");
                            }

                            if (users.Any(x => x.EmployeeCode == item.EmployeeCode && x.Id != user.Id))
                            {
                                excelValidator.InvalidLogics.Add($"Employee Code :{item.EmployeeCode} In DB");
                            }

                            usersToUpdate.Add(item);


                            continue;
                        }

                        if (users.Any(x => !item.Email.IsNullOrEmpty() && x.Email == item.Email))
                        {
                            excelValidator.InvalidLogics.Add($"Email :{item.Email} In DB");
                        }

                        if (users.Any(x => x.EmployeeCode == item.EmployeeCode))
                        {
                            excelValidator.InvalidLogics.Add($"EmployeeCode :{item.EmployeeCode} In DB");
                        }
                        
                        usersToCreate.Add(item);
                    }
                }

                if (excelValidator.InvalidLogics.Count == 0)
                {
                    foreach (var item in usersToCreate)
                    {
                        var newUser = ObjectMapper.Map<CreateUpdateUseDto, User>(item);
                        newUser.PasswordHash = _userManager.PasswordHasher.HashPassword(newUser, item.Password);
                        
                        var roleNames = new List<string>();
                        if (!item.Roles.IsNullOrEmpty())
                        {
                            foreach (var name in item.Roles)
                            {
                                roleNames.Add(roles.FirstOrDefault(x=>x.RoleCode == name).Name);
                            }
                        }
                        await _userManager.CreateAsync(newUser);
                        await UpdateRolesForUser(newUser.UserName, roleNames);

                    }

                    foreach (var item in usersToUpdate)
                    {
                        var user = users.FirstOrDefault(x => x.PhoneNumber == item.PhoneNumber);
                        var editUser = ObjectMapper.Map(item, user);
                        var roleNames = new List<string>();
                        if (!item.Roles.IsNullOrEmpty())
                        {
                            if (!item.Roles.IsNullOrEmpty())
                            {
                                foreach (var name in item.Roles)
                                {
                                    roleNames.Add(roles.FirstOrDefault(x=>x.RoleCode == name).Name);
                                }
                            }
                        }
                        
                        await _userManager.UpdateAsync(editUser);
                        await UpdateRolesForUser(editUser.UserName, roleNames);

                    }
                    excelValidator.IsSuccessful = true;

                }

            }


            return excelValidator;
        }

        private UserExcelDto _readUserRowFromExcel(int row, ExcelWorksheet worksheet)
        {
            var user = new UserExcelDto()
            {
                EmployeeCode = worksheet.Cells[row, 1].Value?.ToString().Trim(),
                FirstName = worksheet.Cells[row, 2].Value?.ToString().Trim(),
                LastName = worksheet.Cells[row, 3].Value?.ToString().Trim(),
                Gender = worksheet.Cells[row, 4].Value?.ToString().Trim(),
                DOB = worksheet.Cells[row, 5].Value?.ToString().Trim(),
                Password = worksheet.Cells[row, 6].Value?.ToString().Trim(),
                UserName = worksheet.Cells[row, 7].Value?.ToString().Trim(),
                PhoneNumber = worksheet.Cells[row, 7].Value?.ToString().Trim(),
                Email = worksheet.Cells[row, 8].Value?.ToString().Trim(),
                Roles = worksheet.Cells[row, 9].Value?.ToString().Trim().Split(',').ToList(),
                Row = row
            };
            return user;
        }

        private bool _isNullExcelRow(UserExcelDto user)
        {
            if (user.EmployeeCode.IsNullOrWhiteSpace()
                & user.Password.IsNullOrWhiteSpace()
                & user.PhoneNumber.IsNullOrWhiteSpace()
                & user.Email.IsNullOrWhiteSpace())
            {
                return false;
            }

            return true;
        }

        private List<Cell> ValidateUserRow(int row, UserExcelDto userExcelDto, out CreateUpdateUseDto user)
        {
            UserValidator validator = new UserValidator();
            user = new CreateUpdateUseDto();

            var invalidCells = new List<Cell>();


            if (!validator.ValidateEmployeeCode(userExcelDto.EmployeeCode))
            {
                invalidCells.Add(new Cell()
                {
                    Row = row,
                    Col = 1,
                });
            }


            if (!validator.ValidateName(userExcelDto.FirstName))
            {
                invalidCells.Add(new Cell()
                {
                    Row = row,
                    Col = 2,
                });
            }

            if (!validator.ValidateName(userExcelDto.LastName))
            {
                invalidCells.Add(new Cell()
                {
                    Row = row,
                    Col = 3,
                });
            }

            int genderResult;
            if (!validator.ValidateGender(userExcelDto.Gender, out genderResult))
            {
                invalidCells.Add(new Cell()
                {
                    Row = row,
                    Col = 4,
                });
            }

            DateTime dobResult;
            if (!validator.ValidateDate(userExcelDto.DOB, out dobResult))
            {
                invalidCells.Add(new Cell()
                {
                    Row = row,
                    Col = 5,
                });
            }

            if (!validator.ValidatePassword(userExcelDto.Password))
            {
                invalidCells.Add(new Cell()
                {
                    Row = row,
                    Col = 6,
                });
            }

            if (!validator.ValidatePhone(userExcelDto.PhoneNumber))
            {
                invalidCells.Add(new Cell()
                {
                    Row = row,
                    Col = 7,
                });
            }

            if (!validator.ValidateEmail(userExcelDto.Email))
            {
                invalidCells.Add(new Cell()
                {
                    Row = row,
                    Col = 8,
                });
            }
            

            if (invalidCells.Count == 0)
            {
                user.EmployeeCode = userExcelDto.EmployeeCode;
                user.FirstName = userExcelDto.FirstName;
                user.LastName = userExcelDto.LastName;
                user.Gender = genderResult > 2 ? Gender.Unknown : (Gender) genderResult;
                user.DOB = dobResult;
                user.Password = userExcelDto.Password;
                user.PhoneNumber = userExcelDto.PhoneNumber;
                user.Email = userExcelDto.Email;
                user.Roles = userExcelDto.Roles?? new List<string>();
                user.UserName = userExcelDto.UserName;
            }


            return invalidCells;
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
            var user = ObjectMapper.Map<CreateUserDto, User>(input);
            _checkDuplicateByCreating(user);

            var result = await _userManager.CreateAsync(user, input.Password);
            if (!result.Succeeded)
            {
                throw new GlobalException(result.Errors?.FirstOrDefault().Description, HttpStatusCode.BadRequest);
            }

            return ObjectMapper.Map<User, UserDto>(user);
        }

        private void _checkDuplicateByCreating(User input)
        {
            input.Email = input.Email?.Trim();
            input.PhoneNumber = input.PhoneNumber.Trim();
            input.EmployeeCode = input.EmployeeCode.Trim();

            var result = _userRepository.CheckDuplicateInformation(input.Email, input.PhoneNumber, input.EmployeeCode);

            if (result.ExistEmail)
            {
                throw new GlobalException(HttpMessage.Duplicate.DuplicateEmail, HttpStatusCode.BadRequest);
            }

            if (result.ExistPhoneNumber)
            {
                throw new GlobalException(HttpMessage.Duplicate.DuplicatePhoneNumber, HttpStatusCode.BadRequest);
            }

            if (result.ExistEmployeeCode)
            {
                throw new GlobalException(HttpMessage.Duplicate.DuplicateEmployeeCode, HttpStatusCode.BadRequest);
            }
        }

        public async Task<UserDto> UpdateAsync(UpdateUserDto input, Guid id)
        {
            var item = await _userManager.FindByIdAsync(id.ToString());

            if (item == null)
            {
                throw new GlobalException(HttpMessage.NotFound, HttpStatusCode.BadRequest);
            }

            await _checkDuplicateByUpdating(input, id);

            var user = ObjectMapper.Map(input, item);
            var result = await _userManager.UpdateAsync(user);
            if (!result.Succeeded)
            {
                throw new GlobalException(result.Errors?.FirstOrDefault().Description, HttpStatusCode.BadRequest);
            }

            return ObjectMapper.Map<User, UserDto>(user);
            ;
        }


        private async Task _checkDuplicateByUpdating(UpdateUserDto input, Guid id)
        {
            input.Email = input.Email?.Trim();
            input.PhoneNumber = input.PhoneNumber.Trim();
            input.EmployeeCode = input.EmployeeCode.Trim();

            var result =
                _userRepository.CheckDuplicateInformation(input.Email, input.PhoneNumber, input.EmployeeCode, id);

            if (result.ExistEmail)
            {
                throw new GlobalException(HttpMessage.Duplicate.DuplicateEmail, HttpStatusCode.BadRequest);
            }

            if (result.ExistPhoneNumber)
            {
                throw new GlobalException(HttpMessage.Duplicate.DuplicatePhoneNumber, HttpStatusCode.BadRequest);
            }

            if (result.ExistEmployeeCode)
            {
                throw new GlobalException(HttpMessage.Duplicate.DuplicateEmployeeCode, HttpStatusCode.BadRequest);
            }
        }

        public async Task DeleteAsync(Guid id)
        {
            var item = await _userManager.FindByIdAsync(id.ToString());
            if (item == null)
            {
                throw new GlobalException(HttpMessage.NotFound, HttpStatusCode.BadRequest);
            }

            var result = await _userManager.DeleteAsync(item);
            if (!result.Succeeded)
            {
                throw new GlobalException(result.Errors?.FirstOrDefault().Description, HttpStatusCode.BadRequest);
            }
        }

        public async Task<TokenDto> SignInAsync(UserModel input)
        {
            var user = await _userManager.FindByNameAsync(input.UserName);
            if (user == null)
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

        public async Task UpdateRolesForUser(string userName, List<string> roles)
        {
            var user = await _userManager.FindByNameAsync(userName);
            var oldRoles = await _userManager.GetRolesAsync(user);
            await _userManager.RemoveFromRolesAsync(user, oldRoles);
            await _userManager.AddToRolesAsync(user, roles);
        }

        public Task<UserProfileModel> UpdateUserProfileAsync(UserProfileModel userProfileModel)
        {
            throw new NotImplementedException();
        }

        public async Task<UserPasswordUpdateModel> ChangePasswordAsync(UserPasswordUpdateModel userDto)
        {

            return null;
        }

        public async Task<UserDto> SetPasswordAsync(UserModel input)
        {
     
            return null;
        }

        public async Task<TokenDto> RefreshTokenAsync(TokenModel token)
        {
            if (token is null) throw new GlobalException(HttpMessage.Unauthorized, HttpStatusCode.Unauthorized);


            var principal = GetPrincipalFromExpiredToken(token.AccessToken);
            var userName = principal.Identity.Name;

            var user = await _userManager.FindByNameAsync(userName);

            if (user == null || user.RefreshToken != user.RefreshToken)
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