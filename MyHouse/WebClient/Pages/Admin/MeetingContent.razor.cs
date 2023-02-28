using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Blazorise;
using Contract.MeetingContents;
using Core.Enum;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Radzen;
using WebClient.Exceptions;
using WebClient.Setting;

namespace WebClient.Pages.Admin
{
    public partial class MeetingContent
    {
        public List<MeetingContentDto> MeetingContents = new List<MeetingContentDto>();
        public CreateUpdateMeetingContentDto NewMeetingContent = new CreateUpdateMeetingContentDto();
        public CreateUpdateMeetingContentDto EditingMeetingContent = new CreateUpdateMeetingContentDto();
        public Guid EditingMeetingContentId { get; set; }
        [Inject] IMessageService _messageService { get; set; }


        public Modal CreateModal;
        public Modal EditingModal;
        public IBrowserFile? NewFile { get; set; }
        public IBrowserFile? EditFile { get; set; }


        public string HeaderTitle = "Work Schedule";


        public MeetingContent()
        {
        }


        protected override async void OnAfterRender(bool firstRender)
        {
            if (firstRender)
            {
                await InvokeAsync(async () =>
                {
                    await GetMeetingContents();
                    StateHasChanged();
                }, ActionType.GetList, false);
            }
        }


        public async Task GetMeetingContents()
        {
            MeetingContents = await _meetingContentService.GetListAsync();
        }


        public async Task CreateMeetingContent()
        {
            await InvokeAsync(async () =>
            {
                if (NewFile is not null)
                {
                    var fileDto = await _uploadService.UploadDocumentFile(NewFile);
                    NewMeetingContent.Path = fileDto.Path;
                    NewMeetingContent.FileName = fileDto.FileName;
                    NewMeetingContent.Url = fileDto.Url;
                }

                NewMeetingContent.CreatedBy = await GetUserIdAsync();


                await _meetingContentService.CreateAsync(input: NewMeetingContent);
                HideNewModal();
                await GetMeetingContents();
            }, ActionType.Create, true);
        }

        public async Task UpdateMeetingContent()
        {
            await InvokeAsync(async () =>
            {
                if (EditFile is not null)
                {
                    var fileDto = await _uploadService.UploadDocumentFile(NewFile);
                    EditingMeetingContent.Path = fileDto.Path;
                    EditingMeetingContent.FileName = fileDto.FileName;
                    EditingMeetingContent.Url = fileDto.Url;
                }

                EditingMeetingContent.CreatedBy = await GetUserIdAsync();
                await _meetingContentService.UpdateAsync(EditingMeetingContent, EditingMeetingContentId);
                HideEditModal();
                await GetMeetingContents();
            }, ActionType.Update, true);
        }

        public async Task DeleteMeetingContent(Guid id)
        {
            await InvokeAsync(async () =>
            {
                await _meetingContentService.DeleteAsync(id);
                HideEditModal();
                await GetMeetingContents();
            }, ActionType.Delete, true);
        }

        public async Task ShowConfirmMessage(Guid id)
        {
            if (await _messageService.Confirm("Are you sure you want to confirm?", "Confirmation"))
            {
                await DeleteMeetingContent(id);
            }
        }

        public void ShowNewModal()
        {
            NewMeetingContent = new CreateUpdateMeetingContentDto();
            CreateModal.Show();
        }

        public void HideNewModal()
        {
            CreateModal.Hide();
        }


        public Task ShowEditingModal(MeetingContentDto unitTypeDto)
        {
            EditingMeetingContent = new CreateUpdateMeetingContentDto();
            EditingMeetingContent = ObjectMapper.Map<MeetingContentDto, CreateUpdateMeetingContentDto>(unitTypeDto);
            EditingMeetingContentId = unitTypeDto.Id;
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