using System;

namespace Contract.Dashboards
{
    public class DashboardFilter
    {
        public DateTime? StartDay { get; set;}
        public DateTime? EndDay { get; set; }
        public Guid? UnitTypeId { get; set; }
    }
}