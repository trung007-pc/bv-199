using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Contract.Dashboards;
using Contract.UnitTypes;
using WebClient.RequestHttp;

namespace WebClient.Service.Dashboards
{
    public class DashboardService : IDashboardService
    {
        public async Task<UnitReviewStatisticsDto> GetUnitReviewStatistics(DashboardFilter input)
        {
            return await RequestClient.PostAPIAsync<UnitReviewStatisticsDto>($"dashboard/get-unit-statistics/",input);
        }

        public async Task<List<UnitTypeDto>> LookUpUnitTypes()
        {
            return await RequestClient.GetAPIAsync<List<UnitTypeDto>>($"dashboard/look-up-unit-types");
        }
    }
}