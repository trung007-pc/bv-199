using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Application.Dashboards;
using Contract.Dashboards;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("api/dashboard/")]
    public class DashboardController : ControllerBase,IDashboardService
    {
        private DashboardService _dashboardService;
        
        public DashboardController(DashboardService dashboardService)
        {
            _dashboardService = dashboardService;
        }
        
        [HttpGet]
        [Route("get-part-statistics/{start:?}/{end:?}")]
        public async Task<PartReviewStatisticsDto> GetPartReviewStatisticsByDateRange(DateTime? start = null ,DateTime? end = null)
        {   
            return await _dashboardService.GetPartReviewStatisticsByDateRange(start,end);
        }
        
    }
}