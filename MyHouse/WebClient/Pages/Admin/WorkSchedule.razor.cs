using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Blazorise;
using Contract.WorkSchedules;
using Core.Enum;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Radzen;
using WebClient.Exceptions;
using WebClient.LanguageResources;
using WebClient.Setting;

namespace WebClient.Pages.Admin
{
    public partial class WorkSchedule
    {
        public List<WorkScheduleDto> WorkSchedules = new List<WorkScheduleDto>();
        public CreateUpdateWorkScheduleDto NewWorkSchedule = new CreateUpdateWorkScheduleDto();
        public CreateUpdateWorkScheduleDto EditingWorkSchedule = new CreateUpdateWorkScheduleDto();
        public Guid EditingWorkScheduleId { get; set; }
        [Inject] IMessageService _messageService { get; set; }


        public Modal CreateModal;
        public Modal EditingModal;
        public IBrowserFile? NewFile { get; set; }
        public IBrowserFile? EditFile { get; set; }
        
        public string HeaderTitle = "Work Schedule";
        public bool IsLoading { get; set; } = true;


        public WorkSchedule()
        {
           
        }


        protected override async void OnAfterRender(bool firstRender)
        {
            if (firstRender)
            {
                await InvokeAsync(async () =>
                {
                    HeaderTitle = L["WorkSchedule"];
                    await GetWorkSchedules();
                    StateHasChanged();
                }, ActionType.GetList, false);
            }
        }


        

        public async Task GetWorkSchedules()
        {
            IsLoading = true;
            WorkSchedules = await _unitTypeService.GetListAsync();
            IsLoading = false;
        }


        public async Task CreateWorkSchedule()
        {
            await InvokeAsync(async () =>
            {
                if (NewFile is not null)
                {
                    var fileDto = await _uploadService.UploadDocumentFile(NewFile);
                    NewWorkSchedule.Path = fileDto.Path;
                    NewWorkSchedule.FileName = fileDto.FileName;
                    NewWorkSchedule.Url = fileDto.Url;
                    NewWorkSchedule.Extentions = fileDto.Extension;
                }

                NewWorkSchedule.CreatedBy = await GetUserIdAsync();


                await _unitTypeService.CreateAsync(input: NewWorkSchedule);
                HideNewModal();
                await GetWorkSchedules();
            }, ActionType.Create, true);
        }

        public async Task UpdateWorkSchedule()
        {
            await InvokeAsync(async () =>
            {
                
                if (EditFile is not null)
                {
                    var fileDto = await _uploadService.UploadDocumentFile(EditFile);
                    EditingWorkSchedule.Path = fileDto.Path;
                    EditingWorkSchedule.FileName = fileDto.FileName;
                    EditingWorkSchedule.Url = fileDto.Url;
                    EditingWorkSchedule.Extentions = fileDto.Extension;

                }
               
                EditingWorkSchedule.CreatedBy = await GetUserIdAsync();
                await _unitTypeService.UpdateAsync(EditingWorkSchedule, EditingWorkScheduleId);
                HideEditModal();
                await GetWorkSchedules();
            }, ActionType.Update, true);
        }

        public async Task DeleteWorkSchedule(Guid id)
        {
            await InvokeAsync(async () =>
            {
                await _unitTypeService.DeleteAsync(id);
                HideEditModal();
                await GetWorkSchedules();
            }, ActionType.Delete, true);
        }

        public async Task ShowConfirmMessage(Guid id)
        {
            if (await _messageService.Confirm(L["Confirmation.Message"], L["Confirmation.Message"]))
            {
                await DeleteWorkSchedule(id);
            }
        }

        public void ShowNewModal()
        {
            NewWorkSchedule = new CreateUpdateWorkScheduleDto();
            CreateModal.Show();
        }

        public void HideNewModal()
        {
            CreateModal.Hide();
        }


        public Task ShowEditingModal(WorkScheduleDto dto)
        {
            EditFile = null;
            EditingWorkSchedule = new CreateUpdateWorkScheduleDto();
            EditingWorkSchedule = ObjectMapper.Map<WorkScheduleDto, CreateUpdateWorkScheduleDto>(dto);
            EditingWorkScheduleId = dto.Id;
            return EditingModal.Show();
        }

        public void HideEditModal()
        {
            EditingModal.Hide();
        }

        public MudBlazor.Color SetColorByStatus(ScheduleStatus status)
        {
            switch (status)
            {
                case ScheduleStatus.Anticipation:
                {
                    return MudBlazor.Color.Primary;
                }

                case ScheduleStatus.Cancel:
                {
                    return MudBlazor.Color.Error;
                }

                case ScheduleStatus.Finish:
                {
                    return MudBlazor.Color.Success;
                }

                case ScheduleStatus.Issuing:
                {
                    return MudBlazor.Color.Info;
                }
            }

            return MudBlazor.Color.Tertiary;
        }


        async Task OnChangeFileAtNewModal(InputFileChangeEventArgs e)
        {
            await InvokeAsync(async () =>
            {
                NewFile = null;
                NewFile = e.File;

                if (e.File.Size > BlazorSetting.Document_FILE_LENGTH_LIMIT)
                    throw new FailedOperation("File size is too big. please choose file have less 10Mb");
            }, ActionType.UploadFile);
        }


        async Task OnChangeFileAtEditModal(InputFileChangeEventArgs e)
        {
            await InvokeAsync(async () =>
            {
                EditFile = null;
                EditFile = e.File;

                if (e.File.Size > BlazorSetting.Document_FILE_LENGTH_LIMIT)
                    throw new FailedOperation("File size is too big. please choose file have less 10Mb");
            }, ActionType.UploadFile);
        }
        
    }
}