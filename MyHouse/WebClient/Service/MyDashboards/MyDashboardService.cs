using System.Threading.Tasks;
using Contract.MeetingContents;
using Contract.MyDashboards;
using WebClient.RequestHttp;

namespace WebClient.Service.MyDashboards
{
    public class MyDashboardService : IMyDashboardService
    {
        public async Task<GlobalStatistics> GetGlobalStatistics(GlobalStatisticFilter filter)
        {
            return await RequestClient.PostAPIAsync<GlobalStatistics>("my-dashboard/get-chart-statistics",filter);

        }

        public async Task<MyStatistics> GetMyStatistics(MyStatisticFilter filter)
        {
            return await RequestClient.PostAPIAsync<MyStatistics>("my-dashboard/get-my-statistics",filter);
        }
    }
}