using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Application.Dashboards;
using Contract.Dashboards;
using Contract.UnitTypes;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("api/dashboard/")]
     [Authorize]
    public class DashboardController : ControllerBase,IDashboardService
    {
        private DashboardService _dashboardService;
        
        public DashboardController(DashboardService dashboardService)
        {
            _dashboardService = dashboardService;
        }
        
        [HttpPost]
        [Route("get-unit-statistics")]
        public async Task<UnitReviewStatisticsDto> GetUnitReviewStatistics(DashboardFilter input)
        {
            return await _dashboardService.GetUnitReviewStatistics(input);
        }

        [HttpGet]
        [Route("look-up-unit-types")]
        public async Task<List<UnitTypeDto>> LookUpUnitTypes()
        {
            return await _dashboardService.LookUpUnitTypes();
        }
    }
}