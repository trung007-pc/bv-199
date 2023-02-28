using System.Threading.Tasks;
using Contract.MyDashboards;
using Radzen;

namespace WebClient.Pages.Admin
{
    public partial class MyDashboard
    {
        public string HeaderTitle = "My Dashboard";

        public MyWorkStatistics MyWorkStatistics { get; set; } = new MyWorkStatistics();

        public ChartStatistics ChartStatistics { get; set; } = new ChartStatistics();
        public ChartStatisticFilter StatisticFilter { get; set; } = new ChartStatisticFilter();
        public MyWorkFilter MyWorkFilter { get; set; } = new MyWorkFilter();

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                //
                // ChartStatistics = await _myDashboardService.GetChartStatistics(StatisticFilter);
                // MyWorkStatistics = await _myDashboardService.GetMyWorkStatistics(MyWorkFilter);
            }
        }

        public async Task Init()
        {
            var userId = await GetUserIdAsync();
            StatisticFilter.FolderTop = 5;
            StatisticFilter.UserId = userId;

            MyWorkFilter.UserId = userId;
            
        }
        
        
        void OnSeriesClick(SeriesClickEventArgs args)
        {
           
        }
        
        
    }
}