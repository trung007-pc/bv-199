using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Contract.Dashboards
{
    public interface IDashboardService
    {
       Task<PartReviewStatisticsDto> GetPartReviewStatisticsByDateRange(DateTime? start ,DateTime? end);
    }
}