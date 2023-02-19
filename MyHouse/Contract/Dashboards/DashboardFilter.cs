using System;
using Contract.Base;

namespace Contract.Dashboards
{
    public class DashboardFilter : FilterBase
    {

        public Guid? UnitTypeId { get; set; }
    }
}