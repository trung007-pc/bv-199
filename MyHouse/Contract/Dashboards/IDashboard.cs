using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Contract.UnitTypes;

namespace Contract.Dashboards
{
    public interface IDashboardService
    {
       Task<UnitReviewStatisticsDto> GetUnitReviewStatistics(DashboardFilter input);
       Task<List<UnitTypeDto>> LookUpUnitTypes();
    }
}