using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Contract.Dashboards
{
    public interface IDashboardService
    {
       Task<UnitReviewStatisticsDto> GetUnitReviewStatisticsByDateRange(DateTime? start ,DateTime? end);
    }
}