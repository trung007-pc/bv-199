using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Blazorise;
using Blazorise.States;
using Contract.Identity.RoleManager;
using Contract.Identity.UserManager;
using Core.Enum;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Radzen;

namespace WebClient.Pages.Admin
{
    public partial class UserManager
    {
        [Inject] IMessageService _messageService { get; set; }
        public List<UserWithNavigationDto> UsersWithNav { get; set; } = new List<UserWithNavigationDto>();
        public CreateUpdateUserWithNavDto NewUser { get; set; } = new CreateUpdateUserWithNavDto();
        public UpdateUserNameWithNavDto EditUser { get; set; } = new UpdateUserNameWithNavDto();
        public List<RoleDto> Roles = new List<RoleDto>();
        public List<string> RoleNames = new List<string>();
        public List<string> SelectedRoles = new List<string>();
        public Guid EditUserId { get; set; }

        public Modal CreateModal = new Modal();
        public Modal EditModal = new Modal();
        public string HeaderTitle = "User";


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
                await _userManagerService.CreateWithNavigationAsync(NewUser);
                HideNewModal();
                await GetUsers();
            }, ActionType.Create, true);
        }

        public async Task UpdateUser()
        {
            await InvokeAsync(async () =>
            {
                await _userManagerService.UpdateUserNameWithNavigationAsync(EditUser, EditUserId);
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
            NewUser = new CreateUpdateUserWithNavDto();
            SelectedRoles = new List<string>();
            CreateModal.Show();
        }

        public void HideNewModal()
        {
            NewUser = new CreateUpdateUserWithNavDto();
            CreateModal.Hide();
        }


        public Task ShowEditModal(Guid id)
        {
            EditUser = new UpdateUserNameWithNavDto();
            SelectedRoles = new List<string>();

            var userWithNavDto = UsersWithNav.FirstOrDefault(x => x.UserDto.Id == id);
            EditUser.Roles = userWithNavDto.RoleNames;
            SelectedRoles = EditUser.Roles;
            EditUserId = id;
            EditUser = ObjectMapper.Map<UserDto, UpdateUserNameWithNavDto>(userWithNavDto.UserDto);
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

  
    }
}