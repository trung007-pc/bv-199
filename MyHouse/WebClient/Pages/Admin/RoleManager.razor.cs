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
        private List<RoleDto> Roles = new List<RoleDto>();
        private CreateUpdateRoleDto NewRole = new CreateUpdateRoleDto();
        private CreateUpdateRoleDto EditRole = new CreateUpdateRoleDto();
        private Guid EditRoleId { get; set; }
        [Inject] IMessageService _messageService { get; set; }


        private Modal CreateModal;
        private Modal EditModal;
        private string HeaderTitle = "Role";

        private IEnumerable<string> Claims = new List<string>();

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

        private async Task GetRoles()
        {
            Roles = await _roleManagerService.GetListAsync();
        }

        private async Task CreateRole()
        {
            await InvokeAsync(async () =>
            {
                await _roleManagerService.CreateAsync(input: NewRole);
                HideNewModal();
                await GetRoles();
            }, ActionType.Create, true);
        }

        private async Task UpdateRole()
        {
            await InvokeAsync(async () =>
            {
                await _roleManagerService.UpdateAsync(EditRole, EditRoleId);
                HideEditModal();
                await GetRoles();
            },ActionType.Update,true);
            
        }

        private async Task DeleteRole(Guid id)
        {
            await InvokeAsync(async () =>
            {
                await _roleManagerService.DeleteAsync(id);
                HideEditModal();
                await GetRoles();
            },ActionType.Delete,true);
            
        }

        private async Task ShowConfirmMessage(Guid id)
        {
            if (await _messageService.Confirm("Are you sure you want to confirm?", "Confirmation"))
            {
                await DeleteRole(id);
            }
        }

        private void ShowNewModal()
        {
            NewRole = new CreateUpdateRoleDto();
            CreateModal.Show();
        }

        private void HideNewModal()
        {
            CreateModal.Hide();
        }


        private Task ShowEditModal(RoleDto roleModel)
        {
            EditRole = new CreateUpdateRoleDto();
            EditRole = ObjectMapper.Map<RoleDto, CreateUpdateRoleDto>(roleModel);
            EditRoleId = roleModel.Id;
            return EditModal.Show();
        }

        private void HideEditModal()
        {
            EditModal.Hide();
        }
        
        
        void OnChange(IEnumerable<int> value, string name)
        {
           
        }
    }
}