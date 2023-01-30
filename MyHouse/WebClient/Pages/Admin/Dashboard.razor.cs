using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using BlazorDateRangePicker;
using Contract.Dashboards;
using Contract.UnitTypes;
using Core.Enum;
using Radzen;

namespace WebClient.Pages.Admin
{
    public partial class Dashboard
    {
        public List<DataItem> UnitReviewdataItems = new List<DataItem>();
        public List<DataItem> DetailUnitReviewdataItems = new List<DataItem>();

        public bool IsLoading { get; set; } = true;
        
        public Dictionary<string, DateRange> DateRanges { get; set; } = new Dictionary<string, DateRange>();
        public (DateTimeOffset? StartDay, DateTimeOffset? EndDay) Timeline = (null, null);
        public UnitReviewStatisticsDto Stat = new UnitReviewStatisticsDto();

        public string HeaderTitle { get; set; } = "Dashboard";

        public DashboardFilter Filter { get; set; }
        public List<UnitTypeDto> Types { get; set; }


        public Dashboard()
        {
            Filter = new DashboardFilter();
        }

        protected override async Task OnInitializedAsync()
        {
           
        }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                await InvokeAsync(async () =>
                {
                    DateRanges = await GetDateRangePickers();
                    await GetUnitStatisticsByReviewDateRange();
                    await GetUnitTypes();
                    StateHasChanged();
                }, ActionType.GetList, false);
            }
        }

        public async Task GetUnitStatisticsByReviewDateRange()
        {
                Stat = await _dashboardService.GetUnitReviewStatistics(Filter);
                UnitReviewdataItems = Stat.UnitReviewItems;
                UnitReviewdataItems.Add(new DataItem(){Label = "",Value = 0});
                DetailUnitReviewdataItems = Stat.DetailUnitReviewItems;
                IsLoading = false;
        }
        
        

        public async Task OnChangedDate()
        {
            (Filter.StartDay, Filter.EndDay) = GetDateTimeFromOffSet(Timeline.StartDay,Timeline.EndDay);
;            await GetUnitStatisticsByReviewDateRange();
        }


        void OnSeriesClick(SeriesClickEventArgs args)
        {
           
        }

        public async Task GetUnitTypes()
        {
            Types =  await _dashboardService.LookUpUnitTypes();
        }
        
        public async Task OnChangeSelectedTypes(object value)
        {
            Filter.UnitTypeId = (Guid?)value;
            await GetUnitStatisticsByReviewDateRange();
        }
    }
   
}