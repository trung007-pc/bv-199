using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Contract.Dashboards;
using WebClient.RequestHttp;

namespace WebClient.Service.Dashboards
{
    public class DashboardService : IDashboardService
    {
        public async Task<UnitReviewStatisticsDto> GetUnitReviewStatisticsByDateRange(DateTime? start ,DateTime? end)
        {
            return await RequestClient.GetAPIAsync<UnitReviewStatisticsDto>($"dashboard/get-unit-statistics/{start:MM-dd-yyyy HH:mm:ss}{(start.HasValue ? '/' : ' ')}{end:MM-dd-yyyy HH:mm:ss}");
        }
    }
}