using System.Threading.Tasks;
using Contract.MeetingContents;
using Contract.MyDashboards;
using WebClient.RequestHttp;

namespace WebClient.Service.MyDashboards
{
    public class MyDashboardService : IMyDashboardService
    {
        public async Task<ChartStatistics> GetChartStatistics(ChartStatisticFilter filter)
        {
            return await RequestClient.PostAPIAsync<ChartStatistics>("my-dashboard/get-chart-statistics",filter);

        }

        public async Task<MyWorkStatistics> GetMyWorkStatistics(MyWorkFilter filter)
        {
            return await RequestClient.PostAPIAsync<MyWorkStatistics>("my-dashboard/my-work-statistics",filter);
        }
    }
}