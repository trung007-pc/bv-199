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
        public CreateUpdateRoleDto EditRole = new CreateUpdateRoleDto();
        public Guid EditRoleId { get; set; }
         [Inject]  IMessageService _messageService { get; set; }


        public Modal CreateModal;
        public Modal EditModal;
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
                await _roleManagerService.UpdateAsync(EditRole, EditRoleId);
                HideEditModal();
                await GetRoles();
            },ActionType.Update,true);
            
        }

        public async Task DeleteRole(Guid id)
        {
            await InvokeAsync(async () =>
            {
                await _roleManagerService.DeleteAsync(id);
                HideEditModal();
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
            CreateModal.Show();
        }

        public void HideNewModal()
        {
            CreateModal.Hide();
        }


        public Task ShowEditModal(RoleDto roleModel)
        {
            EditRole = new CreateUpdateRoleDto();
            EditRole = ObjectMapper.Map<RoleDto, CreateUpdateRoleDto>(roleModel);
            EditRoleId = roleModel.Id;
            return EditModal.Show();
        }

        public void HideEditModal()
        {
            EditModal.Hide();
        }
        
        
        void OnChange(IEnumerable<int> value, string name)
        {
           
        }
    }
}