using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Blazorise;
using Contract.Identity.RoleManager;
using Contract.UnitTypes;
using Core.Enum;
using Microsoft.AspNetCore.Components;
using Radzen;

namespace WebClient.Pages.Admin
{
    public partial class UnitType
    {
        public List<UnitTypeDto> UnitTypes = new List<UnitTypeDto>();
        public CreateUpdateUnitTypeDto NewUnitType = new CreateUpdateUnitTypeDto();
        public CreateUpdateUnitTypeDto EditingUnitType = new CreateUpdateUnitTypeDto();
        public Guid EditingUnitId { get; set; }
        [Inject] IMessageService _messageService { get; set; }


        public Modal CreateModal;
        public Modal EditingModal;
        

        public string HeaderTitle = "Unit Type";


        public UnitType()
        {
        }


        protected override async void OnAfterRender(bool firstRender)
        {
            if (firstRender)
            {
                await InvokeAsync(async () =>
                {
                    await GetUnitTypes();
                    StateHasChanged();
                }, ActionType.GetList, false);
            }
        }



        public void ShowTooltip(ElementReference elementReference)
        {
            _tooltipService.Open(elementReference, "Copy TypeId To Clipboard!");
        }
         async Task CopyToClipboard(Guid id)
        {
            string pathBase = Path.Combine(_navigationManager.BaseUri,$"bv-199/unit-review/{id.ToString()}");
            await _clipboardService.CopyToClipboard(pathBase);
            NotifyMessage(NotificationSeverity.Success,"Copied",2000);
        }


        public async Task GetUnitTypes()
        {
            UnitTypes = await _unitTypeService.GetListAsync();
        }
        

        public async Task CreateUnitType()
        {
            await InvokeAsync(async () =>
            {
                await _unitTypeService.CreateAsync(input: NewUnitType);
                HideNewModal();
                await GetUnitTypes();
            }, ActionType.Create, true);
        }

        public async Task UpdateUnitType()
        {
            await InvokeAsync(async () =>
            {
                await _unitTypeService.UpdateAsync(EditingUnitType, EditingUnitId);
                HideEditModal();
                await GetUnitTypes();
            }, ActionType.Update, true);
        }

        public async Task DeleteUnitType(Guid id)
        {
            await InvokeAsync(async () =>
            {
                await _unitTypeService.DeleteAsync(id);
                HideEditModal();
                await GetUnitTypes();
            }, ActionType.Delete, true);
        }

        public async Task ShowConfirmMessage(Guid id)
        {
            if (await _messageService.Confirm("Are you sure you want to confirm?", "Confirmation"))
            {
                await DeleteUnitType(id);
            }
        }

        public void ShowNewModal()
        {
            NewUnitType = new CreateUpdateUnitTypeDto();
            CreateModal.Show();
        }

        public void HideNewModal()
        {
            CreateModal.Hide();
        }


        public Task ShowEditingModal(UnitTypeDto unitTypeDto)
        {
            EditingUnitType = new CreateUpdateUnitTypeDto();
            EditingUnitType = ObjectMapper.Map<UnitTypeDto, CreateUpdateUnitTypeDto>(unitTypeDto);
            EditingUnitId = unitTypeDto.Id;
            return EditingModal.Show();
        }

        public void HideEditModal()
        {
            EditingModal.Hide();
        }
        
    }
}