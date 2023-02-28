using System.Threading.Tasks;
using Application.MyDashboards;
using Contract.MyDashboards;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("api/my-dashboard/")]
    [Authorize]
    public class MyDashboardController : IMyDashboardService
    {
        private MyDashboardService _myDashboardService;
        
        public MyDashboardController(MyDashboardService myDashboardService)
        {
            _myDashboardService = myDashboardService;
        }
        
        [HttpPost]
        [Route("get-chart-statistics")]
        public async Task<ChartStatistics> GetChartStatistics(ChartStatisticFilter filter)
        {
            return await _myDashboardService.GetChartStatistics(filter);
        }

           
        [HttpPost]
        [Route("get-my-work-statistics")]
        public async Task<MyWorkStatistics> GetMyWorkStatistics(MyWorkFilter filter)
        {
            return await _myDashboardService.GetMyWorkStatistics(filter);
        }
    }
}