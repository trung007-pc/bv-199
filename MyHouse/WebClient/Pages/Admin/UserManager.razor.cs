using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Blazorise;
using Blazorise.States;
using Contract.Identity.RoleManager;
using Contract.Identity.UserManager;
using Core.Enum;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Radzen;
using WebClient.Exceptions;
using WebClient.Helper;

namespace WebClient.Pages.Admin
{
    public partial class UserManager
    {
        [Inject] IMessageService _messageService { get; set; }
        public List<UserWithNavigationPropertiesDto> UsersWithNav { get; set; } = new List<UserWithNavigationPropertiesDto>();
        public CreateUserDto NewUser { get; set; } = new CreateUserDto();
        public CreateUserDto UserExcel { get; set; } = new CreateUserDto();
        public UpdateUserDto EditUser { get; set; } = new UpdateUserDto();
        public List<RoleDto> Roles = new List<RoleDto>();
        public List<string> RoleNames = new List<string>();
        public List<string> SelectedRoles = new List<string>();
        public Guid EditUserId { get; set; }

        public Modal CreateModal = new Modal();
        public Modal EditModal = new Modal();
        public Modal ImportFileModal = new Modal();
        public Modal Create1Modal = new Modal();
        public IBrowserFile? EnclosedFile { get; set; }

        public ExcelValidator ExcelValidator { get; set; } = new ExcelValidator();

        public string HeaderTitle = "User";
        public bool IsLoading { get; set; } = true;

        public bool TriggeredWithoutFile { get; set; } = false;

        public UserManager()
        {
        }


        protected override async Task OnInitializedAsync()
        {

        }

        protected override async void OnAfterRender(bool firstRender)
        {
            if (firstRender)
            {
                await InvokeAsync(async () =>
                {
                    await GetUsers();
                    await GetRoles();
                    IsLoading = false;
                    StateHasChanged();
                }, ActionType.GetList, false);


            }
        }

        public async Task GetUsers()
        {
            UsersWithNav = await _userManagerService.GetListWithNavigationAsync();
        }

        public async Task GetRoles()
        {
            Roles = await _roleManagerService.GetListAsync();
            RoleNames = Roles.Select(x => x.Name).ToList();
        }

        public async Task CreateUser()
        {
            await InvokeAsync(async () =>
            {
                await _userManagerService.CreateUserWithRolesAsync(NewUser);
                HideNewModal();
                await GetUsers();
            }, ActionType.Create, true);
        }

        public async Task UpdateUser()
        {
            await InvokeAsync(async () =>
            {
                await _userManagerService.UpdateUserWithRolesAsync(EditUser, EditUserId);
                await GetUsers();
                await HideEditModal();
                
            }, ActionType.Update, true);
        }

        public async Task DeleteUser(Guid id)
        {
            await InvokeAsync(async () =>
            {
                await _userManagerService.DeleteAsync(id);
                await GetUsers();
            },ActionType.Delete,true );
        }
        
        
        public async Task ShowConfirmMessage(Guid id)
        {
            if ( await _messageService.Confirm( "Are you sure you want to confirm?", "Confirmation" ) )
            {
                await DeleteUser(id);
            }
        }


        public void ShowNewModal()
        {
            NewUser = new CreateUserDto();
            SelectedRoles = new List<string>();
            CreateModal.Show();
        }

        
        
        public Task ShowImportExcelFileModal()
        {
            EnclosedFile = null;
            UserExcel = new CreateUserDto();
            ExcelValidator = new ExcelValidator();
            TriggeredWithoutFile = false;
            return ImportFileModal.Show();
        }

        public void HideImportExcelFileModal()
        {
            ImportFileModal.Hide();
        }

        public void HideNewModal()
        {
            NewUser = new CreateUserDto();
            CreateModal.Hide();
        }


        public Task ShowEditModal(UserWithNavigationPropertiesDto userWithNavigationPropertiesDto)
        {
            EditUser = new UpdateUserDto();
            SelectedRoles = new List<string>();
            
            SelectedRoles = userWithNavigationPropertiesDto.RoleNames;
            
            EditUserId = userWithNavigationPropertiesDto.UserDto.Id;
            EditUser = ObjectMapper.Map<UserDto, UpdateUserDto>(userWithNavigationPropertiesDto.UserDto);
            EditUser.Roles = userWithNavigationPropertiesDto.RoleNames;
            
            return EditModal.Show();
        }

        public BadgeStyle ChooseColorByNumber(int i)
        {
            
            
            switch (i)
            {
                case 1:
                {
                    return BadgeStyle.Info;
                }
                case 2:
                {
                    return BadgeStyle.Primary;
                }
                case 3:
                {
                    return BadgeStyle.Success;
                }
                case 4:
                {
                    return BadgeStyle.Light;
                }
                default:
                {
                    return BadgeStyle.Danger;
                }
            }
        }

        public void OnChooseFile()
        {
            this.StateHasChanged();
        }


        void OnEditSelectedRoles(object value)
        {
            var selectedRoles = ((IEnumerable<string>) value).ToList();
            EditUser.Roles = selectedRoles;
        }

        void OnCreateSelectedRoles(object value)
        {
            var selectedRoles = ((IEnumerable<string>) value).ToList();
            NewUser.Roles = selectedRoles;
        }

        public Task HideEditModal()
        {
            return EditModal.Hide();
        }

        async Task ShowLoading()
        {
            IsLoading = true;

            await Task.Yield();

            IsLoading = false;
        }

        public async Task OnChangeFile(InputFileChangeEventArgs e)
        {
            EnclosedFile = e.GetMultipleFiles().FirstOrDefault();
        }

        public async Task ImportUserDataOfExcel()
        {
            if (EnclosedFile!= null)
            {
                TriggeredWithoutFile = false;
                await InvokeAsync(async () =>
                {
                    var fileDto = await _uploadService.UploadExcelFileOfUsers(EnclosedFile);
                    ExcelValidator = await _userManagerService.CreateUsersFromCSVFile(fileDto);
                    if (ExcelValidator.IsSuccessful)
                    {
                        HideImportExcelFileModal();
                        await GetUsers();
                    }
                    else
                    {
                        throw new FailedOperation("");
                    }

                }, ActionType.UploadFile, true);
            }
            else
            {
                TriggeredWithoutFile = true;
            }
        }

        public async Task DownloadExcelSampleFileOfUser()
        {
            string pathBase = Path.Combine(_webHostEnvironment.WebRootPath,@"excels\userExcelSample\sample.csv");
            var bytes = await FileHelper.GetBytesOfExcelFile(pathBase);
            await _downloadFile.DownloadFileAsync(bytes,"csv");
        }

        public async Task DownloadInstructionFileOfCreatingUser()
        {
            string pathBase = Path.Combine(_webHostEnvironment.WebRootPath,@"excels\userExcelSample\instruction.csv");
            var bytes = await FileHelper.GetBytesOfExcelFile(pathBase);
            await _downloadFile.DownloadFileAsync(bytes,"csv");
        }

    }
}