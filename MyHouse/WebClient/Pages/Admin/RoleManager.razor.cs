using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Blazorise;
using Contract.Identity.RoleManager;
using Contract.Identity.UserManager;
using Core.Const;
using Core.Enum;
using Microsoft.AspNetCore.Components;


namespace WebClient.Pages.Admin
{
    public partial class RoleManager
    {
        public List<RoleDto> Roles = new List<RoleDto>();
        public CreateUpdateRoleDto NewRole = new CreateUpdateRoleDto();
        public CreateUpdateRoleDto EditingRole = new CreateUpdateRoleDto();
        public Guid EditingRoleId { get; set; }
         [Inject]  IMessageService _messageService { get; set; }


        public Modal NewModal;
        public Modal EditingModal;
        public string HeaderTitle = "Role";

        public IEnumerable<string> Claims = new List<string>();

        public RoleManager()
        {
        }
        
       

        protected override async void OnAfterRender(bool firstRender)
        {
            if (firstRender)
            {
                await InvokeAsync(async () =>
                {
                    await GetRoles();
                    StateHasChanged();
                }, ActionType.GetList, false);
            }
        }

        public async Task GetRoles()
        {
            Roles = await _roleManagerService.GetListAsync();
        }

        public async Task CreateRole()
        {
            await InvokeAsync(async () =>
            {
                await _roleManagerService.CreateAsync(input: NewRole);
                HideNewModal();
                await GetRoles();
            }, ActionType.Create, true);
        }

        public async Task UpdateRole()
        {
            await InvokeAsync(async () =>
            {
                await _roleManagerService.UpdateAsync(EditingRole, EditingRoleId);
                HideEditingModal();
                await GetRoles();
            },ActionType.Update,true);
            
        }

        public async Task DeleteRole(Guid id)
        {
            await InvokeAsync(async () =>
            {
                await _roleManagerService.DeleteAsync(id);
                HideEditingModal();
                await GetRoles();
            },ActionType.Delete,true);
            
        }

        public async Task ShowConfirmMessage(Guid id)
        {
            if (await _messageService.Confirm("Are you sure you want to confirm?", "Confirmation"))
            {
                await DeleteRole(id);
            }
        }

        public void ShowNewModal()
        {
            NewRole = new CreateUpdateRoleDto();
            NewModal.Show();
        }

        public void HideNewModal()
        {
            NewModal.Hide();
        }


        public Task ShowEditingModal(RoleDto roleDto)
        {
            EditingRole = new CreateUpdateRoleDto();
            EditingRole = ObjectMapper.Map<RoleDto, CreateUpdateRoleDto>(roleDto);
            EditingRoleId = roleDto.Id;
            return EditingModal.Show();
        }

        public void HideEditingModal()
        {
            EditingModal.Hide();
        }
    }
}