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
        public async Task<GlobalStatistics> GetGlobalStatistics(GlobalStatisticFilter filter)
        {
            return await _myDashboardService.GetGlobalStatistics(filter);
        }

           
        [HttpPost]
        [Route("get-my-statistics")]
        public async Task<MyStatistics> GetMyStatistics(MyStatisticFilter filter)
        {
            return await _myDashboardService.GetMyStatistics(filter);
        }
    }
}