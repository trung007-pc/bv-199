using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Blazorise;
using Contract.Units;
using Contract.Uploads;
using Core.Enum;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Http;
using Radzen;
using WebClient.Components;
using WebClient.Service.Upload;

namespace WebClient.Pages.Admin
{
    public partial class UnitManager
    {
        [Inject] IMessageService _messageService { get; set; }
        private List<UnitDto> Units { get; set; }
        private Modal CreateModal = new Modal();
        private Modal EditModal = new Modal();
        private CreateUpdateUnitDto NewUnit { get; set; }

        private CreateUpdateUnitDto EditUnit { get; set; }
        private Guid EditUnitId { get; set; }

        private long maxFileSize = 1024 * 15;
        
        private IBrowserFile? NewFile { get; set; }
        private IBrowserFile? EditFile { get; set; }

        private string HeaderTitle { get; set; } = "Part";
        
        public UnitManager()
        {
            Units = new List<UnitDto>();
            NewUnit = new CreateUpdateUnitDto();
            EditUnit = new CreateUpdateUnitDto();
        }


        protected async override Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                await GetDateRangePickers();
                await GetList();
                StateHasChanged();
            }
        }

        private async Task GetList()
        {
            Units = await _unitService.GetListAsync();
            StateHasChanged();
        }

        private void ShowNewModal()
        {
            NewUnit = new CreateUpdateUnitDto();
            NewFile = null;
            CreateModal.Show();
        }

        private void HideNewModal()
        {
            CreateModal.Hide();
        }

        private async void CreateUnit()
        {
            await InvokeAsync(async () =>
                {
                    var fileDto = new FileDto();
                    if (NewFile != null)
                    { 
                        fileDto =  await _uploadService.UploadImage(NewFile);
                        NewUnit.ImageUrl = fileDto.Url;
                    }
                    NewUnit.FileName = fileDto.FileName;
                    NewUnit.Path = fileDto.Path;

                    await _unitService.CreateAsync(NewUnit);
                    await GetList();
                    HideNewModal();
                }, ActionType.Create,true);
        }

        private async Task ShowConfirmMessage(Guid id)
        {
            if ( await _messageService.Confirm( "Are you sure you want to confirm?", "Confirmation" ) )
            {
                await InvokeAsync(async () =>
                {
                    await DeleteUnit(id);
                },ActionType.Delete,true);
            }
        }
        private async Task DeleteUnit(Guid id)
        {
    
                await _unitService.DeleteAsync(id);
                await GetList();
        }

        async Task OnChangeFileAtNewModal(InputFileChangeEventArgs e)
        {
            
            NewFile = e.GetMultipleFiles().FirstOrDefault();
        }
        
        async Task OnChangeFileAtEditModal(InputFileChangeEventArgs e)
        {
            EditFile = e.GetMultipleFiles().FirstOrDefault();
        }

        void OnChange(int value, string name)
        {
            Console.Write($"{name} value changed to {value}");
        }

        private async Task UpdateUnit()
        {
           await  InvokeAsync(async () =>
            {
                var fileDto = new FileDto();
                if (EditFile != null)
                {
                    fileDto = await _uploadService.UploadImage(EditFile);
                    EditUnit.ImageUrl = fileDto.Url;
                }

                EditUnit.FileName = fileDto.FileName;
                EditUnit.Path = fileDto.Path;
                await _unitService.UpdateAsync(EditUnit, EditUnitId);
                await GetList();
                HideEditModal();
            },ActionType.Update,true);
        }

        private Task ShowEditModal(UnitDto unitDto)
        {
            EditUnit = new CreateUpdateUnitDto();
            EditUnit = ObjectMapper.Map<UnitDto, CreateUpdateUnitDto>(unitDto);
            EditFile = null;
            EditUnitId = unitDto.Id;
            return EditModal.Show();
        }

        private void HideEditModal()
        {
            EditModal.Hide();
        }
    }
}