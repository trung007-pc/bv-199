using System.Threading.Tasks;

namespace Contract.MyDashboards
{
    public interface IMyDashboardService
    {
        public Task<GlobalStatistics> GetGlobalStatistics(GlobalStatisticFilter filter);
        
        public Task<MyStatistics> GetMyStatistics(MyStatisticFilter filter);

    }
}