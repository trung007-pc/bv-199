using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlazorDateRangePicker;
using Contract.MeetingContents;
using Contract.MyDashboards;
using Contract.SendingFiles;
using Contract.WorkSchedules;
using Core.Enum;
using Core.Extension;
using Radzen;
using WebClient.Components;

namespace WebClient.Pages.Admin
{
    public partial class MyDashboard
    {
        public string HeaderTitle = "My Dashboard";

        public MyStatistics MyStatistics { get; set; } = new MyStatistics();

        public GlobalStatistics GlobalStatistics { get; set; } = new GlobalStatistics();
        public GlobalStatisticFilter StatisticFilter { get; set; } = new GlobalStatisticFilter();
        public MyStatisticFilter MyStatisticFilter { get; set; } = new MyStatisticFilter();

        public bool IsLoading { get; set; } = true;
        public bool IsLoadingChart { get; set; } = true;
        public Dictionary<string, DateRange> DateRanges { get; set; } = new Dictionary<string, DateRange>();
        public (DateTimeOffset? StartDay, DateTimeOffset? EndDay) Timeline = (null, null);
        
        public RZModel ScheduleFileView { get; set;}
        public RZModel ContentFileView { get; set;}
        
        public MeetingContentDto SelectedContent { get; set; }
        public WorkScheduleDto  SelectedSchedule { get; set; }


        protected override async Task OnInitializedAsync()
        {
           
        }
        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {            
                await Init();
                await GetGlobalStatistics();
                await GetMyStatistics();
                IsLoading = false;
                StateHasChanged();
            }
        }

        public async Task GetGlobalStatistics()
        {
            IsLoadingChart = true;
            GlobalStatistics = await _myDashboardService.GetGlobalStatistics(StatisticFilter);
            IsLoadingChart = false;
        }

        public async Task GetMyStatistics()
        {
            MyStatistics = await _myDashboardService.GetMyStatistics(MyStatisticFilter);
        }
        public async Task Init()
        {
         
            (DateRanges,Timeline.StartDay,Timeline.EndDay) = await GetDateRangePickersWithDefault();
            
            var userId = await GetUserIdAsync();
            StatisticFilter.FolderTop = 5;
            StatisticFilter.UserId = userId;
            
            (StatisticFilter.StartDay, StatisticFilter.EndDay) = GetDateTimeFromOffSet(Timeline.StartDay, Timeline.EndDay);
            (MyStatisticFilter.StartDay, MyStatisticFilter.EndDay) = GetDateTimeFromOffSet(Timeline.StartDay, Timeline.EndDay);
            MyStatisticFilter.UserId = userId;
        }
        
        
        void OnSeriesClick(SeriesClickEventArgs args)
        {
           
        }
        public async Task OnChangedDate()
        {
            (MyStatisticFilter.StartDay, MyStatisticFilter.EndDay) = GetDateTimeFromOffSet(Timeline.StartDay, Timeline.EndDay);
            (StatisticFilter.StartDay, StatisticFilter.EndDay) = GetDateTimeFromOffSet(Timeline.StartDay,Timeline.EndDay);
            await GetGlobalStatistics();
            await GetMyStatistics();
        }
        
        async Task GotoViewDocumentFile(SendingFileDto dto)
        {
            await UpdateRead(dto);
            _navigationManager.NavigateTo($"view-document-file?fileId={dto.FileId}");
        }

        public async Task UpdateRead(SendingFileDto dto)
        {
            var item =ObjectMapper.Map<SendingFileDto,CreateUpdateSendingFileDto>(dto);
            item.Status = true;
            await _sendingFileService.UpdateAsync(item,dto.Id);
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
        
        
        public async Task ShowScheduleFileView(WorkScheduleDto item)
        {
            SelectedSchedule = item;
            await ScheduleFileView.ShowModel();
        }

        public void HideScheduleFileView()
        {
             ScheduleFileView.HideModel();
        }
        
        public async Task ShowMeetingContentFileView(MeetingContentDto item)
        {
            SelectedContent = item;
            await ContentFileView.ShowModel();
        }

        public void HideMeetingContentFileView()
        {
            ContentFileView.HideModel();
        }
        
        
        
        
        
    }
}