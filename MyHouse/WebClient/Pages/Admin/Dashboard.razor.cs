using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using BlazorDateRangePicker;
using Contract.Dashboards;
using Core.Enum;
using Radzen;

namespace WebClient.Pages.Admin
{
    public partial class Dashboard
    {
        private List<DataItem> UnitReviewdataItems = new List<DataItem>();
        private List<DataItem> DetailUnitReviewdataItems = new List<DataItem>();

        private bool IsLoading { get; set; } = true;
        
        private DateTimeOffset? StartDate { get; set;}
        
        private DateTimeOffset? EndDate { get; set;}

        private Dictionary<string, DateRange> DateRanges { get; set; } = new Dictionary<string, DateRange>();
        private UnitReviewStatisticsDto Stat = new UnitReviewStatisticsDto();

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
                    await GetUnitStatisticsByReviewDateRange();
                    StateHasChanged();
                }, ActionType.GetList, false);
            }
        }

        public async Task GetUnitStatisticsByReviewDateRange()
        {
                Stat = await _dashboardService.GetUnitReviewStatisticsByDateRange(StartDate?.DateTime,EndDate?.DateTime);
                UnitReviewdataItems = Stat.UnitReviewItems;
                if (UnitReviewdataItems.Count() < 1)
                {
                    UnitReviewdataItems.Add(new DataItem(){Label = "",Value = 1});
                }
                DetailUnitReviewdataItems = Stat.DetailUnitReviewItems;
                
                if (DetailUnitReviewdataItems.Count() < 1)
                {
                    DetailUnitReviewdataItems.Add(new DataItem(){Label = "",Value = 1});
                }
                
                IsLoading = false;
        }
        
        

        public async Task OnChangedDate()
        {
            await GetUnitStatisticsByReviewDateRange();
        }


        void OnSeriesClick(SeriesClickEventArgs args)
        {
           
        }


    }
   
}