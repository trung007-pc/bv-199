using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Blazorise;
using Contract.Units;
using Contract.UnitTypes;
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
        public List<UnitWithNavPropertiesDto> Units { get; set; }
        public Modal CreateModal = new Modal();
        public Modal EditModal = new Modal();
        public CreateUpdateUnitDto NewUnit { get; set; }

        public CreateUpdateUnitDto EditUnit { get; set; }
        public Guid EditUnitId { get; set; }

        public long maxFileSize = 1024 * 15;
        
        public IBrowserFile? NewFile { get; set; }
        public IBrowserFile? EditFile { get; set; }
        
        public List<UnitTypeDto> Types { get; set; }
        public Guid? OnEditSelectedTypeId { get; set; }


        public string HeaderTitle { get; set; } = "Part";
        
        public UnitManager()
        {
            Units = new List<UnitWithNavPropertiesDto>();
            NewUnit = new CreateUpdateUnitDto();
            EditUnit = new CreateUpdateUnitDto();
        }


        protected async override Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                await InvokeAsync(async () =>
                {
                    await GetDateRangePickers();
                    await GetList();
                    await GetUnitTypes();
                    StateHasChanged();
                }, ActionType.GetList, false);
   
            }
        }

        public async Task GetList()
        {
            Units = await _unitService.GetListWithNavPropertiesAsync(new UnitFilter());
            StateHasChanged();
        }

        public async Task GetUnitTypes()
        {
            Types =  await _unitService.LookUpUnitTypes();
        }

        public void ShowNewModal()
        {
            NewUnit = new CreateUpdateUnitDto();
            NewFile = null;
            CreateModal.Show();
        }

        public void HideNewModal()
        {
            CreateModal.Hide();
        }

        public async void CreateUnit()
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

        public async Task ShowConfirmMessage(Guid id)
        {
            if ( await _messageService.Confirm( "Are you sure you want to confirm?", "Confirmation" ) )
            {
                await InvokeAsync(async () =>
                {
                    await DeleteUnit(id);
                },ActionType.Delete,true);
            }
        }
        public async Task DeleteUnit(Guid id)
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

        public async Task UpdateUnit()
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

        public Task ShowEditModal(UnitWithNavPropertiesDto unitWithNavProperties)
        {
            var unitDto = unitWithNavProperties.Unit;
            var type = unitWithNavProperties.UnitType;
            
            EditUnit = ObjectMapper.Map<UnitDto, CreateUpdateUnitDto>(unitDto);
            EditFile = null;
            
            EditUnitId = unitDto.Id;

            if (type.Id != Guid.Empty)
            {
                EditUnit.UnitTypeId = type.Id;
                OnEditSelectedTypeId = type.Id;
            }
         
            return EditModal.Show();
        }

        public void HideEditModal()
        {
            EditModal.Hide();
        }

        public void OnNewSelectedTypes(object value)
        {
            NewUnit.UnitTypeId = (Guid)value;
        }
        
        public void OnEditSelectedTypes(object value)
        {
            EditUnit.UnitTypeId = (Guid?)value;
        }
        
        
    }
}