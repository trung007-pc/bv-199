using System.Threading.Tasks;

namespace Contract.MyDashboards
{
    public interface IMyDashboardService
    {
        public Task<ChartStatistics> GetChartStatistics(ChartStatisticFilter filter);
        
        public Task<MyWorkStatistics> GetMyWorkStatistics(MyWorkFilter filter);

    }
}