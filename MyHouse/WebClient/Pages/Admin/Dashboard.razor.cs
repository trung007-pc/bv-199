using System;
using System.Collections.Generic;
using System.Globalization;
using System.Threading.Tasks;
using BlazorDateRangePicker;
using Contract.Dashboards;
using Core.Enum;
using Radzen;

namespace WebClient.Pages.Admin
{
    public partial class Dashboard
    {
        private List<DataItem> PartReviewdataItems = new List<DataItem>();
        private List<DataItem> DetailPartReviewdataItems = new List<DataItem>();

        private bool IsLoading { get; set; } = true;
        
        private DateTimeOffset? StartDate { get; set;}
        
        private DateTimeOffset? EndDate { get; set;}

        private Dictionary<string, DateRange> DateRanges { get; set; } = new Dictionary<string, DateRange>();
        private PartReviewStatisticsDto Stat = new PartReviewStatisticsDto();

        private string HeaderTitle { get; set; } = "Dashboard";
        
        
     

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
                    await GetPartStatisticsByReviewDateRange();
                    IsLoading = false;
                    StateHasChanged();
                }, ActionType.GetList, false);
            }
        }

        public async Task GetPartStatisticsByReviewDateRange()
        {
                Stat = await _dashboardService.GetPartReviewStatisticsByDateRange(StartDate?.DateTime,EndDate?.DateTime);
                PartReviewdataItems = Stat.PartReviewItems;
                DetailPartReviewdataItems = Stat.DetailPartReviewItems;
        }

        public async Task OnChangedDate()
        {
            await GetPartStatisticsByReviewDateRange();
        }


        void OnSeriesClick(SeriesClickEventArgs args)
        {
           
        }


    }
   
}